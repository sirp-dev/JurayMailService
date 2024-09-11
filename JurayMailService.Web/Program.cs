using Domain.Models;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Extensions;
using PostmarkEmailService;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using JurayMailService.Web.Background;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AppConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddApplicationCustomServices();



builder.Services.AddTransient<PostmarkClient>(_ => new PostmarkClient(builder.Configuration.GetSection("PostmarkSettings")["ServerToken"]));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// Configure Identity
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDBContext>()
    .AddDefaultTokenProviders();
builder.Services.AddRazorPages();
//builder.Services.AddHostedService<BackgroundServiceJob>();
builder.Services.AddHostedService<TimerService>();

//builder.Services.AddSingleton<BackgroundServiceJob>();
//builder.Services.AddHostedService<BackgroundServiceJob>(provider => provider.GetService<BackgroundServiceJob>());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
