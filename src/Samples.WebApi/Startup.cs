﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Samples.WebApi
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
            services
                .AddHealthChecks()
                .AddAsyncCheck("My healthy check", MyCustomHealthyCheck)
                .AddAsyncCheck("My unhealthy check", MyCustomUnhealthyCheck)
                .AddAsyncCheck("My degraded check", MyCustomDegradedCheck);

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private Task<HealthCheckResult> MyCustomHealthyCheck(CancellationToken cancellationToken)
        {
            var data = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>
            {
                {"sample1", "value1"}, {"sample2", "value2"}, {"sample3", "value3"}
            });

            return Task.FromResult(HealthCheckResult.Healthy("Check complete", data));
        }

        private Task<HealthCheckResult> MyCustomUnhealthyCheck(CancellationToken cancellationToken)
        {
            var data = new Exception("this is a failed exception");

            return Task.FromResult(HealthCheckResult.Unhealthy("Check complete", data));
        }

        private Task<HealthCheckResult> MyCustomDegradedCheck(CancellationToken cancellationToken)
        {
            var data = new Exception("this is a degraded exception");

            return Task.FromResult(HealthCheckResult.Degraded("Check complete", data));
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

            app.UseHealthChecks("/health/core", new HealthCheckOptions()
            {
                ResponseWriter = WriteResponse
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static Task WriteResponse(HttpContext httpContext,
            HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";
            var r = JsonConvert.SerializeObject(result);
            return httpContext.Response.WriteAsync(r);
            //var json =
            //    new JObject(
            //    new JProperty("status", result.Status.ToString()),
            //    new JProperty("totalduration", result.TotalDuration.ToString()),
            //    new JProperty("results", new JObject(result.Entries.Select(pair =>
            //                new JProperty(pair.Key, new JObject(
            //                        new JProperty("status", pair.Value.Status.ToString()),
            //                        new JProperty("description", pair.Value.Description),
            //                        new JProperty("exception", pair.Value.Exception == null ? string.Empty : pair.Value.Exception.Message),
            //                        new JProperty("duration", pair.Value.Duration.ToString()),
            //                        new JProperty("data",
            //                            new JObject(pair.Value.Data.Select(p => new JProperty(p.Key, p.Value))
            //                            )
            //                        )
            //                    )
            //                )
            //            )
            //        )
            //    )
            //);

            //return httpContext.Response.WriteAsync(
            //    json.ToString(Formatting.Indented));
        }
    }
}
