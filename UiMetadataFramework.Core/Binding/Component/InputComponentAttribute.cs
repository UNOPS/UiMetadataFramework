namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Declares that the decorated class is an input component.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, Inherited = false)]
	public class InputComponentAttribute : ComponentAttribute
	{
		/// <inheritdoc />
		public InputComponentAttribute(
			string name,
			Type? metadataFactory = null) : base(name, metadataFactory)
		{
		}

		/// <summary>
		/// Gets or sets value indicating whether input should never be explicitly rendered on the client.
		/// If this value is set to true, then <see cref="InputFieldMetadata.Hidden"/> will always
		/// be true.
		/// </summary>
		public bool AlwaysHidden { get; set; }
	}
}