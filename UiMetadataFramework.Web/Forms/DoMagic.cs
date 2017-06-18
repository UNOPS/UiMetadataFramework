namespace UiMetadataFramework.Web.Forms
{
	using System;
	using global::MediatR;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;

	[Form(Label = "Do some magic", PostOnLoad = false)]
	public class DoMagic : IForm<DoMagic.Request, DoMagic.Response>
	{
		public Response Handle(Request message)
		{
			return new Response
			{
				FirstName = message.FirstName,
				Weight = message.Weight,
				DateOfBirth = message.DateOfBirth,
				Height = message.Height
			};
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
}