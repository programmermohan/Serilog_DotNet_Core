using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace SerilogExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log. Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                //Log.Information("Application Started"); don't want to store application started so commented, we can uncomment if we want to log
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "application failed to start");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }

            #region Commented....

            //ConfigureLogger();
            //Log.Information(messageTemplate: "Application Started");
            //try
            //{
            //    CreateHostBuilder(args).Build().Run();
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}

            #endregion  Commented....
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseSerilog();
                });

        //public static void ConfigureLogger()
        //{
        //    Log.Logger = new LoggerConfiguration()
        //        .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-mm-dd} {MachineName} {ThreadId} {Message} {Exception:1} {NewLine}")
        //        .WriteTo.File(path: @"log.txt", outputTemplate: "{Timestamp:yyyy-mm-dd} {MachineName} {ThreadId} {Message} {Exception:1} {NewLine}")
        //        .Enrich.WithThreadId()
        //        .Enrich.WithThreadName()
        //        .CreateLogger();
        //}
    }
}
