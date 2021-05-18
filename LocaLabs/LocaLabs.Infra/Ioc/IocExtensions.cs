using LocaLabs.Domain.Abstractions;
using LocaLabs.Domain.Entities;
using LocaLabs.Infra.Data;
using LocaLabs.Infra.Data.Base;
using LocaLabs.Infra.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IocExtensions
    {
        private const string ConnectionKey = "Connections:RelationalData";

        public static IServiceCollection AddAppData(this IServiceCollection services, IConfiguration config)
        {
            var connectionStr = config[ConnectionKey];
            if (string.IsNullOrEmpty(connectionStr))
                throw new DataException("Missing Relational Data Configuration");

            services.AddDbContext<DataContext>(opt =>
                opt.UseSqlite(connectionStr, s =>
                    s.MigrationsAssembly("LocaLabs.Infra")));

            services.AddScoped<DataContext, DataContext>();
            services.AddScoped<ITemplateContentService, LocalFileTemplateContentService>();
            services.AddScoped<ITemplateGenerator, HandlebarTemplateGenerator>();

            InjectAllRepositories(services);

            return services;
        }

        private static void InjectAllRepositories(IServiceCollection services)
        {
            var repositoryType = typeof(Repository);
            var repositories = Assembly.GetExecutingAssembly()
                .DefinedTypes
                .Where(w => w.BaseType != null)
                .Where(w => w.BaseType.IsAssignableTo(repositoryType))
                .Select(s => new
                {
                    Service = s,
                    Abstractions = s.GetInterfaces()
                });

            foreach (var repository in repositories)
                foreach (var repositoryInterface in repository.Abstractions)
                    services.AddScoped(repositoryInterface, repository.Service);
        }
    }
}