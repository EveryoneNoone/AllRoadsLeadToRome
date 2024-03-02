using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("order")]
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
    public async Task<dynamic> Create(CancellationToken ct = default)
    {
        await _orderService.Create(ct);
        return Ok();
    }
}