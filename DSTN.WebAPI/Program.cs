using DSTN.Application.Services.TimeZoneConfigurator;
using DSTN.Application.Services.TimeZoneNotifier;
using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;
using DSTN.Infrastructure;
using DSTN.Infrastructure.Persistence;
using DSTN.Infrastructure.Persistence.Helpers;
using DSTN.WebAPI.Workers;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(configuration.GetSection("Logging"));
builder.Logging.AddDebug();
builder.Logging.AddConsole();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;



services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

services.AddScoped<IQueryBuilder<ObservedTimeZone>, QueryBuilder<ObservedTimeZone>>();
services.AddScoped<IQueryBuilder<Notification>, QueryBuilder<Notification>>();
services.AddScoped<ISystemTimeZoneProvider, SystemTimeZoneProvider>();
services.AddScoped<ITimeZoneConfiguratorService, TimeZoneConfiguratorService>();
services.AddScoped<ITimeZoneNotifierService, TimeZoneNotifierService>();


services.AddHostedService<DSTNotificationWorker>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); 
}

app.Run();


public partial class Program { }