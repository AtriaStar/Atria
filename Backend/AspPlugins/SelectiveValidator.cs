using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Backend.AspPlugins;

/// <summary>
/// Prevents properties with a <see cref="JsonIgnoreAttribute"/> from causing validation failures when their class is being passed to the API.
/// </summary>
public class SelectiveValidator : IObjectModelValidator {
    private readonly IObjectModelValidator _baseValidator;

    public SelectiveValidator(IObjectModelValidator baseValidator) {
        _baseValidator = baseValidator;
    }

    public void Validate(ActionContext actionContext, ValidationStateDictionary? validationState, string prefix, object? model) {
        var preExisting = actionContext.ModelState.Keys.ToArray();
        _baseValidator.Validate(actionContext, validationState, prefix, model);
        if (model == null) { return; }

        var ignoredAttributes = new Lazy<HashSet<string>>(() => model.GetType().GetProperties()
            .Where(x => x.HasAttribute<JsonIgnoreAttribute>())
            .Select(x => x.Name)
            .ToHashSet());

        foreach (var key in actionContext.ModelState.Keys.Except(preExisting)) {
            if (ignoredAttributes.Value.Contains(key)) {
                actionContext.ModelState.Remove(key);
            }
        }
    }
}

public static class SelectiveValidatorExtensions {
    public static IServiceCollection AddSelectiveValidator(this IServiceCollection services) {
        var baseValidator = services.First(x => x.ServiceType == typeof(IObjectModelValidator));
        services.Remove(baseValidator);
        services.AddSingleton<IObjectModelValidator, SelectiveValidator>(s
            => new((IObjectModelValidator) baseValidator.ImplementationFactory!(s)));
        return services;
    }
}
