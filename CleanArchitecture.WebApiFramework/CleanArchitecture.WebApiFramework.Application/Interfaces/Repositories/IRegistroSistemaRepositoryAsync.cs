using CleanArchitecture.WebApiFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories
{
    public interface IRegistroSistemaRepositoryAsync : IGenericRepositoryAsync<ClaveRegistro>
    {
        Task<bool> IsUniqueClaveAsync(ClaveRegistro claveRegistro);

        Task<ClaveRegistro> CreateKeyAsync(ClaveRegistro claveRegistro);

        Task<ClaveRegistro> UpdateClaveAsync(ClaveRegistro claveRegistro);

        Task<ClaveRegistro> LoadAsync(string key);

        Task<List<ClaveRegistro>> GetKeyValuesAsync(IEnumerable<ClaveRegistro> clavesToRead);
        
        Task UpdateGlobalAsync(IEnumerable<ClaveRegistro> clavesToUpdate);

        Task<List<ClaveRegistro>> GetKeysAsync(string key);

        Task<List<ClaveRegistro>> GetParametersAsync(string key);

        Task<List<ClaveRegistro>> GetKeyEnumAsync(string key);

        //Task AlterKeyAsync(string oldKey, string newKey);


    }
}
