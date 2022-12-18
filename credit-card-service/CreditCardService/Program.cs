using CreditCardService.Collections;
using CreditCardService.Configuration;
using CreditCardService.Middlewares;
using CreditCardService.Models;
using CreditCardService.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<CreditCardDBSettings>(builder.Configuration.GetSection("CreditCardDBSettings"));
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICreditCardCollection, CrediCardCollection>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseGlobalExceptionMiddleware();

app.MapControllers();

app.Run();
public partial class Program { }
