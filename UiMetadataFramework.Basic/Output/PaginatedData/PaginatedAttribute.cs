namespace UiMetadataFramework.Basic.Output.PaginatedData;

using UiMetadataFramework.Core.Binding;

/// <summary>
/// Represents a paginated data configuration.
/// </summary>
/// <param name="paginator">Name of the input field which will control the pagination parameters.</param>
public class PaginatedAttribute(string paginator) : ComponentConfigurationAttribute
{
	/// <summary>
	/// Name of the input field which will control the pagination parameters.
	/// </summary>
	[ConfigurationProperty("Paginator")]
	public string Paginator { get; } = paginator;
}