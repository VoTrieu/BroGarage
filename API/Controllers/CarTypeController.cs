using BroGarage.API.Attributes;
using BroGarage.API.Data;
using BroGarage.API.Data.Entities;
using BroGarage.API.Shared.Extensions;
using BroGarage.API.Shared.Models;
using BroGarage.API.Shared.RequestModels.CarType;
using BroGarage.API.Shared.ResponseModels.CarType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BroGarage.API.Controllers;

[Route("car-type")]
[Auth]
public class CarTypeController : BaseController
{
    public CarTypeController(DatabaseContext db) : base(db)
    {
    }

    const string EXISTS_NAME = "Dòng xe đã có trong hệ thống";

    [HttpGet("get-all")]
    public async Task<ActionResult<ResponseModel<CarTypeResModel[]>>> GetAll()
    {
        ResponseModel<CarTypeResModel[]> response = new();

        var query = await db.CarTypes
            .OrderByDescending(n => n.TypeId)
            .Select(n => new CarTypeResModel()
            {
                TypeId = n.TypeId,
                ManufacturerId = n.ManufacturerId ?? 0,
                TypeName = n.TypeName ?? "",
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime()
            })
            .ToArrayAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("get-pagination")]
    public async Task<ActionResult<ResponseModel<PaginationModel<CarTypeResModel[]>>>> GetPagination(string? keyword = "", int pageSize = 10, int pageIndex = 1)
    {
        ResponseModel<PaginationModel<CarTypeResModel[]>> response = new();

        var query = db.CarTypes
            .Where(n =>
                (string.IsNullOrEmpty(keyword) || EF.Functions.Like(EF.Functions.Collate(n.TypeName ?? "", COLLATION), $"%{keyword}%"))
            )
            .OrderByDescending(n => n.TypeId);

        int skipItem = pageSize * (pageIndex - 1);
        int totalRow = query != null ? await query.CountAsync() : 0;
        int totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRow) / pageSize));

        var pagination = new PaginationModel<CarTypeResModel[]>()
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
            .Select(n => new CarTypeResModel()
            {
                TypeId = n.TypeId,
                ManufacturerId = n.ManufacturerId ?? 0,
                TypeName = n.TypeName ?? "",
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
    public async Task<ActionResult<ResponseModel<int>>> Add(CarTypeAddReqModel req)
    {
        ResponseModel<int> response = new();

        if (await db.CarTypes.AnyAsync(n => n.TypeName == req.TypeName && n.ManufacturerId == req.ManufacturerId))
        {
            response.Message = EXISTS_NAME;
            return Ok(response);
        }

        var newCarType = new CarTypeEntity()
        {
            ManufacturerId = req.ManufacturerId,
            TypeName = req.TypeName,
            CreatedUserId = USER_ID
        };

        await db.CarTypes.AddAsync(newCarType);

        await db.SaveChangesAsync();

        response.Result = newCarType.TypeId;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<ResponseModel<CarTypeResModel>>> GetById(int id)
    {
        ResponseModel<CarTypeResModel> response = new();

        var query = await db.CarTypes
            .Where(n => n.TypeId == id)
            .Select(n => new CarTypeResModel
            {
                TypeId = n.TypeId,
                ManufacturerId = n.ManufacturerId ?? 0,
                TypeName = n.TypeName ?? "",
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime()
            })
            .FirstOrDefaultAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpPut("edit")]
    public async Task<ActionResult<ResponseModel>> Edit(CarTypeEditReqModel req)
    {
        ResponseModel response = new();

        var query = await db.CarTypes
            .Where(n => n.TypeId == req.TypeId)
            .AsTracking()
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        if (await db.CarTypes.AnyAsync(n => n.TypeName == req.TypeName && n.ManufacturerId == req.ManufacturerId && n.TypeId != req.TypeId))
        {
            response.Message = EXISTS_NAME;
            return Ok(response);
        }

        query.ManufacturerId = req.ManufacturerId;
        query.TypeName = req.TypeName;
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

        var query = await db.CarTypes
            .Where(n => n.TypeId == id)
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        if (await db.Cars.AnyAsync(n => n.CarTypeId == id))
        {
            response.Message = IN_USED_MSG;
            return Ok(response);
        }

        db.CarTypes.Remove(query);

        await db.SaveChangesAsync();

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("get-by-manufacturer")]
    public async Task<ActionResult<ResponseModel<CarTypeResModel[]>>> GetByManufacturer(int manufacturerId)
    {
        ResponseModel<CarTypeResModel[]> response = new();

        var query = await db.CarTypes
            .Where(n => n.ManufacturerId == manufacturerId)
            .Select(n => new CarTypeResModel()
            {
                TypeId = n.TypeId,
                ManufacturerId = n.ManufacturerId ?? 0,
                TypeName = n.TypeName ?? "",
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime()
            })
            .ToArrayAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }
}
