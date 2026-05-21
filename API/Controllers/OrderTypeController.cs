using BroGarage.API.Attributes;
using BroGarage.API.Data;
using BroGarage.API.Shared.Models;
using BroGarage.API.Shared.ResponseModels.OrderType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BroGarage.API.Controllers;

[Route("order-type")]
[Auth]
public class OrderTypeController : BaseController
{
    public OrderTypeController(DatabaseContext db) : base(db)
    {
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<OrderTypeResModel[]>> GetAll()
    {
        ResponseModel<OrderTypeResModel[]> response = new();

        var query = await db.OrderTypes
            .Select(n => new OrderTypeResModel
            (
                n.TypeId,
                n.TypeName ?? ""
            ))
            .ToArrayAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }
}
