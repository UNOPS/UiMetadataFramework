﻿namespace UiMetadataFramework.Basic.Inputs.Textarea
{
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Represents an input field for multiline text.
	/// </summary>
	[InputComponent(ControlName)]
	public class TextareaValue
	{
		internal const string ControlName = "textarea";

		/// <summary>
		/// The value of the input field.
		/// </summary>
		public string? Value { get; set; }
	}
}