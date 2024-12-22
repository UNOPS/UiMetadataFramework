namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Declares that the decorated class is an input component.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, Inherited = false)]
	public class InputComponentAttribute : ComponentAttribute
	{
		/// <inheritdoc />
		public InputComponentAttribute(
			string name,
			Type? metadataFactory = null) : base(name, metadataFactory)
		{
		}
	}
}