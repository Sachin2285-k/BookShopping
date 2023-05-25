using Ecomm_Project_1.DataAccess.Data;
using Ecomm_Project_1.DataAccess.Repository;
using Ecomm_Project_1.DataAccess.Repository.IRepository;
using Ecomm_Project_1.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm_Project_1
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
            string cs = Configuration.GetConnectionString("conStr");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(cs));

            //  services.AddScoped<ICategoryRepository, CategoryRepository>();
            //  services.AddScoped<ICoverTypeRepository, CoverTypeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmailSender, EmailSender>();

            // Stripe
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            //Email Settings
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddDatabaseDeveloperPageExceptionFilter();

            // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentity<IdentityUser, IdentityRole>().
                AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                options.LogoutPath = $"/Identity/Account/Logout";

            });

            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "1651686111953332";
                options.AppSecret = "6a72496f86b5624c5f380fe53f8c56b3";
            });

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "119675400806-ksj67d3hkf0q5rn8g3a3s2gm271557a2.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });

            //Session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Stripe
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["Secretkey"];

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
