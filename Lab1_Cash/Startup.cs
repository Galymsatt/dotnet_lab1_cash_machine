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


            int cash = 100000;
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

            app.Map("/login", Login);
            app.Map("/options", Options);


            void Options(IApplicationBuilder app)
            {
                app.Run(async context =>
                {
                    if (context.Request.Query.ContainsKey("option")) {
                        if (context.Request.Query["option"] == "1")
                        {
                            await context.Response.WriteAsync($" Your cash: {cash}");
                        }
                        else if (context.Request.Query["option"] == "2")
                        {
                            int insertedCash = Int32.Parse(context.Request.Query["cash"]);
                            cash += insertedCash;
                            await context.Response.WriteAsync($" The transaction was successful: You entered - {insertedCash}" +
                                $"\n Now on your account(cash): {cash}");
                        }
                        else if (context.Request.Query["option"] == "3")
                        {
                            int withdrawnCash = Int32.Parse(context.Request.Query["cash"]);
                            cash -= withdrawnCash;
                            await context.Response.WriteAsync($" The transaction was successful: You have withdrawn - {withdrawnCash}" +
                                $"\n Now on your account(cash): {cash}");
                        }
                    }
                });
            }

        }

         
        private void Login(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                if (context.Request.Query.ContainsKey("pin") && context.Request.Query["pin"] == "1234")
                {
                    await context.Response.WriteAsync(" You are logged in! \n" +
                        "\n To see cash - after host enter /options?option=1" +
                        "\n To enter cash - after host enter /options?option=2&cash=****" +
                        "\n To take cash - after host enter /options?option=3&cash=****");
                }
                else
                {
                    await context.Response.WriteAsync("Invalid pin! try again");
                }
            });
        }

        
    }
}
