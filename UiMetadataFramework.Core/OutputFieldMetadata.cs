namespace UiMetadataFramework.Core
{
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// <see cref="FieldMetadata"/> for an output component.
	/// </summary>
	public class OutputFieldMetadata : FieldMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OutputFieldMetadata"/> class.
		/// </summary>
		public OutputFieldMetadata(Component component) : base(component)
		{
		}

		/// <summary>
		/// Creates a copy of the specified instance.
		/// </summary>
		/// <param name="metadata">Instance to copy.</param>
		protected OutputFieldMetadata(OutputFieldMetadata metadata) : base(metadata)
		{
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.ToDescriptiveString();
		}
	}
}