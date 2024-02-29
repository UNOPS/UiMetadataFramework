namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Declares that the decorated class is an output component.
	/// </summary>
	public class OutputComponentAttribute : ComponentAttribute
	{
		/// <inheritdoc />
		public OutputComponentAttribute(
			string name,
			Type? metadataFactory = null) : base(name, metadataFactory)
		{
		}
	}
}