using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Superdigital.DataBase;
using Superdigital.FakeDatabases;
using Superdigital.Handlers;
using Superdigital.Handlers.Account;
using Swashbuckle.AspNetCore.Swagger;

namespace Superdigital.Api
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
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })

                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Projeto para teste na SuperDigital",
                    Description = "Projeto para teste na SuperDigital",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Victor Silva de Oliveira", Email = "victor.s.o@outlook.com.com", Url = "github.com" }
                });
            });

            services.AddScoped<IDataBase<Models.AccountDetail>, AccountsFakeDatabase>();
            services.AddScoped<IDataBase<Models.Operation>, OperationFakeDateBase>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountHandler, AccountHandler>();
            services.AddScoped<TransferHandler>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto para teste na SuperDigital");
            });


        }
    }
}
