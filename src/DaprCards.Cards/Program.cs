using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Actors.AspNetCore;
using DaprCards.Cards.Actors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DaprCards.Cards
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .Configure(
                            app =>
                            {
                                app.Use(
                                    (context, next) =>
                                    {
                                        //Do some work here
                                        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                                        //Pass the request on down to the next pipeline (Which is the MVC middleware)
                                        return next();
                                    });
                            }
                        )
                        .UseStartup<Startup>()
                        .UseActors(
                            runtime =>
                            {
                                runtime.RegisterActor<CardActor>();
                            })
                        .UseUrls("http://localhost:5000/");
                });
    }
}
