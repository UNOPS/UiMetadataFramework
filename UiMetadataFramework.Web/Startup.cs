namespace UiMetadataFramework.Web
{
	using System;
	using global::MediatR;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using StructureMap;
	using StructureMap.TypeRules;
	using UiMetadataFramework.BasicFields.Input;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;
	using UiMetadataFramework.Web.Forms;

	public class Startup
	{
		public Startup(IHostingEnvironment env)
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
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseMvc();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddMvc();

			services.AddMediatR(typeof(DoMagic));

			var container = new Container();
			container.Configure(config =>
			{
				config.For<MetadataBinder>().Use(t => GetMetadataBinder()).Singleton();
				config.For<FormRegister>().Use(t => GetFormRegister(t)).Singleton();

				config.Scan(_ =>
				{
					_.AssembliesFromApplicationBaseDirectory();
					_.WithDefaultConventions();
				});
			});

			// Populate the container using the service collection.
			// This will register all services from the collection
			// into the container with the appropriate lifetime.
			container.Populate(services);

			// Finally, make sure we return an IServiceProvider. This makes
			// ASP.NET use the StructureMap container to resolve its services.
			return container.GetInstance<IServiceProvider>();
		}

		private static FormRegister GetFormRegister(IContext context)
		{
			var register = new FormRegister(context.GetInstance<MetadataBinder>());
			register.RegisterAssembly(typeof(DoMagic).GetAssembly());
			return register;
		}

		private static MetadataBinder GetMetadataBinder()
		{
			var binder = new MetadataBinder();
			binder.RegisterAssembly(typeof(StringInputFieldBinding).GetAssembly());
			return binder;
		}
	}
}