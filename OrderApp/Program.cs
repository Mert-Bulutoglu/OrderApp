using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrderApp.API.Extensions;
using OrderApp.API.Middlewares;
using OrderApp.Caching.Configurations;
using OrderApp.Infrastructure.Mapping;
using OrderApp.Persistance.Context;
using System.Reflection;
using OrderApp.Infrastructure.Validations;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using OrderApp.RabbitMQ.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(OrderDtoValidator).Assembly);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddLoggingExtension();


builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseMySql(builder.Configuration.GetConnectionString("SqlConnection"), new MySqlServerVersion(new Version(8, 0, 11)), mySqlOptions =>
    {
        mySqlOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

var redisConfiguration = builder.Configuration.GetSection("ConnectionStrings").Get<RedisConfiguration>();
builder.Services.AddSingleton(redisConfiguration);

builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetSection("RabbitMQConfiguration"));

builder.Services.AddApplicationServices();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ElasticLoggingMiddleware>();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
