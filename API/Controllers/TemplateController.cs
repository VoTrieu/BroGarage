using BroGarage.API.Attributes;
using BroGarage.API.Data;
using BroGarage.API.Data.Entities;
using BroGarage.API.Shared.Extensions;
using BroGarage.API.Shared.Models;
using BroGarage.API.Shared.RequestModels.Template;
using BroGarage.API.Shared.ResponseModels.Template;
using BroGarage.API.Shared.ResponseModels.TemplateDetail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BroGarage.API.Controllers;

[Route("template")]
[Auth]
public class TemplateController : BaseController
{
    const string CAR_TYPE_NOT_FOUND = "Dòng xe không có trong hệ thống";
    const string MANUFACTURER_NOT_FOUND = "Hãng xe không có trong hệ thống";
    const string PRODUCT_NOT_FOUND = "Linh kiện không có trong hệ thống";

    public TemplateController(DatabaseContext db) : base(db)
    {
    }

    [HttpGet("get-pagination")]
    public async Task<ActionResult<ResponseModel<PaginationModel<TemplateResModel[]>>>> GetPagination(
        int carTypeId = 0,
        int yearOfManufactureFrom = 0, int yearOfManufactureTo = 0,
        int pageSize = 10, int pageIndex = 1)
    {
        ResponseModel<PaginationModel<TemplateResModel[]>> response = new();
        var query = db.Templates
        .Where(n =>
            (carTypeId == 0 || n.CarTypeId == carTypeId)
            && (yearOfManufactureFrom == 0 || n.YearOfManufactureFrom >= yearOfManufactureFrom)
            && (yearOfManufactureTo == 0 || n.YearOfManufactureTo <= yearOfManufactureTo)
        );

        int skipItem = pageSize * (pageIndex - 1);
        int totalRow = query != null ? await query.CountAsync() : 0;
        int totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRow) / pageSize));

        var pagination = new PaginationModel<TemplateResModel[]>()
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
            .OrderByDescending(n => n.TemplateId)
            .Skip(skipItem)
            .Take(pageSize)
            .Select(n => new TemplateResModel()
            {
                TemplateId = n.TemplateId,
                CarTypeId = n.CarTypeId ?? 0,
                CarTypeName = n.CarType.TypeName ?? "",
                ManufaturerId = n.CarType.ManufacturerId ?? 0,
                ManufacturerName = n.CarType.Manufacturer.ManufacturerName ?? "",
                YearOfManufactureFrom = n.YearOfManufactureFrom,
                YearOfManufactureTo = n.YearOfManufactureTo,
                Note = n.Note,
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime(),
                TemplateDetails = n.TemplateDetails.Select(x => new TemplateDetailResModel()
                {
                    TemplateDetailId = x.TemplateDetailId,
                    TemplateId = x.TemplateId ?? 0,
                    ProductId = x.ProductId ?? 0,
                    ProductCode = x.Product.ProductCode ?? "",
                    ProductName = x.Product.ProductName ?? "",
                    UnitName = x.Product.UnitName,
                    UnitPrice = x.Product.UnitPrice,
                    Quantity = x.Quantity,
                    AvatarUrl = x.Product.AvatarUrl
                })
            })
            .ToArrayAsync();

        pagination.Data = data;

        response.Result = pagination;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpPost("add")]
    public async Task<ActionResult<ResponseModel<int>>> Add(TemplateAddReqModel req)
    {
        ResponseModel<int> response = new();

        if (!await db.CarTypes.AnyAsync(n => n.TypeId == req.CarTypeId))
        {
            response.Message = CAR_TYPE_NOT_FOUND;
            return Ok(response);
        }

        if (!await db.Manufacturers.AnyAsync(n => n.ManufacturerId == req.ManufaturerId))
        {
            response.Message = MANUFACTURER_NOT_FOUND;
            return Ok(response);
        }

        var proIds = req.TemplateDetails.Select(n => n.ProductId).ToList();

        if (!await db.Products.AnyAsync(n => proIds.Contains(n.ProductId)))
        {
            response.Message = PRODUCT_NOT_FOUND;
            return Ok(response);
        }

        var newTemplate = new TemplateEntity()
        {
            CarTypeId = req.CarTypeId,
            YearOfManufactureFrom = req.YearOfManufactureFrom,
            YearOfManufactureTo = req.YearOfManufactureTo,
            Note = req.Note,
            TemplateDetails = req.TemplateDetails.Select(x => new TemplateDetailEntity()
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                CreatedUserId = USER_ID
            }).ToArray(),
            CreatedUserId = USER_ID
        };

        await db.Templates.AddAsync(newTemplate);

        await db.SaveChangesAsync();

        response.Result = newTemplate.TemplateId;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<ResponseModel<TemplateResModel>>> GetById(int id)
    {
        ResponseModel<TemplateResModel> response = new();

        var query = await db.Templates
            .Where(n => n.TemplateId == id)
            .Select(n => new TemplateResModel()
            {
                TemplateId = n.TemplateId,
                CarTypeId = n.CarTypeId ?? 0,
                CarTypeName = n.CarType.TypeName ?? "",
                ManufaturerId = n.CarType.ManufacturerId ?? 0,
                ManufacturerName = n.CarType.Manufacturer.ManufacturerName ?? "",
                YearOfManufactureFrom = n.YearOfManufactureFrom,
                YearOfManufactureTo = n.YearOfManufactureTo,
                Note = n.Note,
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime(),
                TemplateDetails = n.TemplateDetails.Select(x => new TemplateDetailResModel()
                {
                    TemplateDetailId = x.TemplateDetailId,
                    TemplateId = x.TemplateId ?? 0,
                    ProductId = x.ProductId ?? 0,
                    ProductCode = x.Product.ProductCode ?? "",
                    ProductName = x.Product.ProductName ?? "",
                    UnitName = x.Product.UnitName,
                    UnitPrice = x.Product.UnitPrice,
                    Quantity = x.Quantity,
                    AvatarUrl = x.Product.AvatarUrl
                })
            })
            .FirstOrDefaultAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpPut("edit")]
    public async Task<ActionResult<ResponseModel>> Edit(TemplateEditReqModel req)
    {
        ResponseModel response = new();

        var query = await db.Templates
            .Where(n => n.TemplateId == req.TemplateId)
            .AsTracking()
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        if (!await db.CarTypes.AnyAsync(n => n.TypeId == req.CarTypeId))
        {
            response.Message = CAR_TYPE_NOT_FOUND;
            return Ok(response);
        }

        if (!await db.Manufacturers.AnyAsync(n => n.ManufacturerId == req.ManufaturerId))
        {
            response.Message = MANUFACTURER_NOT_FOUND;
            return Ok(response);
        }

        var proIds = req.TemplateDetails.Select(n => n.ProductId).ToList();

        if (!await db.Products.AnyAsync(n => proIds.Contains(n.ProductId)))
        {
            response.Message = PRODUCT_NOT_FOUND;
            return Ok(response);
        }

        var reqTemplateDetailIds = req.TemplateDetails.Select(n => n.TemplateDetailId).ToList();

        using var tran = await db.Database.BeginTransactionAsync();

        query.CarTypeId = req.CarTypeId;
        query.YearOfManufactureFrom = req.YearOfManufactureFrom;
        query.YearOfManufactureTo = req.YearOfManufactureTo;
        query.Note = req.Note;
        query.UpdatedUserId = USER_ID;
        query.UpdatedDateTime = DateTime.Now;

        await db.SaveChangesAsync();

        // Xóa detail
        var deleteDetails = await db.TemplateDetails
            .Where(n =>
                n.TemplateId == req.TemplateId
                && !reqTemplateDetailIds.Contains(n.TemplateDetailId)
            )
            .ToArrayAsync();

        if (deleteDetails != null)
        {
            db.TemplateDetails.RemoveRange(deleteDetails);
            await db.SaveChangesAsync();
        }

        // Edit detail
        var updateDetails = await db.TemplateDetails
            .Where(n =>
                n.TemplateId == req.TemplateId
                && reqTemplateDetailIds.Contains(n.TemplateDetailId)
            )
            .AsTracking()
            .ToArrayAsync();

        if (updateDetails != null)
        {
            foreach (var item in updateDetails)
            {
                var reqDetail = req.TemplateDetails.Where(n => n.TemplateDetailId == item.TemplateDetailId).FirstOrDefault();
                if (reqDetail != null)
                {
                    item.ProductId = reqDetail.ProductId;
                    item.Quantity = reqDetail.Quantity;
                    item.UpdatedUserId = USER_ID;
                    item.UpdatedDateTime = DateTime.Now;
                }
            }
            await db.SaveChangesAsync();
        }

        // Add detail
        var insertDetails = req.TemplateDetails.Where(n => n.TemplateDetailId == 0).Select(n => new TemplateDetailEntity()
        {
            TemplateId = req.TemplateId,
            ProductId = n.ProductId,
            Quantity = n.Quantity,
            CreatedUserId = USER_ID
        }).ToList();

        if (insertDetails != null)
        {
            await db.TemplateDetails.AddRangeAsync(insertDetails);

            await db.SaveChangesAsync();
        }

        await tran.CommitAsync();

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<ResponseModel>> Delete(int id)
    {
        ResponseModel response = new();

        var query = await db.Templates
            .Where(n => n.TemplateId == id)
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        db.Templates.Remove(query);

        await db.SaveChangesAsync();

        response.IsSuccess = true;

        return Ok(response);
    }
}
