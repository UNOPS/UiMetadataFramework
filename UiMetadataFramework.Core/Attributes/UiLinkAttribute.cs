namespace UiMetadataFramework.Core.Attributes
{
	using UiMetadataFramework.Core.Outputs;

	public sealed class UiLinkAttribute : UiPropertyAttribute
	{
		/// <summary>
		/// Creates a new instance of <see cref="UiLinkAttribute"/> class.
		/// </summary>
		/// <param name="entityType">Type of entity to which the reference refers.</param>
		public UiLinkAttribute(string entityType)
		{
			this.Type = HyperLinkProperty.Typename;
			this.Parameters = entityType;
		}

		public PropertyMetadata ToProperty(string name)
		{
			return new HyperLinkProperty(name)
			{
				Type = this.Type,
				Parameters = this.Parameters
			};
		}
	}
}