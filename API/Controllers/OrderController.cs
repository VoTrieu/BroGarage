using ClosedXML.Report;
using EFCore.BulkExtensions;
using BroGarage.API.Attributes;
using BroGarage.API.Data;
using BroGarage.API.Data.Entities;
using BroGarage.API.Shared.Enums;
using BroGarage.API.Shared.Extensions;
using BroGarage.API.Shared.Models;
using BroGarage.API.Shared.RequestModels.Order;
using BroGarage.API.Shared.ResponseModels.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BroGarage.API.Controllers;

[Route("order")]
[Auth]
public class OrderController : BaseController
{
    private readonly IConfiguration configuration;

    const string INVALID_STATUS = "Trạng thái không hợp lệ";
    const string INVALID_TYPE = "Loại đơn hàng không hợp lệ";
    const string CAR_NOT_FOUND = "Xe không có trong hệ thống";
    const string INVALID_PAYMENT_METHOD = "Phương thức thanh toán không hợp lệ (CASH hoặc TRANSFER)";
    const string PRODUCT_NOT_FOUND = "Linh kiện không có trong hệ thống";
    const string TEMPLATE_NOT_FOUNT = "Mẫu phiếu cáo cáo sửa chữa không có trong hệ thống";
    const string PRODUCT_DUPLICATE = "Linh kiện bị trùng lặp";

    private readonly decimal VAT = 0;

    public OrderController(DatabaseContext db, IConfiguration configuration) : base(db)
    {
        this.configuration = configuration;

        VAT = configuration.GetValue<decimal>("VAT");
    }

