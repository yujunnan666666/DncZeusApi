/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using AutoMapper;
using DncZeus.Api.Auth;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.SF;
using DncZeus.Api.Extensions.AuthContext;
using DncZeus.Api.Extensions.CustomException;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace DncZeus.Api
{
    public class Startup
    {
        //private IApiVersionDescriptionProvider provider;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o =>
                o.AddPolicy("*",
                    builder => builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials()
                ));
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppAuthenticationSettings>(appSettingsSection);
            // JWT
            var appSettings = appSettingsSection.Get<AppAuthenticationSettings>();
            services.AddJwtBearerAuthentication(appSettings);
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddAutoMapper();

            //session
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
               /* options.CheckConsentNeeded = context => false;*/
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //解除文件大小
            services.Configure<FormOptions>(x =>
            {
                
                x.MultipartBodyLengthLimit = 60000000;
                
            });
            



            services.Configure<WebEncoderOptions>(options =>
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
            );

            /////////////////////////////////////////////////////////////////////////////////////////
            ///add by ltb 2019-10-07  
            ///多版本控制
            services.AddApiVersioning(option =>
            {
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = false;
            })
            .AddMvcCore().AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVV";
                option.AssumeDefaultVersionWhenUnspecified = true;
            });
            /////////////////////////////////////////////////////////////////////////////////////////

            services
                .AddMvc(config =>
                {
                    //config.Filters.Add(new ValidateModelAttribute());
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<DncZeusDbContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                // 如果使用SQL Server 2008数据库，请添加UseRowNumberForPaging的选项
                // 参考资料:https://github.com/aspnet/EntityFrameworkCore/issues/4616
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),b=>b.UseRowNumberForPaging())
                );
            services.AddDbContext<SFAuthDbContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                // 如果使用SQL Server 2008数据库，请添加UseRowNumberForPaging的选项
                // 参考资料:https://github.com/aspnet/EntityFrameworkCore/issues/4616
                options.UseSqlServer(Configuration.GetConnectionString("SFConnection_Test"), b => b.UseRowNumberForPaging())
                );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "RBAC Management System API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            /////////////////////////////////////////////////////////////////////////////////////////
            ///add by ltb 2019-10-07
            ///增加V3版本
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v3", new Info { Title = "TECH Management System API", Version = "v3" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v4", new Info { Title = "SF Management System API", Version = "v4" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            ///增加V5版本
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v5", new Info { Title = "DEVELOP Management System API", Version = "v5" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            ///增加V6版本
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v6", new Info { Title = "Pmc  System API", Version = "v6" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            ///增加V7版本
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v7", new Info { Title = "Weixin  System API", Version = "v7" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            ///增加V8版本
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v8", new Info { Title = "Finance  System API", Version = "v8" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            /////////////////////////////////////////////////////////////////////////////////////////

            // 注入日志
            services.AddLogging(config => 
            {
                config.AddLog4Net();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            app.UseDeveloperExceptionPage();
            //app.UseExceptionHandler("/error/500");
            //app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();
            //session
            app.UseSession();
            app.UseFileServer();
            app.UseAuthentication();
            app.UseCors("*");
            app.ConfigureCustomExceptionMiddleware();
            
            var serviceProvider = app.ApplicationServices;
            var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            AuthContextService.Configure(httpContextAccessor);

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                     name: "areaRoute",
                     template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "apiDefault",
                    template: "api/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger(o =>
            {
                o.PreSerializeFilters.Add((document, request) =>
                {
                    document.Paths = document.Paths.ToDictionary(p => p.Key.ToLowerInvariant(), p => p.Value);
                });
            });
            app.UseSwaggerUI(c =>
            {
                
                c.SwaggerEndpoint("/swagger/v3/swagger.json", "TECH API V3"); //add by ltb 2019-10-07 增加V2版本
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RBAC API V1");
                c.SwaggerEndpoint("/swagger/v4/swagger.json", "Sf API V4");
                c.SwaggerEndpoint("/swagger/v5/swagger.json", "Develop API V5");
                c.SwaggerEndpoint("/swagger/v6/swagger.json", "Pmc API V6");
                c.SwaggerEndpoint("/swagger/v7/swagger.json", "Weixin API V7");
                c.SwaggerEndpoint("/swagger/v8/swagger.json", "Finance API V8");
                //c.RoutePrefix = "";
            });

        }
    }
}
