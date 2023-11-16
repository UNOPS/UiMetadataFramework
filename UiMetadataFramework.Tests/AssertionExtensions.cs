namespace UiMetadataFramework.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using FluentAssertions;
	using UiMetadataFramework.Core;
	using Xunit;

	public static class AssertionExtensions
	{
		public static InputFieldMetadata AssertHasInputField(
			this IEnumerable<InputFieldMetadata> fields,
			string id)
		{
			var matching = fields
				.Where(t => t.Id == id)
				.ToList();

			matching.Should().HaveCount(1);

			return matching[0];
		}

		public static InputFieldMetadata AssertHasInputField(
			this IEnumerable<InputFieldMetadata> fields,
			string id,
			string type,
			string label,
			bool hidden = false,
			int orderIndex = 0,
			bool required = false,
			string[]? eventHandlers = null)
		{
			var field = fields
				.Where(t => t.Id == id)
				.Where(t => t.Hidden == hidden)
				.Where(t => t.Type == type)
				.Where(t => t.OrderIndex == orderIndex)
				.Where(t => t.Label == label)
				.Where(t => t.Required == required)
				.FirstOrDefault(t => eventHandlers == null || eventHandlers.All(p => t.EventHandlers?.Any(x => x.Id == p) == true));

			field.Should().NotBeNull("field '{0}' is expected to exist", id);

			return field!;
		}

		public static OutputFieldMetadata AssertHasOutputField(
			this IEnumerable<OutputFieldMetadata> fields,
			string id)
		{
			var matching = fields
				.Where(t => t.Id == id)
				.ToList();

			matching.Should().HaveCount(1, "field '{0}' is expected to exist", id);

			return matching[0];
		}

		public static OutputFieldMetadata AssertHasOutputField(
			this IEnumerable<OutputFieldMetadata> fields,
			string id,
			string type,
			string label,
			bool hidden = false,
			int orderIndex = 0,
			string[]? eventHandlers = null)
		{
			var field = fields
				.Where(t => t.Id == id)
				.Where(t => t.Hidden == hidden)
				.Where(t => t.Type == type)
				.Where(t => t.OrderIndex == orderIndex)
				.Where(t => t.Label == label)
				.FirstOrDefault(t => eventHandlers == null || eventHandlers.All(p => t.EventHandlers?.Any(x => x.Id == p) == true));

			field.Should().NotBeNull("field '{0}' is expected to exist", id);

			return field!;
		}

		public static InputFieldMetadata HasCustomProperty<T>(
			this InputFieldMetadata field,
			string property,
			Func<T, bool> assertion,
			string message)
			where T : class
		{
			return field.HasCustomPropertyInternal(property, assertion, message);
		}

		public static OutputFieldMetadata HasCustomProperty<T>(
			this OutputFieldMetadata field,
			string property,
			Func<T, bool> assertion,
			string? message = null)
			where T : class
		{
			return field.HasCustomPropertyInternal(property, assertion, message);
		}

		public static OutputFieldMetadata HasCustomProperty<T>(
			this OutputFieldMetadata field,
			string name,
			T value)
		{
			return field.AssertHasCustomProperty(name, value);
		}

		public static InputFieldMetadata HasCustomProperty<T>(
			this InputFieldMetadata field,
			string name,
			T value)
		{
			return field.AssertHasCustomProperty(name, value);
		}

		public static FormMetadata HasCustomProperty<T>(
			this FormMetadata metadata,
			string name,
			T value)
		{
			if (metadata.CustomProperties?.TryGetValue(name, out var actual) is true)
			{
				Assert.Equal(value, actual);

				return metadata;
			}

			throw new Exception($"Custom property '{name}' is missing.");
		}

		private static TFieldMetadata AssertHasCustomProperty<TValue, TFieldMetadata>(
			this TFieldMetadata field,
			string name,
			TValue value)
			where TFieldMetadata : IFieldMetadata
		{
			if (field.CustomProperties?.TryGetValue(name, out var actual) is true)
			{
				Assert.Equal(value, actual);

				return field;
			}

			throw new Exception($"Custom property '{name}' is missing.");
		}

		private static TFieldMetadata HasCustomPropertyInternal<TFieldMetadata, T>(
			this TFieldMetadata field,
			string property,
			Func<T, bool> assertion,
			string? message = null)
			where T : class
			where TFieldMetadata : IFieldMetadata
		{
			var customProperties = (T?)field.CustomProperties?[property];

			if (customProperties == null)
			{
				throw new Exception($"Custom property '{property}' is missing.");
			}

			customProperties
				.Should()
				.NotBeNull($"field '{property}' is expected to have custom properties");

			Assert.True(assertion(customProperties), message);

			return field;
		}
	}
}