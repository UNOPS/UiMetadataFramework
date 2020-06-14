namespace UiMetadataFramework.Basic.Input.Dropdown
{
	using System;
	using Humanizer;

	/// <summary>
	/// Represents an item that should be avaialble for user to pick
	/// as one of the options from an input field such as dropdown.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	public class OptionAttribute : Attribute
	{
		public OptionAttribute(string label, string value)
		{
			this.Label = label;
			this.Value = value;
		}

		public OptionAttribute(object value)
		{
			var v = (Enum)value;
			this.Label = v.Humanize();
			this.Value = v.ToString();
		}

		public OptionAttribute()
		{
		}

		public string Label { get; set; }
		public string Value { get; set; }
	}
}