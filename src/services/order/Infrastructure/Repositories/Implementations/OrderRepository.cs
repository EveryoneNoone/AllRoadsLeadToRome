using Application.Repositories.Interfaces;

namespace Infrastructure.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    public Task Create(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}