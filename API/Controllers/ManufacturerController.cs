using BroGarage.API.Attributes;
using BroGarage.API.Data;
using BroGarage.API.Data.Entities;
using BroGarage.API.Shared.Extensions;
using BroGarage.API.Shared.Models;
using BroGarage.API.Shared.RequestModels.Manufacturer;
using BroGarage.API.Shared.ResponseModels.CarType;
using BroGarage.API.Shared.ResponseModels.Manufacturer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BroGarage.API.Controllers;

[Route("manufacturer")]
[Auth]
public class ManufacturerController : BaseController
{
    public ManufacturerController(DatabaseContext db) : base(db)
    {
    }

    const string EXISTS_NAME = "Hãng xe đã có trong hệ thống";

    [HttpGet("get-all")]
    public async Task<ActionResult<ResponseModel<ManufacturerResModel[]>>> GetAll()
    {
        ResponseModel<ManufacturerResModel[]> response = new();

        var query = await db.Manufacturers
            .OrderByDescending(n => n.ManufacturerId)
            .Select(n => new ManufacturerResModel()
            {
                ManufacturerId = n.ManufacturerId,
                ManufacturerName = n.ManufacturerName ?? "",
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime()
            })
            .ToArrayAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("get-pagination")]
    public async Task<ActionResult<ResponseModel<PaginationModel<ManufacturerResModel[]>>>> GetPagination(
        string? keyword = "", 
        int pageSize = 10, 
        int pageIndex = 1,
        string? orderBy = "ManufacturerId",
        string sortDirection = "desc")
    {
        ResponseModel<PaginationModel<ManufacturerResModel[]>> response = new();

        var query = db.Manufacturers
            .Where(n =>
                (string.IsNullOrEmpty(keyword) || EF.Functions.Like(EF.Functions.Collate(n.ManufacturerName ?? "", COLLATION), $"%{keyword}%"))
            )
            .ApplySort(orderBy, sortDirection);

        int totalRow = query != null ? await query.CountAsync() : 0;
        var (totalPage, skipItem) = PaginationExtensions.GetPaginationMetadata(totalRow, pageSize, pageIndex);

        var pagination = new PaginationModel<ManufacturerResModel[]>()
        {
            PageSize = pageSize,
            PageIndex = pageIndex,
            TotalPage = totalPage,
            TotalRow = totalRow,
            OrderBy = orderBy,
            SortDirection = sortDirection
        };

        if (query == null)
        {
            response.IsSuccess = true;
            response.Result = pagination;
            return Ok(response);
        }

        var data = await query
            .Paginate(pageSize, pageIndex)
            .Select(n => new ManufacturerResModel()
            {
                ManufacturerId = n.ManufacturerId,
                ManufacturerName = n.ManufacturerName ?? "",
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
    public async Task<ActionResult<ResponseModel<int>>> Add(ManufacturerAddReqModel req)
    {
        ResponseModel<int> response = new();

        if (await db.Manufacturers.AnyAsync(n => n.ManufacturerName == req.ManufacturerName))
        {
            response.Message = EXISTS_NAME;
            return Ok(response);
        }

        var newManufacturer = new ManufacturerEntity()
        {
            ManufacturerName = req.ManufacturerName,
            CreatedUserId = USER_ID
        };

        await db.Manufacturers.AddAsync(newManufacturer);

        await db.SaveChangesAsync();

        response.Result = newManufacturer.ManufacturerId;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<ResponseModel<ManufacturerResModel>>> GetById(int id)
    {
        ResponseModel<ManufacturerResModel> response = new();

        var query = await db.Manufacturers
            .Where(n => n.ManufacturerId == id)
            .Select(n => new ManufacturerResModel()
            {
                ManufacturerId = n.ManufacturerId,
                ManufacturerName = n.ManufacturerName ?? "",
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime()
            })
            .FirstOrDefaultAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpPut("edit")]
    public async Task<ActionResult<ResponseModel>> Edit(ManufacturerEditReqModel req)
    {
        ResponseModel response = new();

        var query = await db.Manufacturers
            .Where(n => n.ManufacturerId == req.ManufacturerId)
            .AsTracking()
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        if (await db.Manufacturers.AnyAsync(n => n.ManufacturerName == req.ManufacturerName && n.ManufacturerId != req.ManufacturerId))
        {
            response.Message = EXISTS_NAME;
            return Ok(response);
        }

        query.ManufacturerName = req.ManufacturerName;
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

        var query = await db.Manufacturers
            .Where(n => n.ManufacturerId == id)
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        db.Manufacturers.Remove(query);

        await db.SaveChangesAsync();

        response.IsSuccess = true;

        return Ok(response);
    }
}
