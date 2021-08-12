using CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories;
using CleanArchitecture.WebApiFramework.Infrastructure.Persistence.Repository;
using CleanArchitecture.WebApiFramework.Infrastructure.Persistence.Contexts;
using CleanArchitecture.WebApiFramework.Domain.Common;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.WebApiFramework.Application.Interfaces;


namespace CleanArchitecture.WebApiFramework.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FrameworkDbContext _dbContext;
        private bool disposed;
        private Hashtable _repositories;
        

        public UnitOfWork(FrameworkDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IGenericRepositoryAsync<TEntity> Repository<TEntity>() where TEntity : AuditableBaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepositoryAsync<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepositoryAsync<TEntity>)_repositories[type];
        }



        public async Task<int> Commit(CancellationToken cancellationToken)
        {

            try
            {
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        public async Task<int> ComitAndRemoveCache(CancellationToken cancellationToken)
        {
            var result =  await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }

        public Task Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }

       
    }
}