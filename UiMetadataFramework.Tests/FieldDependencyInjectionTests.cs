namespace UiMetadataFramework.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Basic;
	using UiMetadataFramework.Basic.Input;
	using UiMetadataFramework.Basic.Input.Typeahead;
	using UiMetadataFramework.Core.Binding;
	using Xunit;

	public class FieldDependencyInjectionTests
	{
		public class Request
		{
			[TypeaheadInputField(typeof(CategorySource))]
			public TypeaheadValue<int> CategoryId { get; set; }
		}

		public class Database
		{
			public IEnumerable<TypeaheadItem<int>> GetCategories()
			{
				return new List<TypeaheadItem<int>>
				{
					new TypeaheadItem<int>("one", 1),
					new TypeaheadItem<int>("two", 2),
					new TypeaheadItem<int>("3", 3)
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

			metadataBinder.RegisterAssembly(typeof(StringInputFieldBinding).GetTypeInfo().Assembly);

			var fields = metadataBinder.BindInputFields<Request>().ToList();
			var categoryInputField = fields.Single(t => t.Id == nameof(Request.CategoryId));
			var customProperties = (TypeaheadCustomProperties)categoryInputField.CustomProperties;

			// Ensure that the inline source has 3 items.
			var source = customProperties.Source as IEnumerable<TypeaheadItem<int>>;
			Assert.NotNull(source);
			Assert.Equal(3, source.Count());
		}
	}
}