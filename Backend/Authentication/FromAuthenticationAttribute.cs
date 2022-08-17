using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Backend.Authentication;

[AttributeUsage(AttributeTargets.Parameter)]
public class FromAuthenticationAttribute : BindingBehaviorAttribute {
    public FromAuthenticationAttribute() : base(BindingBehavior.Never) { }
}
