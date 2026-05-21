using ClosedXML.Excel;
using BroGarage.API.Models;
using BroGarage.API.Data;
using BroGarage.API.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BroGarage.API.Controllers;

[Route("[controller]")]
[ApiController]
[Produces("application/json")]
public class BaseController : ControllerBase
{
    protected DatabaseContext db = null!;

    protected int USER_ID => Request.HttpContext.Items.TryGetValue("User", out object? value) ? (value as UserJwtModel)?.UserId ?? 0 : 0;

    protected const string NOT_FOUND_MSG = "Dữ liệu không có trong hệ thống";

    protected const string IN_USED_MSG = "Dữ liệu đang được sử dụng";

    protected const string FROM_DATE_INVALID = "Từ ngày không đúng định dạng";

    protected const string TO_DATE_INVALID = "Đến ngày không đúng định dạng";

    protected const string COLLATION = "SQL_Latin1_General_CP1_CI_AI";

    protected const string EXCEL_CONTENT_TYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    public BaseController(DatabaseContext db)
    {
        this.db = db;
    }

    protected FileContentResult MessageFile(string message)
    {
        using var memoryStream = new MemoryStream();

        XLWorkbook workbook = new();

        IXLWorksheet worksheet = workbook.AddWorksheet("Sheet1");

        worksheet.Cell(1, 1).Value = message;

        workbook.SaveAs(memoryStream);

        var bytes = memoryStream.ToArray();

        return File(bytes, EXCEL_CONTENT_TYPE);
    }

    protected FileContentResult ErrorFile<T>(ResponseModel<T?>? response)
    {
        using var memoryStream = new MemoryStream();

        XLWorkbook workbook = new();

        IXLWorksheet worksheet = workbook.AddWorksheet("Sheet1");

        if (response == null)
        {
            worksheet.Cell(1, 1).Value = "Null response";
            goto End;
        }

        if (!response.IsSuccess)
        {
            worksheet.Cell(1, 1).Value = response.Message;
            goto End;
        }

        if (response.Result == null)
        {
            worksheet.Cell(1, 1).Value = "Null result";
            goto End;
        }

    End:

        workbook.SaveAs(memoryStream);

        var bytes = memoryStream.ToArray();

        return File(bytes, EXCEL_CONTENT_TYPE);
    }
}
