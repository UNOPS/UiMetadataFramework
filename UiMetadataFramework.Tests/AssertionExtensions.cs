namespace UiMetadataFramework.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UiMetadataFramework.Core;
	using Xunit;

	public static class AssertionExtensions
	{
		public static InputFieldMetadata AssertHasInputField(
			this IEnumerable<InputFieldMetadata> fields,
			string id,
			string type,
			string label,
			bool hidden = false,
			int orderIndex = 0,
			bool required = false,
			string[] inputProcessorIds = null)
		{
			var field = fields.FirstOrDefault(t =>
				t.Id == id &&
				t.Hidden == hidden &&
				t.Type == type &&
				t.OrderIndex == orderIndex &&
				t.Label == label &&
				t.Required == required &&
				inputProcessorIds == null || inputProcessorIds?.All(p => t.Processors.Any(x => x.Id == p)) == true);

			Assert.NotNull(field);

			return field;
		}

		public static OutputFieldMetadata AssertHasOutputField(
			this IEnumerable<OutputFieldMetadata> fields,
			string id,
			string type,
			string label,
			bool hidden = false,
			int orderIndex = 0)
		{
			var field = fields.FirstOrDefault(t =>
				t.Id == id &&
				t.Hidden == hidden &&
				t.Type == type &&
				t.OrderIndex == orderIndex &&
				t.Label == label);

			Assert.NotNull(field);

			return field;
		}

		public static InputFieldMetadata HasCustomProperties<T>(this InputFieldMetadata field, Func<T, bool> assertion, string message)
			where T : class
		{
			var customProperties = (T)field.CustomProperties;

			Assert.NotNull(customProperties);

			Assert.True(assertion(customProperties), message);

			return field;
		}
	}
}