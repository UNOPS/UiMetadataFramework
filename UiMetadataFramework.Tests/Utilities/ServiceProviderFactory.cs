namespace UiMetadataFramework.Tests.Utilities;

using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UiMetadataFramework.Basic.Output.Text;
using UiMetadataFramework.Basic.Server;
using UiMetadataFramework.Core.Binding;

public class ServiceProviderFactory
{
	public static IServiceProvider CreateServiceProvider()
	{
		var services = new ServiceCollection();

		services.AddSingleton<DependencyInjectionContainer>(ctx => new DependencyInjectionContainer(ctx.GetRequiredService));

		services.AddSingleton(ctx =>
		{
			var binder = new MetadataBinder(ctx);
			binder.RegisterAssembly(typeof(StringOutputComponentBinding).Assembly);

			return binder;
		});

		services.AddSingleton(ctx =>
		{
			var binder = ctx.GetRequiredService<MetadataBinder>();
			var formRegister = new FormRegister(binder);
			formRegister.RegisterForm(typeof(MediatrTests.Magic));

			return formRegister;
		});

		services.AddTransient<FormRunner>();

		services.AddMediatR(
			typeof(MediatrTests).Assembly,
			typeof(FormRunner).Assembly);

		return new DefaultServiceProviderFactory().CreateServiceProvider(services);
	}
}