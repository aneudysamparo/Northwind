using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Application.Common.Behaviours;
using Northwind.Application.Common.Services;

namespace Northwind.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IAppService), typeof(AppService));
            services.Scan(scan =>
                scan.FromAssemblies(Assembly.GetAssembly(typeof(ServiceAttribute)), Assembly.GetAssembly(typeof(DependencyInjection)))
                    .AddClasses(classes => classes.WithAttribute<ServiceAttribute>(s => s.Lifetime == ServiceLifetime.Scoped))
                        .AsSelfWithInterfaces()
                        .WithScopedLifetime()
                    .AddClasses(classes => classes.WithAttribute<ServiceAttribute>(s => s.Lifetime == ServiceLifetime.Singleton))
                        .AsSelfWithInterfaces()
                        .WithSingletonLifetime()
                    .AddClasses(classes => classes.WithAttribute<ServiceAttribute>(s => s.Lifetime == ServiceLifetime.Transient))
                        .AsSelfWithInterfaces()
                        .WithTransientLifetime()
            );

            return services;
        }
    }
}
