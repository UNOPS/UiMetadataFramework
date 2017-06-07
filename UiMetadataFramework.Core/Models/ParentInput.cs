namespace UiMetadataFramework.Core.Models
{
	public class ParentInput
	{
		public ParentInput(string name, object value)
		{
			this.Name = name;
			this.Value = value;
		}

		public ParentInput()
		{
		}

		public string Name { get; set; }
		public object Value { get; set; }
	}
}