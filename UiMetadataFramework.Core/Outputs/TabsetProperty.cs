namespace UiMetadataFramework.Core.Outputs
{
	public class TabsetProperty : PropertyMetadata
	{
		public const string Typename = "tabset";

		public TabsetProperty(string name, string defaultTab = null) : base(name, Typename)
		{
			this.DefaultTab = defaultTab;
		}

		/// <summary>
		/// Gets or sets name of the default tab.
		/// </summary>
		public string DefaultTab { get; set; }
	}
}