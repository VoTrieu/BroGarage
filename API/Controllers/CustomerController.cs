using BroGarage.API.Attributes;
using BroGarage.API.Data;
using BroGarage.API.Data.Entities;
using BroGarage.API.Shared.Extensions;
using BroGarage.API.Shared.Models;
using BroGarage.API.Shared.RequestModels.Car;
using BroGarage.API.Shared.RequestModels.Customer;
using BroGarage.API.Shared.ResponseModels.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BroGarage.API.Controllers;

[Route("customer")]
[Auth]
public class CustomerController : BaseController
{
    public CustomerController(DatabaseContext db) : base(db)
    {
    }

    const string NOT_FOUND_TYPE = "Loại khách hàng không có trong hệ thống";
    const string EXISTS_PHONE_NUMBER = "Số điện thoại đã có trong hệ thống";
    const string NOT_FOUND_CAR_TYPE = "Dòng xe không có trong hệ thống";
    const string NOT_FOUND_MANUFACTURER = "Hãng sản xuất không có trong hệ thống";

    [HttpGet("get-pagination")]
    public async Task<ActionResult<ResponseModel<PaginationModel<CustomerResModel[]>>>> GetPagination(string? keyword = "", int typeId = 0, int pageSize = 10, int pageIndex = 1)
    {
        ResponseModel<PaginationModel<CustomerResModel[]>> response = new();

        var query = db.Customers
            .Where(n =>
                (
                    (string.IsNullOrEmpty(keyword) || EF.Functions.Like(EF.Functions.Collate(n.PhoneNumber ?? "", COLLATION), $"%{keyword}%"))
                    || (string.IsNullOrEmpty(keyword) || EF.Functions.Like(EF.Functions.Collate(n.FullName ?? "", COLLATION), $"%{keyword}%"))
                )
                && (typeId == 0 || n.TypeId == typeId)
            );

        int skipItem = pageSize * (pageIndex - 1);
        int totalRow = query != null ? await query.CountAsync() : 0;
        int totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRow) / pageSize));

        var pagination = new PaginationModel<CustomerResModel[]>()
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
            .OrderByDescending(n => n.CustomerId)
            .Skip(skipItem)
            .Take(pageSize)
            .Select(n => new CustomerResModel()
            {
                CustomerId = n.CustomerId,
                TypeId = n.TypeId ?? 0,
                TypeName = n.CustomerType.TypeName ?? "",
                FullName = n.FullName ?? "",
                PhoneNumber = n.PhoneNumber ?? "",
                Representative = n.Representative,
                TaxCode = n.TaxCode,
                Address = n.Address,
                Email = n.Email,
                Note = n.Note,
                AvatarUrl = n.AvatarUrl,
                Cars = n.Cars.Select(c => new CustomerCarResModel()
                {
                    CarId = c.CarId,
                    CarTypeId = c.CarTypeId ?? 0,
                    TypeName = c.CarType.TypeName ?? "",
                    ManufacturerId = c.CarType.ManufacturerId ?? 0,
                    ManufacturerName = c.CarType.Manufacturer.ManufacturerName ?? "",
                    Representative = n.Representative,
                    LicensePlate = c.LicensePlate ?? "",
                    YearOfManufacture = c.YearOfManufacture,
                    VIN = c.VIN,
                    AvatarUrl = c.AvatarUrl,
                    CreatedDate = c.CreatedDate.ToDate(),
                    CreatedTime = c.CreatedTime.ToTime()
                }),
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
    public async Task<ActionResult<ResponseModel<int>>> Add(CustomerAddReqModel req)
    {
        ResponseModel<int> response = new();

        if (!await db.CustomerTypes.AnyAsync(n => n.TypeId == req.TypeId))
        {
            response.Message = NOT_FOUND_TYPE;
            return Ok(response);
        }

        if (await db.Customers.AnyAsync(n => n.PhoneNumber == req.PhoneNumber))
        {
            response.Message = EXISTS_PHONE_NUMBER;
            return Ok(response);
        }

        var reqCarTypeIds = req.Cars.Where(n => n.CarTypeId != 0).Select(n => n.CarTypeId).ToList();

        foreach (var item in reqCarTypeIds)
        {
            if (!db.CarTypes.Any(n => n.TypeId == item))
            {
                response.Message = NOT_FOUND_CAR_TYPE;
                return Ok(response);
            }
        }

        var newCustomer = new CustomerEntity()
        {
            TypeId = req.TypeId,
            FullName = req.FullName,
            PhoneNumber = req.PhoneNumber,
            Representative = req.Representative,
            TaxCode = req.TaxCode,
            Address = req.Address,
            Email = req.Email,
            Note = req.Note,
            AvatarUrl = req.AvatarUrl,
            Cars = req.Cars.Select(c => new CarEntity()
            {
                CarTypeId = c.CarTypeId,
                LicensePlate = c.LicensePlate,
                YearOfManufacture = c.YearOfManufacture,
                VIN = c.VIN,
                AvatarUrl = c.AvatarUrl,
                CreatedUserId = USER_ID
            }).ToList(),
            CreatedUserId = USER_ID
        };

        await db.AddAsync(newCustomer);

        await db.SaveChangesAsync();

        response.Result = newCustomer.CustomerId;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<ResponseModel<CustomerResModel>>> GetById(int id)
    {
        ResponseModel<CustomerResModel> response = new();

        var query = await db.Customers
            .Where(n => n.CustomerId == id)
            .Select(n => new CustomerResModel()
            {
                CustomerId = n.CustomerId,
                TypeId = n.TypeId ?? 0,
                TypeName = n.CustomerType.TypeName ?? "",
                FullName = n.FullName ?? "",
                PhoneNumber = n.PhoneNumber ?? "",
                Representative = n.Representative,
                TaxCode = n.TaxCode ?? "",
                Address = n.Address,
                Email = n.Email,
                Note = n.Note,
                AvatarUrl = n.AvatarUrl,
                Cars = n.Cars.Select(c => new CustomerCarResModel()
                {
                    CarId = c.CarId,
                    CarTypeId = c.CarTypeId ?? 0,
                    TypeName = c.CarType.TypeName ?? "",
                    ManufacturerId = c.CarType.ManufacturerId ?? 0,
                    ManufacturerName = c.CarType.Manufacturer.ManufacturerName ?? "",
                    Representative = n.Representative,
                    LicensePlate = c.LicensePlate ?? "",
                    YearOfManufacture = c.YearOfManufacture,
                    VIN = c.VIN,
                    AvatarUrl = c.AvatarUrl,
                    CreatedDate = c.CreatedDate.ToDate(),
                    CreatedTime = c.CreatedTime.ToTime()
                }),
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime()
            })
            .FirstOrDefaultAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpPut("edit")]
    public async Task<ActionResult<ResponseModel>> Edit(CustomerEditReqModel req)
    {
        ResponseModel response = new();

        if (!await db.CustomerTypes.AnyAsync(n => n.TypeId == req.TypeId))
        {
            response.Message = NOT_FOUND_TYPE;
            return Ok(response);
        }

        var reqCarTypeIds = req.Cars.Where(n => n.CarTypeId != 0).Select(n => n.CarTypeId).ToList();

        foreach (var item in reqCarTypeIds)
        {
            if (!db.CarTypes.Any(n => n.TypeId == item))
            {
                response.Message = NOT_FOUND_CAR_TYPE;
                return Ok(response);
            }
        }

        var customer = await db.Customers
            .Where(n => n.CustomerId == req.CustomerId)
            .AsTracking()
            .FirstOrDefaultAsync();

        if (customer == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        if (await db.Customers.AnyAsync(n => n.PhoneNumber == req.PhoneNumber && n.CustomerId != req.CustomerId))
        {
            response.Message = EXISTS_PHONE_NUMBER;
            return Ok(response);
        }

        var reqCarIds = req.Cars.Select(n => n.CarId).ToList();

        var dbCars = await db.Cars
            .Where(n => n.CustomerId == req.CustomerId)
            .AsTracking()
            .ToArrayAsync();

        using var tran = await db.Database.BeginTransactionAsync();

        customer.TypeId = req.TypeId;
        customer.FullName = req.FullName;
        customer.PhoneNumber = req.PhoneNumber;
        customer.TaxCode = req.TaxCode;
        customer.Representative = req.Representative;
        customer.Address = req.Address;
        customer.Email = req.Email;
        customer.Note = req.Note;
        customer.AvatarUrl = req.AvatarUrl;
        customer.UpdatedUserId = USER_ID;
        customer.UpdatedDateTime = DateTime.Now;

        await db.SaveChangesAsync();

        if (dbCars != null)
        {
            // Delete car
            var deleteCars = db.Cars
                .Where(n =>
                    n.CustomerId == req.CustomerId
                    && !reqCarIds.Contains(n.CarId)
                );

            // Kiểm tra xe có trong Order hay không, nếu có thì không cho xóa
            if (await db.Cars.AnyAsync(n => deleteCars.Any(x => x.CarId == n.CarId && n.Orders.Any())))
            {
                response.Message = "Xe đã được sử dụng cho đơn hàng";
                return Ok(response);
            }

            if (deleteCars != null)
            {
                db.Cars.RemoveRange(deleteCars);
                await db.SaveChangesAsync();
            }

            // Update car
            var updateCars = await db.Cars
                .Where(n =>
                    n.CustomerId == req.CustomerId
                    && reqCarIds.Contains(n.CarId)
                )
                .AsTracking()
                .ToArrayAsync();

            if (updateCars != null)
            {
                foreach (var item in updateCars)
                {
                    var reqCar = req.Cars.Where(n => n.CarId == item.CarId).FirstOrDefault();
                    if (reqCar != null)
                    {
                        item.CarTypeId = reqCar.CarTypeId;
                        item.LicensePlate = reqCar.LicensePlate;
                        item.YearOfManufacture = reqCar.YearOfManufacture;
                        item.VIN = reqCar.VIN;
                        item.AvatarUrl = reqCar.AvatarUrl;
                        item.UpdatedUserId = USER_ID;
                        item.UpdatedDateTime = DateTime.Now;
                    }
                }
                await db.SaveChangesAsync();
            }

            // Insert car
            var insertCars = req.Cars.Where(n => n.CarId == 0).Select(n => new CarEntity()
            {
                CustomerId = req.CustomerId,
                CarTypeId = n.CarTypeId,
                LicensePlate = n.LicensePlate,
                YearOfManufacture = n.YearOfManufacture,
                VIN = n.VIN,
                AvatarUrl = n.AvatarUrl,
                CreatedUserId = USER_ID
            }).ToList();

            if (insertCars != null)
            {
                await db.Cars.AddRangeAsync(insertCars);

                await db.SaveChangesAsync();
            }
        }

        await tran.CommitAsync();

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<ResponseModel>> Delete(int id)
    {
        ResponseModel response = new();

        var query = await db.Customers
            .Where(n => n.CustomerId == id)
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        if (await db.Orders.AnyAsync(n => n.Car.CustomerId == id))
        {
            response.Message = "Khách hàng đã phát sinh đơn hàng";
            return Ok(response);
        }

        db.Customers.Remove(query);

        await db.SaveChangesAsync();

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpPost("car/add")]
    public async Task<ActionResult<ResponseModel>> AddCar(CarAddSingleResModel req)
    {
        ResponseModel response = new();

        if (!await db.CarTypes.AnyAsync(n => n.TypeId == req.CarTypeId))
        {
            response.Message = NOT_FOUND_CAR_TYPE;
            return Ok(response);
        }

        await db.Cars.AddAsync(new CarEntity()
        {
            CustomerId = req.CustomerId,
            CarTypeId = req.CarTypeId,
            LicensePlate = req.LicensePlate,
            YearOfManufacture = req.YearOfManufacture,
            VIN = req.VIN,
            AvatarUrl = req.AvatarUrl,
            CreatedUserId = USER_ID
        });

        await db.SaveChangesAsync();

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpPost("car/edit")]
    public async Task<ActionResult<ResponseModel>> EditCar(CarEditSingleReqModel req)
    {
        ResponseModel response = new();

        var query = await db.Cars
            .Where(n => n.CarId == req.CarId)
            .AsTracking()
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        if (!await db.CarTypes.AnyAsync(n => n.TypeId == req.CarTypeId))
        {
            response.Message = NOT_FOUND_CAR_TYPE;
            return Ok(response);
        }

        query.CarTypeId = req.CarTypeId;
        query.LicensePlate = req.LicensePlate;
        query.YearOfManufacture = req.YearOfManufacture;
        query.VIN = req.VIN;
        query.AvatarUrl = req.AvatarUrl;
        query.UpdatedUserId = USER_ID;
        query.UpdatedDateTime = DateTime.Now;

        await db.SaveChangesAsync();

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpDelete("car/delete")]
    public async Task<ActionResult<ResponseModel>> DeleteCar(int id)
    {
        ResponseModel response = new();

        var query = await db.Cars
            .Where(n => n.CarId == id)
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        db.Cars.Remove(query);

        await db.SaveChangesAsync();

        response.IsSuccess = true;

        return Ok(response);
    }
}
