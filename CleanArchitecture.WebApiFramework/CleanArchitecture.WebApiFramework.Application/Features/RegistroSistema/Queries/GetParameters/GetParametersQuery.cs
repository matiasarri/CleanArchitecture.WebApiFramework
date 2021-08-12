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

namespace CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Queries.GetParameters
{

    public partial class GetParametersQuery : IRequest<Response<List<ClaveRegistro>>>
    {
        public string Clave { get; set; }

    }
    public class GetParametersCommandHandler : IRequestHandler<GetParametersQuery, Response<List<ClaveRegistro>>>
    {
        private readonly IRegistroSistemaRepositoryAsync _RegistroSistemaRepository;

        public GetParametersCommandHandler(IRegistroSistemaRepositoryAsync registroSistemaRepository)
        {
            _RegistroSistemaRepository = registroSistemaRepository;
        }

        public async Task<Response<List<ClaveRegistro>>> Handle(GetParametersQuery query, CancellationToken cancellationToken)
        {
           List<ClaveRegistro> _Parameters =  await _RegistroSistemaRepository.GetParametersAsync(query.Clave);
                     
           return new Response<List<ClaveRegistro>>(_Parameters);
        }

    
    }
}
