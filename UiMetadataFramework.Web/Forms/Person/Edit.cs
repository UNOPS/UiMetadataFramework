namespace UiMetadataFramework.Web.Forms.Person
{
	using System;
	using System.Collections.Generic;
	using UiMetadataFramework.Basic.EventHandlers;
	using UiMetadataFramework.Basic.Input;
	using UiMetadataFramework.Basic.Input.Typeahead;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.Web.Forms.Pickers;
	using UiMetadataFramework.Web.Metadata;
	using UiMetadataFramework.Web.Metadata.ClientFunctions;
	using UiMetadataFramework.Web.Metadata.EventHandlers;
	using UiMetadataFramework.Web.Metadata.Record;

	[MyForm(Id = "EditPerson", PostOnLoad = true, SubmitButtonLabel = "Save changes")]
	[LogToConsole(FormEvents.FormLoaded)]
	public class Edit : MyForm<Edit.Request, Edit.Response>
	{
		protected override Response Handle(Request message)
		{
			var person = SearchPeople.FamilyPerson.RandomFamilyPerson(message.Name);

			if (message.Operation.Value == RecordRequestOperation.Post)
			{
				person.DateOfBirth = message.DateOfBirth;

				if (message.Height == null)
				{
					throw new ArgumentNullException(nameof(message.Height));
				}

				if (message.Weight == null)
				{
					throw new ArgumentNullException(nameof(message.Weight));
				}

				person.Height = message.Height.Value;
				person.Weight = message.Weight.Value;
			}

			return new Response
			{
				Name = person.FirstName.Label,
				DateOfBirth = person.DateOfBirth,
				Height = person.Height,
				Weight = person.Weight,
				Metadata = new MyFormResponseMetadata
				{
					Title = person.FirstName.Label,
					FunctionsToRun = message.Operation.Value == RecordRequestOperation.Post
						? new List<ClientFunctionMetadata>
						{
							new GrowlNotification("Changes to the user record were saved.", "success").GetClientFunctionMetadata()
						}
						: null
				}
			};
		}

		public static FormLink FormLink(string personName, string label)
		{
			return new FormLink
			{
				Label = label,
				Form = typeof(Edit).GetFormId(),
				InputFieldValues = new Dictionary<string, object>
				{
					{ nameof(Request.Name), personName },
					{ nameof(Request.Operation), new DropdownValue<RecordRequestOperation>(RecordRequestOperation.Get) }
				}
			};
		}

		public class Response : RecordResponse
		{
			[OutputField(Hidden = true)]
			public DateTime? DateOfBirth { get; set; }

			[OutputField(Hidden = true)]
			public int Height { get; set; }

			[OutputField(Hidden = true)]
			[LogToConsole(FormEvents.ResponseHandled)]
			public string Name { get; set; }

			[OutputField(Hidden = true)]
			public decimal Weight { get; set; }
		}

		public class Request : RecordRequest<Response>
		{
			[BindToOutput(nameof(Response.DateOfBirth))]
			public DateTime? DateOfBirth { get; set; }

			[BindToOutput(nameof(Response.Height))]
			public int? Height { get; set; }

			[BindToOutput(nameof(Response.Name))]
			[LogToConsole(FormEvents.FormLoaded)]
			public string Name { get; set; }

			[TypeaheadInputField(typeof(PersonTypeaheadRemoteSource))]
			public MultiSelect<string> Spouse { get; set; }

			[BindToOutput(nameof(Response.Weight))]
			public int? Weight { get; set; }
		}
	}
}