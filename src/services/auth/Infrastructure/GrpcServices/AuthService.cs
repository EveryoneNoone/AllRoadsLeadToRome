using AuthApi;
using Core.Entities;
using Grpc.Core;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.GrpcServices;

public class AuthService : AuthGrpc.AuthGrpcBase
{
    private readonly UserManager<User> _userManager;

    public AuthService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        
        return new GetUserResponse
        {
            Id = user?.Id ?? string.Empty,
        };
    }
}
