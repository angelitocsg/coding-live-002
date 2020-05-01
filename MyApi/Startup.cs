using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApi.Hubs;

namespace MyApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(new[] { "http://localhost:3000" })
                    .AllowCredentials();
            }));

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ClickCounterHub>("clickcounter");
            });

            hostApplicationLifetime.ApplicationStarted.Register(() =>
            {
                var serviceProvider = app.ApplicationServices;
                var clickCounter = (IHubContext<ClickCounterHub>)serviceProvider.GetService(typeof(IHubContext<ClickCounterHub>));

                var timer = new System.Timers.Timer(1000);
                timer.Enabled = true;
                timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
                {
                    clickCounter.Clients.All.SendAsync("sendUptime", DateTime.Now.ToString("dddd d MMMM yyyy HH:mm:ss"));
                };
                timer.Start();
            });
        }
    }
}
