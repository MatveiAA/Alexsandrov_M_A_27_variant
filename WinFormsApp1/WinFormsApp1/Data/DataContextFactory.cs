using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ElectronicsStore.Data;

public sealed class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseNpgsql(DataContext.GetConnectionString())
            .Options;
        return new DataContext(options);
    }
}
