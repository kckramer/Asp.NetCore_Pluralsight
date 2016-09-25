using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IO;
using OdeToFood.Services;
using Microsoft.AspNetCore.Routing;
using OdeToFood.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;

namespace OdeToFood
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json");
            builder.SetBasePath(Directory.GetCurrentDirectory());
            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<OdeToFoodDbContext>
                    (options => options.UseSqlServer(Configuration["database:connection"]));

            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<OdeToFoodDbContext>();

            services.AddSingleton(provider => Configuration);
            services.AddSingleton<IGreeter, Greeter>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment environment, 
                            IHostingEnvironment appEnvironment, ILoggerFactory loggerFactory,
                            IGreeter greeter)
        {
            loggerFactory.AddConsole();

            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWelcomePage("/welcome");

            app.UseFileServer();

            app.UseNodeModules(appEnvironment);

            app.UseIdentity();

            app.UseMvc(ConfigureRoutes);

            app.Run(async (context) =>
            {
                var greeting = greeter.GetGreeting();
                await context.Response.WriteAsync(greeting);
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", 
                "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
