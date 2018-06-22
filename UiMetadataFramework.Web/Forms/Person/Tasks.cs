namespace UiMetadataFramework.Web.Forms.Person
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::MediatR;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.Web.Metadata;

	[Form(Id = "Tasks", Label = "Tasks", PostOnLoad = true)]
	public class Tasks : MyForm<Tasks.Request, Tasks.Response>
	{
		public enum TaskCategory
		{
			Work,
			Chores,
			Hobby,
			Health,
			Family
		}

		[OutputFieldType("task-status")]
		public enum TaskStatus
		{
			Pending,
			InProgress,
			Done
		}

		protected override Response Handle(Request message)
		{
			message.TaskOwnerName = "buy milk";
			var random = new Random(message.TaskOwnerName.GetHashCode());

			return new Response
			{
				Metadata = new MyFormResponseMetadata
				{
					Title = "Tasks for " + message.TaskOwnerName
				},
				Tasks = Enumerable.Range(0, random.Next(0, 20)).Select(t => Item.Random(t)).ToList()
			};
		}

		public static InlineForm Form(string taskOwnerName)
		{
			return new InlineForm
			{
				Form = typeof(Tasks).GetFormId(),
				InputFieldValues = new Dictionary<string, object>
				{
					{ nameof(Request.TaskOwnerName), taskOwnerName }
				}
			};
		}

		public class Response : FormResponse<MyFormResponseMetadata>
		{
			[OutputField(Label = "Recent tasks")]
			public IList<Item> Tasks { get; set; }
		}

		public class Request : IRequest<Response>
		{
			[InputField(Label = "Name")]
			public string TaskOwnerName { get; set; }
		}

		public class Item
		{
			private static readonly string[] Tasks =
			{
				"buy milk",
				"fix fridge",
				"cook dinner",
				"finish homework assignment",
				"exercise",
				"finish work",
				"write report",
				"do code-reviews",
				"read book",
				"do laundry",
				"clean room",
				"buy vegetables",
				"prepare food"
			};

			public TextValue<TaskCategory> Category { get; set; }

			[OutputField(Label = "Due date")]
			public DateTime DueDate { get; set; }

			public string Name { get; set; }
			public TaskStatus Status { get; set; }

			public static Item Random(int? seed = null)
			{
				var random = seed != null ? new Random(seed.Value) : new Random();

				var item = new Item
				{
					DueDate = DateTime.Today.AddDays(random.Next(0, 14)),
					Name = Tasks[random.Next(0, Tasks.Length - 1)],
					Status = (TaskStatus)random.Next(0, 2),
					Category = new TextValue<TaskCategory>((TaskCategory)random.Next(0, 4))
				};

				return item;
			}
		}
	}
}