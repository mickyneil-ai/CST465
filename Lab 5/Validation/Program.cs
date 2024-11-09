var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // Catches unhandled exceptions and generates a 500 status code
    app.UseExceptionHandler("/Error");

    // Handles other status codes, such as 404
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}

app.UseStaticFiles();
app.MapControllers();

app.Run();
