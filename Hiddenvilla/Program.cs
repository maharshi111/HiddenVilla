
using Business.Repository;
using Business.Repository.Irepository;
using DataAccess.Data;
using Hiddenvilla.Data;
using Hiddenvilla.Service;
using Hiddenvilla.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// In program.cs we register our service 
// @using namespace is used to provide the reference of where the file is 
//@inject actually helps to use the particular file in our code 
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
builder.Services.AddScoped<IDbInitialzer, DbInitializer>();
builder.Services.AddScoped<IHotelImageRepository, HotelImageRepository>();

builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders().AddDefaultUI();

//see if the above line does't work try this 
//Microsoft.Extensions.DependencyInjection.ServiceCollectionExtensions.AddAutoMapper(builder.Services,AppDomain.CurrentDomain.GetAssemblies());
//public void ConfigureSevices(IServiceCollection services)
//{
//	services.AddDbContext<ApplicationDbContext>;
//}

var dbInitializer = builder.Services.BuildServiceProvider().GetRequiredService<IDbInitialzer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseAuthentication(); ;
app.UseAuthorization();
dbInitializer.Initalize();
app.MapRazorPages();
app.Run();
