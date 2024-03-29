﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Platformservice.Models;

namespace Platformservice.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                   SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(),isProd);         
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("---> Migrating to production");

                try
                {
                    context.Database.Migrate();
                }
                catch(Exception ex) 
                { 
                    Console.WriteLine($"---> Could not run Migrations {ex.Message}");
                }
              
                
            }
            if(!context.Platforms.Any()) 
            {
                Console.WriteLine("---> Seeding Data");
                context.Platforms.AddRange(
                        new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                        new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                        new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                );
                context.SaveChanges();

            }
            else
            {
                Console.WriteLine("-----> we already have data");
            }

        }
    }
}
