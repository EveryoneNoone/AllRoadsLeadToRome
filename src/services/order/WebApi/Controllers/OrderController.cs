using AllRoadsLeadToRome.Service.Order.Application.Dtos;
using AllRoadsLeadToRome.Service.Order.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AllRoadsLeadToRome.Service.Order.WebApi.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [
        HttpPost,
        Route("")
    ]
    public async Task<ActionResult<int>> Create([FromBody] AddOrderRequestDto request, CancellationToken ct = default)
    {
        var id = await _orderService.Create(request, ct);
        return Ok(id);
    }
}