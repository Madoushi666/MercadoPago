using MercadoPago.CheckoutAPI.Infrastructure.Persistences.Contexts;
using MercadoPago.CheckoutAPI.Infrastructure.Persistences.Interfaces;
using MercadoPago.CheckoutAPI.Infrastructure.Persistences.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MercadoPago.CheckoutAPI.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(StoreDbContext).Assembly.FullName;
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("StoreDB"), sqlOptions => 
                    {
                        sqlOptions.MigrationsAssembly(assembly);
                        sqlOptions.MigrationsHistoryTable("LogMigracionesEF");             
                    }                  
                );
            }, ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            return services;
        }
    }
}
