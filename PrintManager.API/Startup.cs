using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using PrintManager.API.Extensions;
using PrintManager.API.Middlewares;
using PrintManager.Persistence;
using PrintManager.Persistence.Mappings;

namespace PrintManager.API
{
    public class Startup(IConfiguration configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddSwaggerGenNewtonsoftSupport();
            services.AddDocumentation();

            services.AddEndpointsApiExplorer();
            services.AddLogging();
            services.AddExceptionHandler();

            services.AddDomains();
            services.AddRepositories();

            services.AddAutoMapper(typeof(DbMappingProfile));

            services.AddDbContext<PrintingManagementContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString(nameof(PrintingManagementContext)));
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
}