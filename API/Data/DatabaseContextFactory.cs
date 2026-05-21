using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BroGarage.API.Data;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();
        if (!File.Exists(Path.Combine(basePath, "appsettings.json")))
        {
            basePath = Path.Combine(basePath, "API");
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Missing DefaultConnection connection string.");

        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlServer(connectionString, sqlServer =>
            {
                sqlServer.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                sqlServer.CommandTimeout(configuration.GetValue("CommandTimeoutInSecond", 30));
            })
            .Options;

        return new DatabaseContext(options);
    }
}
