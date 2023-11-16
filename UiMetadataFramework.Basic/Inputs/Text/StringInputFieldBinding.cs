﻿namespace UiMetadataFramework.Basic.Inputs.Text
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class StringInputFieldBinding : InputFieldBinding
	{
		internal const string ControlName = "text";

		/// <inheritdoc />
		public StringInputFieldBinding() : base(typeof(string), ControlName)
		{
		}
	}
}