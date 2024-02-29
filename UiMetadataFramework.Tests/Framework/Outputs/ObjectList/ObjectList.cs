namespace UiMetadataFramework.Tests.Framework.Outputs.ObjectList;

using System.Collections.Generic;
using UiMetadataFramework.Core.Binding;

[OutputComponent("object-list", typeof(ObjectListMetadataFactory))]
[HasConfiguration(typeof(ObjectListAttribute), mandatory: true)]
public class ObjectList<T>
{
	public List<T>? Items { get; set; }
}