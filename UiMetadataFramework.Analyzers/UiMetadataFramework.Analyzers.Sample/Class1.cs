﻿using UiMetadataFramework.Core.Binding;

namespace MyApp
{
	using UiMetadataFramework.Core.Binding;
    using UIMetadataFrameworkAnalyzer.Inputs;

    public class Request
	{
		[InputField(OrderIndex = 10)]
		public TextField FirstName { get; set; }
	}
}

namespace UIMetadataFrameworkAnalyzer.Inputs { 
	[InputFieldType("text-field", mandatoryAttribute: typeof(TextFieldAttribute))]
	public class TextField
	{
		public string Value { get; set; }
    }
    public class TextFieldAttribute : InputFieldAttribute
    {
    }
}