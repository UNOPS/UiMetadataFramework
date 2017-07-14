namespace UiMetadataFramework.Web.Forms
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::MediatR;
	using UiMetadataFramework.Basic.Input;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.Web.Forms.Person;
	using UiMetadataFramework.Web.Metadata;

	[MyForm(Label = "Search people", PostOnLoad = true, SubmitButtonLabel = "Search")]
	public class SearchPeople : IMyForm<SearchPeople.Request, SearchPeople.Response>
	{
		public Response Handle(Request message)
		{
			var height = message.Height == 0 || message.Height == null ? 170 : message.Height.Value;
			var weight = message.Weight;

			return new Response
			{
				FirstName = PersonInfo.Link(message.FirstName),
				Weight = weight,
				DateOfBirth = message.DateOfBirth,
				Height = height,
				OtherPeople = new List<FamilyPerson>
				{
					FamilyPerson.RandomFamilyPerson(height * 1.23m, weight * 1.23m),
					FamilyPerson.RandomFamilyPerson(height * 1.51m, weight * 1.51m),
					FamilyPerson.RandomFamilyPerson(height * 1.14m, weight * 1.11m)
				},
				Metadata = new MyFormResponseMetadata
				{
					Title = "Searching for " + message.FirstName
				}
			};
		}

		public class Response : MyFormResponse
		{
			[OutputField(Label = "DoB", OrderIndex = 2)]
			public DateTime? DateOfBirth { get; set; }

			[OutputField(Label = "First name", OrderIndex = 1)]
			public FormLink FirstName { get; set; }

			[OutputField(Hidden = true)]
			public int Height { get; set; }

			[OutputField(OrderIndex = 10)]
			public IList<FamilyPerson> OtherPeople { get; set; }

			[OutputField(Hidden = true)]
			public decimal Weight { get; set; }
		}

		public class FamilyPerson : Person
		{
			[OutputField(OrderIndex = 20)]
			public List<Person> Relatives { get; set; }

			public static FamilyPerson RandomFamilyPerson(string name)
			{
				var r = new Random(name.GetHashCode());
				return RandomFamilyPerson(r.Next(150, 200), r.Next(40, 120));
			}

			public static FamilyPerson RandomFamilyPerson()
			{
				var r = new Random();
				return RandomFamilyPerson(r.Next(150, 200), r.Next(40, 120));
			}

			public static FamilyPerson RandomFamilyPerson(decimal height, decimal weight)
			{
				var person = Random(height, weight);
				var random = new Random((int)Math.Round(height * weight)).Next(0, 5);

				return new FamilyPerson
				{
					FirstName = person.FirstName,
					DateOfBirth = person.DateOfBirth,
					Height = person.Height,
					Weight = person.Weight,
					Relatives = Enumerable.Range(0, random).Select(t => Random(170 + t, 65 + t)).ToList()
				};
			}
		}

		public class Person
		{
			private static readonly string[] Names = { "Jack", "John", "Jane", "Jeanne", "Joe", "Alice", "Moh", "Andreh", "Elly" };

			[OutputField(Label = "DoB", OrderIndex = 2)]
			public DateTime? DateOfBirth { get; set; }

			[OutputField(Label = "First name", OrderIndex = -1)]
			public FormLink FirstName { get; set; }

			[OutputField(Hidden = true)]
			public int Height { get; set; }

			[OutputField(Hidden = true)]
			public decimal Weight { get; set; }

			public static Person Random()
			{
				var r = new Random();
				return Random(r.Next(150, 200), r.Next(40, 120));
			}

			public static Person Random(decimal height, decimal weight)
			{
				var r = new Random((int)Math.Round(height * weight));

				var name = Names[r.Next(0, Names.Length - 1)];
				var dateOfBirth = new DateTime(r.Next(1970, 2001), r.Next(1, 12), r.Next(1, 28));

				return new Person
				{
					DateOfBirth = dateOfBirth,
					FirstName = new FormLink
					{
						Label = name,
						Form = typeof(SearchPeople).FullName,
						InputFieldValues = new Dictionary<string, object>
						{
							{ nameof(Request.FirstName), name },
							{ nameof(Request.Height), (int)height },
							{ nameof(Request.Weight), (int)weight },
							{ nameof(Request.DateOfBirth), dateOfBirth },
							{ nameof(Request.FavouriteDayOfWeek), new DropdownValue<DayOfWeek>((DayOfWeek)r.Next(0, 6)) },
							{ nameof(Request.FirstDayOfWeek), new DropdownValue<DayOfWeek>((DayOfWeek)r.Next(0, 1)) },
							{ nameof(Request.IsRegistered), r.Next(0, 1) == 1 }
						}
					},
					Height = (int)height,
					Weight = (int)weight
				};
			}
		}

		public class Request : IRequest<Response>
		{
			[InputField(Label = "DoB", OrderIndex = 2)]
			public DateTime? DateOfBirth { get; set; }

			public DropdownValue<DayOfWeek?> FavouriteDayOfWeek { get; set; }

			[Option(DayOfWeek.Sunday)]
			[Option(DayOfWeek.Monday)]
			[InputField(Required = true)]
			public DropdownValue<DayOfWeek> FirstDayOfWeek { get; set; }

			[InputField(Label = "First name", OrderIndex = 1, Required = true)]
			public string FirstName { get; set; }

			[InputField(Hidden = true)]
			public int? Height { get; set; }

			public bool? IsRegistered { get; set; }

			[InputField(Hidden = false)]
			public decimal Weight { get; set; }
		}
	}
}