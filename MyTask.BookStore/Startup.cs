using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyTask.BookStore.Data;
using MyTask.BookStore.Models;
using MyTask.BookStore.Repository;
using MyTask.BookStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTask.BookStore
{
    public class Startup
    {
        //using this constructure to read from appSettings.json. In this case using to read connection string from appSettings
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //connection string: "Server=serverName;Database=DBname;Integrated Security=true;"
            //connection string: "Server=DESKTOP-HGOLPLP\\SQLEXPRESS;Database=BookStore;Trusted_Connection=True;MultipleActiveResultSets=true"
            services.AddDbContext<BookStoreContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))); 
            //services.AddDatabaseDeveloperPageExceptionFilter();

            //Add dependencis
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserService, UserService>();  
            services.AddScoped<IEmailService, EmailService>();

            //Add Singletone for Message dependensi to use IOptionMonitor
            services.AddSingleton<IMessageRepository, MessageRepository>();

            //Add configuration services for using IOptions
            services.Configure<SMTPConfigModel>(configuration.GetSection("SMTPConfig"));
            services.Configure<NewBookAlertModel>("InternalBook", configuration.GetSection("NewBookAlert"));
            services.Configure<NewBookAlertModel>("ThirdPartyBook", configuration.GetSection("ThirdPartyBook"));

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();

            //add suport of identity framework 
            services.AddIdentity<ApplicationUserModel, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;

                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
            });

            //To redirect Authotized page to login page
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = configuration["Application:LoginPath"];
            });

#if DEBUG //use this method only in debuging mode
            services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(option =>
            {
                //by using true or fals hear we can enable or desable client side validation
                option.HtmlHelperOptions.ClientValidationEnabled = true;
            });//my changes: to compile run time
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseHttpsRedirection();
            app.UseStaticFiles();//to use static files such as images scc js, ... in the folder named wwwroot

            app.UseRouting();

            //for using identity framework
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapDefaultControllerRoute();
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller}/{action}/{id?}");

                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                //name: "AboutUs",
                //pattern: "About-Us", 
                //defaults: new {Controller = "Home", action = "AboutUs" });

                //endpoints.MapRazorPages();
            });
        }
    }
}
