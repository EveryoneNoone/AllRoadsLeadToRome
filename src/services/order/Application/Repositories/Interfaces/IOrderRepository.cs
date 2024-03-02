namespace Application.Repositories.Interfaces;

public interface IOrderRepository
{ 
    Task Create(CancellationToken ct);
}