using AllRoadsLeadToRome.Core.Enums;
using AllRoadsLeadToRome.Service.Order.Application.Dtos;
using AllRoadsLeadToRome.Service.Order.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        Route(""),
        Authorize(Roles = "Administrator")
    ]
    public async Task<ActionResult<int>> Create([FromBody] AddOrderRequestDto request, CancellationToken ct = default)
    {
        var id = await _orderService.Create(request, ct);
        return Ok(id);
    }

    [
        HttpGet,
        Route("{id}"),
        Authorize()
    ]
    public async Task<ActionResult<int>> GetById(int id, CancellationToken ct = default)
    {
        var order = await _orderService.GetById(id, ct);
        return Ok(order);
    }

    [
        HttpGet,
        Route(""),
        Authorize(Roles = "Administrator")
    ]
    public async Task<ActionResult<int>> GetAll(CancellationToken ct = default)
    {
        var all = await _orderService.GetAll(ct);
        return Ok(all);
    }

    [
        HttpPatch,
        Route("{id}/complete"),
        Authorize(Roles = "Administrator")
    ]
    public async Task<ActionResult<int>> MakeCompleted(int id, CancellationToken ct = default)
    {
        await _orderService.ChangeStatus(id, OrderStatus.Completed, ct);
        return Ok();
    }
}