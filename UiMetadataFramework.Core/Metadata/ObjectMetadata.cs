namespace UiMetadataFramework.Core.Metadata
{
	using System;
	using System.Linq.Expressions;

	public class ObjectMetadata<TModel>
	{
		public ObjectMetadata<TProperty> In<TProperty>(Expression<Func<TModel, TProperty>> property)
		{
			return new ObjectMetadata<TProperty>();
		}

		public PropertyMetadata In<TModel2>(Expression<Func<TModel2, object>> property)
		{
			return MetadataBinder.Default.Property(property);
		}

		public PropertyMetadata Property<TProperty>(Expression<Func<TModel, TProperty>> property)
		{
			return MetadataBinder.Default.Property(property);
		}
	}
}