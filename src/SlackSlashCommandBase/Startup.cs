using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SlackSlashCommandBase.Models.Slack;
using SlackSlashCommandBase.Services.Slack;

namespace SlackSlashCommandBase
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
            services.Configure<SlackSettings>(Configuration.GetSection("Slack"));
            services.AddScoped<IHashService, HmacSha256HashService>();
            services.AddScoped<ISignatureValidationService, SlackSignatureValidationService>();
            services.AddScoped<ISlackDelayedResponseService, SlackDelayedResponseService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Hangfire 
            services.AddHangfire(config =>
            {
                // Update this to use a persistent storage type in production
                config.UseMemoryStorage();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Hangfire
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            // Set number of Hangfire retry attempts
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 2 });
        }
    }
}
