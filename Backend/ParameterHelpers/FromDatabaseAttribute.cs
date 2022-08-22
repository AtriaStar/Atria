﻿using Microsoft.AspNetCore.Mvc;
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
                await db.Entry(obj).Reference(property.Name).LoadAsync();
            }

            bindingContext.Result = ModelBindingResult.Success(obj);
        }
    }
}