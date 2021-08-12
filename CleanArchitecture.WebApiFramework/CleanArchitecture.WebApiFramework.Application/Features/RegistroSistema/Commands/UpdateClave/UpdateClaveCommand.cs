using AutoMapper;
using CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories;
using CleanArchitecture.WebApiFramework.Application.Wrappers;
using CleanArchitecture.WebApiFramework.Application.Exceptions;
using CleanArchitecture.WebApiFramework.Domain.Entities;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.CreateClave;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.WebApiFramework.Application.Funciones;
using FluentValidation.Results;


namespace CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.UpdateClave
{

    public partial class UpdateClaveCommand : CreateClaveCommand, IRequest<Response<ClaveRegistro>>
    {
    }

    public class UpdateClaveCommandHandler : IRequestHandler<UpdateClaveCommand, Response<ClaveRegistro>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegistroSistemaRepositoryAsync _RegistroSistemaRepository;
        private readonly IMapper _mapper;
        public UpdateClaveCommandHandler(IUnitOfWork unitOfWork, IRegistroSistemaRepositoryAsync registroSistemaRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _RegistroSistemaRepository = registroSistemaRepository;
            _mapper = mapper;
        }

        public async Task<Response<ClaveRegistro>> Handle(UpdateClaveCommand request, CancellationToken cancellationToken)
        {

            var updateClave = _mapper.Map<ClaveRegistro>(request);

            // validación de la clave
            ClaveRegistroValidator validator = new ClaveRegistroValidator(_RegistroSistemaRepository);
            ValidationResult results = validator.Validate(updateClave);
            if (!results.IsValid)   
            {
                Response<ClaveRegistro> response = new Response<ClaveRegistro>(results.ToString());
                response.Errors = new ValidationException(results.Errors).Errors;
                return response;
            }

            var clave = await _RegistroSistemaRepository.LoadAsync(updateClave.FullPath());

            if (clave == null)
            {
              
                clave = updateClave;

                await _unitOfWork.Repository<ClaveRegistro>().AddAsync(Helpers.EliminarAcentos(clave));
                
            }
            else
            {
                clave.Clave1 = request.Clave1;
                clave.Clave2 = request.Clave2;
                clave.Clave3 = request.Clave3;
                clave.Clave4 = request.Clave4;
                clave.Clave5 = request.Clave5;
                clave.Valor = request.Valor;
                clave.Comentario = request.Comentario;
                clave.ValoresPosibles = request.ValoresPosibles;
                clave.ValorPredeterminado = request.ValorPredeterminado;

                await _unitOfWork.Repository<ClaveRegistro>().UpdateAsync(Helpers.EliminarAcentos(clave));
               
            }

            await _unitOfWork.Commit(cancellationToken);

            return new Response<ClaveRegistro>(clave);

        }


    }
}
