using Microsoft.EntityFrameworkCore;
using CashRegister.Infrastructure.Context;
using Microsoft.OpenApi.Models;
using MediatR;
using CashRegister.Infrastructure;
using CashRegister.API.Configurations;

var builder = WebApplication.CreateBuilder(args);
static void RegisterService(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CashRegister.API", Version = "v1" });
});

builder.Services.AddDbContext<CashRegisterDBContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("CashRegisterDBConnection")
    ));
builder.Services.AddMediatR(typeof(Program));
RegisterService(builder.Services);

//builder.Services.RegisterAutoMapper();
var app = builder.Build();

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
