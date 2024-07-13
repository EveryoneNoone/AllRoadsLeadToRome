using Core.Entities;
using Infrastructure;
using Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebApi;
using WebApi.Controllers;
using WebApi.Models;
using Xunit;

public class AccountControllerTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly AccountController _controller;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AppDbContext _context;

    public AccountControllerTests()
    {
        var services = new ServiceCollection();

        // Использование общего метода для настройки конфигурации
        var configuration = ConfigurationHelper.GetConfiguration();
        services.AddSingleton<IConfiguration>(configuration);

        // Настройка DbContext для использования базы данных в памяти
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase("TestDatabase");
        });

        // Вызов метода ConfigureServices из Startup
        var startup = new Startup(configuration);
        startup.ConfigureServices(services);

        // Создание ServiceProvider для получения сервисов
        _serviceProvider = services.BuildServiceProvider();

        // Получение зависимостей
        _context = _serviceProvider.GetRequiredService<AppDbContext>();
        _userManager = _serviceProvider.GetRequiredService<UserManager<User>>();
        _signInManager = _serviceProvider.GetRequiredService<SignInManager<User>>();

        var jwtSettings = _serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value;

        _controller = new AccountController(
            _userManager,
            _signInManager,
            Options.Create(jwtSettings));

        //// Настройка HttpContext для SignInManager и UserManager
        //var httpContext = new DefaultHttpContext();
        //var contextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
        //contextAccessor.HttpContext = httpContext;

        ////_signInManager.Context = httpContext;
        ////_userManager.Context = httpContext;

        // Применение DbInitializer
        DbInitializer.Initialize(_context, _userManager, _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>()).Wait();
    }

    [Fact]
    public async Task Register_ReturnsOk_WhenRegistrationIsSuccessful()
    {
        // Arrange
        var registerModel = new RegisterModel
        {
            FullName = "John Doe",
            Email = "john.doe@example.com",
            Password = "Password123!",
            DriverApproved = true,
            Type = UserType.User,
            NotificationPreference = NotificationType.Email
        };

        // Act
        var result = await _controller.Register(registerModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var registerResult = Assert.IsType<RegisterResult>(okResult.Value);
        Assert.Equal("User registered successfully", registerResult.Message);
    }

    [Fact]
    public async Task Login_ReturnsOk_WithValidToken()
    {
        // Arrange
        var user = new User
        {
            UserName = "john.doe@example.com",
            Email = "john.doe@example.com",
            FullName = "John Doe",
            Type = UserType.User,
            NotificationPreference = NotificationType.Email,
            DriverApproved = false,
            RefreshToken = string.Empty,
            RefreshTokenExpiryTime = DateTime.UtcNow
        };
        await _userManager.CreateAsync(user, "Password123!");
        var loginModel = new LoginModel
        {
            Email = "john.doe@example.com",
            Password = "Password123!",
            RememberMe = false
        };

        //// Настройка HttpContext для вызова SignInManager
        var httpContext = new DefaultHttpContext();
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        try
        {

            var result = await _controller.Login(loginModel);
            // Act

            var okResult = Assert.IsType<OkObjectResult>(result);
            var loginResult = Assert.IsType<dynamic>(okResult.Value);
            Assert.NotNull(loginResult.Token);
            // Assert
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    [Fact]
    public async Task RefreshToken_ReturnsOk_WithNewToken()
    {
        // Arrange
        var user = new User
        {
            UserName = "john.doe@example.com",
            Email = "john.doe@example.com",
            RefreshToken = "existingRefreshToken",
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1)
        };
        await _userManager.CreateAsync(user, "Password123!");
        var tokenRequestModel = new TokenRequestModel
        {
            Token = "existingToken",
            RefreshToken = "existingRefreshToken"
        };

        // Настройка HttpContext для вызова SignInManager
        var httpContext = new DefaultHttpContext();
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        // Act
        var result = await _controller.RefreshToken(tokenRequestModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var refreshTokenResult = Assert.IsType<dynamic>(okResult.Value);
        Assert.NotNull(refreshTokenResult.Token);
        Assert.NotNull(refreshTokenResult.RefreshToken);
    }
}
