namespace UiMetadataFramework.Core.Inputs
{
	using System.Collections.Generic;

	public class CheckboxInput : InputMetadata
	{
		public CheckboxInput(string name) : base(name, "checkbox")
		{
		}

		public CheckboxInput(string name, List<InputMetadata> dependencies) : base(name, "checkbox")
		{
		}
	}
}