    [HttpGet("get-pagination")]
    public async Task<ActionResult<ResponseModel<PaginationModel<OrderResModel[]>>>> GetPagination
    (
        string? keyword = "",
        int customerId = 0,
        int carId = 0,
        string? createdFromDate = "",
        string? createdToDate = "",
        int statusId = 0,
        int typeId = 0,
        string? dateInFrom = "",
        string? dateInTo = "",
        string? dateOutFrom = "",
        string? dateOutTo = "",
        int pageSize = 10,
        int pageIndex = 1,
        string? orderBy = "OrderId",
        string sortDirection = "desc"
    )
    {
        ResponseModel<PaginationModel<OrderResModel[]>> response = new();

        var fromDateValue = createdFromDate?.ToDateTime();
        var toDateValue = createdToDate?.ToDateTime();

        var dateInFromValue = dateInFrom?.ToDateTime();
        var dateInToValue = dateInTo?.ToDateTime();

        var dateOutFromValue = dateOutFrom?.ToDateTime();
        var dateOutToValue = dateOutTo?.ToDateTime();

        if (!string.IsNullOrEmpty(createdFromDate))
        {
            if (fromDateValue == null)
            {
                response.Message = FROM_DATE_INVALID;
                return Ok(response);
            }
        }

        if (!string.IsNullOrEmpty(createdToDate))
        {
            if (toDateValue == null)
            {
                response.Message = TO_DATE_INVALID;
                return Ok(response);
            }
        }

        if (!string.IsNullOrEmpty(dateInFrom))
        {
            if (dateInFromValue == null)
            {
                response.Message = "Ngày vào không đúng định dạng (từ ngày)";
                return Ok(response);
            }
        }

        if (!string.IsNullOrEmpty(dateInFrom))
        {
            if (dateInFromValue == null)
            {
                response.Message = "Ngày vào không đúng định dạng (đến ngày)";
                return Ok(response);
            }
        }

        if (!string.IsNullOrEmpty(dateOutFrom))
        {
            if (dateOutFromValue == null)
            {
                response.Message = "Ngày ra không đúng định dạng (từ ngày)";
                return Ok(response);
            }
        }

        if (!string.IsNullOrEmpty(dateOutTo))
        {
            if (dateOutToValue == null)
            {
                response.Message = "Ngày ra không đúng định dạng (đến ngày)";
                return Ok(response);
            }
        }

        var query = db.Orders
            .Where(n =>
                (customerId == 0 || n.Car.CustomerId == customerId)
                && (carId == 0 || n.CarId == carId)
                && (fromDateValue == null || n.CreatedDate >= fromDateValue.Value.Date)
                && (toDateValue == null || n.CreatedDate <= toDateValue.Value.Date)
                && (statusId == 0 || n.StatusId == statusId)
                && (typeId == 0 || n.TypeId == typeId)
                && (dateInFromValue == null || n.DateIn >= dateInFromValue.Value.Date)
                && (dateInToValue == null || n.DateIn <= dateInToValue.Value.Date)
                && (dateOutFromValue == null || n.DateOutActual >= dateOutFromValue.Value.Date)
                && (dateOutToValue == null || n.DateOutActual <= dateOutToValue.Value.Date)
                && (
                    (string.IsNullOrEmpty(keyword) || EF.Functions.Like(EF.Functions.Collate(n.Car.LicensePlate ?? "", COLLATION), $"%{keyword}%"))
                    || (string.IsNullOrEmpty(keyword) || EF.Functions.Like(EF.Functions.Collate(n.Car.Customer.FullName ?? "", COLLATION), $"%{keyword}%"))
                )
            )
            .ApplySort(orderBy, sortDirection);

        int totalRow = query != null ? await query.CountAsync() : 0;
        var (totalPage, skipItem) = PaginationExtensions.GetPaginationMetadata(totalRow, pageSize, pageIndex);

        var pagination = new PaginationModel<OrderResModel[]>()
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
            .Select(n => new OrderResModel()
        {
            OrderId = n.OrderId,
            CarId = n.CarId ?? 0,
            Car = new OrderCarResModel()
            {
                CarId = n.Car.CarId,
                CarTypeId = n.Car.CarTypeId ?? 0,
                TypeName = n.Car.CarType.TypeName ?? "",
                ManufaturerId = n.Car.CarType.ManufacturerId ?? 0,
                ManufacturerName = n.Car.CarType.Manufacturer.ManufacturerName ?? "",
                CustomerId = n.Car.CustomerId ?? 0,
                LicensePlate = n.Car.LicensePlate ?? "",
                YearOfManufacture = n.Car.YearOfManufacture,
                VIN = n.Car.VIN,
                Customer = new OrderCarCustomerResModel()
                {
                    CustomerId = n.Car.Customer.CustomerId,
                    TypeId = n.Car.Customer.TypeId ?? 0,
                    TypeName = n.Car.Customer.CustomerType.TypeName ?? "",
                    FullName = n.Car.Customer.FullName ?? "",
                    PhoneNumber = n.Car.Customer.PhoneNumber ?? "",
                    Representative = n.Car.Customer.Representative,
                    TaxCode = n.Car.Customer.TaxCode,
                    Address = n.Car.Customer.Address,
                    Email = n.Car.Customer.Email,
                    Note = n.Car.Customer.Note,
                    CreatedDate = n.Car.Customer.CreatedDate.ToDate(),
                    CreatedTime = n.Car.Customer.CreatedTime.ToTime()
                },
                CreatedDate = n.Car.CreatedDate.ToDate(),
                CreatedTime = n.Car.CreatedTime.ToTime()
            },
            StatusId = n.StatusId ?? 0,
            StatusName = n.Status.StatusName ?? "",
            TypeId = n.TypeId ?? 0,
            TypeName = n.OrderType.TypeName ?? "",
            TemplateId = n.TemplateId,
            OrderCode = n.OrderCode,
            OrderDate = n.OrderDate.ToDate(),
            DateIn = n.DateIn.ToDate(),
            DateOutEstimated = n.DateOutEstimated.ToDate(),
            DateOutActual = n.DateOutActual != null ? n.DateOutActual.Value.ToDate() : "",
            ODOCurrent = n.ODOCurrent,
            ODONext = n.ODONext,
            ODOUnit = n.ODOUnit,
            ExpiredInDate = n.ExpiredInDate.ToDate(),
            IsInvoice = n.IsInvoice,
            AdvancePayment = n.AdvancePayment,
            PaymentMethod = n.PaymentMethod,
            Diagnosis = n.Diagnosis,
            CustomerNote = n.CustomerNote,
            InternalNote = n.InternalNote,
            VAT = n.VAT,
            Discount = n.Discount,
            OrderDetails = n.OrderDetails.Select(x => new OrderDetailResModel()
            {
                OrderDetailId = x.OrderDetailId,
                OrderId = x.Order.OrderId,
                ProductId = x.ProductId ?? 0,
                ProductCode = x.Product.ProductCode ?? "",
                ProductName = x.Product.ProductName ?? "",
                UnitName = x.Product.UnitName,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                Comment = x.Comment,
                IsHideProduct = x.IsHideProduct,
                CreatedDate = x.CreatedDate.ToDate(),
                CreatedTime = x.CreatedTime.ToTime()
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
    public async Task<ActionResult<ResponseModel<int>>> Add(OrderAddReqModel req)
    {
        ResponseModel<int> response = new();

        if (!await db.Cars.AnyAsync(n => n.CarId == req.CarId))
        {
            response.Message = CAR_NOT_FOUND;
            return Ok(response);
        }

        if (!await db.OrderStatuses.AnyAsync(n => n.StatusId == req.StatusId))
        {
            response.Message = INVALID_STATUS;
            return Ok(response);
        }

        if (!await db.OrderTypes.AnyAsync(n => n.TypeId == req.TypeId))
        {
            response.Message = INVALID_TYPE;
            return Ok(response);
        }

        if (req.PaymentMethod != PaymentMethodEnum.CASH && req.PaymentMethod != PaymentMethodEnum.TRANSFER)
        {
            response.Message = INVALID_PAYMENT_METHOD;
            return Ok(response);
        }

        if (req.TemplateId != null)
        {
            if (!await db.Templates.AnyAsync(n => n.TemplateId == req.TemplateId))
            {
                response.Message = TEMPLATE_NOT_FOUNT;
                return Ok(response);
            }
        }

        if (req.OrderDetails.Select(n => n.ProductId).Distinct().Count() != req.OrderDetails.Count)
        {
            response.Message = PRODUCT_DUPLICATE;
            return Ok(response);
        }

        var orderExpiredDateInDay = configuration.GetValue<int>("OrderExpiredDateInDay");
        var oDONextInKm = configuration.GetValue<int>("ODONextInKm");

        if (req.ODONext == 0)
        {
            req.ODONext = req.ODOCurrent + oDONextInKm;
        }

        var now = DateTime.Now;

        var orderDate = now.Date;
        var dateIn = now.Date;
        var dateOutEstimated = now.Date;
        var expiredInDate = now.AddDays(orderExpiredDateInDay).Date;

        if (!string.IsNullOrEmpty(req.OrderDate))
        {
            var date = req.OrderDate.ToDateTime();
            if (date != null)
            {
                orderDate = date.Value.Date;
            }
        }

        if (!string.IsNullOrEmpty(req.DateIn))
        {
            var date = req.DateIn.ToDateTime();
            if (date != null)
            {
                dateIn = date.Value.Date;
            }
        }

        if (!string.IsNullOrEmpty(req.DateOutEstimated))
        {
            var date = req.DateOutEstimated.ToDateTime();
            if (date != null)
            {
                dateOutEstimated = date.Value.Date;
            }
        }

        if (!string.IsNullOrEmpty(req.ExpiredInDate))
        {
            var date = req.ExpiredInDate.ToDateTime();
            if (date != null)
            {
                expiredInDate = date.Value.Date;
            }
        }

        var reqProductIds = req.OrderDetails.Select(n => n.ProductId).ToList();

        foreach (var item in reqProductIds)
        {
            if (!db.Products.Any(n => n.ProductId == item))
            {
                response.Message = PRODUCT_NOT_FOUND;
                return Ok(response);
            }
        }

        var dbProducts = await db.Products
            .Where(n => reqProductIds.Contains(n.ProductId))
            .Select(n => new
            {
                n.ProductId,
                n.UnitPrice
            })
            .ToArrayAsync();

        if (reqProductIds.Count != dbProducts.Length)
        {
            response.Message = PRODUCT_NOT_FOUND;
            return Ok(response);
        }

        var newOrder = new OrderEntity()
        {
            CarId = req.CarId,
            StatusId = req.StatusId,
            TypeId = req.TypeId,
            TemplateId = req.TemplateId,
            OrderCode = req.OrderCode,
            OrderDate = orderDate,
            DateIn = dateIn,
            DateOutEstimated = dateOutEstimated,
            ODOCurrent = req.ODOCurrent,
            ODONext = req.ODONext,
            ODOUnit = req.ODOUnit,
            ExpiredInDate = expiredInDate,
            IsInvoice = req.IsInvoice,
            AdvancePayment = req.AdvancePayment,
            PaymentMethod = req.PaymentMethod,
            Diagnosis = req.Diagnosis,
            CustomerNote = req.CustomerNote,
            InternalNote = req.InternalNote,
            VAT = VAT,
            Discount = req.Discount,
            OrderDetails = req.OrderDetails.Select(n => new OrderDetailEntity()
            {
                ProductId = n.ProductId,
                UnitPrice = dbProducts.Where(x => x.ProductId == n.ProductId).FirstOrDefault()?.UnitPrice ?? 0,
                Quantity = n.Quantity,
                Comment = n.Comment,
                IsHideProduct = n.IsHideProduct,
                CreatedUserId = USER_ID
            }).ToList(),
            CreatedUserId = USER_ID
        };

        await db.Orders.AddAsync(newOrder);

        await db.SaveChangesAsync();

        response.Result = newOrder.OrderId;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpPut("edit-status")]
    public async Task<ActionResult<ResponseModel>> EditStatus(OrderEditStatusReqModel req)
    {
        ResponseModel response = new();

        var query = await db.Orders
            .Where(n => n.OrderId == req.OrderId)
            .AsTracking()
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        if (!await db.OrderStatuses.AnyAsync(n => n.StatusId == req.StatusId))
        {
            response.Message = INVALID_STATUS;
            return Ok(response);
        }

        query.StatusId = req.StatusId;
        query.UpdatedUserId = USER_ID;
        query.UpdatedDateTime = DateTime.Now;

        await db.SaveChangesAsync();

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("get-by-customer")]
    public async Task<ActionResult<ResponseModel<OrderResModel[]>>> GetByCustomer(int customerId)
    {
        ResponseModel<OrderResModel[]> response = new();

        var query = await db.Orders
            .Where(n => n.Car.CustomerId == customerId)
            .OrderByDescending(n => n.OrderId)
            .Select(n => new OrderResModel()
            {
                OrderId = n.OrderId,
                CarId = n.CarId ?? 0,
                Car = new OrderCarResModel()
                {
                    CarId = n.Car.CarId,
                    CarTypeId = n.Car.CarTypeId ?? 0,
                    TypeName = n.Car.CarType.TypeName ?? "",
                    ManufaturerId = n.Car.CarType.ManufacturerId ?? 0,
                    ManufacturerName = n.Car.CarType.Manufacturer.ManufacturerName ?? "",
                    CustomerId = n.Car.CustomerId ?? 0,
                    LicensePlate = n.Car.LicensePlate ?? "",
                    YearOfManufacture = n.Car.YearOfManufacture,
                    VIN = n.Car.VIN,
                    Customer = new OrderCarCustomerResModel()
                    {
                        CustomerId = n.Car.Customer.CustomerId,
                        TypeId = n.Car.Customer.TypeId ?? 0,
                        TypeName = n.Car.Customer.CustomerType.TypeName ?? "",
                        FullName = n.Car.Customer.FullName ?? "",
                        PhoneNumber = n.Car.Customer.PhoneNumber ?? "",
                        Representative = n.Car.Customer.Representative,
                        TaxCode = n.Car.Customer.TaxCode,
                        Address = n.Car.Customer.Address,
                        Email = n.Car.Customer.Email,
                        Note = n.Car.Customer.Note,
                        CreatedDate = n.Car.Customer.CreatedDate.ToDate(),
                        CreatedTime = n.Car.Customer.CreatedTime.ToTime()
                    },
                    CreatedDate = n.Car.CreatedDate.ToDate(),
                    CreatedTime = n.Car.CreatedTime.ToTime()
                },
                StatusId = n.StatusId ?? 0,
                StatusName = n.Status.StatusName ?? "",
                TemplateId = n.TemplateId,
                TypeId = n.TypeId ?? 0,
                TypeName = n.OrderType.TypeName ?? "",
                OrderCode = n.OrderCode,
                OrderDate = n.OrderDate.ToDate(),
                DateIn = n.DateIn.ToDate(),
                DateOutEstimated = n.DateOutEstimated.ToDate(),
                DateOutActual = n.DateOutActual != null ? n.DateOutActual.Value.ToDate() : "",
                ODOCurrent = n.ODOCurrent,
                ODONext = n.ODONext,
                ODOUnit = n.ODOUnit,
                ExpiredInDate = n.ExpiredInDate.ToDate(),
                IsInvoice = n.IsInvoice,
                AdvancePayment = n.AdvancePayment,
                PaymentMethod = n.PaymentMethod,
                Diagnosis = n.Diagnosis,
                CustomerNote = n.CustomerNote,
                InternalNote = n.InternalNote,
                VAT = n.VAT,
                Discount = n.Discount,
                OrderDetails = n.OrderDetails.Select(x => new OrderDetailResModel()
                {
                    OrderDetailId = x.OrderDetailId,
                    OrderId = x.Order.OrderId,
                    ProductId = x.ProductId ?? 0,
                    ProductCode = x.Product.ProductCode ?? "",
                    ProductName = x.Product.ProductName ?? "",
                    UnitName = x.Product.UnitName,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity,
                    Comment = x.Comment,
                    IsHideProduct = x.IsHideProduct,
                    CreatedDate = x.CreatedDate.ToDate(),
                    CreatedTime = x.CreatedTime.ToTime()
                }),
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime()
            })
            .ToArrayAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("get-by-car")]
    public async Task<ActionResult<PaginationModel<OrderResModel[]>>> GetByCar(int carId)
    {
        ResponseModel<OrderResModel[]> response = new();

        var query = await db.Orders
            .Where(n => n.CarId == carId)
            .Select(n => new OrderResModel()
            {
                OrderId = n.OrderId,
                CarId = n.CarId ?? 0,
                Car = new OrderCarResModel()
                {
                    CarId = n.Car.CarId,
                    CarTypeId = n.Car.CarTypeId ?? 0,
                    TypeName = n.Car.CarType.TypeName ?? "",
                    ManufaturerId = n.Car.CarType.ManufacturerId ?? 0,
                    ManufacturerName = n.Car.CarType.Manufacturer.ManufacturerName ?? "",
                    CustomerId = n.Car.CustomerId ?? 0,
                    LicensePlate = n.Car.LicensePlate ?? "",
                    YearOfManufacture = n.Car.YearOfManufacture,
                    VIN = n.Car.VIN,
                    Customer = new OrderCarCustomerResModel()
                    {
                        CustomerId = n.Car.Customer.CustomerId,
                        TypeId = n.Car.Customer.TypeId ?? 0,
                        TypeName = n.Car.Customer.CustomerType.TypeName ?? "",
                        FullName = n.Car.Customer.FullName ?? "",
                        PhoneNumber = n.Car.Customer.PhoneNumber ?? "",
                        Representative = n.Car.Customer.Representative,
                        TaxCode = n.Car.Customer.TaxCode,
                        Address = n.Car.Customer.Address,
                        Email = n.Car.Customer.Email,
                        Note = n.Car.Customer.Note,
                        CreatedDate = n.Car.Customer.CreatedDate.ToDate(),
                        CreatedTime = n.Car.Customer.CreatedTime.ToTime()
                    },
                    CreatedDate = n.Car.CreatedDate.ToDate(),
                    CreatedTime = n.Car.CreatedTime.ToTime()
                },
                StatusId = n.StatusId ?? 0,
                StatusName = n.Status.StatusName ?? "",
                TypeId = n.TypeId ?? 0,
                TypeName = n.OrderType.TypeName ?? "",
                TemplateId = n.TemplateId,
                OrderCode = n.OrderCode,
                OrderDate = n.OrderDate.ToDate(),
                DateIn = n.DateIn.ToDate(),
                DateOutEstimated = n.DateOutEstimated.ToDate(),
                DateOutActual = n.DateOutActual != null ? n.DateOutActual.Value.ToDate() : "",
                ODOCurrent = n.ODOCurrent,
                ODONext = n.ODONext,
                ODOUnit = n.ODOUnit,
                ExpiredInDate = n.ExpiredInDate.ToDate(),
                IsInvoice = n.IsInvoice,
                AdvancePayment = n.AdvancePayment,
                PaymentMethod = n.PaymentMethod,
                Diagnosis = n.Diagnosis,
                CustomerNote = n.CustomerNote,
                InternalNote = n.InternalNote,
                VAT = n.VAT,
                Discount = n.Discount,
                OrderDetails = n.OrderDetails.Select(x => new OrderDetailResModel()
                {
                    OrderDetailId = x.OrderDetailId,
                    OrderId = x.Order.OrderId,
                    ProductId = x.ProductId ?? 0,
                    ProductCode = x.Product.ProductCode ?? "",
                    ProductName = x.Product.ProductName ?? "",
                    UnitName = x.Product.UnitName,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity,
                    Comment = x.Comment,
                    IsHideProduct = x.IsHideProduct,
                    CreatedDate = x.CreatedDate.ToDate(),
                    CreatedTime = x.CreatedTime.ToTime()
                }),
                CreatedDate = n.CreatedDate.ToDate(),
                CreatedTime = n.CreatedTime.ToTime()
            })
            .ToArrayAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<ResponseModel>> Delete(int id)
    {
        ResponseModel response = new();

        var query = await db.Orders
            .Where(n => n.OrderId == id)
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        db.Orders.RemoveRange(query);

        await db.SaveChangesAsync();

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<OrderResModel>> GetById(int id)
    {
        ResponseModel<OrderResModel> response = new();

        var query = await db.Orders
            .Where(n => n.OrderId == id)
            .OrderByDescending(n => n.OrderId)
            .Select(n => new OrderResModel()
            {
                OrderId = n.OrderId,
                CarId = n.CarId ?? 0,
                Car = new OrderCarResModel()
                {
                    CarId = n.Car.CarId,
                    CarTypeId = n.Car.CarTypeId ?? 0,
                    TypeName = n.Car.CarType.TypeName ?? "",
                    ManufaturerId = n.Car.CarType.ManufacturerId ?? 0,
                    ManufacturerName = n.Car.CarType.Manufacturer.ManufacturerName ?? "",
                    CustomerId = n.Car.CustomerId ?? 0,
                    LicensePlate = n.Car.LicensePlate ?? "",
                    YearOfManufacture = n.Car.YearOfManufacture,
                    VIN = n.Car.VIN,
                    Customer = new OrderCarCustomerResModel()
                    {
                        CustomerId = n.Car.Customer.CustomerId,
                        TypeId = n.Car.Customer.TypeId ?? 0,
                        TypeName = n.Car.Customer.CustomerType.TypeName ?? "",
                        FullName = n.Car.Customer.FullName ?? "",
                        PhoneNumber = n.Car.Customer.PhoneNumber ?? "",
                        Representative = n.Car.Customer.Representative,
                        TaxCode = n.Car.Customer.TaxCode,
                        Address = n.Car.Customer.Address,
                        Email = n.Car.Customer.Email,
                        Note = n.Car.Customer.Note,
                        CreatedDate = n.Car.Customer.CreatedDate.ToDate(),
                        CreatedTime = n.Car.Customer.CreatedTime.ToTime()
                    },
                    CreatedDate = n.Car.CreatedDate.ToDate(),
                    CreatedTime = n.Car.CreatedTime.ToTime()
                },
                StatusId = n.StatusId ?? 0,
                StatusName = n.Status.StatusName ?? "",
                TypeId = n.TypeId ?? 0,
                TypeName = n.OrderType.TypeName ?? "",
                TemplateId = n.TemplateId,
                OrderCode = n.OrderCode,
                OrderDate = n.OrderDate.ToDate(),
                DateIn = n.DateIn.ToDate(),
                DateOutEstimated = n.DateOutEstimated.ToDate(),
                DateOutActual = n.DateOutActual != null ? n.DateOutActual.Value.ToDate() : "",
                ODOCurrent = n.ODOCurrent,
                ODONext = n.ODONext,
                ODOUnit = n.ODOUnit,
                ExpiredInDate = n.ExpiredInDate.ToDate(),
                IsInvoice = n.IsInvoice,
                AdvancePayment = n.AdvancePayment,
                PaymentMethod = n.PaymentMethod,
                Diagnosis = n.Diagnosis,
                CustomerNote = n.CustomerNote,
                InternalNote = n.InternalNote,
                VAT = n.VAT,
                Discount = n.Discount,
                OrderDetails = n.OrderDetails.Select(x => new OrderDetailResModel()
                {
                    OrderDetailId = x.OrderDetailId,
                    OrderId = x.Order.OrderId,
                    ProductId = x.ProductId ?? 0,
                    ProductCode = x.Product.ProductCode ?? "",
                    ProductName = x.Product.ProductName ?? "",
                    UnitName = x.Product.UnitName,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity,
                    Comment = x.Comment,
                    IsHideProduct = x.IsHideProduct,
                    CreatedDate = x.CreatedDate.ToDate(),
                    CreatedTime = x.CreatedTime.ToTime()
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
    public async Task<ActionResult> Edit(OrderEditResModel req)
    {
        ResponseModel response = new();

        var query = await db.Orders
            .Where(n => n.OrderId == req.OrderId)
            .AsTracking()
            .FirstOrDefaultAsync();

        if (query == null)
        {
            response.Message = NOT_FOUND_MSG;
            return Ok(response);
        }

        if (!await db.Cars.AnyAsync(n => n.CarId == req.CarId))
        {
            response.Message = CAR_NOT_FOUND;
            return Ok(response);
        }

        if (!await db.OrderStatuses.AnyAsync(n => n.StatusId == req.StatusId))
        {
            response.Message = INVALID_STATUS;
            return Ok(response);
        }

        if (!await db.OrderTypes.AnyAsync(n => n.TypeId == req.TypeId))
        {
            response.Message = INVALID_TYPE;
            return Ok(response);
        }

        if (req.PaymentMethod != PaymentMethodEnum.CASH && req.PaymentMethod != PaymentMethodEnum.TRANSFER)
        {
            response.Message = INVALID_PAYMENT_METHOD;
            return Ok(response);
        }

        if (req.TemplateId != null)
        {
            if (!await db.Templates.AnyAsync(n => n.TemplateId == req.TemplateId))
            {
                response.Message = TEMPLATE_NOT_FOUNT;
                return Ok(response);
            }
        }

        if (req.OrderDetails.Select(n => n.ProductId).Distinct().Count() != req.OrderDetails.Count)
        {
            response.Message = PRODUCT_DUPLICATE;
            return Ok(response);
        }

        var orderExpiredDateInDay = configuration.GetValue<int>("OrderExpiredDateInDay");
        var oDONextInKm = configuration.GetValue<int>("ODONextInKm");

        if (req.ODONext == 0)
        {
            req.ODONext = req.ODOCurrent + oDONextInKm;
        }

        var now = DateTime.Now;

        var orderDate = now.Date;
        var inputDate = now.Date;
        var outputDateEstimated = now.Date;
        var expiredInDate = now.AddDays(orderExpiredDateInDay).Date;

        if (!string.IsNullOrEmpty(req.OrderDate))
        {
            var date = req.OrderDate.ToDateTime();
            if (date != null)
            {
                orderDate = date.Value.Date;
            }
        }

        if (!string.IsNullOrEmpty(req.DateIn))
        {
            var date = req.DateIn.ToDateTime();
            if (date != null)
            {
                inputDate = date.Value.Date;
            }
        }

        if (!string.IsNullOrEmpty(req.DateOutEstimated))
        {
            var date = req.DateOutEstimated.ToDateTime();
            if (date != null)
            {
                outputDateEstimated = date.Value.Date;
            }
        }

        if (!string.IsNullOrEmpty(req.ExpiredInDate))
        {
            var date = req.ExpiredInDate.ToDateTime();
            if (date != null)
            {
                expiredInDate = date.Value.Date;
            }
        }

        var reqProductIds = req.OrderDetails.Select(n => n.ProductId).ToList();

        foreach (var item in reqProductIds)
        {
            if (!db.Products.Any(n => n.ProductId == item))
            {
                response.Message = PRODUCT_NOT_FOUND;
                return Ok(response);
            }
        }

        var dbProducts = await db.Products
            .Where(n => reqProductIds.Contains(n.ProductId))
            .Select(n => new
            {
                n.ProductId,
                n.UnitPrice
            })
            .ToArrayAsync();

        var detailIds = req.OrderDetails.Select(n => n.OrderDetailId).ToList();

        using var tran = await db.Database.BeginTransactionAsync();

        query.CarId = req.CarId;
        query.StatusId = req.StatusId;
        query.TypeId = req.TypeId;
        query.TemplateId = req.TemplateId;
        query.OrderCode = req.OrderCode;
        query.OrderDate = orderDate;
        query.DateIn = inputDate;
        query.DateOutEstimated = outputDateEstimated;
        query.ODOCurrent = req.ODOCurrent;
        query.ODONext = req.ODONext;
        query.ODOUnit = req.ODOUnit;
        query.ExpiredInDate = expiredInDate;
        query.IsInvoice = req.IsInvoice;
        query.AdvancePayment = req.AdvancePayment;
        query.PaymentMethod = req.PaymentMethod;
        query.Diagnosis = req.Diagnosis;
        query.CustomerNote = req.CustomerNote;
        query.InternalNote = req.InternalNote;
        query.Discount = req.Discount;
        query.UpdatedUserId = USER_ID;
        query.UpdatedDateTime = DateTime.Now;

        await db.SaveChangesAsync();

        if (req.OrderDetails != null)
        {
            // Delete order detail
            var deleteDetails = await db.OrderDetails
                .Where(n => n.OrderId == req.OrderId && !detailIds.Contains(n.OrderDetailId))
                .ToArrayAsync();

            if (deleteDetails != null)
            {
                db.OrderDetails.RemoveRange(deleteDetails);
                await db.SaveChangesAsync();
            }

            // Update order detail
            var updateDetails = await db.OrderDetails
            .Where(n =>
                n.OrderId == req.OrderId
                && detailIds.Contains(n.OrderDetailId)
            )
            .AsTracking()
            .ToArrayAsync();

            if (updateDetails != null)
            {

                foreach (var item in updateDetails)
                {
                    var reqDetail = req.OrderDetails.Where(n => n.OrderDetailId == item.OrderDetailId).FirstOrDefault();
                    if (reqDetail != null)
                    {
                        item.ProductId = reqDetail.ProductId;
                        item.UnitPrice = dbProducts.Where(x => x.ProductId == item.ProductId).FirstOrDefault()?.UnitPrice ?? 0;
                        item.Quantity = reqDetail.Quantity;
                        item.Comment = reqDetail.Comment;
                        item.IsHideProduct = reqDetail.IsHideProduct;
                        item.UpdatedUserId = USER_ID;
                        item.UpdatedDateTime = DateTime.Now;
                    }
                }
                await db.SaveChangesAsync();
            }

            // Insert order detail
            var insertDetails = req.OrderDetails.Where(n => n.OrderDetailId == 0).Select(n => new OrderDetailEntity()
            {
                OrderId = req.OrderId,
                ProductId = n.ProductId,
                UnitPrice = dbProducts.Where(n => n.ProductId == n.ProductId).FirstOrDefault()?.UnitPrice ?? 0,
                Quantity = n.Quantity,
                CreatedUserId = USER_ID
            }).ToList();

            if (insertDetails != null)
            {
                await db.OrderDetails.AddRangeAsync(insertDetails);

                await db.SaveChangesAsync();
            }
        }

        await tran.CommitAsync();

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("export")]
    public async Task<FileResult> Export(
        string? keyword = "",
        int customerId = 0,
        int carId = 0,
        string? createdFromDate = "",
        string? createdToDate = "",
        int statusId = 0,
        int typeId = 0,
        string? dateInFrom = "",
        string? dateInTo = "",
        string? dateOutFrom = "",
        string? dateOutTo = "")
    {
        var fromDateValue = createdFromDate?.ToDateTime();
        var toDateValue = createdToDate?.ToDateTime();

        var dateInFromValue = dateInFrom?.ToDateTime();
        var dateInToValue = dateInTo?.ToDateTime();

        var dateOutFromValue = dateOutFrom?.ToDateTime();
        var dateOutToValue = dateOutTo?.ToDateTime();

        if (!string.IsNullOrEmpty(createdFromDate))
        {
            if (fromDateValue == null)
            {
                return MessageFile("Ngày tạo không đúng định dạng (từ ngày)");
            }
        }

        if (!string.IsNullOrEmpty(createdToDate))
        {
            if (toDateValue == null)
            {
                return MessageFile("Ngày tạo không đúng định dạng (đến ngày)");
            }
        }

        if (!string.IsNullOrEmpty(dateInFrom))
        {
            if (dateInFromValue == null)
            {
                return MessageFile("Ngày vào không đúng định dạng (từ ngày)");
            }
        }

        if (!string.IsNullOrEmpty(dateInFrom))
        {
            if (dateInFromValue == null)
            {
                return MessageFile("Ngày vào không đúng định dạng (đến ngày)");
            }
        }

        if (!string.IsNullOrEmpty(dateOutFrom))
        {
            if (dateOutFromValue == null)
            {
                return MessageFile("Ngày ra không đúng định dạng (từ ngày)");
            }
        }

        if (!string.IsNullOrEmpty(dateOutTo))
        {
            if (dateOutToValue == null)
            {
                return MessageFile("Ngày ra không đúng định dạng (đến ngày)");
            }
        }

        var query = await db.Orders
            .Where(n =>
                (customerId == 0 || n.Car.CustomerId == customerId)
                && (carId == 0 || n.CarId == carId)
                && (fromDateValue == null || n.CreatedDate >= fromDateValue.Value.Date)
                && (toDateValue == null || n.CreatedDate <= toDateValue.Value.Date)
                && (statusId == 0 || n.StatusId == statusId)
                && (typeId == 0 || n.TypeId == typeId)
                && (dateInFromValue == null || n.DateIn >= dateInFromValue.Value.Date)
                && (dateInToValue == null || n.DateIn <= dateInToValue.Value.Date)
                && (dateOutFromValue == null || n.DateOutActual >= dateOutFromValue.Value.Date)
                && (dateOutToValue == null || n.DateOutActual <= dateOutToValue.Value.Date)
                && (
                    (string.IsNullOrEmpty(keyword) || EF.Functions.Like(EF.Functions.Collate(n.Car.LicensePlate ?? "", COLLATION), $"%{keyword}%"))
                    || (string.IsNullOrEmpty(keyword) || EF.Functions.Like(EF.Functions.Collate(n.Car.Customer.FullName ?? "", COLLATION), $"%{keyword}%"))
                )
            )
            .Select(n => new
            {
                n.OrderId,
                n.Car.LicensePlate,
                OrderCode = n.OrderCode ?? "",
                OrderStatus = n.Status.StatusName,
                DateIn = n.DateIn.Date,
                n.DateOutEstimated,
                n.DateOutActual,
                CarType = n.Car.CarType.TypeName,
                n.Car.Customer.FullName,
                n.Car.Customer.Representative,
                CustomerType = n.Car.Customer.CustomerType.TypeName,
                n.Car.Customer.TaxCode,
                n.Car.Customer.PhoneNumber,
                n.Car.VIN,
                n.ODOCurrent,
                Total = n.OrderDetails.Sum(n => n.Quantity * n.UnitPrice) - n.Discount
            })
            .ToArrayAsync();

        string appFolderPath = Directory.GetCurrentDirectory();

        string templateFolderPath = Path.Combine(appFolderPath, "Templates");

        string templateFilePath = Path.Combine(templateFolderPath, "ExportOrderTemplate.xlsx");

        var template = new XLTemplate(templateFilePath);

        if (!System.IO.File.Exists(templateFilePath))
        {
            return MessageFile("Template file not found");
        }

        using var memoryStream = new MemoryStream();

        template.AddVariable("Orders", query);

        template.Generate();

        template.SaveAs(memoryStream);

        var now = DateTime.Now;

        string fileName = $"ExportOrder_{now:dd_MM_yyyy_H_mm}.xlsx";

        var bytes = memoryStream.ToArray();

        return File(bytes, EXCEL_CONTENT_TYPE, fileName);
    }
}
