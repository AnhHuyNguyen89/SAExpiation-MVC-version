using Microsoft.EntityFrameworkCore;
using SAExpiations.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Add the Expiations DB Context
builder.Services.AddDbContext<ExpiationsContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ExpiationsContext") ??
throw new InvalidOperationException("Connection string for Expiation Context not found!")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
