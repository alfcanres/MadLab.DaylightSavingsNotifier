using DSTN.AdminApp.WinForms.Properties;
using DSTN.AdminApp.WinForms.Repository.TimeZoneConfigurator;
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
            

            services.AddHttpClient(Settings.Default.ClientName, client =>
            {
                client.BaseAddress = new Uri(Settings.Default.BaseAddress);
            });

            services.AddTransient<ITimeZoneConfiguratorService, TimeZoneConfiguratorService>();

 
            var serviceProvider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();
            System.Windows.Forms.Application.Run(new FrmMain(serviceProvider));
        }
    }
}