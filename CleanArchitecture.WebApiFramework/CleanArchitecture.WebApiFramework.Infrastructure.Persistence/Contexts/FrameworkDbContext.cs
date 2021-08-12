using CleanArchitecture.WebApiFramework.Application.Interfaces;
using CleanArchitecture.WebApiFramework.Domain.Common;
using CleanArchitecture.WebApiFramework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.Extensions.Configuration;



namespace CleanArchitecture.WebApiFramework.Infrastructure.Persistence.Contexts
{
    public class FrameworkDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public FrameworkDbContext(DbContextOptions<FrameworkDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ClaveRegistro> ClavesRegistroSistema { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            base.OnModelCreating(builder);

            builder.Entity<ClaveRegistro>()
            .HasIndex(p => new { p.Clave1, p.Clave2, p.Clave3, p.Clave4, p.Clave5 })
            .IsUnique().HasName("IX_CLAVE_UNICA_REGISTRO_SISTEMA");
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // esta opcion es necesaria para procesar las excepciones de EF con el paquete
            //EntityFrameworkCore.Exception.SqlServer en al capa de Application
            optionsBuilder.UseExceptionProcessor();
        }
    }
}
