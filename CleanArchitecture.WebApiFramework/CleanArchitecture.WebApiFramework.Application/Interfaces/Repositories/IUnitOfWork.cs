using CleanArchitecture.WebApiFramework.Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepositoryAsync<T> Repository<T>() where T : AuditableBaseEntity;

        Task<int> Commit(CancellationToken cancellationToken);

        Task<int> ComitAndRemoveCache(CancellationToken cancellationToken);

        Task Rollback();
    }
}