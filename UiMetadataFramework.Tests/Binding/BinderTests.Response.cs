namespace UiMetadataFramework.Tests.Binding
{
	using System;
	using System.Collections.Generic;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	public partial class BinderTests
	{
		public class Response : FormResponse
		{
			public IList<string> Categories { get; set; }

			[OutputField(Label = "DoB", OrderIndex = 2)]
			public DateTime DateOfBirth { get; set; }

			[OutputField(Label = "First name", OrderIndex = 1)]
			public string FirstName { get; set; }

			[OutputField(Hidden = true)]
			public int Height { get; set; }

			public IList<Person> OtherPeople { get; set; }

			[OutputField(Hidden = true)]
			[OutputFieldEventHandler]
			public decimal Weight { get; set; }
		}
	}
}