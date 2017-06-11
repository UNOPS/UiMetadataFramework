namespace UiMetadataFramework.Tests
{
	using System;
	using System.Reflection;
	using global::MediatR;
	using UiMetadataFramework.BasicFields.Output;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;
	using Xunit;

	public class MediatrTests
	{
		[Form(Label = "Do some magic", PostOnLoad = false)]
		public class DoMagic : IForm<DoMagic.Request, DoMagic.Response>
		{
			public Response Handle(Request message)
			{
				return new Response();
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
			var binder = new MetadataBinder();
			binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);

			var formRegister = new FormRegister(binder);
			formRegister.RegisterAssembly(typeof(DoMagic).GetTypeInfo().Assembly);
			
			var formMetadata = formRegister.GetForm(typeof(DoMagic).FullName);

			Assert.NotNull(formMetadata);
			Assert.True(formMetadata.Id == typeof(DoMagic).FullName);
			Assert.True(formMetadata.Label == "Do some magic");
			Assert.True(formMetadata.PostOnLoad == false);
			Assert.True(formMetadata.InputFields.Count == 5);
			Assert.True(formMetadata.OutputFields.Count == 4);
		}
	}
}