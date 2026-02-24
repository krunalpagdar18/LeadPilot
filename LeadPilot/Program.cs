

using LeadPilot.Middleware;
using LeadPilot.Models;
using LeadPilot.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Net;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error() // only exceptions
    .WriteTo.File("Logs/errors-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 2)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:5000");
builder.Host.UseSerilog();
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<LeadPilotDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DBEntities")));


#region Service Layer Dependecies
builder.Services.AddScoped<SerLead>();
builder.Services.AddScoped<SerLeadSource>();
builder.Services.AddScoped<SerLeadStatus>();
builder.Services.AddScoped<SerEmail>();
builder.Services.AddScoped<SerN8n>();
#endregion

builder.Services.AddHttpClient<SerN8n>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
});
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
