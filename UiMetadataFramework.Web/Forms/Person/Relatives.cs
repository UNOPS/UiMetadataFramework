namespace UiMetadataFramework.Web.Forms.Person
{
	using System.Collections.Generic;
	using global::MediatR;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.Web.Metadata;

	[Form(Id = "Relatives", PostOnLoad = true)]
	public class Relatives : MyForm<Relatives.Request, Relatives.Response>
	{
		protected override Response Handle(Request message)
		{
			var person = SearchPeople.FamilyPerson.RandomFamilyPerson(message.Name);
			return new Response
			{
				Tabs = PersonInfo.GetTabs(typeof(Relatives).GetFormId(), message.Name),
				Relatives = person.Relatives,
				Actions = new ActionList(
					ShowMessage.FormLink("Edit"),
					SearchPeople.FormLink("View similar", message.Name)),
				Metadata = new MyFormResponseMetadata
				{
					Title = message.Name
				}
			};
		}

		public class Response : MyFormResponse
		{
			[OutputField(OrderIndex = -15)]
			public ActionList Actions { get; set; }

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