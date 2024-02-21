namespace UiMetadataFramework.Tests.Framework.Outputs.ObjectList;

using System.Collections.Generic;
using UiMetadataFramework.Core.Binding;

[OutputComponent("object-list", typeof(ObjectListAttribute))]
public class ObjectList<T>
{
	public List<T>? Items { get; set; }
}