namespace UiMetadataFramework.Web
{
    using global::MediatR;
    using Lamar;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using UiMetadataFramework.Basic.Input;
    using UiMetadataFramework.Core.Binding;
    using UiMetadataFramework.MediatR;
    using UiMetadataFramework.Web.Forms;
    using UiMetadataFramework.Web.Middleware;

    public class Startup
    {
        public const string CorsAllowAllPolicy = "AllowAll";

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureContainer(ServiceRegistry registry)
        {
            registry.For<MetadataBinder>().Use(GetMetadataBinder).Singleton();
            registry.For<FormRegister>().Use(GetFormRegister).Singleton();

            registry.Scan(_ =>
            {
                _.AssembliesFromApplicationBaseDirectory();
                _.WithDefaultConventions();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy(CorsAllowAllPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            // Add framework services.
            services.AddMvc(
                    options => { options.EnableEndpointRouting = false; })
                .AddNewtonsoftJson(
                    options =>
                    {
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    });

            services.AddMediatR(typeof(SearchPeople));
            services.AddMediatR(typeof(InvokeForm));
        }

        private static FormRegister GetFormRegister(IServiceContext context)
        {
            var register = new FormRegister(context.GetInstance<MetadataBinder>());
            register.RegisterAssembly(typeof(SearchPeople).Assembly);
            return register;
        }

        private static MetadataBinder GetMetadataBinder(IServiceContext context)
        {
            var binder = new MetadataBinder(new DependencyInjectionContainer(context.GetInstance));
            binder.RegisterAssembly(typeof(StringInputFieldBinding).Assembly);
            binder.RegisterAssembly(typeof(SearchPeople).Assembly);
            return binder;
        }
    }
}