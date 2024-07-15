using AllRoadsLeadToRome.Core.Auth;
using AllRoadsLeadToRome.Service.Order.Application;
using AllRoadsLeadToRome.Service.Order.Infrastructure;
using AllRoadsLeadToRome.Service.Order.Infrastructure.Context;
using AllRoadsLeadToRome.Service.Order.Infrastructure.GrpcServices;
using AuthApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState);
        var error = problemDetails.Errors.FirstOrDefault();
        var message = "Bad request";
        if (error.Value.Length > 0)
        {
            message = $"{error.Value[0]}";

            if (!message.Contains(error.Key))
            {
                message = $"{error.Key}: {message}";
            }
        }

        return new ObjectResult(new { message = $"{message}" })
        {
            StatusCode = 400,
        };
    };
});

//builder.Services.AddGrpcClient<AuthGrpc.AuthGrpcClient>(options =>
//    {
//        //options.Address = new Uri("https://localhost:7243");
//        options.Address = new Uri("https://authservice_webapi:8081");
//    })
//    .ConfigurePrimaryHttpMessageHandler(() =>
//    {
//        var handler = new HttpClientHandler();
//        // Skip SSL certificate validation
//        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
//        return handler;
//    });
// .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
// {
//     ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
// });


builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("OrderDB"));
});

//builder.Services.AddScoped<OrderDbContext>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomSwaggerGen();
builder.Services.AddCustomAuthentication(builder.Configuration);

ServicesIoC.ConfigureServices(builder.Services);
InfrastructureConfigureServices.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

//app.MapGrpcService<OrderService>();

app.Run();