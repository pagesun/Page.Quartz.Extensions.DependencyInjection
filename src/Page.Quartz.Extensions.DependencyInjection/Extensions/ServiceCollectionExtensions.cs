using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Page.Quartz.Extensions.DependencyInjection.QuartzUti;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Page.Quartz.Extensions.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     add quartz services
        /// </summary>
        /// <param name="services">service collection</param>
        /// <param name="configuration">app config</param>
        /// <returns>add quartz services and load jobs from dependency libraries</returns>
        public static IServiceCollection AddQuartz(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterQuartzBase(configuration);

            var assemblies = AppDomain.CurrentDomain.GetCurrentPathAssembly();

            services.AutoRegisterJob(typeof(IJob), assemblies);
            return services;
        }


        /// <summary>
        ///     add quartz services
        /// </summary>
        /// <param name="services">service collection</param>
        /// <param name="configuration">app config</param>
        /// <param name="assemblies">load jobs from these assembly</param>
        /// <returns></returns>
        public static IServiceCollection AddQuartz(this IServiceCollection services, IConfiguration configuration,
            Func<List<Assembly>> assemblies)
        {
            services.RegisterQuartzBase(configuration);

            services.AutoRegisterJob(typeof(IJob), assemblies.Invoke());

            return services;
        }

        #region private

        /// <summary>
        ///     add quartz base services
        /// </summary>
        /// <param name="services">service collection</param>
        /// <param name="configuration">app config</param>
        /// <returns></returns>
        private static IServiceCollection RegisterQuartzBase(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IJobFactory, JobFactory>();

            services.AddSingleton(provider =>
            {
                var option = new QuartzOption(configuration);
                var schedulerFactory = new StdSchedulerFactory(option.ToProperties());
                var scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                return scheduler;
            });
            services.AddHostedService<QuartzService>();
            return services;
        }

        /// <summary>
        ///     auto load jobs
        /// </summary>
        /// <param name="services">service collection</param>
        /// <param name="interfaceType">IJobs interface <see cref="IJob" /></param>
        /// <param name="assemblies">load jobs from these assembly</param>
        /// <returns></returns>
        private static IServiceCollection AutoRegisterJob(this IServiceCollection services, Type interfaceType,
            IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                    .Where(type => type.IsClass && interfaceType.IsAssignableFrom(type));

                foreach (var type in types)
                {
                    var serviceDescriptor =
                        new ServiceDescriptor(type, type, ServiceLifetime.Singleton);
                    if (!services.Contains(serviceDescriptor))
                        services.Add(serviceDescriptor);
                }
            }

            return services;
        }

        #endregion
    }
}