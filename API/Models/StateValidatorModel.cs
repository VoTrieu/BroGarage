using BroGarage.API.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BroGarage.API.Models;

public class StateValidatorModel
{
    public static IActionResult ValidateModelState(ActionContext context)
    {
        IEnumerable<ValidationErrorModel> errResult;

        var errs = context.ModelState.Where(x => x.Value?.Errors.Count > 0).ToList();

        errResult = errs.Select(n => new ValidationErrorModel()
        {
            FieldName = n.Key,
            Message = n.Value?.Errors.FirstOrDefault()?.ErrorMessage ?? ""
        });

        var firstErr = errResult.FirstOrDefault();

        string firstMsg = $"{firstErr?.Message} [{firstErr?.FieldName}]";

        return new OkObjectResult(new ResponseModel()
        {
            Message = firstMsg,
            Result = errResult,
            Code = 500
        });
    }
}

public class ValidationErrorModel
{
    public string FieldName { get; set; } = "";

    public string Message { get; set; } = "";
}
