using UiMetadataFramework.Core.Binding;

class Program
{
    static void Main()
    {
        // This should trigger the warning in your analyzer
        var binding = new InputFieldBinding(typeof(string), "SomeClientType");
    }
}
