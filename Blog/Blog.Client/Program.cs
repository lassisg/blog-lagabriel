using Blog.Client;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

builder.Services.AddTransient<IBlogApi, BlogApiWebClient>();
builder.Services
    .AddHttpClient(
        "Api",
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));


await builder.Build().RunAsync();