namespace UiMetadataFramework.MediatR
{
	using System;

	/// <summary>
	/// Represents a function which can get form's unique id.
	/// </summary>
	public delegate string GetFormIdDelegate(Type formType);
}