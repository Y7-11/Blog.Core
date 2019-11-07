using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Blog.Core
{
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",new Info
                {
                    Version = "v0.1.0",
                    Title = "Blog.Core API",
                    Description = "框架说明文档"
                });
                var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Blog.Core.xml");
                c.IncludeXmlComments(xmlPath,true);//默认的第二个参数是false，这个是controller的注释，记得修改

                var xmlModelPath = Path.Combine(basePath, "Blog.Core.Model.xml");
                c.IncludeXmlComments(xmlModelPath);
            });           

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                #region Swagger

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                    c.RoutePrefix = "";//路径配置，设置为空，表示直接访问该文件，
                    //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，
                    //这个时候去launchSettings.json中把"launchUrl": "swagger/index.html"去掉， 然后直接访问localhost:8001/index.html即可
                });

                #endregion
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseMvc();
        }
    }
}
