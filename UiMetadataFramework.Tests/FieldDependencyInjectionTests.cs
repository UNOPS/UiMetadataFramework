namespace UiMetadataFramework.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Basic;
	using UiMetadataFramework.Basic.Inputs.Text;
	using UiMetadataFramework.Basic.Inputs.Typeahead;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;
	using Xunit;

	public class FieldDependencyInjectionTests
	{
		public class Request
		{
			[Typeahead(typeof(CategorySource))]
			public TypeaheadValue<int>? CategoryId { get; set; }
		}

		public class Database
		{
			public IEnumerable<TypeaheadItem<int>> GetCategories()
			{
				return new List<TypeaheadItem<int>>
				{
					new("one", 1),
					new("two", 2),
					new("3", 3)
				};
			}
		}

		public class CategorySource : ITypeaheadInlineSource<int>
		{
			private readonly Database db;

			public CategorySource(Database db)
			{
				this.db = db;
			}

			public IEnumerable<TypeaheadItem<int>> GetItems()
			{
				return this.db.GetCategories();
			}
		}

		private static DependencyInjectionContainer GetDependencyInjectionContainer()
		{
			var container = new DefaultDependencyInjectionContainer();
			var defaultFunction = container.GetInstanceFunc;

			container.GetInstanceFunc = t =>
			{
				if (t == typeof(CategorySource))
				{
					return new CategorySource(new Database());
				}

				return defaultFunction(t);
			};

			return container;
		}

		[Fact]
		public void DependencyInjectionInFieldBindingWorks()
		{
			var metadataBinder = new MetadataBinder(GetDependencyInjectionContainer());

			metadataBinder.RegisterAssembly(typeof(StringInputComponentBinding).GetTypeInfo().Assembly);

			var fields = metadataBinder.Inputs.GetFields(typeof(Request)).ToList();
			var categoryInputField = fields.Single(t => t.Id == nameof(Request.CategoryId));

			// Ensure that the inline source has 3 items.
			var source = categoryInputField.CustomProperties?["Source"] as IEnumerable<TypeaheadItem<int>>;
			Assert.NotNull(source);
			Assert.Equal(3, source!.Count());
		}
	}
}