namespace UiMetadataFramework.Core
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Metadata;

	public interface IHasPropertyMetadata
	{
		IList<PropertyMetadata> Outputs { get; }
	}
}