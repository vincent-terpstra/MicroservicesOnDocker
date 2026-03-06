using CommandsService.Data;
using CommandsService.Endpoints;
using CommandsService.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<CommandsDbContext>(
    opt => opt.UseInMemoryDatabase(":memory:"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.InitializeDatabase();

app.UseHttpsRedirection();
app.UseExceptionHandlingMiddleware();
app.MapGroup("api/commands").MapCommandRoutes();

app.Run();

