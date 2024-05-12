using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using PrintManager.API.Extensions;
using PrintManager.API.Middlewares;
using PrintManager.Persistence;
using PrintManager.Persistence.Mappings;
using System.Globalization;

namespace PrintManager.API;

/// <summary>
/// Класс для конфигурации приложения при запуске.
/// </summary>
public class Startup(IConfiguration configuration)
{
    /// <summary>
    /// Метод для конфигурации сервисов приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");

        services.AddControllers().AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.Converters.Add(new StringEnumConverter());
        });

        services.AddSwaggerGenNewtonsoftSupport();
        services.AddDocumentation();

        services.AddEndpointsApiExplorer();
        services.AddLogging();
        services.AddExceptionHandler();

        services.AddCacheServices();
        services.AddServices();
        services.AddRepositories();
        services.AddFilters();

        services.AddMemoryCache();

        services.AddAutoMapper(typeof(DbMappingProfile));

        services.AddDbContext<PrintingManagementContext>(
            options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(PrintingManagementContext)));
            });
    }

    /// <summary>
    /// Метод для настройки конвейера запросов HTTP.
    /// </summary>
    /// <param name="app">Построитель приложения.</param>
    /// <param name="env">Среда выполнения приложения.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        CultureInfo[] supportedCultures =
        [
            new CultureInfo("en"),
        ];

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("ru"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCookiePolicy(new()
        {
            MinimumSameSitePolicy = SameSiteMode.Strict,
            HttpOnly = HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.Always
        });

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}