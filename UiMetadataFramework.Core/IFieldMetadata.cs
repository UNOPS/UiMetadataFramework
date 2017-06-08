namespace UiMetadataFramework.Core
{
	public interface IFieldMetadata
	{
		string Id { get; }
		string Label { get; }
		string Type { get; }
		bool Hidden { get; }
		int OrderIndex { get; }
	}
}