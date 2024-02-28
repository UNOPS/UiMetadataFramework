﻿namespace UiMetadataFramework.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Threading;
	using System.Threading.Tasks;
	using global::MediatR;
	using Microsoft.Extensions.DependencyInjection;
	using UiMetadataFramework.Basic;
	using UiMetadataFramework.Basic.Output.Text;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;
	using Xunit;

	public class MediatrTests
	{
		public class MyForm : FormAttribute
		{
			public override IDictionary<string, object?> GetCustomProperties(Type type)
			{
				var menuAttribute = type.GetTypeInfo().GetCustomAttribute<MenuAttribute>();

				return new Dictionary<string, object?> { { "ParentMenu", menuAttribute?.ParentMenu } };
			}
		}

		public class MenuAttribute : Attribute
		{
			public MenuAttribute(string parentMenu)
			{
				this.ParentMenu = parentMenu;
			}

			public string ParentMenu { get; set; }
		}

		public class BaseForm : Form<BaseForm.Request, BaseForm.Response>, IComparable
		{
			public int CompareTo(object? obj)
			{
				// This method is just to make sure IForms can implement any arbitrary non-generic interface.
				throw new NotImplementedException();
			}

			protected override Response Handle(Request message)
			{
				return new Response();
			}

			public class Response : FormResponse
			{
				[OutputField(Label = "DoB", OrderIndex = 2)]
				public DateTime? DateOfBirth { get; set; }

				[OutputField(Label = "First name", OrderIndex = 1)]
				public string? FirstName { get; set; }

				[OutputField(Hidden = true)]
				public int Height { get; set; }

				[OutputField(Hidden = true)]
				public decimal Weight { get; set; }
			}

			public class Request : IRequest<Response>
			{
				[InputField(Label = "DoB", OrderIndex = 2, Required = true)]
				public DateTime DateOfBirth { get; set; }

				[InputField(Label = "First name", OrderIndex = 1, Required = true)]
				public string FirstName { get; set; } = null!;

				[InputField(Hidden = true)]
				public int Height { get; set; }

				public bool IsRegistered { get; set; }

				[InputField(Hidden = true)]
				public decimal Weight { get; set; }
			}
		}

		[MyForm(Id = "Magic", Label = "Do some magic", PostOnLoad = false, CloseOnPostIfModal = true)]
		[Menu("Magical tools")]
		[LogFormEvent(FormEvents.FormLoaded)]
		public class Magic : BaseForm
		{
		}

		[Form(Id = "Magic")]
		public class FormWithDuplicateId : BaseForm
		{
		}

		[Form]
		public class FormWithoutId : BaseForm
		{
		}

		public class LogFormEvent : Attribute, IFormEventHandlerAttribute
		{
			public LogFormEvent(string eventName)
			{
				this.RunAt = eventName;
			}

			public string Id { get; } = "log-form-event";
			public string RunAt { get; }

			public EventHandlerMetadata ToMetadata(Type formType, MetadataBinder binder)
			{
				return new EventHandlerMetadata(this.Id, this.RunAt);
			}
		}

		private static IServiceProvider GetDiContainer()
		{
			var di = new DefaultDependencyInjectionContainer();
			var binder = new MetadataBinder(di);
			binder.RegisterAssembly(typeof(StringOutputComponentBinding).GetTypeInfo().Assembly);

			var formRegister = new FormRegister(binder);
			formRegister.RegisterForm(typeof(Magic));

			var services = new ServiceCollection();

			services.AddSingleton(di);
			services.AddSingleton(binder);
			services.AddSingleton(formRegister);

			services.AddMediatR(
				typeof(MediatrTests).Assembly,
				typeof(InvokeForm).Assembly,
				typeof(StringOutputComponentBinding).Assembly);

			var provider = new DefaultServiceProviderFactory().CreateServiceProvider(services);
			return provider;
		}

		[Fact]
		public void CanGetFormsFromRegistry()
		{
			var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			binder.RegisterAssembly(typeof(StringOutputComponentBinding).GetTypeInfo().Assembly);

			var formRegister = new FormRegister(binder);
			formRegister.RegisterForm(typeof(Magic));

			var formMetadata = formRegister.GetFormInfo(typeof(Magic))?.Metadata;

			Assert.NotNull(formMetadata);
			Assert.Equal("Magical tools", formMetadata!.CustomProperties?["ParentMenu"]);

			Assert.True(formMetadata.Id == "Magic");
			Assert.True(formMetadata.Label == "Do some magic");
			Assert.True(formMetadata.PostOnLoad == false);
			Assert.True(formMetadata.CloseOnPostIfModal);
			Assert.True(formMetadata.InputFields.Count == 5);
			Assert.True(formMetadata.OutputFields.Count == 4);

			var formEventHandler = formMetadata.EventHandlers!.First();
			Assert.True(formEventHandler.Id == "log-form-event");
			Assert.True(formEventHandler.RunAt == FormEvents.FormLoaded);
		}

		[Fact]
		public async Task CanInvokeFormWithDictionaryRequest()
		{
			var di = GetDiContainer();

			var mediator = di.GetService<IMediator>();

			var response = await mediator.Send(
				new InvokeForm.Request
				{
					Form = typeof(Magic).GetFormId(),
					InputFieldValues = new BaseForm.Request
					{
						FirstName = "John",
						Height = 1,
						DateOfBirth = DateTime.Now,
						IsRegistered = true,
						Weight = 2
					}.ToDictionary()
				},
				CancellationToken.None);

			Assert.NotNull(response);
		}

		[Fact]
		public async Task CanInvokeFormWithObjectRequest()
		{
			var di = GetDiContainer();

			var mediator = di.GetService<IMediator>();

			var response = await mediator.Send(
				new InvokeForm.Request
				{
					Form = typeof(Magic).GetFormId(),
					InputFieldValues = new
					{
						FirstName = "John",
						Height = 1,
						DateOfBirth = DateTime.Now,
						IsRegistered = true,
						Weight = 2
					}
				},
				CancellationToken.None);

			Assert.NotNull(response);
		}

		[Fact]
		public void DuplicateCallsToRegisterSameFormAreIgnored()
		{
			var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			binder.RegisterAssembly(typeof(StringOutputComponentBinding).GetTypeInfo().Assembly);

			var formRegister = new FormRegister(binder);
			formRegister.RegisterForm(typeof(Magic));
			formRegister.RegisterForm(typeof(Magic));
		}

		[Fact]
		public void DuplicateFormIdsAreNotAllowed()
		{
			var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			binder.RegisterAssembly(typeof(StringOutputComponentBinding).GetTypeInfo().Assembly);

			var formRegister = new FormRegister(binder);
			formRegister.RegisterForm(typeof(Magic));
			Assert.Throws<InvalidConfigurationException>(() => formRegister.RegisterForm(typeof(FormWithDuplicateId)));
		}

		[Fact]
		public void TypeNameIsUsedAsDefaultFormId()
		{
			var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			binder.RegisterAssembly(typeof(StringOutputComponentBinding).GetTypeInfo().Assembly);

			var formRegister = new FormRegister(binder);
			formRegister.RegisterForm(typeof(FormWithoutId));

			var metadata = formRegister.GetFormInfo(typeof(FormWithoutId))!.Metadata;

			Assert.Equal(typeof(FormWithoutId).FullName, metadata.Id);
		}
	}
}