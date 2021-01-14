using Api.Models;
using Api.Security;
using Api.Services.Interfaces;
using Api.Services;
using Api.Repository.Interfaces;
using Api.Repository;

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

namespace Api
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
            // Ativando o uso de cache via Redis
            /*
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration =
                    Configuration.GetConnectionString("RedisConnection");
                options.InstanceName = "master";
            });
            */

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // ********************
            // Configuração do CORS
            // ********************

            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.
            //corsBuilder.WithOrigins("http://localhost:56573"); // for a specific url. Don't add a forward slash on the end!
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            // ********************
            // Configuração do escopo
            // ********************

            // Base de dados
            services.AddDbContext<DbProdContext>(options => options.UseSqlServer(Configuration.GetConnectionString("baseProducao")));

            // Serviços e seus repositórios

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IDocumentosService, DocumentosService>();
            services.AddScoped<IDocumentosRepository, DocumentosRepository>();

            services.AddScoped<IEmailsService, EmailsService>();
            services.AddScoped<IEmailsRepository, EmailsRepository>();

            services.AddScoped<ILoginService, LoginService>();

            services.AddScoped<IPessoasService, PessoasService>();
            services.AddScoped<IPessoasRepository, PessoasRepository>();

            services.AddScoped<ISenhasService, SenhasService>();
            services.AddScoped<ISenhasRepository, SenhasRepository>();

            // ********************
            // Configuração da autenticação
            // ********************

            // Instancia a configuração de chave RSA e registra o singleton dos services
            var signingConfiguration = new SigningConfiguration();
            services.AddSingleton(signingConfiguration);

            // Instancia e busca a configuração de tokens
            var tokenConfiguration = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfigurations")
                )
                .Configure(tokenConfiguration);

            // Salva o singleton dos tokens
            services.AddSingleton(tokenConfiguration);

            // Configura a autenticação
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = signingConfiguration.Key,
                    ValidAudience = tokenConfiguration.Audience,
                    ValidIssuer = tokenConfiguration.Issuer,
                    // Tolerance time for the expiration of a token (used in case
                    // of time synchronization problems between different
                    // computers involved in the communication process)
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Evita com que os models entrem em loop
            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            // Permite o uso do token como meio de autorização de acesso aos recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build()
                );
            });

            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "Back-end padrão para gerenciamento de requisições HTTP.",
                    Contact = new OpenApiContact
                    {
                        Name = "Desenvolvedor: Sergio Martins",
                        Url = new Uri("https://www.linkedin.com/in/sergioramosm/")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Autorização JWT com Bearer scheme." +
                    "\r\n\r\nInsira \"Bearer \" mais o seu token no campo abaixo." +
                    "\r\n\r\nExemplo: \"Bearer 12345abcdef\"",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            if (env.IsProduction())
            {
                app.UseHttpsRedirection();
            }

            app.UseCors("SiteCorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Enable Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                c.DefaultModelsExpandDepth(-1);
            });

            //Starting our API in Swagger page
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);
        }
    }
}
