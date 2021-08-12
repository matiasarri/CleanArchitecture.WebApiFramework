using AutoMapper;
using CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories;
using CleanArchitecture.WebApiFramework.Application.Wrappers;
using CleanArchitecture.WebApiFramework.Application.Exceptions;
using CleanArchitecture.WebApiFramework.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.WebApiFramework.Application.Funciones;
using System;

namespace CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.AlterClave
{

    public partial class AlterClaveCommand : IRequest<Response<ClaveRegistro>>
    {
        public string oldKey { get; set; }
        public string newKey { get; set; }

    }

    public class AlterClaveCommandHandler : IRequestHandler<AlterClaveCommand, Response<ClaveRegistro>>
    {

        const string KEY_SEPARATOR = @"\";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegistroSistemaRepositoryAsync _RegistroSistemaRepository;
        private readonly IMapper _mapper;
        public AlterClaveCommandHandler(IUnitOfWork unitOfWork, IRegistroSistemaRepositoryAsync registroSistemaRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _RegistroSistemaRepository = registroSistemaRepository;
            _mapper = mapper;
        }

        public async Task<Response<ClaveRegistro>> Handle(AlterClaveCommand request, CancellationToken cancellationToken)
        {
            
            var oldClave = await _RegistroSistemaRepository.LoadAsync(request.oldKey);

            if (oldClave == null)
            {
                return new Response<ClaveRegistro>($"Clave no encontrada.");
            }
            else
            {

                string[] parameters = request.newKey.Split(KEY_SEPARATOR);
                int ix = parameters.GetLength(0);

                switch (ix)
                {
                    case 1:
                        oldClave.Clave1 = parameters[0];
                        break;
                    case 2:
                        oldClave.Clave1 = parameters[0];
                        oldClave.Clave2 = parameters[1];
                        break;
                    case 3:
                        oldClave.Clave1 = parameters[0];
                        oldClave.Clave2 = parameters[1];
                        oldClave.Clave3 = parameters[2];
                        break;
                    case 4:
                        oldClave.Clave1 = parameters[0];
                        oldClave.Clave2 = parameters[1];
                        oldClave.Clave3 = parameters[2];
                        oldClave.Clave4 = parameters[3];
                        break;
                    case 5:
                        oldClave.Clave1 = parameters[0];
                        oldClave.Clave2 = parameters[1];
                        oldClave.Clave3 = parameters[2];
                        oldClave.Clave4 = parameters[3];
                        oldClave.Clave5 = parameters[4];
                        break;

                }


                await _unitOfWork.Repository<ClaveRegistro>().UpdateAsync(Helpers.EliminarAcentos(oldClave));
               
            }

            try
            {
                await _unitOfWork.Commit(cancellationToken);
                return new Response<ClaveRegistro>(oldClave);

            }
            catch (Exception ex)
            {
                return new ProcessException<ClaveRegistro>(ex).Process();
            }

        }


    }
}
