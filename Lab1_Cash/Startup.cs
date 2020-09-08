using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lab1_Cash
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("To login enter card and type pin(in serach line: /login?pin=****) hint: pin is 1234");
                });
            });

            app.Map("/login", Index);
            app.Map("/cash", Cash);


        }

        private static void Index(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                if (context.Request.Query.ContainsKey("pin") && context.Request.Query["pin"] == "1234")
                {
                    await context.Response.WriteAsync("you are logged in! " +
                        "\n to see cash - after host enter /cash/option=1" +
                        "\n to task cash - after host enter /cash/option=2" +
                        "\n to insert cash - after host enter /cash/option=3");
                }
                else
                {
                    await context.Response.WriteAsync("invalid pin! try again");
                }
            });
        }

        private static void Cash(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                if (context.Request.Query.ContainsKey("pin") && context.Request.Query["pin"] == "1234")
                {
                    await context.Response.WriteAsync("you are logged in! " +
                        "\n to see cash - after host enter /cash/option=1" +
                        "\n to task cash - after host enter /cash/option=2" +
                        "\n to insert cash - after host enter /cash/option=3");
                }
                else
                {
                    await context.Response.WriteAsync("invalid pin! try again");
                }
            });
        }
    }
}
