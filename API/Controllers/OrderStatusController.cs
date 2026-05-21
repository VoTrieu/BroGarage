using BroGarage.API.Attributes;
using BroGarage.API.Data;
using BroGarage.API.Shared.Models;
using BroGarage.API.Shared.ResponseModels.OrderStatus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BroGarage.API.Controllers;

[Route("order-status")]
[Auth]
public class OrderStatusController : BaseController
{
    public OrderStatusController(DatabaseContext db) : base(db)
    {
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<ResponseModel<OrderStatusResModel[]>>> GetAll()
    {
        ResponseModel<OrderStatusResModel[]> response = new();

        var query = await db.OrderStatuses
            .Select(n => new OrderStatusResModel()
            {
                StatusId = n.StatusId,
                StatusName = n.StatusName ?? ""
            })
            .ToArrayAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }
}
