using BroGarage.API.Attributes;
using BroGarage.API.Data;
using BroGarage.API.Shared.Models;
using BroGarage.API.Shared.ResponseModels.CustomerType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BroGarage.API.Controllers;

[Route("customer-type")]
[Auth]
public class CustomerTypeController : BaseController
{
    public CustomerTypeController(DatabaseContext db) : base(db)
    {
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<ResponseModel<CustomerTypeGetAllResModel[]>>> GetAll()
    {
        ResponseModel<CustomerTypeGetAllResModel[]> response = new();

        var query = await db.CustomerTypes
            .Select(n => new CustomerTypeGetAllResModel()
            {
                TypeId = n.TypeId,
                TypeName = n.TypeName ?? ""
            })
            .ToArrayAsync();

        response.Result = query;

        response.IsSuccess = true;

        return Ok(response);
    }
}
