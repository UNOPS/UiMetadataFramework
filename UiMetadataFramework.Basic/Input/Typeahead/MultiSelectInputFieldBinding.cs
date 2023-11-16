// ReSharper disable MemberCanBePrivate.Global

namespace UiMetadataFramework.Basic.Input.Typeahead
{
	using System.Collections.Generic;
	using System.Linq;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Represents an input field where multiple values can be selected.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[InputFieldType("multiselect")]
	public class MultiSelect<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MultiSelect{T}"/> class.
		/// </summary>
		public MultiSelect()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MultiSelect{T}"/> class.
		/// </summary>
		public MultiSelect(params T[] values)
		{
			this.Items ??= new List<T>();
			foreach (var value in values.Distinct())
			{
				this.Items.Add(value);
			}
		}

		/// <summary>
		/// Gets or sets the list of items that are selected.
		/// </summary>
		public IList<T>? Items { get; set; }
	}
}