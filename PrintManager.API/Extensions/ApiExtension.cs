﻿using Microsoft.OpenApi.Models;
using PrintManager.API.Middlewares;
using PrintManager.Application.Filters.Installation;
using PrintManager.Application.Filters.Branch;
using PrintManager.Application.Interfaces;
using PrintManager.Application.Services;
using PrintManager.Logic.Stores;
using PrintManager.Persistence.Repositories;
using PrintManager.Application.Filters.Printer;
using PrintManager.Application.Filters.Job;
using PrintManager.Application.Filters.Employee;

namespace PrintManager.API.Extensions;

/// <summary>
/// Расширения для настройки сервисов приложения.
/// </summary>
public static class ApiExtension
{
    /// <summary>
    /// Добавление сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IConnectionTypeService, ConnectionTypeService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IInstallationService, InstallationService>();
        services.AddScoped<IPrinterService, PrinterService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<ICSVService, CSVService>();

        return services;
    }

    /// <summary>
    /// Добавление обработчика исключений.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
    {
        services.AddTransient<GlobalExceptionHandlingMiddleware>();

        return services;
    }

    /// <summary>
    /// Добавление репозиториев.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBranchStore, BranchRepository>();
        services.AddScoped<IConnectionTypeStore, ConnectionTypeRepository>();
        services.AddScoped<IEmployeeStore, EmployeeRepository>();
        services.AddScoped<IInstallationStore, InstallationRepository>();
        services.AddScoped<IPrinterStore, PrinterRepository>();
        services.AddScoped<IJobStore, JobRepository>();
        services.AddScoped<IStatusStore, StatusRepository>();

        return services;
    }

    /// <summary>
    /// Добавление сервисов кэширования.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    public static IServiceCollection AddCacheServices(this IServiceCollection services)
    {
        services.AddSingleton<IInstallationMemoryCache, InstallationMemoryCache>();

        return services;
    }

    /// <summary>
    /// Добавление фильтров.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        services.AddScoped<Installation_ValidateInstallationIdFilterAttribute>();
        services.AddScoped<Branch_ValidateBranchIdFilterAttribute>();
        services.AddScoped<Printer_ValidatePrinterIdFilterAttribute>();
        services.AddScoped<Job_ValidateJobIdFilterAttribute>();
        services.AddScoped<Job_ValidateFileCountFilterAttribute>();
        services.AddScoped<Employee_ValidateEmployeeIdFilterAttribute>();

        return services;
    }

    /// <summary>
    /// Добавление документации Swagger.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    public static IServiceCollection AddDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            var basePath = AppContext.BaseDirectory;

            var xmlPath = Path.Combine(basePath, "PrintManagerAPI.xml");

            c.IncludeXmlComments(xmlPath);

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "REST API PRINT MANAGER",
                Version = "v1",
                Description = "REST, CORS, Routing",
                Contact = new OpenApiContact
                {
                    Name = "Aleksander Felyugin"
                },
                License = new OpenApiLicense
                {
                    Name = "Лицензия",
                }
            });
        });

        return services;
    }
}
