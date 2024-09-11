using Application.Extensions;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using PostmarkEmailService;
using WorkerService;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Email Service";
});
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<PostmarkClient>(_ => new PostmarkClient(builder.Configuration.GetSection("PostmarkSettings")["ServerToken"]));
//builder.Services.AddApplicationCustomServices();
builder.Services.AddScoped<IEmailSendingStatusRepository, EmailSendingStatusRepository>();
 
var host = builder.Build();
host.Run();
