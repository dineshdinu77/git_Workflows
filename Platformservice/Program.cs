using Microsoft.EntityFrameworkCore;
using Platformservice.SyncDataServices.HTTP;
using Platformservice.Data;
using Platformservice.AsyncDataServices;

namespace Platformservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            /*
                        ConfigurationManager configuration = builder.Configuration;*/

            if (builder.Environment.IsProduction())
            {
                builder.Services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
            }
            Console.WriteLine("--->using InMem");
            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseInMemoryDatabase("InMem"));


            // Add services to the container.


            builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

            builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            Console.WriteLine($"--->CommandService end point {builder.Configuration["CommandService"]}");

            var app = builder.Build();

            // Configure the HTTP request pipeline.

         
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }
            PrepDb.PrepPopulation(app, app.Environment.IsProduction());

            /* app.UseHttpsRedirection();*/

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
