namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Used to decorate an attribute implementing <see cref="ICustomPropertyAttribute"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class CustomPropertyConfigAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets a value indicating whether the custom property will hold
		/// an array of values or a single value. If set to true, then the associated
		/// <see cref="ICustomPropertyAttribute"/> can be applied multiple times
		/// and the value from each attribute usage will be added to the final array, which
		/// will constitute the custom property's value.
		/// </summary>
		public bool IsArray { get; set; }
	}
}