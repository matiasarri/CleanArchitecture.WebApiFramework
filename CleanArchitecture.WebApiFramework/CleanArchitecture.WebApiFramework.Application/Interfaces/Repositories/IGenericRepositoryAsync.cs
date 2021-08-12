using System;
using System.Collections.Generic;
using CleanArchitecture.WebApiFramework.Domain.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace CleanArchitecture.WebApiFramework.Application.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : class, IEntity
    {

        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }


}
