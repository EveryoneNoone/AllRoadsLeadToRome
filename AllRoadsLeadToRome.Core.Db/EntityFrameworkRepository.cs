using Microsoft.EntityFrameworkCore;

namespace AllRoadsLeadToRome.Core.Db;

public class EntityFrameworkRepository<T> where T : BaseEntityFrameworkEntity
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public EntityFrameworkRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> GetById(int id, CancellationToken ct)
    {
        var entity = await _dbSet.FindAsync(id, ct);
        if (entity == null)
        {
            throw new Exception($"Not found entity {nameof(T)}");
        }

        return entity;
    }

    public async Task<IEnumerable<T>> GetAll(CancellationToken ct)
    {
        return await _dbSet.ToListAsync(cancellationToken: ct);
    }

    public async Task<T> Add(T entity, CancellationToken ct)
    {
        entity.CreatedDate = DateTime.UtcNow;
        await _dbSet.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task Update(int id, Action<T> applyPatch, CancellationToken ct)
    {
        var entity = await GetById(id, ct);
        applyPatch(entity);

        await _context.SaveChangesAsync(ct);
    }

    public async Task Delete(int id, CancellationToken ct)
    {
        var entity = await GetById(id, ct);
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}