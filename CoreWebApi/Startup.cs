using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using LightInject;
using Business.Interface;
using Business.Service;
using Data.Repository.Interface;
using Data.Repository.Service;
using Swashbuckle.AspNetCore.Swagger;

namespace CoreWebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IProductRepsitory, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddMvc().AddControllersAsServices();

            services.AddApiVersioning(a =>
            {
                a.ReportApiVersions = true;
                a.AssumeDefaultVersionWhenUnspecified = true;
                a.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddMvc();

            services.AddSwaggerGen(a =>
            {
                a.SwaggerDoc("v1", new Info { Title = "CoreWebAPI", Version = "1.0" });

                var path = System.AppDomain.CurrentDomain.BaseDirectory + @"CoreWebApi.xml";
                a.IncludeXmlComments(path);
            });
        }

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

            app.UseHttpsRedirection();
            app.UseMvc();

            var containerOptions = new ContainerOptions { EnablePropertyInjection = false };
            var container = new ServiceContainer(containerOptions);

            IoCRegister.Configure(container);

            app.UseSwagger();
            app.UseSwaggerUI(a =>
            {
                a.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
