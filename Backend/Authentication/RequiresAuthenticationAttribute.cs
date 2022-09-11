namespace Backend.Authentication;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequiresAuthenticationAttribute : Attribute { }
