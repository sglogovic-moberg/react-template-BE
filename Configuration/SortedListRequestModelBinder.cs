using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ReactAppBackend.Configuration;
/// <summary>
/// https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/custom-model-binding
/// </summary>
public sealed class SortedListRequestModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        // we expect string to be in "[-]{columnName}, [-]{columnName}" format
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        var criterionStrings = valueProviderResult.FirstValue?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? Array.Empty<string>();
        var criteria = new List<Helpers.SortCriterion>();
        foreach (var criterionString in criterionStrings)
        {
            var isDescending = criterionString.StartsWith('-');
            var column = isDescending ? criterionString[1..] : criterionString;
            var sortDirection = isDescending ? Helpers.SortDirection.Descending : Helpers.SortDirection.Ascending;

            var criterion = new Helpers.SortCriterion(column, sortDirection);
            criteria.Add(criterion);
        }

        bindingContext.Result = ModelBindingResult.Success(criteria);

        return Task.CompletedTask;
    }
}

public sealed class SortedListRequestModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(List<Helpers.SortCriterion>))
        {
            return new BinderTypeModelBinder(typeof(SortedListRequestModelBinder));
        }

        return null;
    }
}