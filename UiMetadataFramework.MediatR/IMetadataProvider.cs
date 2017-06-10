namespace UiMetadataFramework.MediatR
{
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	public interface IMetadataProvider
	{
		FormMetadata GetMetadata(MetadataBinder binder);
	}
}