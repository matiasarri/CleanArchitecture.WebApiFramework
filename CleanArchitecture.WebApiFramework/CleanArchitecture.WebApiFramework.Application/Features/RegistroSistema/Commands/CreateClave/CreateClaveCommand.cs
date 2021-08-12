using AutoMapper;
using CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories;
using CleanArchitecture.WebApiFramework.Application.Wrappers;
using CleanArchitecture.WebApiFramework.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.WebApiFramework.Application.Funciones;
using FluentValidation.Results;
using CleanArchitecture.WebApiFramework.Application.Exceptions;
using System;



namespace CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.CreateClave
{

    public partial class CreateClaveCommand : IRequest<Response<ClaveRegistro>>
    {
     
        public string Clave1 { get; set; }

        public string Clave2 { get; set; }

        public string Clave3 { get; set; }

        public string Clave4 { get; set; }

        public string Clave5 { get; set; }

        public string Valor { get; set; }

        public string Comentario { get; set; }

        public string ValoresPosibles { get; set; }

        public string ValorPredeterminado { get; set; }
    }
    public class CreateClaveCommandHandler : IRequestHandler<CreateClaveCommand, Response<ClaveRegistro>>
    {
        private readonly IRegistroSistemaRepositoryAsync _RegistroSistemaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateClaveCommandHandler(IUnitOfWork unitOfWork, IRegistroSistemaRepositoryAsync registroSistemaRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _RegistroSistemaRepository = registroSistemaRepository;
            _mapper = mapper;
        }

        public async Task<Response<ClaveRegistro>> Handle(CreateClaveCommand request, CancellationToken cancellationToken)
        {
            var clave = _mapper.Map<ClaveRegistro>(request);
            clave = Helpers.EliminarAcentos(clave);

            // validación de la clave
            ClaveRegistroValidator validator = new ClaveRegistroValidator(_RegistroSistemaRepository);
            ValidationResult results = validator.Validate(clave);
            if (!results.IsValid)
            {
                Response<ClaveRegistro> response = new Response<ClaveRegistro>(results.ToString());
                response.Errors = new ValidationException(results.Errors).Errors;
                return response;
            }

            try
            {
                await _unitOfWork.Repository<ClaveRegistro>().AddAsync(clave);
                await _unitOfWork.Commit(cancellationToken);
                return new Response<ClaveRegistro>(clave);

            }
            catch (Exception ex)
            {
                return new ProcessException<ClaveRegistro>(ex).Process();
            }
         
        }
    }
}
