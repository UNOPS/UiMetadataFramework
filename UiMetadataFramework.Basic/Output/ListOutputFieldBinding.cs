namespace UiMetadataFramework.Basic.Output
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Binding;

	public class ListOutputFieldBinding : OutputFieldBinding
	{
		public const string ControlName = "values";

		public ListOutputFieldBinding() : base(typeof(ValueList<>), ControlName)
		{
		}
	}

	public class ValueList<T>
	{
		public IList<T> Items { get; set; }
	}
}