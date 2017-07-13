namespace UiMetadataFramework.Web.Forms.Person
{
	using System;
	using System.Collections.Generic;
	using global::MediatR;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;

	[Form(Label = "Person", PostOnLoad = true)]
	public class PersonInfo : IForm<PersonInfo.Request, PersonInfo.Response>
	{
		public Response Handle(Request message)
		{
			var person = DoMagic.FamilyPerson.RandomFamilyPerson(message.Name);
			return new Response
			{
				Tabs = GetTabs(typeof(PersonInfo).FullName, message.Name),
				DateOfBirth = person.DateOfBirth,
				Weight = person.Weight,
				Height = person.Height,
				FirstName = message.Name,
				Relatives = person.Relatives
			};
		}

		internal static Tabstrip GetTabs(string currentTab, string name)
		{
			return new Tabstrip
			{
				CurrentTab = currentTab,
				Tabs =
				{
					new Tab
					{
						Label = "Basic info",
						Form = nameof(PersonInfo),
						InputFieldValues = new Dictionary<string, object>
						{
							{ nameof(Request.Name), name }
						}
					},
					new Tab
					{
						Label = "Relatives",
						Form = nameof(Relatives),
						InputFieldValues = new Dictionary<string, object>
						{
							{ nameof(Relatives.Request.Name), name }
						},
						Style = "important"
					}
				}
			};
		}

		public class Response : FormResponse
		{
			[OutputField(Label = "DoB", OrderIndex = 2)]
			public DateTime? DateOfBirth { get; set; }

			[OutputField(Label = "First name", OrderIndex = -1)]
			public string FirstName { get; set; }

			[OutputField(Hidden = true)]
			public int Height { get; set; }

			[OutputField(OrderIndex = 100)]
			public List<DoMagic.Person> Relatives { get; set; }

			[OutputField(OrderIndex = -10)]
			public Tabstrip Tabs { get; set; }

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