namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// This attribute can be applied to a property to indicate that it should not
	/// be treated as a field and so that no <see cref="FieldMetadata"/> is generated for it.
	/// </summary>
	public class NotFieldAttribute : Attribute
	{
	}
}