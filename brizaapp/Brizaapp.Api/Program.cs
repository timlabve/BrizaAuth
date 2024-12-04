using Brizaapp.Api.Extensions;
using Brizaapp.Utils.Middlewares;
using Microsoft.AspNetCore.Hosting;
using NLog;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);


LogManager.Configuration = new NLogLoggingConfiguration(
    builder.Configuration.GetSection("NLog"));


builder.Logging.ClearProviders(); 
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); 
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command",
    Microsoft.Extensions.Logging.LogLevel.Warning); 
builder.Logging.AddNLog();

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureServices(builder.Configuration);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Brizaapp.Identity.Controllers.AuthController).Assembly)
    .AddControllersAsServices(); 

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
