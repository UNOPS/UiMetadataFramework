namespace UiMetadataFramework.Web.Forms.Person
{
	using System.Collections.Generic;
	using global::MediatR;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;
	using UiMetadataFramework.Web.Metadata;

	[Form(PostOnLoad = true)]
	public class Relatives : IMyForm<Relatives.Request, Relatives.Response>
	{
		public Response Handle(Request message)
		{
			var person = SearchPeople.FamilyPerson.RandomFamilyPerson(message.Name);
			return new Response
			{
				Tabs = PersonInfo.GetTabs(typeof(Relatives).FullName, message.Name),
				Relatives = person.Relatives,
				Metadata = new MyFormResponseMetadata
				{
					Title = message.Name
				}
			};
		}

		public class Response : MyFormResponse
		{
			public List<SearchPeople.Person> Relatives { get; set; }

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