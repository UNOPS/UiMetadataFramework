namespace UiMetadataFramework.Web.Forms.Person
{
	using System.Collections.Generic;
	using global::MediatR;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;

	[Form(PostOnLoad = true)]
	public class Relatives : IForm<Relatives.Request, Relatives.Response>
	{
		public Response Handle(Request message)
		{
			var person = DoMagic.FamilyPerson.RandomFamilyPerson(message.Name);
			return new Response
			{
				Tabs = PersonInfo.GetTabs(typeof(PersonInfo).FullName, message.Name),
				Relatives = person.Relatives
			};
		}

		public class Response : FormResponse
		{
			public List<DoMagic.Person> Relatives { get; set; }

			[OutputField(OrderIndex = -1)]
			public Tabstrip Tabs { get; set; }
		}

		public class Request : IRequest<Response>
		{
			[InputField(Required = true, Hidden = true)]
			public string Name { get; set; }
		}
	}
}