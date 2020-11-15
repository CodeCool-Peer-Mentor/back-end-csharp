namespace Codecool.PeerMentors
{
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Codecool.PeerMentors.Entities;
    using Codecool.PeerMentors.Services;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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
            SetPostgreSQL(services);

            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<PeerMentorDbContext>();

            SetAuthentication(services);

            services.AddControllers();

            services.AddScoped<IAuthService, AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
                options.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials()
                       .SetIsOriginAllowed((_) => true));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void SetPostgreSQL(IServiceCollection services)
        {
            string dbHost = Configuration["DB:HOST"];
            string dbName = Configuration["DB:NAME"];
            string dbUsername = Configuration["DB:USERNAME"];
            string dbPassword = Configuration["DB:PASSWORD"];

            services.AddDbContext<PeerMentorDbContext>(options =>
                options.UseNpgsql($"Host={dbHost};Database={dbName};Username={dbUsername};Password={dbPassword}"));
        }

        private void SetAuthentication(IServiceCollection services)
        {
            services
                .ConfigureApplicationCookie(options =>
                {
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                })
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
        }
    }
}
