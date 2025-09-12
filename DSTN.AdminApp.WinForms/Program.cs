using DSTN.Application.Services.TimeZoneConfigurator;
using DSTN.Application.Services.TimeZoneNotifier;
using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;
using DSTN.Infrastructure.Persistence;
using DSTN.Infrastructure.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;





namespace DSTN.AdminApp.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();

            // Add logging to console
            services.AddLogging();



            // Register AppDbContext with SQLite
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(@"Data Source=Database\app.db"));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IQueryBuilder<ObservedTimeZone>, QueryBuilder<ObservedTimeZone>>();
            services.AddTransient<IQueryBuilder<Notification>, QueryBuilder<Notification>>();

            services.AddTransient<ITimeZoneConfiguratorService, TimeZoneConfiguratorService>();
            services.AddTransient<ITimeZoneNotifierService, TimeZoneNotifierService>();


            var serviceProvider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();
            System.Windows.Forms.Application.Run(new FrmMain(serviceProvider));
        }
    }
}