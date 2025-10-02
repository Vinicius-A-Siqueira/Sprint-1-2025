using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using Mottu.Fleet.Application.DTOs;

namespace Mottu.Fleet.Application.Validators;
public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username é obrigatório")
            .MaximumLength(100).WithMessage("Username deve ter no máximo 100 caracteres");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password é obrigatório")
            .Length(6, 100).WithMessage("Password deve ter entre 6 e 100 caracteres");

        RuleFor(x => x.Profile)
            .NotEmpty().WithMessage("Profile é obrigatório")
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .MaximumLength(150);

        RuleFor(x => x.Phone)
            .MaximumLength(20);
    }
}