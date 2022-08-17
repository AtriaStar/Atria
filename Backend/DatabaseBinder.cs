using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace Backend;

public class FromDatabaseAttribute : ModelBinderAttribute {
    public FromDatabaseAttribute() : base(typeof(DatabaseBinder)) { }

    private class DatabaseBinder : IModelBinder {
        public async Task BindModelAsync(ModelBindingContext bindingContext) {
            var db = bindingContext.HttpContext.RequestServices.GetRequiredService<AtriaContext>();
            if (!long.TryParse(bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue, out var id)) {
                return;
            }
            var obj = await db.FindAsync(bindingContext.ModelType, id);
            bindingContext.Result = ModelBindingResult.Success(obj);
        }
    }
}
