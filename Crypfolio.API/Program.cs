using Mapster;
using Crypfolio.Api.Endpoints;
using Crypfolio.Application.Mapping;
using Crypfolio.Infrastructure.Extensions;

MappingConfig.RegisterMappings();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMapster();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices();
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


