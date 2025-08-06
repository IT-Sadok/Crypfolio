using Mapster;
using Crypfolio.Api.Endpoints;
using Crypfolio.API.Endpoints;
using Crypfolio.Application.Mapping;
using Crypfolio.Infrastructure.Extensions;
using Crypfolio.Middleware;

MappingConfig.RegisterMappings();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMapster();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices();
builder.Services.AddInfrastructure(builder.Configuration);

builder.WebHost.ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
    .CaptureStartupErrors(true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapAssetEndpoints();
app.MapAuthEndpoints();
app.MapWalletEndpoints();
app.MapExchangeAccountEndpoints();

app.Run();

public partial class Program { }