using CleanArchitecture.WebApiFramework.Application.Exceptions;
using CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories;
using CleanArchitecture.WebApiFramework.Application.Wrappers;
using CleanArchitecture.WebApiFramework.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.DeleteClave
{
    public class DeleteClaveCommand : IRequest<Response<ClaveRegistro>>
    {
        public string Clave { get; set; }
        public class DeleteClaveCommandHandler : IRequestHandler<DeleteClaveCommand, Response<ClaveRegistro>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IRegistroSistemaRepositoryAsync _RegistroSistemaRepository;

            public DeleteClaveCommandHandler(IUnitOfWork unitOfWork, IRegistroSistemaRepositoryAsync RegistroSistemaRepository)
            {
                _RegistroSistemaRepository = RegistroSistemaRepository;
                _unitOfWork = unitOfWork;
            }
            public async Task<Response<ClaveRegistro>> Handle(DeleteClaveCommand command, CancellationToken cancellationToken)
            {
                List<ClaveRegistro> claves = await _RegistroSistemaRepository.GetKeysAsync(command.Clave);
                if (claves == null || claves.Count == 0) return new Response<ClaveRegistro>($"Clave no encontrada.");

                foreach (var clave in claves)
                {
                    await _unitOfWork.Repository<ClaveRegistro>().DeleteAsync(clave);
                }

                try
                {
                    await _unitOfWork.Commit(cancellationToken);
                    return new Response<ClaveRegistro>(command.Clave) { Succeeded = true, Message = claves.Count == 1 ? " Clave eliminada" : "Claves eliminadas" };

                }
                catch (Exception ex)
                {
                    return new ProcessException<ClaveRegistro>(ex).Process();
                }

            }
        }
    }
}
