namespace UiMetadataFramework.Web.Metadata
{
	using UiMetadataFramework.MediatR;

	public class MyFormAttribute : FormAttribute
	{
		/// <summary>
		/// Gets or sets text for the submit button.
		/// </summary>
		public string SubmitButtonLabel { get; set; } = "Submit";

		public override object GetCustomProperties()
		{
			return new
			{
				this.SubmitButtonLabel
			};
		}
	}
}