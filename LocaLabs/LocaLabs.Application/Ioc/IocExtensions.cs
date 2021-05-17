using LocaLabs.Application.Services;
using MediatR;
using Microsoft.Extensions.ObjectPool;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IocExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            AddPools(services);
            AddServices(services);
            services.AddMediatR(typeof(IEncryptService));

            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IEncryptService, EncryptService>();
            services.AddScoped<INotificationService, NotificationService>();
        }

        private static void AddPools(IServiceCollection services)
        {
            var objectPoolProvider = new DefaultObjectPoolProvider();
            var stringBuilderPool = objectPoolProvider.CreateStringBuilderPool();

            services.AddSingleton(stringBuilderPool);
        }
    }
}