namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// <see cref="ICustomPropertyAttribute"/> implementation for <see cref="bool"/> properties.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	public class BooleanPropertyAttribute : Attribute, ICustomPropertyAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BooleanPropertyAttribute"/> class.
		/// </summary>
		/// <param name="name">Name of the custom property.</param>
		/// <param name="value">Value of the custom property.</param>
		public BooleanPropertyAttribute(string name, bool value)
		{
			this.Name = name;
			this.Value = value;
		}

		/// <summary>
		/// Gets or sets value for the custom property.
		/// </summary>
		public bool Value { get; set; }

		/// <inheritdoc />
		public string Name { get; set; }

		/// <inheritdoc />
		public object GetValue()
		{
			throw new NotImplementedException();
		}
	}
}