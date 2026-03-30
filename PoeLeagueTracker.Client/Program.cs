using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PoeLeagueTracker.Client;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        var backendApiUrl = builder.Configuration["BackendApiUrl"];

        Uri apiBaseAddress;

        if (!string.IsNullOrEmpty(backendApiUrl))
        {
            apiBaseAddress = new Uri(backendApiUrl);
        }
        else
        {
            apiBaseAddress = new Uri(new Uri(builder.HostEnvironment.BaseAddress), "api/");
        }

        builder.Services.AddHttpClient("PoeTrackerApi", client =>
            client.BaseAddress = apiBaseAddress)
            .AddStandardResilienceHandler();

        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("PoeTrackerApi"));

        await builder.Build().RunAsync();
    }
}