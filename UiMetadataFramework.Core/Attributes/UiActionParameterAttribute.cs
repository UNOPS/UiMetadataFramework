namespace UiMetadataFramework.Core.Attributes
{
	using System;

	public class UiActionParameterAttribute : Attribute
	{
		public UiActionParameterAttribute(string parameter, string property)
		{
			this.Parameter = parameter;
			this.Property = property;
		}

		public string Parameter { get; set; }
		public string Property { get; set; }
	}
}