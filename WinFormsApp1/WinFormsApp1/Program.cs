using ElectronicsStore.Data;
using ElectronicsStore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ElectronicsStore;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        try
        {
            var logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "ElectronicsStore", "Logs", "store-.log");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .AddInMemoryCollection(new Dictionary<string, string?> { ["Serilog:WriteTo:0:Args:path"] = logPath })
                .Build();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            using var services = DependencyContainer.Configure(Log.Logger);
            using (var context = services.GetRequiredService<IDbContextFactory<DataContext>>().CreateDbContext())
                context.Database.Migrate();
            using var form = services.GetRequiredService<MainForm>();
            Application.Run(form);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Не удалось запустить приложение");
            MessageBox.Show("Не удалось запустить магазин. Проверьте настройки PostgreSQL и переменную " +
                "ELECTRONICS_STORE_CONNECTION. Подробности: %LOCALAPPDATA%\\ElectronicsStore\\Logs.",
                "Магазин электроники", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
