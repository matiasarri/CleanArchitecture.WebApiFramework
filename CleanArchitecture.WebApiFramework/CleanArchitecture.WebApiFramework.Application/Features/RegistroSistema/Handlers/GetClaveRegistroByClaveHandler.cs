using CleanArchitecture.WebApiFramework.Application.Exceptions;
using CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories;
using CleanArchitecture.WebApiFramework.Domain.Entities;
using CleanArchitecture.WebApiFramework.Application.Wrappers;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Queries.GetClave;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Handlers
{
    public class GetClaveRegistroByClaveQueryHandler : IRequestHandler<GetClaveRegistroByClaveQuery, Response<ClaveRegistro>>
    {
        private readonly IRegistroSistemaRepositoryAsync _RegistroSistemaRepository;
        public GetClaveRegistroByClaveQueryHandler(IRegistroSistemaRepositoryAsync registroSistemaRepository)
        {
            _RegistroSistemaRepository = registroSistemaRepository;
        }
        public async Task<Response<ClaveRegistro>> Handle(GetClaveRegistroByClaveQuery query, CancellationToken cancellationToken)
        {
            var clave = await _RegistroSistemaRepository.LoadAsync(query.Clave);
            if (clave == null) throw new ApiException($"Clave no encontrado.");
            return new Response<ClaveRegistro>(clave);
        }
    }
}
