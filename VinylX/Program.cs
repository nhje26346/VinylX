using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VinylX.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using VinylX.Models;
using VinylX.DiscogsImport;
using System.Xml;

ImportHelper helper = new ImportHelper();
helper.SplitXml("C:\\Temp\\Discogs\\discogs_20240201_labels.xml", "labels","label",100000,"C:\\Temp\\Discogs\\SplitOutput");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VinylXContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VinylXContext") ?? throw new InvalidOperationException("Connection string 'VinylXContext' not found.")));

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())  //TODO Remove 24-28
{
    var services = scope.ServiceProvider;
    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
