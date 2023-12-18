namespace MyApp
{
	using UiMetadataFramework.Core.Binding;

	public class Request
	{
		[InputField(OrderIndex = 10)]
		public TextField FirstName { get; set; }
	}
    
	[InputFieldType("text-field", mandatoryAttribute: typeof(TextFieldAttribute))]
	public class TextField
	{
		public string Value { get; set; }
	}

	public class TextFieldAttribute : InputFieldAttribute
	{
	}
}