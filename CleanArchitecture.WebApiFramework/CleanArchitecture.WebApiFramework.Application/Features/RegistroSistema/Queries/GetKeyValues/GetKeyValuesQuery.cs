using AutoMapper;
using CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories;
using CleanArchitecture.WebApiFramework.Application.Wrappers;
using CleanArchitecture.WebApiFramework.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.WebApiFramework.Application.Funciones;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Queries.GetKeyValues
{

    public partial class GetKeyValuesQuery : IRequest<Response<List<ClaveRegistro>>>
    {
       public IEnumerable<ClaveRegistro> claves { get; set; }

    }
    public class GetKeyValuesQueryHandler : IRequestHandler<GetKeyValuesQuery, Response<List<ClaveRegistro>>>
    {
        private readonly IRegistroSistemaRepositoryAsync _RegistroSistemaRepository;
        public GetKeyValuesQueryHandler(IRegistroSistemaRepositoryAsync registroSistemaRepository)
        {
            _RegistroSistemaRepository = registroSistemaRepository;
        }

        public async Task<Response<List<ClaveRegistro>>> Handle(GetKeyValuesQuery query, CancellationToken cancellationToken)
        {
                        
           List<ClaveRegistro> ClavesLeidas =  await _RegistroSistemaRepository.GetKeyValuesAsync(query.claves);

           foreach (var key in query.claves)
           {

                ClaveRegistro key1 = ClavesLeidas.FirstOrDefault(p => p.Clave1 == key.Clave1 && p.Clave2 == key.Clave2 && p.Clave3 == key.Clave3 && p.Clave4 == key.Clave4 && p.Clave5 == key.Clave5);
                if (key1 == null)
                {
                    ClaveRegistro newKey = await _RegistroSistemaRepository.CreateKeyAsync(key);

                    ClavesLeidas.Add(newKey);

                }

           }

           return new Response<List<ClaveRegistro>>(ClavesLeidas);
        }

    
    }
}
