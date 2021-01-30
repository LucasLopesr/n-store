﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NStore.WebApp.MVC.CustomExceptions;
using NStore.WebApp.MVC.Extensions;
using System.Globalization;

namespace NStore.WebApp.MVC.Configuration
{
    public static class WebAppConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.Configure<AppSettings>(configuration);

            return services;
        }

        public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            //if (env.IsDevelopment())
            //{
            //    
            //}
            //else
            //{
            //    app.UseExceptionHandler("/erro/500");
            //    app.UseStatusCodePagesWithRedirects("/erro/{0}");
            //    app.UseHsts();
            //}

            app.UseExceptionHandler("/erro/500");
            app.UseStatusCodePagesWithRedirects("/erro/{0}");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityConfiguration();

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions 
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Catalogo}/{action=Index}/{id?}");
            });
            return app;
        }
    }
}
