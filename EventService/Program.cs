using System;
using EventService.EventProcessing;
using EventService.Interface;
using EventService.Models;
using EventService.Repository;


//using ProductService.Interface;
//using ProductService.Models;
//using CustomerService.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option => 
option.UseInMemoryDatabase("InMem"));
builder.Services.AddScoped<IEvent,EventRepo>();
builder.Services.AddSingleton<IEventProcessor,EventProcessor>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//PrepDb.PrepPopulate(app);

app.Run();
