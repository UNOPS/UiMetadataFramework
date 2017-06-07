namespace UiMetadataFramework.Core.UI.Inputs
{
	using System.Collections.Generic;

	public class CheckboxInput : Input
	{
		public CheckboxInput(string name) : base(name, "checkbox")
		{
		}

		public CheckboxInput(string name, List<Input> dependencies) : base(name, "checkbox", dependencies: dependencies)
		{
		}
	}
}