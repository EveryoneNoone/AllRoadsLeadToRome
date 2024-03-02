namespace Application.Services.Interfaces;

public interface IOrderService
{
    Task Create(CancellationToken ct);
}