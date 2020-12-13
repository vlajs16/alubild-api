using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SeedData;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AlubildContext>();
                    var roleManager = services.GetRequiredService<RoleManager<Role>>();

                    context.Database.Migrate();

                    Seed.SeedRoles(roleManager);
                    Seed.SeedCategories(context);
                    Seed.SeedColors(context);
                    Seed.SeedManufacturers(context);
                    Seed.SeedQualities(context);
                    Seed.SeedGlassQualities(context);
                    Seed.SeedGuides(context);
                    Seed.SeedTabakera(context);
                    Seed.SeedTypologies(context);
                    Seed.SeedTypologyModels(context);
                    Seed.SeedGlassPackages(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
