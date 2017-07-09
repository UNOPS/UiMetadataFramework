namespace UiMetadataFramework.Web.Forms
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::MediatR;
	using UiMetadataFramework.Basic.Input;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;

	[Form(Label = "Do some magic", PostOnLoad = true)]
	public class DoMagic : IForm<DoMagic.Request, DoMagic.Response>
	{
		public Response Handle(Request message)
		{
			var height = message.Height == 0 || message.Height == null ? 170 : message.Height.Value;
			var weight = message.Weight == 0 || message.Weight == null ? 65 : message.Weight.Value;

			return new Response
			{
				FirstName = message.FirstName,
				Weight = weight,
				DateOfBirth = message.DateOfBirth,
				Height = height,
				OtherPeople = new List<FamilyPerson>
				{
					FamilyPerson.RandomFamilyPerson(height * 1.23m, weight * 1.23m),
					FamilyPerson.RandomFamilyPerson(height * 1.51m, weight * 1.51m),
					FamilyPerson.RandomFamilyPerson(height * 1.14m, weight * 1.11m)
				}
			};
		}

		public class Response : FormResponse
		{
			[OutputField(Label = "DoB", OrderIndex = 2)]
			public DateTime? DateOfBirth { get; set; }

			[OutputField(Label = "First name", OrderIndex = 1)]
			public string FirstName { get; set; }

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
			public string FirstName { get; set; }

			[OutputField(Hidden = true)]
			public int Height { get; set; }

			[OutputField(Hidden = true)]
			public decimal Weight { get; set; }

			public static Person Random(decimal height, decimal weight)
			{
				var random = new Random((int)Math.Round(height * weight)
					).Next(0, Names.Length - 1);

				return new Person
				{
					DateOfBirth = DateTime.Today.AddYears(-20),
					FirstName = Names[random],
					Height = (int)height,
					Weight = (int)weight
				};
			}
		}

		public class Request : IRequest<Response>
		{
			[InputField(Label = "DoB", OrderIndex = 2)]
			public DateTime? DateOfBirth { get; set; }

			[InputField(Label = "First name", OrderIndex = 1, Required = true)]
			public string FirstName { get; set; }

			[InputField(Hidden = true)]
			public int? Height { get; set; }

			public bool? IsRegistered { get; set; }

			[InputField(Hidden = true)]
			public decimal? Weight { get; set; }

			public DropdownValue<DayOfWeek?> FavouriteDayOfWeek { get; set; }

			[Option(DayOfWeek.Sunday)]
			[Option(DayOfWeek.Monday)]
			[InputField(Required = true)]
			public DropdownValue<DayOfWeek> FirstDayOfWeek { get; set; }
		}
	}
}