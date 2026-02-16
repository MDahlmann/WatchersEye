using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PoeLeagueTracker.Client;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration["BackendApiUrl"];

builder.Services.AddHttpClient("PoeTrackerApi", client =>
{
    client.BaseAddress = new Uri(apiUrl!);
});

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("PoeTrackerApi"));

builder.Services.AddRadzenComponents();

await builder.Build().RunAsync();