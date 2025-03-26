using System;
using System.IO;
using CustomerService.AsyncDataServices;
using CustomerService.Interface;
using CustomerService.Models;
using CustomerService.Repository;
using CustomerService.SyncDataServices.Grpc;
using CustomerService.SyncDataServices.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Inject IWebHostEnvironment
var env = builder.Environment;
Console.WriteLine($"Current Environment: {env.EnvironmentName}");

// Load configuration
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Get MySQL password from environment variables (SECURE)
string mysqlPassword = Environment.GetEnvironmentVariable("MYSQL_ROOT_PASSWORD") ?? "";
Console.WriteLine($"Password: {mysqlPassword}");
if (string.IsNullOrWhiteSpace(mysqlPassword))
{
    Console.WriteLine("Warning: MYSQL_ROOT_PASSWORD is not set. Ensure it is configured correctly.");
}

// Get database connection string
string connectionString = builder.Configuration.GetConnectionString("CustomerSvcConn");

// Inject MySQL password into connection string if it's missing
if (!string.IsNullOrWhiteSpace(mysqlPassword) && connectionString.Contains("__MYSQL_ROOT_PASSWORD__"))
{
    connectionString = connectionString.Replace("__MYSQL_ROOT_PASSWORD__", mysqlPassword);
}

Console.WriteLine($"ConnectionString: {connectionString}");

// Add services to the container
if (env.IsProduction())
{
    Console.WriteLine("Running in Production mode (Using MySQL)");
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}
else
{
    Console.WriteLine("Running in Development mode (Using In-Memory Database)");
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMem"));
}

// Dependency Injections
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddHttpClient<IEventDataClient, HttpEventDataClient>();
builder.Services.AddSingleton<IMessageBusClient,MessageBusClient>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddGrpc();

// Swagger (API Documentation)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure migrations are applied BEFORE the app starts handling requests
Console.WriteLine("Running Database Migrations...");
PrepDb.PrepPopulate(app, env.IsProduction());

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Register gRPC service
app.MapGrpcService<GrpcCustomerService>();

// Serve the proto file
app.MapGet("/protos/customers.proto", async context =>
{
    await context.Response.WriteAsync(await File.ReadAllTextAsync("protos/customers.proto"));
});

Console.WriteLine("Application is starting...");
app.Run();
