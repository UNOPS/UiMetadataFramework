// ReSharper disable MemberCanBePrivate.Global

namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// <see cref="ICustomPropertyAttribute"/> implementation for <see cref="int"/> properties.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	[CustomPropertyConfig(IsArray = false)]
	public class IntPropertyAttribute : Attribute, ICustomPropertyAttribute
	{
		/// <summary>
		/// Initializes a new instance of <see cref="StringPropertyAttribute"/> class.
		/// </summary>
		/// <param name="name">Name of the custom property.</param>
		/// <param name="value">Value of the custom property.</param>
		public IntPropertyAttribute(string name, int value)
		{
			this.Name = name;
			this.Value = value;
		}

		/// <summary>
		/// Gets or sets value for the custom property.
		/// </summary>
		public int Value { get; set; }

		/// <inheritdoc />
		public string Name { get; set; }

		/// <inheritdoc />
		public object GetValue(Type type, MetadataBinder binder)
		{
			return this.Value;
		}
	}
}