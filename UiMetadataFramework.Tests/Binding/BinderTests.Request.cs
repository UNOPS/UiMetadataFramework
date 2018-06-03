namespace UiMetadataFramework.Tests.Binding
{
	using System;
	using UiMetadataFramework.Basic.Input;
	using UiMetadataFramework.Core.Binding;

	public partial class BinderTests
	{
		public class Request
		{
			[Option("Low", "L")]
			[Option("Mid", "M")]
			[Option("High", "H")]
			[Documentation("1")]
			[Documentation("2")]
			public DropdownValue<string> Category { get; set; }

			[InputField(Label = "DoB", OrderIndex = 2)]
			public DateTime DateOfBirth { get; set; }

			public DropdownValue<DayOfWeek?> Day { get; set; }

			[StringProperty("secret", "password")]
			[Option(DayOfWeek.Sunday)]
			[Option(DayOfWeek.Monday)]
			public DropdownValue<DayOfWeek> FirstDayOfWeek { get; set; }

			[InputField(Label = "First name", OrderIndex = 1, Required = true)]
			public string FirstName { get; set; }

			[InputField(Hidden = true)]
			public int? Height { get; set; }

			public bool IsRegistered { get; set; }

			[IntProperty("number-1", 1)]
			[IntProperty("number-2", 2)]
			public TextareaValue Notes { get; set; }

			public DateTime? SubmissionDate { get; set; }

			[InputField(Hidden = true)]
			[InputFieldEventHandler]
			public decimal Weight { get; set; }
		}
	}
}