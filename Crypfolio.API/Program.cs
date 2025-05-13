using Crypfolio.Api.Endpoints;
using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;
using Crypfolio.Application.Services;
using Crypfolio.Infrastructure.Repositories;
using Mapster;
using Crypfolio.Application.Mapping; 

MappingConfig.RegisterMappings();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMapster();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAssetRepository, InMemoryAssetRepository>();
builder.Services.AddSingleton<AssetService>();
builder.Services.AddScoped<IAssetService, AssetService>();

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

app.Run();


