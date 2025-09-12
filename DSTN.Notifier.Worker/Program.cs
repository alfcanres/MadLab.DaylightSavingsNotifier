using DSTN.Notifier.Worker;
//using DSTN.Infrastructure.Persistence;
//using DSTN.Infrastructure.Persistence.Helpers;
//using DSTN.Application.Services.TimeZoneConfigurator;
//using DSTN.Application.Services.TimeZoneNotifier;
//using DSTN.Domain.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using DSTN.Domain.Entities;


var builder = Host.CreateApplicationBuilder(args);


//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlite("Data Source=dstn.db"));

//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//builder.Services.AddScoped<IQueryBuilder<ObservedTimeZone>, QueryBuilder<ObservedTimeZone>>();
//builder.Services.AddScoped<IQueryBuilder<Notification>, QueryBuilder<Notification>>();

//builder.Services.AddScoped<TimeZoneConfiguratorService>();
//builder.Services.AddScoped<TimeZoneNotifierService>();


builder.Services.AddHostedService<Worker>();
var host = builder.Build();
host.Run();
