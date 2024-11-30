using Lab8.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Add Output Caching service
builder.Services.AddOutputCache();

builder.Services.AddMemoryCache();
builder.Services.AddTransient<IImageRepository, CachingDbImageRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

// Use Output Caching middleware
app.UseOutputCache();

app.MapControllers();

app.Run();
