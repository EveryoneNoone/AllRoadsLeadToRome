using AllRoadsLeadToRome.Service.Order.Application;
using AllRoadsLeadToRome.Service.Order.Infrastructure;
using AllRoadsLeadToRome.Service.Order.Infrastructure.Context;
using AllRoadsLeadToRome.Service.Order.Infrastructure.GrpcServices;
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

builder.Services
    .AddGrpcClient<OrderGrpc.OrderGrpcClient>((options) =>
    {
        options.Address = new Uri("https://localhost:5237");
    });

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DB"));
});

//builder.Services.AddScoped<OrderDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


ServicesIoC.ConfigureServices(builder.Services);
InfrastructureConfigureServices.ConfigureServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGrpcService<OrderService>();

app.Run();