using Lab_9.Areas.Identity.Data;
using Lab_9.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddDbContext<Lab_9Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Lab_9ContextConnection")));

builder.Services.AddDefaultIdentity<Lab_9User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<Lab_9Context>();

builder.Services.AddRazorPages();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
