using AllRoadsLeadToRome.Service.Order.Application;
using AllRoadsLeadToRome.Service.Order.Infrastructure;
using AllRoadsLeadToRome.Service.Order.Infrastructure.Context;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DB"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
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
    });

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



app.Run();