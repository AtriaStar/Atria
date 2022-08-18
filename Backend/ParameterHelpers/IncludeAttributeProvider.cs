using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Backend.ParameterHelpers;

public class IncludeAttributeProvider : IValidationMetadataProvider {
    public void CreateValidationMetadata(ValidationMetadataProviderContext context) {
        foreach (var attribute in context.Attributes.OfType<IncludeAttribute>()) {
            context.ValidationMetadata.ValidatorMetadata.Add(attribute);
        }
    }
}
