using System;
using System.IO;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using TennisApi.Config;
using TennisApi.Filters.Exceptions;
using TennisApi.Filters.Validators;
using TennisApi.Service.JWT;
using TennisApi.Shared.Configs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TennisApi.Interactors;
using TennisApi.Interactors.Auth;
using TennisApi.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace TennisApi
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new ApiExceptionFilter());
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;
            });

            //cors
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy
                        //.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(_ => true)
                        .AllowCredentials();
                });
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen( c=> {
                c.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My Api", Version = "v1" });
                c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey
                //});

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
             {
                 var oidcAuth = Configuration.GetSection("configs");
                 var secret = Encoding.ASCII.GetBytes(oidcAuth.GetValue<string>("clientSecret"));

                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(secret),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });

            // DataBase Context
            services.AddDbContext<TennisDBContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:main_database"]);
            }, ServiceLifetime.Transient);
            services.AddDbContext<TennisDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:main_database"]), ServiceLifetime.Transient);


            // Main Configurations
            services.Configure<AuthConfig>(Configuration.GetSection("configs"));


            // MediatR
            services.AddMediatR(typeof(LoginHandler));
            services.AddMediatR(typeof(Startup));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));


            //autofac registrations
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacInjectBootStrapper>();
            NativeInjectBootStrapper.RegisterServices(services);
            builder.Populate(services);
            var container = builder.Build();

            //ContextProvider.Setup(container.Resolve<IComponentContext>());
            //MyAssist.Setup();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
                c.RouteTemplate = "docs/{documentName}/docs.json";
            });
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "docs";
                c.SwaggerEndpoint("/docs/v1/docs.json", "API");
                c.InjectStylesheet("/swagger-ui/custom.css");
                c.InjectJavascript("/swagger-ui/custom.js");
            });

            app.UseCors("default");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
