using Microsoft.OpenApi.Models;
using PrintManager.API.Middlewares;
using PrintManager.Applpication.Interfaces;
using PrintManager.Applpication.Services;
using PrintManager.Logic.Stores;
using PrintManager.Persistence.Repositories;

namespace PrintManager.API.Extensions
{
    public static class ApiExtension
    {
        public static IServiceCollection AddDomains(this IServiceCollection services)
        {
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IConnectionTypeService, ConnectionTypeService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IInstallationService, InstallationService>();
            services.AddScoped<IPrinterService, PrinterService>();
            services.AddScoped<IPrintJobNameService, PrintJobNameService>();
            services.AddScoped<IPrintSessionService, PrintSessionService>();
            services.AddScoped<IStatusService, StatusService>();

            return services;
        }

        public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandlingMiddleware>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBranchStore, BranchRepository>();
            services.AddScoped<IConnectionTypeStore, ConnectionTypeRepository>();
            services.AddScoped<IEmployeeStore, EmployeeRepository>();
            services.AddScoped<IInstallationStore, InstallationRepository>();
            services.AddScoped<IPrinterStore, PrinterRepository>();
            services.AddScoped<IPrintJobNameStore, PrintJobNameRepository>();
            services.AddScoped<IPrintSessionStore, PrintSessionRepository>();
            services.AddScoped<IStatusStore, StatusRepository>();

            return services;
        }

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
}
