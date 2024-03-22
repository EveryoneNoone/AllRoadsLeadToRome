using Core.Entities;

namespace Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByIdAsync(Guid roleId);
        Task<bool> AddRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(Guid roleId);
    }
}
