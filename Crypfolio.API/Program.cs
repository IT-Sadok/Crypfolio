using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Mapster;
using Crypfolio.Api.Endpoints;
using Crypfolio.API.Endpoints;
using Crypfolio.Application.Mapping;
using Crypfolio.Infrastructure.Extensions;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

MappingConfig.RegisterMappings();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables(); //For Azure overrides

if (!builder.Environment.IsDevelopment())
{
    var keyVaultUri = new Uri("https://crypfolio-kv.vault.azure.net/");
    builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential(), new KeyVaultSecretManager());
}

builder.Services.AddMapster();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler(options =>
{
    options.ExceptionHandler = async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new { error = "Something went wrong" });
    };
});

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

app.MapGet("/health", () => "OK");
//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseExceptionHandler();

app.MapAssetEndpoints();
app.MapAuthEndpoints();
app.MapWalletEndpoints();
app.MapExchangeAccountEndpoints();

app.Run();

public partial class Program { }