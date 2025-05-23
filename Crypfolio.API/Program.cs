using Mapster;
using Crypfolio.Api.Endpoints;
using Crypfolio.Application.Mapping;
using Crypfolio.Application.Services;
using Crypfolio.Application.Interfaces;
using Crypfolio.Infrastructure.Extensions;
using Crypfolio.Infrastructure.Persistence;

MappingConfig.RegisterMappings();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMapster();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<AssetService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapAssetEndpoints();

app.MapAuthEndpoints();


app.Run();


