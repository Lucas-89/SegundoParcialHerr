using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SegundoParcialHerr.Data;
using SegundoParcialHerr.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AutorContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AutorContext") ?? throw new InvalidOperationException("Connection string 'AutorContext' not found.")));

// Add services to the container.
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true) // aca pide el mail de confirmacion
    .AddRoles<IdentityRole>() //sumo esta parte
    .AddEntityFrameworkStores<AutorContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<ILibroService, LibroService>();
builder.Services.AddScoped<ISucursalService, SucursalService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
