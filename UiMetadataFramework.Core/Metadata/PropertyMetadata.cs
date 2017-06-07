namespace UiMetadataFramework.Core.Metadata
{
	using System;

	public class PropertyMetadata
	{
		public PropertyMetadata(string name, string type, object parameters = null)
		{
			this.Type = type;
			this.Parameters = parameters;
			this.Name = name;
			this.Label = GetDefaultLabel(name);
		}

		public bool Hidden { get; set; }
		public bool IsList { get; set; }
		public string Label { get; set; }

		public string Name { get; }
		public int OrderIndex { get; set; } = int.MaxValue;
		public virtual object Parameters { get; set; }

		public string Type { get; internal set; }

		public static string GetDefaultLabel(string name)
		{
			if (name == null)
			{
				return null;
			}

			var lastDot = name.LastIndexOf(".", StringComparison.OrdinalIgnoreCase);

			if (lastDot == -1)
			{
				return name;
			}

			return lastDot < name.Length - 1 ? name.Substring(lastDot + 1) : null;
		}
	}
}