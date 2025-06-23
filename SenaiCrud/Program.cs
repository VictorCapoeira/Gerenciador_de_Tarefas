using Microsoft.EntityFrameworkCore;
using SenaiCrud.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configura o DbContext com a connection string do appsettings.json
builder.Services.AddDbContext<SenaiCrudDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("SaborBrasilConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SaborBrasilConnection"))
    )
);

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

// Garante que a rota padrão abre o Index do HomeController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
