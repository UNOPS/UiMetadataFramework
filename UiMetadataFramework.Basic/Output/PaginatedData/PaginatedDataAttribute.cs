namespace UiMetadataFramework.Basic.Output.PaginatedData;

using UiMetadataFramework.Core.Binding;

/// <summary>
/// Represents a paginated data configuration.
/// </summary>
/// <param name="paginator">Name of the input field which will control the pagination parameters.</param>
public class PaginatedDataAttribute(string paginator) : ConfigurationDataAttribute
{
	/// <summary>
	/// Name of the input field which will control the pagination parameters.
	/// </summary>
	public string Paginator { get; } = paginator;
}