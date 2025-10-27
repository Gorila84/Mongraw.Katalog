using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mongraw.Katalog.Domain.Models;
using Mongraw.Katalog.Web.Data;
using Mongraw.Katalog.Web.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.Configure<AppConfig>(builder.Configuration);

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.RegisterGenericMethods();
builder.Services.RegisterFluentValidators();
builder.Services.MappingRegister();

#if DEBUG
builder.Services.AddSwaggerGen();
#endif

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    //.WriteTo.File("Logs/log-.txt",
    //    rollingInterval: RollingInterval.Day,
    //    rollOnFileSizeLimit: true,
    //    fileSizeLimitBytes: 3_000_000,
    //    retainedFileCountLimit: 10)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // adres Angulara
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

#if DEBUG
app.UseSwagger();
app.UseSwaggerUI();
#endif

app.UseCors("AllowAngularDev");

app.UseSerilogRequestLogging();

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

app.MapControllers();
app.MapRazorPages();

app.Run();