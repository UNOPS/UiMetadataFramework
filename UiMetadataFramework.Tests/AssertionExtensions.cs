namespace UiMetadataFramework.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using UiMetadataFramework.Core;
	using Xunit;

	public static class AssertionExtensions
	{
		public static void AssertHasInputField(
			this IEnumerable<InputFieldMetadata> fields,
			string id,
			string type,
			string label,
			bool hidden = false,
			int orderIndex = 0,
			bool required = false,
			string defaultValueSourceType = null,
			string defaultValueSourceId = null)
		{
			var hasField = fields.Any(t =>
				t.Id == id &&
				t.Hidden == hidden &&
				t.Type == type &&
				t.OrderIndex == orderIndex &&
				t.Label == label &&
				t.Required == required &&
				t.DefaultValue?.Type == defaultValueSourceType &&
				t.DefaultValue?.Id == defaultValueSourceId);

			Assert.True(hasField);
		}

		public static void AssertHasOutputField(
			this IEnumerable<OutputFieldMetadata> fields,
			string id,
			string type,
			string label,
			bool hidden = false,
			int orderIndex = 0)
		{
			var hasField = fields.Any(t =>
				t.Id == id &&
				t.Hidden == hidden &&
				t.Type == type &&
				t.OrderIndex == orderIndex &&
				t.Label == label);

			Assert.True(hasField);
		}
	}
}