using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductCatalog.Infra.Sql;
using SimpleInjector;

namespace ProductCatalog.Web
{
    public class Startup
    {
        private readonly Container _container = new Container();
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProductCatalogContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Hexagonal")));
            
            services.AddControllers();
            services.AddSimpleInjector(_container, options =>
            {
                options
                    .AddAspNetCore()
                    .AddControllerActivation();
            });
            
            ContainerRegistrations.Register(_container);
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