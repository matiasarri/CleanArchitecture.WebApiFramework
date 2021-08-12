using CleanArchitecture.WebApiFramework.Application.Interfaces.Repositories;
using CleanArchitecture.WebApiFramework.Domain.Entities;
using FluentValidation;
using System.Collections.Generic;


namespace CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.CreateClave
{
    public class ClaveRegistroValidator : AbstractValidator<ClaveRegistro>
    {
        private readonly IRegistroSistemaRepositoryAsync RegistroSistemaRepository;

        public ClaveRegistroValidator(IRegistroSistemaRepositoryAsync registroSistemaRepository)
        {
            this.RegistroSistemaRepository = registroSistemaRepository;

            CascadeMode = CascadeMode.Stop;
            
            Include(new MaxAndMinimunLength());

            Include(new ValidarValor());

        }

    }

    public class MaxAndMinimunLength : AbstractValidator<ClaveRegistro>
    {
        public MaxAndMinimunLength()
        {

            RuleFor(p => p.Clave1)
                .NotEmpty().WithMessage("{PropertyName} es requerida.")
                .Length(2, 50);

            RuleFor(p => p.Clave2)
               .Length(2, 50).When(x => !string.IsNullOrEmpty(x.Clave2));

            RuleFor(p => p.Clave3)
               .Length(2, 50).When(x => !string.IsNullOrEmpty(x.Clave3));

            RuleFor(p => p.Clave4)
                .Length(2, 50).When(x => !string.IsNullOrEmpty(x.Clave4));

            RuleFor(p => p.Clave5)
                .Length(2, 50).When(x => !string.IsNullOrEmpty(x.Clave5));

        }
    }

    public class ValidarValor : AbstractValidator<ClaveRegistro>
    {
        public ValidarValor()
        {
            RuleFor(p => p.Valor)
               .Must((o, valor) => { return IsValid(o.ValoresPosibles, valor); })
               .WithMessage("El valor {PropertyValue} no corresponde a uno de los valores posibles")
               .When(p => !string.IsNullOrEmpty(p.Valor) && !string.IsNullOrEmpty(p.ValoresPosibles));


        }
        public bool IsValid(string valoresPosibles, string valor)
        {
            return valoresPosibles.Contains(valor);
        }

    }
    

}
