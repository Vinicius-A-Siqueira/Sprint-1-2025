using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mottu.Fleet.Application.DTOs;

namespace Mottu.Fleet.Application.Validators;


public class CreatePatioValidator : AbstractValidator<CreatePatioDto>
{
    public CreatePatioValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome do pátio é obrigatório")
            .MaximumLength(100);
        RuleFor(x => x.Endereco)
            .NotEmpty().WithMessage("Endereço é obrigatório")
            .MaximumLength(255);
        RuleFor(x => x.Cidade)
            .MaximumLength(100)
            .When(x => x.Cidade != null);
        RuleFor(x => x.Estado)
            .MaximumLength(2)
            .When(x => x.Estado != null);
        RuleFor(x => x.Cep)
            .MaximumLength(10)
            .When(x => x.Cep != null);
        RuleFor(x => x.Capacidade).GreaterThan(0).WithMessage("Capacidade deve ser maior que zero");
        RuleFor(x => x.Telefone)
            .MaximumLength(20)
            .When(x => x.Telefone != null);
    }
}

