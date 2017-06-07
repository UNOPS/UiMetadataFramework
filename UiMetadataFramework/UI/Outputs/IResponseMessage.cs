namespace UiMetadataFramework.Core.UI.Outputs
{
	using UiMetadataFramework.Core.Models;

	/// <summary>
	/// Response message which should be displayed in the UI with ResponseAction
	/// </summary>
	public interface IResponseMessage
	{
		Message ResponseMessage { get; }
	}
}