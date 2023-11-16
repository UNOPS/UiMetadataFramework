namespace UiMetadataFramework.Basic.Inputs.Paginator;

using UiMetadataFramework.Basic.Output.PaginatedData;
using UiMetadataFramework.Core.Binding;

/// <summary>
/// Input component for controlling pagination parameters of a <see cref="PaginatedData{T}"/> 
/// </summary>
public class Paginator
{
	/// <summary>
	/// Indicates the order in which results should be sorted. This property works
	/// in combination with <see cref="OrderBy"/>. If this property is <c>null</c>,
	/// then no special order will be applied. 
	/// </summary>
	[InputField(Hidden = true)]
	public bool? Ascending { get; set; }

	/// <summary>
	/// The name of the property by which results should be sorted.
	/// </summary>
	[InputField(Hidden = true, Required = false)]
	public string? OrderBy { get; set; }

	/// <summary>
	/// Page number to retrieve. If <c>null</c>, then the first page will be retrieved.
	/// </summary>
	[InputField(Hidden = true)]
	public int? PageIndex { get; set; }

	/// <summary>
	/// Number of records to retrieve per page. If <c>null</c>, then the default page size will be used.
	/// </summary>
	[InputField(Hidden = true)]
	public int? PageSize { get; set; }
}