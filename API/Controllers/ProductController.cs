using ClosedXML.Excel;
using BroGarage.API.Attributes;
using BroGarage.API.Data;
using BroGarage.API.Data.Entities;
using BroGarage.API.Shared.Extensions;
using BroGarage.API.Shared.Models;
using BroGarage.API.Shared.RequestModels.Product;
using BroGarage.API.Shared.ResponseModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BroGarage.API.Controllers;

[Route("product")]
[Auth]
public class ProductController : BaseController
{
    public ProductController(DatabaseContext db) : base(db)
    {
    }

    const string EXISTS_CODE = "Mã sản phẩm đã có trong hệ thống";
    const string EXISTS_NAME = "Tên sản phẩm đã có trong hệ thống";

    [HttpGet("get-pagination")]
    public async Task<ActionResult<ResponseModel<PaginationModel<ProductResModel[]>>>> GetPagination(string? keyword = "", int pageSize = 10, int pageIndex = 1)
    {
        ResponseModel<PaginationModel<ProductResModel[]>> response = new();

        var query = db.Products
            .Where(n =>
                (
                    (string.IsNullOrEmpty(keyword) || EF.Functions.Like(EF.Functions.Collate(n.ProductCode ?? "", COLLATION), $"%{keyword}%"))
                    || (string.IsNullOrEmpty(keyword) || EF.Functions.Like(EF.Functions.Collate(n.ProductName ?? "", COLLATION), $"%{keyword}%"))
                    || (string.IsNullOrEmpty(keyword) || EF.Functions.Like(EF.Functions.Collate(n.Remark ?? "", COLLATION), $"%{keyword}%"))
                )
            )
            .OrderByDescending(n => n.ProductId);

        int skipItem = pageSize * (pageIndex - 1);
        int totalRow = query != null ? await query.CountAsync() : 0;
        int totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRow) / pageSize));

        var pagination = new PaginationModel<ProductResModel[]>()
        {
            PageSize = pageSize,
            PageIndex = pageIndex,
            TotalPage = totalPage,
            TotalRow = totalRow
        };

        if (query == null)
        {
            response.IsSuccess = true;
            response.Result = pagination;
            return Ok(response);
        }

        var data = await query
            .Skip(skipItem)
            .Take(pageSize)
            .Select(n => new ProductResModel()
            {
                ProductId = n.ProductId,
                ProductCode = n.ProductCode ?? "",
                ProductName = n.ProductName ?? "",
                Remark = n.Remark,
                UnitName = n.UnitName,
                UnitPrice = n.UnitPrice,
                Quantity = n.Quantity,
                AvatarUrl = n.AvatarUrl,
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime()
            })
            .ToArrayAsync();

        pagination.Data = data;

        response.Result = pagination;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpPost("add")]
    public async Task<ActionResult<ResponseModel<int>>> Add(ProductAddReqModel req)
    {
        ResponseModel<int> response = new();

        if (await db.Products.AnyAsync(n => n.ProductCode == req.ProductCode))
        {
            response.Message = EXISTS_CODE;
            return Ok(response);
        }

        if (await db.Products.AnyAsync(n => n.ProductName == req.ProductName))
        {
            response.Message = EXISTS_NAME;
            return Ok(response);
        }

        var newProduct = new ProductEntity()
        {
            ProductCode = req.ProductCode,
            ProductName = req.ProductName,
            Remark = req.Remark,
            UnitName = req.UnitName,
            UnitPrice = req.UnitPrice,
            Quantity = req.Quantity,
            AvatarUrl = req.AvatarUrl,
            CreatedUserId = USER_ID
        };

        await db.Products.AddAsync(newProduct);

        await db.SaveChangesAsync();

        response.Result = newProduct.ProductId;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<ResponseModel<ProductResModel>>> GetById(int id)
    {
        ResponseModel<ProductResModel> response = new();

        var query = await db.Products
            .Where(n => n.ProductId == id)
            .Select(n => new ProductResModel()
            {
                ProductId = n.ProductId,
                ProductCode = n.ProductCode ?? "",
                ProductName = n.ProductName ?? "",
                Remark = n.Remark,
                UnitName = n.UnitName,
                UnitPrice = n.UnitPrice,
                Quantity = n.Quantity,
                AvatarUrl = n.AvatarUrl,
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime()
            })
            .FirstOrDefaultAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpPut("edit")]
    public async Task<ActionResult<ResponseModel>> Edit(ProductEditReqModel req)
    {
        ResponseModel response = new();

        var query = await db.Products
            .Where(n => n.ProductId == req.ProductId)
            .AsTracking()
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        if (await db.Products.AnyAsync(n => n.ProductCode == req.ProductCode && n.ProductId != req.ProductId))
        {
            response.Message = EXISTS_CODE;
            return Ok(response);
        }

        if (await db.Products.AnyAsync(n => n.ProductName == req.ProductName && n.ProductId != req.ProductId))
        {
            response.Message = EXISTS_NAME;
            return Ok(response);
        }

        query.ProductCode = req.ProductCode;
        query.ProductName = req.ProductName;
        query.UnitName = req.UnitName;
        query.UnitPrice = req.UnitPrice;
        query.Quantity = req.Quantity;
        query.Remark = req.Remark;
        query.AvatarUrl = req.AvatarUrl;
        query.UpdatedUserId = USER_ID;
        query.UpdatedDateTime = DateTime.Now;

        await db.SaveChangesAsync();

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<ResponseModel>> Delete(int id)
    {
        ResponseModel response = new();

        var query = await db.Products
            .Where(n => n.ProductId == id)
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        if (await db.OrderDetails.AnyAsync(n => n.ProductId == id))
        {
            response.Message = "Phụ tùng đã được sử dụng cho đơn hàng";
            return Ok(response);
        }

        db.Products.Remove(query);

        await db.SaveChangesAsync();

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("export")]
    public async Task<FileResult> Export()
    {
        var products = await db.Products
            .Select(n => new
            {
                n.ProductId,
                n.ProductCode,
                n.ProductName,
                n.UnitName,
                n.UnitPrice,
                n.Quantity,
                n.Remark
            })
            .ToArrayAsync();

        using var memoryStream = new MemoryStream();

        XLWorkbook workbook = new();

        IXLWorksheet worksheet = workbook.AddWorksheet("Sheet1");

        worksheet.Cell("A1").Value = "Id";
        worksheet.Cell("B1").Value = "Mã phụ tùng";
        worksheet.Cell("C1").Value = "Tên phụ tùng";
        worksheet.Cell("D1").Value = "Đơn vị tính";
        worksheet.Cell("E1").Value = "Số lượng";
        worksheet.Cell("F1").Value = "Đơn giá";
        worksheet.Cell("G1").Value = "Mô tả";

        var countProduct = products.Count();

        for (int i = 0; i < countProduct; i++)
        {
            var product = products[i];

            var rowIndex = i + 2;

            worksheet.Cell($"A{rowIndex}").SetValue(product.ProductId);
            worksheet.Cell($"B{rowIndex}").SetValue(product.ProductCode);
            worksheet.Cell($"C{rowIndex}").SetValue(product.ProductName);
            worksheet.Cell($"D{rowIndex}").SetValue(product.UnitName);
            worksheet.Cell($"E{rowIndex}").SetValue(product.Quantity);
            worksheet.Cell($"F{rowIndex}").SetValue(product.UnitPrice);
            worksheet.Cell($"G{rowIndex}").SetValue(product.Remark);
        }

        var now = DateTime.Now;

        string fileName = $"ExportProduct_{now:dd_MM_yyyy_H_mm}.xlsx";

        workbook.SaveAs(memoryStream);

        var bytes = memoryStream.ToArray();

        return File(bytes, EXCEL_CONTENT_TYPE, fileName);
    }
}
