using System.Text;
using Demo.DataAccess;
using Demo.DataServices.Implementation;
using Demo.DataServices.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

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
            builder.Services.AddSwaggerGen();

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
            builder.Services.AddScoped<IUtenteDataServices, UtenteDataServices>();
            builder.Services.AddScoped<IRuoloDataServices, RuoloDataServices>();

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