namespace UiMetadataFramework.Tests.Framework.Outputs.ObjectList;

using System.Collections.Generic;
using UiMetadataFramework.Basic.Output;
using UiMetadataFramework.Core.Binding;

[MyOutputComponent("object-list", typeof(ObjectListMetadataFactory))]
[HasConfiguration(typeof(ObjectListAttribute), mandatory: true)]
public class ObjectList<T>
{
	public List<T>? Items { get; set; }
}