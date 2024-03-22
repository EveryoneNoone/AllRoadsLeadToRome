using Application.DTOs;
using Application.Models;

namespace Core.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> RegisterUserAsync(UserRegisterDto userRegisterDto);
        Task<ServiceResponse<string>> LoginUserAsync(UserLoginDto userLoginDto);
        Task<ServiceResponse<string>> RefreshTokenAsync(string token);
        Task<ServiceResponse<bool>> RevokeTokenAsync(string token);
    }
}
