using BandScope.Client.APIs;
using BandScope.Client.Handler;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

namespace BandScope.Client
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // Enables interaction with local storage of browser
            builder.Services.AddBlazoredLocalStorage();

            // Authorization and authentication
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddScoped<AuthorizationHandler>();

            builder.Services.AddHttpClient<ServerApi>("AuthorizedClient", client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7006");
                })
                .AddHttpMessageHandler<AuthorizationHandler>();

            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthorizedClient"));

            builder.Services.AddRadzenComponents();

            await builder.Build().RunAsync();
        }
    }
}
