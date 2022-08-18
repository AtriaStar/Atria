namespace Backend.ParameterHelpers;

/// <summary>
/// Causes the specified foreign key property to be loaded for the associated parameter.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct,
    AllowMultiple = true)]
public class IncludeAttribute : Attribute {
    public string Name { get; }

    public IncludeAttribute(string name) {
        Name = name;
    }
}
