namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// <see cref="ICustomPropertyAttribute"/> implementation for <see cref="string"/> properties.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	[CustomPropertyConfig(IsArray = false)]
	public class StringPropertyAttribute : Attribute, ICustomPropertyAttribute
	{
		/// <summary>
		/// Initializes a new instance of <see cref="StringPropertyAttribute"/> class.
		/// </summary>
		/// <param name="name">Name of the custom property.</param>
		/// <param name="value">Value of the custom property.</param>
		public StringPropertyAttribute(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		/// <summary>
		/// Gets or sets value for the custom property.
		/// </summary>
		public string Value { get; set; }

		/// <inheritdoc />
		public string Name { get; set; }

		/// <inheritdoc />
		public object GetValue()
		{
			return this.Value;
		}
	}
}