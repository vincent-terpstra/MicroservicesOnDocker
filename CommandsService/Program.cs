using CommandsService.Data;
using CommandsService.Endpoints;
using CommandsService.EventHandlers;
using CommandsService.Events;
using CommandsService.Interfaces;
using CommandsService.Middleware;
using CommandsService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CommandsDbContext>(
    opt => opt.UseInMemoryDatabase(":memory:"));
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddScoped<IEventHandler<PlatformPublishedEvent>, PlatformPublishedEventHandler>();
builder.Services.AddHostedService<MessageBusSubscriber>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.InitializeDatabase();

// app.UseHttpsRedirection();
app.UseExceptionHandlingMiddleware();
app.MapCommandRoutes();
app.MapPlatformRoutes();

app.Run();

