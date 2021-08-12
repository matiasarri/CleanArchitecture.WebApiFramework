using CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories;
using CleanArchitecture.WebApiFramework.Application.Wrappers;
using CleanArchitecture.WebApiFramework.Application.Exceptions;
using CleanArchitecture.WebApiFramework.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.UpdateGlobal
{

    public partial class UpdateGlobalCommand : IRequest<Response<string>>
    {
        public IEnumerable<ClaveRegistro> claves { get; set; }

    }

    public class UpdateGlobalCommandHandler : IRequestHandler<UpdateGlobalCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegistroSistemaRepositoryAsync _RegistroSistemaRepository;

        public UpdateGlobalCommandHandler(IUnitOfWork unitOfWork, IRegistroSistemaRepositoryAsync registroSistemaRepository)
        {
            _unitOfWork = unitOfWork;
            _RegistroSistemaRepository = registroSistemaRepository;
        }

        public async Task<Response<string>> Handle(UpdateGlobalCommand request, CancellationToken cancellationToken)
        {

            //UpdateGlobalCommandValidator validator = new UpdateGlobalCommandValidator(_RegistroSistemaRepository);
            //ValidationResult results = validator.Validate(updateClave);
            //if (!results.IsValid)
            //{
            //    Response<ClaveRegistro> response = new Response<ClaveRegistro>(results.ToString());
            //    response.Errors = new ValidationException(results.Errors).Errors;
            //    return response;
            //}


            await _RegistroSistemaRepository.UpdateGlobalAsync(request.claves);
            await _unitOfWork.Commit(cancellationToken);
            return new Response<string>() { Succeeded = true, Message = "Claves actualizadas" } ;

        }


    }
}
