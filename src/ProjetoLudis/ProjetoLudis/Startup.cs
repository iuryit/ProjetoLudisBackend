using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjetoLudis.Data;
using ProjetoLudis.Tabelas;
using ProjetoLudis.Properties;
using ProjetoLudis.Options;
using ProjetoLudis.Servicos.RotaServico;
using ProjetoLudis.Servicos.RotaPontoServico;

namespace ProjetoLudis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<RotaPontoServico>();
            services.AddScoped<RotaServico>();

            services.AddEntityFrameworkNpgsql().AddDbContext<Context>(opt =>
               opt.UseNpgsql(Configuration.GetConnectionString("MyWebApiConnection")));

              services.AddDefaultIdentity<Usuario>()              
                         .AddRoles<IdentityRole>()
                         .AddEntityFrameworkStores<Context>()
                         .AddDefaultTokenProviders();
            services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'V'VVV";
                opt.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(opt => {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
            });

            var apiProviderderDescription = services.BuildServiceProvider()
                                                    .GetService<IApiVersionDescriptionProvider>();
            services.AddSwaggerGen(x =>  {
                foreach (var description in apiProviderderDescription.ApiVersionDescriptions)
                {
                    x.SwaggerDoc(
                        description.GroupName,
                        new Microsoft.OpenApi.Models.OpenApiInfo()
                        {
                            Title = "AppLudis",
                            Version = description.ApiVersion.ToString()
                        }
                        );
                }
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header usando o esquema bearer",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                }
                    );
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {new OpenApiSecurityScheme{Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }}, new List<string>()}
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                x.IncludeXmlComments(xmlCommentsFullPath);
            });
            services.AddControllers();                    
            services.AddAuthorization(options =>
            {
                options.AddPolicy(name: "CargaViewer", configurePolicy: builder => builder.RequireClaim("carga.view", allowedValues: "true"));
                //options.AddPolicy("embarcador", policy => policy.RequireClaim("Store", "embarcador"));
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(X =>
            {
                X.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                X.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(X =>
            {
                X.RequireHttpsMetadata = false;
                X.SaveToken = true;
                X.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidoEm,
                    ValidIssuer = appSettings.Emissor
                };
            });
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IWebHostEnvironment env,
                              IApiVersionDescriptionProvider apiProviderderDescription)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

         /*   var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);*/

            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                foreach (var description in apiProviderderDescription.ApiVersionDescriptions)
                {
                    opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                opt.RoutePrefix = "";

            });

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
