using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Kuaffy.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Kuaffy.DataAccess.Abstract;
using Kuaffy.DataAccess.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<KuaffyContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<KuaffyContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSingleton<IAppointmentDal, AppointmentDal>();
builder.Services.AddSingleton<ICommentDal, CommentDal>();
builder.Services.AddSingleton<ICompanyDal, CompanyDal>();
builder.Services.AddSingleton<ICompanyTypeDal, CompanyTypeDal>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
   .AddEntityFrameworkStores<KuaffyContext>().AddDefaultUI();
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
builder.Services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
builder.Services.AddRazorPages().AddSessionStateTempDataProvider();
//builder.Services.AddDbContext<KuaffyDataContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("KuaffyDataContext") ?? throw new InvalidOperationException("Connection string 'KuaffyDataContext' not found.")));

builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(200);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(200);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new("tr");

    CultureInfo[] cultures = new CultureInfo[]
    {
        new("tr"),
        new("en"),
       
    };

    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
var app = builder.Build();

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
app.UseDatabaseErrorPage();

app.UseHttpsRedirection();
app.UseStaticFiles();
var cultures = new List<CultureInfo> {
    new CultureInfo("en"),
    new CultureInfo("tr")
};
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
