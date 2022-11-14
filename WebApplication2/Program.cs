using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;





using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;


namespace WebApplication2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("app");
                try
                {

                    //await CConX.Seeds.DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);
                    //await CConX.Seeds.DefaultModules.SeedBasicModulesAsync(db);
                    logger.LogInformation("Finished Seeding Default Data in Program.cs");
                    logger.LogInformation("Application Starting");
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "An error occurred seeding the DB");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var isDevelopment = environment == Environments.Development;
                    if (isDevelopment)
                    {
                        try
                        {
                            // if (System.IO.Directory.Exists("Y:\\"))
                            webBuilder.UseWebRoot("Y:\\");
                        }
                        catch (Exception ex)
                        {
                            //webBuilder.UseWebRoot(Environment.CurrentDirectory); 
                            webBuilder.UseWebRoot("Y:\\");
                        }
                    }
                    else
                    {
                        webBuilder.UseWebRoot("C:\\mounts\\cConXFiles\\");
                    }
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseIIS();
                });
    }
}
