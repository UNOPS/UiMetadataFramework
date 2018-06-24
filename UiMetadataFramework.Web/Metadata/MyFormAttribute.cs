namespace UiMetadataFramework.Web.Metadata
{
	using System;
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Binding;

	public class MyFormAttribute : FormAttribute
	{
		/// <summary>
		/// Gets or sets text for the submit button.
		/// </summary>
		public string SubmitButtonLabel { get; set; } = "Submit";

		public override IDictionary<string, object> GetCustomProperties(Type type)
		{
			return base.GetCustomProperties(type).Set(nameof(this.SubmitButtonLabel), this.SubmitButtonLabel);
		}
	}
}