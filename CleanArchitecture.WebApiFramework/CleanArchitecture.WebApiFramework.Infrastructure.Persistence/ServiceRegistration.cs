using CleanArchitecture.WebApiFramework.Application.Interfaces;
using CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories;
using CleanArchitecture.WebApiFramework.Infrastructure.Persistence.Contexts;
using CleanArchitecture.WebApiFramework.Infrastructure.Persistence.Repositories;
using CleanArchitecture.WebApiFramework.Infrastructure.Persistence.Repository;
using CleanArchitecture.WebApiFramework.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApiFramework.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<FrameworkDbContext>(options =>
                    options.UseInMemoryDatabase("FrameworkDb"));
            }
            else
            {
                services.AddDbContext<FrameworkDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(FrameworkDbContext).Assembly.FullName)));
            }
            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            services.AddTransient<IRegistroSistemaRepositoryAsync, RegistroSistemaRepositoryAsync>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion
        }
    }
}
