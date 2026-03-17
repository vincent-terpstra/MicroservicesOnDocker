using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Data.Interfaces;
using PlatformService.Data.Repositories;
using PlatformService.Services.Http;
using PlatformService.Services.Interfaces;
using PlatformService.Services.MessageBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddDbContext();
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<CommandDataHttpClient>(opt =>
    opt.BaseAddress = new Uri(builder.Configuration["CommandService"]!));
builder.Services.AddSingleton<IPlatformPublisher, MessageBusClient>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.InitializeDatabase();

// app.UseHttpsRedirection();
app.MapControllers();
app.Run();

