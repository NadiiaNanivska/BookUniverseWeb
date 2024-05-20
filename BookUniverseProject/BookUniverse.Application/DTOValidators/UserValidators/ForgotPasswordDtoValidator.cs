using BookUniverse.Application.DTOs.UserDTOs;
using BookUniverse.Domain.Common;
using BookUniverse.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookUniverse.Application.DTOValidators.UserValidators
{
    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(user => user.Email)
                    .NotEmpty()
                    .MinimumLength(UserValidationConstants.EMAIL_MIN_LENGTH)
                    .MaximumLength(UserValidationConstants.EMAIL_MAX_LENGTH)
                    .Matches(UserValidationConstants.EMAIL_PATTERN);
        }
    }
}
