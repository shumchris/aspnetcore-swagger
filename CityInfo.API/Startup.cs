using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace CityInfo.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()))
            .AddJsonOptions(o =>
            {
                if ( o.SerializerSettings.ContractResolver != null)
                {
                    var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
                    castedResolver.NamingStrategy = null;
                }
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "My City Info",
                    Version = "v1",
                    Description = "Sample ASP.NET Core Web Api for City and Points of Interest",
                    TermsOfService = "None"
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "CityInfo.API.xml");
                c.IncludeXmlComments(xmlPath);
            });

            
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();

            //Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My City Api v1");
            });

            //Add MVC middleware
            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
