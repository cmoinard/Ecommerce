using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;

namespace Application.Web
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
            
            ConfigureIoC(services);
        }

        private void ConfigureIoC(IServiceCollection services)
        {
            var container = new Container();
            services.AddSimpleInjector(container, options =>
            {
                options
                    .AddAspNetCore()
                    .AddControllerActivation();
            });

            var adapter = new SimpleInjectorContainerAdapter(container);
            var registration = HexagonRegistrationFactory.AllHexagonRegistration(adapter);
            registration.RegisterPrimaryPorts();
            registration.RegisterSecondaryPorts();
            registration.RegisterOther();
            registration.RegisterDbContext(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}