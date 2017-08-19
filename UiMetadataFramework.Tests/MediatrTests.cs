namespace UiMetadataFramework.Tests
{
	using System;
	using System.Reflection;
	using System.Threading.Tasks;
	using global::MediatR;
	using UiMetadataFramework.Basic;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;
	using Xunit;

	public class MediatrTests
	{
		public class MyForm : FormAttribute
		{
			public override object GetCustomProperties(Type type)
			{
				var menuAttribute = type.GetTypeInfo().GetCustomAttribute<MenuAttribute>();

				return new MyFormCustomProperties
				{
					ParentMenu = menuAttribute?.ParentMenu
				};
			}
		}

		public class MyFormCustomProperties
		{
			public string ParentMenu { get; set; }
		}

		public class MenuAttribute : Attribute
		{
			public MenuAttribute(string parentMenu)
			{
				this.ParentMenu = parentMenu;
			}

			public string ParentMenu { get; set; }
		}

		[MyForm(Label = "Do some magic", PostOnLoad = false)]
		[Menu("Magical tools")]
		public class DoMagic : IAsyncForm<DoMagic.Request, DoMagic.Response>, IComparable
		{
			public Task<Response> Handle(Request message)
			{
				return Task.FromResult(new Response());
			}

			public int CompareTo(object obj)
			{
				// This method is just to make sure IForms can implement any arbitrary non-generic interface.
				throw new NotImplementedException();
			}

			public class Response : FormResponse
			{
				[OutputField(Label = "DoB", OrderIndex = 2)]
				public DateTime DateOfBirth { get; set; }

				[OutputField(Label = "First name", OrderIndex = 1)]
				public string FirstName { get; set; }

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
				public string FirstName { get; set; }

				[InputField(Hidden = true)]
				public int Height { get; set; }

				public bool IsRegistered { get; set; }

				[InputField(Hidden = true)]
				public decimal Weight { get; set; }
			}
		}

		[Fact]
		public void CanGetFormsFromRegistry()
		{
			var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);

			var formRegister = new FormRegister(binder);
			formRegister.RegisterAssembly(typeof(DoMagic).GetTypeInfo().Assembly);

			var formMetadata = formRegister.GetFormInfo(typeof(DoMagic).FullName)?.Metadata;

			Assert.NotNull(formMetadata);
			Assert.Equal("Magical tools", ((MyFormCustomProperties)formMetadata.CustomProperties).ParentMenu);

			Assert.True(formMetadata.Id == typeof(DoMagic).FullName);
			Assert.True(formMetadata.Label == "Do some magic");
			Assert.True(formMetadata.PostOnLoad == false);
			Assert.True(formMetadata.InputFields.Count == 5);
			Assert.True(formMetadata.OutputFields.Count == 4);
		}
	}
}