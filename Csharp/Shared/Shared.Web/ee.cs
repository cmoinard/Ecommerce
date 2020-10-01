using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared.Web
{
    public class ListModelBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
            {
                ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            var values = value
                .Split(',')
                .Select(k => (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(k))
                .ToList();

            bindingContext.Result = ModelBindingResult.Success(values);

            return Task.CompletedTask;
        }
    }
}