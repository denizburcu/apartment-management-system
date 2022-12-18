using ApartmentManagement.Core.Contracts;
using ApartmentManagement.Core.IUnitOfWorks;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Core.Models.Configuration;
using ApartmentManagement.Core.Repositories;
using ApartmentManagement.Infrastructure.Services;
using ApartmentManagement.Repository;
using ApartmentManagement.Repository.Repositories;
using ApartmentManagement.Repository.UnitOfWork;
using ApartmentManagement.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Hangfire
builder.Services.AddSingleton(builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
builder.Services.AddHangfire(x =>
{
    x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHangfireServer();

// DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    options =>
    {
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext)).GetName().Name);
    });
});

// Identity
builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.SignIn.RequireConfirmedPhoneNumber = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Services
builder.Services.AddScoped<ISendMailService, SendMailService>();
builder.Services.AddScoped<IApartmentCostService, ApartmentCostService>();
builder.Services.AddScoped<IApartmentService, ApartmentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Repositories
builder.Services.AddScoped<IApartmentCostRepository, ApartmentCostRepository>();
builder.Services.AddScoped <IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();

// Clients
builder.Services.AddScoped<CreditCardClientService>();


var app = builder.Build();

app.MapHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<ISendMailService>("EmailJob", x => x.SendMail(), Cron.DayInterval(1));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHangfireDashboard();

app.Run();
