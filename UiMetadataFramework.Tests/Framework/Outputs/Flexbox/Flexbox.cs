namespace UiMetadataFramework.Tests.Framework.Outputs.Flexbox;

using UiMetadataFramework.Basic.Output;
using UiMetadataFramework.Core.Binding;

[MyOutputComponent("flexbox", metadataFactory: typeof(FlexboxMetadataFactory))]
[HasConfiguration(typeof(FlexboxAttribute), mandatory: true)]
public abstract class Flexbox;