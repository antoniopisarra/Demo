using Demo.Api.Configuration;
using Demo.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace Demo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                //Documentazione
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Demo ASP.NET 8 Web API",
                    Description = "Implementazione dell'autenticazione JWT"
                });

                //Abilitazione di Swagger all'uso di JWT
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Aggiungo Database
            builder.Services.AddDbContext<DemoDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnessioneSqlServer")));

            //Configurazione Logger con scrittura errori su Seq
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .Enrich.WithProperty("Applicazione", "ServerRestAPI")
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
            builder.Host.UseSerilog();

            //Sezione per aggiungere i Services per acesso al database
            builder.Services.DataServices();

            //Configurazione JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
            });

            //Gestione dei Ruoli
            builder.Services.AddAuthorization();

            //Gestione degli utenti collegati
            builder.Services.AddHttpContextAccessor();

            //Registrazione della classe per la creazione dei Bearer Token
            builder.Services.AddSingleton<JwtService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}