using BlazorAppSqlite;
using BlazorAppSqlite.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddDbContextFactory<ClientSideDbContext>(options => options.UseSqlite($"Filename=app.db"));

var host = builder.Build() ;

var dbFactory = builder.Services.BuildServiceProvider().GetService<IDbContextFactory<ClientSideDbContext>>();

var db = dbFactory?.CreateDbContext();

var isCreated = db?.Database.EnsureCreated();

await host.RunAsync();