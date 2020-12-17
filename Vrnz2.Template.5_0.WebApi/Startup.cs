using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Vrnz2.BaseInfra.Assemblies;
using Vrnz2.BaseInfra.Logs;
using Vrnz2.BaseInfra.ServiceCollection;
using Vrnz2.BaseInfra.Settings;
using Vrnz2.BaseInfra.Validations;
using Vrnz2.BaseWebApi.Helpers;
using Vrnz2.BaseWebApi.Validations;
using Vrnz2.Template._5_0.WebApi.Settings;

namespace Vrnz2.Template._5_0.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
                .AddFluentValidation();
            
            services
                .AddSettings<AppSettings>()
                .AddLogs()
                .AddAutoMapper(AssembliesHelper.GetAssemblies())
                .AddMediatR(AssembliesHelper.GetAssemblies<ValidationHelper>())
                .AddIServiceColletion()
                .AddBaseValidations()
                .AddValidation<BaseContracts.DTOs.Ping.Request, PingRequestValidator>()
                .AddScoped<ControllerHelper>()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vrnz2.Template._5_0.WebApi", Version = "v1" });
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vrnz2.Template._5_0.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
