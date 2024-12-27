namespace UiMetadataFramework.Core.Binding;

/// <summary>
/// Configuration for <see cref="MetadataBinder"/>.
/// </summary>
public class MetadataBinderConfiguration
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MetadataBinderConfiguration"/> class.
	/// </summary>
	public MetadataBinderConfiguration(
		IFieldMetadataFactory inputFieldMetadataFactory,
		IFieldMetadataFactory outputFieldMetadataFactory)
	{
		this.InputFieldMetadataFactory = inputFieldMetadataFactory;
		this.OutputFieldMetadataFactory = outputFieldMetadataFactory;
	}

	/// <summary>
	/// <see cref="IFieldMetadataFactory"/> type for inputs.
	/// </summary>
	public IFieldMetadataFactory InputFieldMetadataFactory { get; }

	/// <summary>
	/// <see cref="IFieldMetadataFactory"/> type for outputs.
	/// </summary>
	public IFieldMetadataFactory OutputFieldMetadataFactory { get; }
}