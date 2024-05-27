using Blazored.LocalStorage;
using HiiddenVilla_Client;
using HiiddenVilla_Client.Service;
using HiiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();


builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
builder.Services.AddScoped<IRoomOrderDetailsService, RoomOrderDetailsService>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
await builder.Build().RunAsync();
