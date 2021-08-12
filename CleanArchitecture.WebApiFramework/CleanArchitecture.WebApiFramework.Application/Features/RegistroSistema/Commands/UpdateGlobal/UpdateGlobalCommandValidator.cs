using CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories;
using FluentValidation;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.CreateClave;
using CleanArchitecture.WebApiFramework.Domain.Entities;
using System.Collections.Generic;

namespace CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.UpdateGlobal
{
    public class UpdateGlobalCommandValidator : AbstractValidator<UpdateGlobalCommand>
    {
        private readonly IRegistroSistemaRepositoryAsync RegistroSistemaRepository;

        public UpdateGlobalCommandValidator(IRegistroSistemaRepositoryAsync registroSistemaRepository)  
        {
            this.RegistroSistemaRepository = registroSistemaRepository;

            RuleForEach(x => x.claves).SetValidator(new  ClaveRegistroValidator(RegistroSistemaRepository));


        }

    }
}
