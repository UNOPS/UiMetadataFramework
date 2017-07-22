namespace UiMetadataFramework.Web.Forms.Person
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::MediatR;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Web.Metadata;

	public class Tasks : IMyForm<Tasks.Request, Tasks.Response>
	{
		public enum TaskStatus
		{
			Pending,
			InProgress,
			Done
		}

		public Response Handle(Request message)
		{
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
				Form = typeof(Tasks).FullName,
				InputFieldValues = new Dictionary<string, object>
				{
					{ nameof(Request.TaskOwnerName), taskOwnerName }
				}
			};
		}

		public class Response : FormResponse<MyFormResponseMetadata>
		{
			public IList<Item> Tasks { get; set; }
		}

		public class Request : IRequest<Response>
		{
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
					Status = (TaskStatus)random.Next(0, 2)
				};

				return item;
			}
		}
	}
}