using CodingMilitia.AngularAspNetCoreDockerSample.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using System;

namespace CodingMilitia.AngularAspNetCoreDockerSample.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetry(
                    3,
                    (retryCount) => TimeSpan.FromSeconds(10 * retryCount),
                    (ex, span) =>
                    {
                        logger.LogWarning(ex, "Failed! Waiting {cooldownTime}", span);
                    });

            //seed database
            try
            {
                policy.Execute(() =>
                {
                    using (var scope = host.Services.CreateScope())
                    {
                        var services = scope.ServiceProvider;

                        logger.LogInformation("Seeding database...");
                        using (var db = services.GetRequiredService<CounterContext>())
                        {
                            db.Database.EnsureDeleted();
                            db.Database.EnsureCreated();
                            db.Counters.Add(new Data.Models.Counter { Name = "AngularAspNetCoreDockerSampleCounter", Value = 0 });
                            db.Counters.Add(new Data.Models.Counter { Name = "AngularAspNetCoreDockerSampleCounter2", Value = 100 });
                            db.SaveChanges();
                        }
                        logger.LogInformation("Seeding database complete.");
                    }
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                return;
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
