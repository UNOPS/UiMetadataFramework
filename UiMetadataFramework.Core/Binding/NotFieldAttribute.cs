namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// This attribute can be applied to a property of a <see cref="FormResponse"/> object,
	/// so that no <see cref="OutputFieldMetadata"/> is generated for that property.
	/// </summary>
	public class NotFieldAttribute : Attribute
	{
	}
}