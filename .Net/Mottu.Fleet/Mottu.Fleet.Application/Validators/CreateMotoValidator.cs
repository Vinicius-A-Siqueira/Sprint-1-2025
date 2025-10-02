using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mottu.Fleet.Application.DTOs;


namespace Mottu.Fleet.Application.Validators;

public class CreateMotoValidator : AbstractValidator<CreateMotoDto>
{
    public CreateMotoValidator()
    {
        RuleFor(x => x.Placa)
            .NotEmpty().WithMessage("Placa é obrigatória")
            .MaximumLength(20)
            .Matches(@"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$").WithMessage("Placa deve seguir o padrão brasileiro (ABC1D23)");
        RuleFor(x => x.Modelo)
            .NotEmpty().WithMessage("Modelo é obrigatório")
            .MaximumLength(100);
        RuleFor(x => x.PatioId).GreaterThan(0).WithMessage("PatioId deve ser maior que zero");
        RuleFor(x => x.Ano).InclusiveBetween(2000, 2030);
        RuleFor(x => x.Cor).MaximumLength(50).When(x => x.Cor != null);
        RuleFor(x => x.Quilometragem).GreaterThanOrEqualTo(0);
    }
}