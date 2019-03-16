using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CQRSExample.Application.Genres.Commands;
using CQRSExample.Application.Genres.Commands.CreateGenre;
using CQRSExample.Application.Infraestructure;
using CQRSExample.Domain.Interfaces;
using CQRSExample.Persistance;
using CQRSExample.Persistance.Repositories;
using CQRSExample.WebAPI.Filters;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CQRSExample.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add Repositories
            RegisterRepositories(typeof(Repository<>).GetTypeInfo().Assembly, services);

            // Add AutoMapper
            services.AddAutoMapper();

            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(RequestValidationBehavior<,>).GetTypeInfo().Assembly);

            //Add UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add DbContext using SQL Server Provider
            services.AddDbContext<VidlyContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("VidlyOperations")));

            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(CreateGenreCommandValidator).Assembly));

            // Customize default API behavior
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc();
        }

        private void RegisterRepositories(Assembly assembly, IServiceCollection services)
        {

            var repositories = assembly.GetTypes().Where(type =>
                type.GetTypeInfo().IsClass &&
                !type.GetTypeInfo().IsAbstract &&
                type.Name.EndsWith("Repository"));

            foreach (var repository in repositories)
            {
                var interfaces = repository.GetInterfaces();
                var mainInterfaces = interfaces.Except
                        (interfaces.SelectMany(t => t.GetInterfaces()));
                foreach (var @interface in mainInterfaces)
                {
                    services.AddScoped(@interface, repository);
                }
            }
        }
    }
}
