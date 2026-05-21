using BroGarage.API.Attributes;
using BroGarage.API.Data;
using BroGarage.API.Shared.Extensions;
using BroGarage.API.Shared.Models;
using BroGarage.API.Shared.ResponseModels.Car;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BroGarage.API.Controllers;

[Route("car")]
[Auth]
public class CarController : BaseController
{
    public CarController(DatabaseContext db) : base(db)
    {
    }

    [HttpGet("get-pagination")]
    public async Task<ActionResult<ResponseModel<PaginationModel<CarResModel[]>>>> Filter(string? keyword = "", int pageSize = 10, int pageIndex = 1)
    {
        ResponseModel<PaginationModel<CarResModel[]>> response = new();

        var query = db.Cars
            .Where(n =>
                (string.IsNullOrEmpty(keyword) || EF.Functions.Like(EF.Functions.Collate(n.LicensePlate ?? "", COLLATION), $"%{keyword}%"))
            )
            .OrderByDescending(n => n.CarId);

        int skipItem = pageSize * (pageIndex - 1);
        int totalRow = query != null ? await query.CountAsync() : 0;
        int totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRow) / pageSize));

        var pagination = new PaginationModel<CarResModel[]>()
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
            .Select(n => new CarResModel()
            {
                CarId = n.CarId,
                TypeId = n.CarTypeId ?? 0,
                TypeName = n.CarType.TypeName ?? "",
                ManufacturerId = n.CarType.ManufacturerId ?? 0,
                ManufacturerName = n.CarType.Manufacturer.ManufacturerName ?? "",
                Customer = new CarCustomerResModel()
                {
                    CustomerId = n.Customer.CustomerId,
                    TypeId = n.Customer.TypeId ?? 0,
                    TypeName = n.Customer.CustomerType.TypeName ?? "",
                    FullName = n.Customer.FullName ?? "",
                    PhoneNumber = n.Customer.PhoneNumber ?? "",
                    TaxCode = n.Customer.TaxCode,
                    Address = n.Customer.Address,
                    Email = n.Customer.Email,
                    Note = n.Customer.Note,
                    CreatedDate = n.Customer.CreatedDate.ToDate(),
                    CreatedTime = n.Customer.CreatedTime.ToTime()
                },
                LicensePlate = n.LicensePlate ?? "",
                YearOfManufacture = n.YearOfManufacture,
                VIN = n.VIN,
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

    [HttpGet("get-by-id")]
    public async Task<ActionResult<ResponseModel<CarResModel>>> GetById(int id)
    {
        ResponseModel response = new();

        var query = await db.Cars
            .Where(n => n.CarId == id)
            .Select(n => new CarResModel()
            {
                CarId = n.CarId,
                TypeId = n.CarTypeId ?? 0,
                TypeName = n.CarType.TypeName ?? "",
                ManufacturerId = n.CarType.ManufacturerId ?? 0,
                ManufacturerName = n.CarType.Manufacturer.ManufacturerName ?? "",
                Customer = new CarCustomerResModel()
                {
                    CustomerId = n.Customer.CustomerId,
                    TypeId = n.Customer.TypeId ?? 0,
                    TypeName = n.Customer.CustomerType.TypeName ?? "",
                    FullName = n.Customer.FullName ?? "",
                    PhoneNumber = n.Customer.PhoneNumber ?? "",
                    TaxCode = n.Customer.TaxCode,
                    Address = n.Customer.Address,
                    Email = n.Customer.Email,
                    Note = n.Customer.Note,
                    CreatedDate = n.Customer.CreatedDate.ToDate(),
                    CreatedTime = n.Customer.CreatedTime.ToTime()
                },
                LicensePlate = n.LicensePlate ?? "",
                YearOfManufacture = n.YearOfManufacture,
                VIN = n.VIN,
                AvatarUrl = n.AvatarUrl,
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime()
            })
            .FirstOrDefaultAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("get-by-customer")]
    public async Task<ActionResult<ResponseModel<CarResModel[]>>> GetByCustomer(int customerId)
    {
        ResponseModel response = new();

        var query = await db.Cars
            .Where(n => n.CustomerId == customerId)
            .Select(n => new CarResModel()
            {
                CarId = n.CarId,
                TypeId = n.CarTypeId ?? 0,
                TypeName = n.CarType.TypeName ?? "",
                ManufacturerId = n.CarType.ManufacturerId ?? 0,
                ManufacturerName = n.CarType.Manufacturer.ManufacturerName ?? "",
                Customer = new CarCustomerResModel()
                {
                    CustomerId = n.Customer.CustomerId,
                    TypeId = n.Customer.TypeId ?? 0,
                    TypeName = n.Customer.CustomerType.TypeName ?? "",
                    FullName = n.Customer.FullName ?? "",
                    PhoneNumber = n.Customer.PhoneNumber ?? "",
                    TaxCode = n.Customer.TaxCode,
                    Address = n.Customer.Address,
                    Email = n.Customer.Email,
                    Note = n.Customer.Note,
                    CreatedDate = n.Customer.CreatedDate.ToDate(),
                    CreatedTime = n.Customer.CreatedTime.ToTime()
                },
                LicensePlate = n.LicensePlate ?? "",
                YearOfManufacture = n.YearOfManufacture,
                VIN = n.VIN,
                AvatarUrl = n.AvatarUrl,
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime()
            })
            .ToArrayAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }
}
