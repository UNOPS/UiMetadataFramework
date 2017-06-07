namespace UiMetadataFramework.Core.ResponseActions
{
	public abstract class ResponseAction
	{
		protected ResponseAction(string name)
		{
			this.Name = name;
		}

		public string Name { get; }
	}
}