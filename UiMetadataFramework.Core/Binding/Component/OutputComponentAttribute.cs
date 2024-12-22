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

		/// <summary>
		/// If true then the output field won't have a label, unless one is explicitly given
		/// in `<see cref="OutputFieldAttribute"/>.<see cref="OutputFieldAttribute.Label"/>`.
		/// </summary>
		public bool NoLabelByDefault { get; set; }
	}
}