using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Start2.Client;
using Start2.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var apiUrl = builder.Configuration.GetValue<string>("ApiUrl");
builder.Services.AddHttpClient("api", sp =>
{
    sp.BaseAddress = new Uri(apiUrl);
}).ConfigureHttpClient((client) =>
{
    client.DefaultRequestVersion = new Version(2, 0);
});
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<StateService>();
builder.Services.AddTransient<ApiService>();
builder.Services.AddLocalization();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
