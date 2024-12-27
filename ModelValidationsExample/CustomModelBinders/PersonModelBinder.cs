using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.CustomModelBinders;

public class PersonModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var person = new Person();
        
        // 名称
        var name = bindingContext.ValueProvider.GetValue("Name");
        if (name.Length > 0)
        {
            person.Name = name.FirstValue;
        } 
        
        // 邮箱
        var email = bindingContext.ValueProvider.GetValue("Email");
        if (email.Length > 0)
        {
            person.Email = email.FirstValue;
        }
        
        bindingContext.Result = ModelBindingResult.Success(person);
        return Task.CompletedTask;
    }
}