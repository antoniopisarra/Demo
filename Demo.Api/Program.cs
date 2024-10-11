using Demo.DataAccess;
using Demo.DataServices.Implementation;
using Demo.DataServices.Interface;
using Demo.Model.AutoMapper;
using Microsoft.EntityFrameworkCore;
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

            //Configurazione di AutoMapper indicazione di dove trovare i profili
            builder.Services.AddAutoMapper(typeof(UtenteProfile));

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}