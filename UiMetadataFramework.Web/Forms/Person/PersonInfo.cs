namespace UiMetadataFramework.Web.Forms.Person
{
	using System;
	using System.Collections.Generic;
	using global::MediatR;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;
	using UiMetadataFramework.Web.Metadata;

	[Form(PostOnLoad = true)]
	public class PersonInfo : IMyForm<PersonInfo.Request, PersonInfo.Response>
	{
		public Response Handle(Request message)
		{
			var person = SearchPeople.FamilyPerson.RandomFamilyPerson(message.Name);
			return new Response
			{
				Tabs = GetTabs(typeof(PersonInfo).FullName, message.Name),
				DateOfBirth = person.DateOfBirth,
				Weight = person.Weight,
				Height = person.Height,
				FirstName = message.Name,
				Relatives = person.Relatives,
				Actions = new ActionList(
					ShowMessage.FormLink("Edit"),
					SearchPeople.FormLink("View similar", message.Name)),
				Tasks = Tasks.Form(message.Name),
				Metadata = new MyFormResponseMetadata
				{
					Title = message.Name
				}
			};
		}

		public static FormLink Link(string name)
		{
			return new FormLink
			{
				Label = name,
				Form = typeof(PersonInfo).FullName,
				InputFieldValues = new Dictionary<string, object>
				{
					{ nameof(Request.Name), name }
				}
			};
		}

		internal static Tabstrip GetTabs(string currentTab, string name)
		{
			return new Tabstrip
			{
				CurrentTab = currentTab,
				Tabs = new List<Tab>
				{
					new Tab
					{
						Label = "Basic info",
						Form = typeof(PersonInfo).FullName,
						InputFieldValues = new Dictionary<string, object>
						{
							{ nameof(Request.Name), name }
						}
					},
					new Tab
					{
						Label = "Relatives",
						Form = typeof(Relatives).FullName,
						InputFieldValues = new Dictionary<string, object>
						{
							{ nameof(Relatives.Request.Name), name }
						},
						Style = "important"
					}
				}
			};
		}

		public class Response : MyFormResponse
		{
			[OutputField(OrderIndex = -15)]
			public ActionList Actions { get; set; }

			[OutputField(Label = "DoB", OrderIndex = 2)]
			public DateTime? DateOfBirth { get; set; }

			[OutputField(Label = "First name", OrderIndex = -1)]
			public string FirstName { get; set; }

			[OutputField(Hidden = true)]
			public int Height { get; set; }

			[OutputField(OrderIndex = 100)]
			public List<SearchPeople.Person> Relatives { get; set; }

			[OutputField(OrderIndex = -10)]
			public Tabstrip Tabs { get; set; }

			[OutputField(OrderIndex = 90)]
			public InlineForm Tasks { get; set; }

			[OutputField(Hidden = true)]
			public decimal Weight { get; set; }
		}

		public class Request : IRequest<Response>
		{
			[InputField(Required = true, Hidden = true)]
			public string Name { get; set; }
		}
	}
}