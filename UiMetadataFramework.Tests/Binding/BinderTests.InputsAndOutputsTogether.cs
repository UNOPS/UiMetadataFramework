namespace UiMetadataFramework.Tests.Binding
{
    using UiMetadataFramework.Core.Binding;

    public partial class BinderTests
    {
        public class InputsAndOutputsTogether
        {
            [OutputField]
            public string? Label { get; set; }

            public string? NotField { get; set; }

            [InputField]
            public string? Value { get; set; }
        }
    }
}