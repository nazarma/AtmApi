namespace Atm.Api
{
    using Application.Interfaces;
    using Application.LeastNumberRequest;
    using Application.Repos;
    using Application.Services;
    using AutoMapper;
    using FluentValidation.AspNetCore;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Middleware;
    using Persistence;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(LeastItemsRequest).Assembly);
            services.AddAutoMapper(typeof(LeastItemsRequest).Assembly);
            services.AddMvc()
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<LeastItemsRequest>())
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddScoped<ICashService, CashService>();
            services.AddScoped<ILegalTenderRepo, LegalTenderRepo>();
            services.AddControllers();
            services.AddDbContext<DataContext>(opt => { opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection")); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (env.IsDevelopment())
            {
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
