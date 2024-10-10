using Demo.DataAccess;
using Demo.Model;
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



            // Aggiungo Database
            builder.Services.AddDbContext<DemoDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnessioneSqlServer")));


            //Configurazione Logger con scrittura errori su Seq
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Applicazione","ServerRestAPI")
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            builder.Host.UseSerilog();

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