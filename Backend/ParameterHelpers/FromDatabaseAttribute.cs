using Backend.AspPlugins;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Backend.ParameterHelpers;

/// <summary>
/// Requires a parameter in the route with the controller parameter name + "Id".
/// </summary>
public class FromDatabaseAttribute : ModelBinderAttribute {
    public FromDatabaseAttribute() : base(typeof(DatabaseBinder)) { }

    private class DatabaseBinder : IModelBinder {
        public async Task BindModelAsync(ModelBindingContext bindingContext) {
            var db = bindingContext.HttpContext.RequestServices.GetRequiredService<AtriaContext>();
            if (!long.TryParse(bindingContext.ValueProvider.GetValue(bindingContext.FieldName + "Id").FirstValue, out var id)) {
                bindingContext.Result = ModelBindingResult.Success(DatabaseBinderNullFilter.NullFromDatabase);
                return;
            }
            var obj = await db.FindAsync(bindingContext.ModelType, id);
            if (obj == null) {
                bindingContext.Result = ModelBindingResult.Success(DatabaseBinderNullFilter.NullFromDatabase);
                return;
            }
            foreach (var property in bindingContext.ModelMetadata.ValidatorMetadata.OfType<IncludeAttribute>()) {
                if (obj.GetType().GetProperty(property.Name)!.PropertyType.GetInterface("IEnumerable") == null)
                {
                    await db.Entry(obj).Reference(property.Name).LoadAsync();
                }
                else
                {
                    await db.Entry(obj).Collection(property.Name).LoadAsync();
                }
            }

            bindingContext.ValidationState.Add(obj, new() { SuppressValidation = true });
            bindingContext.Result = ModelBindingResult.Success(obj);
        }
    }
}