using MercadoPago.CheckoutAPI.Interfaces;
using MercadoPago.CheckoutAPI.Services;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddHttpClient("MercadoPagoHttpClient", client =>
{
    var baseUrl = configuration["MercadoPago:UrlBase"];
    var accessToken = configuration["MercadoPago:AccessToken"];

    if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrWhiteSpace(accessToken))
    {
        throw new InvalidOperationException("MercadoPago:UrlBase o MercadoPago:AccessToken no est�n configurados.");
    }

    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
});

builder.Services.AddScoped<IRequestHandlerService, RequestHandlerService>();
builder.Services.AddScoped<IPaymentsService, PaymentsService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
