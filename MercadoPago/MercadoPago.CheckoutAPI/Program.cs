using MercadoPago.CheckoutAPI.Application.Extensions;
using MercadoPago.CheckoutAPI.Application.Settings;
using MercadoPago.CheckoutAPI.Extensions;
using MercadoPago.CheckoutAPI.Infrastructure.Extensions;
using MercadoPago.CheckoutAPI.Middlewares;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var cors = "CORS";

builder.Services.AddHttpClient("MercadoPagoHttpClient", client =>
{
    var baseUrl = configuration["MercadoPago:UrlBase"];
    var accessToken = configuration["MercadoPago:AccessToken"];

    if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrWhiteSpace(accessToken))
    {
        throw new InvalidOperationException("MercadoPago:UrlBase o MercadoPago:AccessToken no están configurados.");
    }

    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
});

builder.Services.AddInjectionInfrastructure(configuration);
builder.Services.AddInjectionApplication();
builder.Services.AddAuthentication(configuration);
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationHandler>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<MercadoPagoSettings>(builder.Configuration.GetSection("MercadoPago"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddCors(options =>
{
    options.AddPolicy(cors, policy => 
    {
        policy.WithOrigins("*");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
    
});

var app = builder.Build();

app.UseCors(cors);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
