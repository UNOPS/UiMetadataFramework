namespace UiMetadataFramework.Core.UI.Inputs
{
	using System.Collections.Generic;

	public class TypeaheadInput : Input
	{
		public const int ItemsPerRequest = 100;

		public TypeaheadInput(string name, string source, bool isParentSelectable = false) : base(name, "typeahead", source)
		{
			this.IsParentSelectable = isParentSelectable;
		}

		public IList<FormParameter> FormParameters { get; set; } = new List<FormParameter>();
		public bool IsParentSelectable { get; private set; }
	}
}