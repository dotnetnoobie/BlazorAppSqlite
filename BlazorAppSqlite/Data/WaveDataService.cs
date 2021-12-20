using BlazorAppSqlite.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppSqlite.Data
{
    public static class WaveDataService
    {

        public static void AddWaveDataDbContext(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContextFactory<ClientSideDbContext>(
                options => options.UseSqlite($"Filename={DataSynchronizer.SqliteDbFilename}"));
            serviceCollection.AddScoped<DataSynchronizer>();
        }
    }
}
