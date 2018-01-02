using CodingMilitia.AngularAspNetCoreDockerSample.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CodingMilitia.AngularAspNetCoreDockerSample.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            //seed database
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    using (var db = services.GetRequiredService<CounterContext>())
                    {
                        db.Database.EnsureDeleted();
                        db.Database.EnsureCreated();
                        db.Counters.Add(new Data.Models.Counter { Name = "AngularAspNetCoreDockerSampleCounter", Value = 0 });
                        db.Counters.Add(new Data.Models.Counter { Name = "AngularAspNetCoreDockerSampleCounter2", Value = 100 });
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
