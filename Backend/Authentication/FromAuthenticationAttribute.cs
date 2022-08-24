using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Backend.Authentication;

[AttributeUsage(AttributeTargets.Parameter)]
public class FromAuthenticationAttribute : BindingBehaviorAttribute, IBindingSourceMetadata {
    public FromAuthenticationAttribute() : base(BindingBehavior.Never) { }
    public BindingSource BindingSource => BindingSource.Custom;
}
