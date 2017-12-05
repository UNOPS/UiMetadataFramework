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
			string[] eventHandlers = null)
		{
			var field = fields.FirstOrDefault(t =>
				t.Id == id &&
				t.Hidden == hidden &&
				t.Type == type &&
				t.OrderIndex == orderIndex &&
				t.Label == label &&
				t.Required == required &&
				eventHandlers == null || eventHandlers?.All(p => t.EventHandlers.Any(x => x.Id == p)) == true);

			Assert.NotNull(field);

			return field;
		}

		public static OutputFieldMetadata AssertHasOutputField(
			this IEnumerable<OutputFieldMetadata> fields,
			string id,
			string type,
			string label,
			bool hidden = false,
			int orderIndex = 0,
			string[] eventHandlers = null)
		{
			var field = fields.FirstOrDefault(t =>
				t.Id == id &&
				t.Hidden == hidden &&
				t.Type == type &&
				t.OrderIndex == orderIndex &&
				t.Label == label &&
				eventHandlers == null || eventHandlers?.All(p => t.EventHandlers.Any(x => x.Id == p)) == true);

			Assert.NotNull(field);

			return field;
		}

		public static InputFieldMetadata HasCustomProperty<T>(this InputFieldMetadata field, string property, Func<T, bool> assertion, string message)
			where T : class
		{
			var customProperties = (T)field.CustomProperties[property];

			Assert.NotNull(customProperties);

			Assert.True(assertion(customProperties), message);

			return field;
		}

		public static OutputFieldMetadata HasCustomProperty<T>(this OutputFieldMetadata field, string name, T value)
		{
			return field.AssertHasCustomProperty(name, value);
		}

		public static InputFieldMetadata HasCustomProperty<T>(this InputFieldMetadata field, string name, T value)
		{
			return field.AssertHasCustomProperty(name, value);
		}

		public static FormMetadata HasCustomProperty<T>(this FormMetadata metadata, string name, T value)
		{
			if (metadata.CustomProperties?.ContainsKey(name) == true)
			{
				var actual = metadata.CustomProperties[name];
				Assert.Equal(value, actual);

				return metadata;
			}

			throw new Exception($"Custom property '{name}' is missing.");
		}

		private static TFieldMetadata AssertHasCustomProperty<TValue, TFieldMetadata>(this TFieldMetadata field, string name, TValue value)
			where TFieldMetadata : IFieldMetadata
		{
			if (field.CustomProperties?.ContainsKey(name) == true)
			{
				var actual = field.CustomProperties[name];
				Assert.Equal(value, actual);

				return field;
			}

			throw new Exception($"Custom property '{name}' is missing.");
		}
	}
}