namespace UiMetadataFramework.Tests.Binding
{
	using System;
	using UiMetadataFramework.Core.Binding;

	public partial class BinderTests
	{
		public class Person
		{
			[OutputField(Label = "DoB", OrderIndex = 2)]
			public DateTime? DateOfBirth { get; set; }

			[OutputField(Label = "First name", OrderIndex = 1)]
			public string FirstName { get; set; }

			[OutputField(Hidden = true)]
			public int Height { get; set; }

			[OutputField(Hidden = true)]
			public decimal Weight { get; set; }
		}
	}
}