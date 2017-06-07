namespace UiMetadataFramework.Core.UI.Inputs
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Metadata;
	using UiMetadataFramework.Core.Models;

	public class Input : PropertyMetadata
	{
		public Input(string name, string type, object parameters = null, List<Input> dependencies = null)
			: base(name, type, parameters)
		{
			this.Dependencies = dependencies;
		}

		public object DefaultValue { get; set; }
		public List<Input> Dependencies { get; set; }
		public bool Disabled { get; set; }
		public ParentInput ParentInput { get; set; }
		public string Placeholder { get; set; }
		public bool Required { get; set; }
	}
}