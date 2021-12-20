using BlazorAppSqlite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Runtime.InteropServices;

namespace BlazorAppSqlite.Data;

// This service synchronizes the Sqlite DB with both the backend server and the browser's IndexedDb storage
class DataSynchronizer
{
    public const string SqliteDbFilename = "app.db";
    private readonly Task firstTimeSetupTask;
    private readonly IDbContextFactory<ClientSideDbContext> dbContextFactory;

    public DataSynchronizer(IJSRuntime js, IDbContextFactory<ClientSideDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
        firstTimeSetupTask = FirstTimeSetupAsync(js);
    }

    public async Task<ClientSideDbContext> GetPreparedDbContextAsync()
    {
        await firstTimeSetupTask;
        return await dbContextFactory.CreateDbContextAsync();
    }

    private async Task FirstTimeSetupAsync(IJSRuntime js)
    {
        //This JS file constantly monitors the MEMFS file and synchronize our sqlite db with a indexedDB stored file
        var module = await js.InvokeAsync<IJSObjectReference>("import", "./dbstorage.js");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("browser")))
        {
            await module.InvokeVoidAsync("synchronizeFileWithIndexedDb", SqliteDbFilename);
        }

        using var db = await dbContextFactory.CreateDbContextAsync();
        await db.Database.EnsureCreatedAsync();
    }
}
