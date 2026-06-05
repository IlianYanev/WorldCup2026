using Microsoft.EntityFrameworkCore;
using WorldCup2026.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Добавяме поддръжка за MVC (Controllers с Изгледи)
builder.Services.AddControllersWithViews();

// 2. РЕГИСТРИРАМЕ КОНТЕКСТА НА БАЗАТА ДАННИ - ТОВА ОПРАВЯ ГРЕШКАТА ОТ СНИМКАТА!
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Конфигуриране на HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Позволява четенето на CSS от wwwroot

app.UseRouting();

app.UseAuthorization();

// Дефиниране на пътищата (Routing)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();