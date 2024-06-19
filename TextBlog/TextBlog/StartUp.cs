using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

public class StartUp
{
    public StartUp(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Добавьте доступ к конфигурации
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();

        // Добавление контекста данных с PostgreSQL
        services.AddDbContext<BlogContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Настройка Identity для использования класса User и типа ключа int
        services.AddIdentity<UserModel, IdentityRole<int>>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            // Другие настройки Identity, если необходимо
        })
    .AddEntityFrameworkStores<BlogContext>();

        // Настройка поддержки Razor Pages
        services.AddRazorPages();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage(); // Страница ошибок для разработки
        }
        else
        {
            app.UseExceptionHandler("/Error"); // Обработка ошибок в продакшене
            app.UseHsts(); // Использование HTTP Strict Transport Security
        }

        app.UseHttpsRedirection(); // Перенаправление HTTP запросов на HTTPS
        app.UseStaticFiles(); // Использование статических файлов (css, js, картинки)

        app.UseRouting(); // Установка маршрутизации

        app.UseAuthentication(); // Использование аутентификации
        app.UseAuthorization(); // Использование авторизации

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages(); // Настройка маршрутизации для Razor Pages
        });
    }

}
