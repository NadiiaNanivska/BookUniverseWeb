namespace BookUniverse.Application.DTOValidators.UserValidators
{
    using BookUniverse.Application.DTOs.UserDTOs;
    using BookUniverse.Domain.Common;
    using FluentValidation;

    public class EditUserDtoValidator : AbstractValidator<EditUserDto>
    {
        public EditUserDtoValidator()
        {
            RuleFor(user => user.Username)
                .MinimumLength(UserValidationConstants.USERNAME_MIN_LENGTH)
                .MaximumLength(UserValidationConstants.USERNAME_MAX_LENGTH);

            RuleFor(user => user.Email)
                .MinimumLength(UserValidationConstants.EMAIL_MIN_LENGTH)
                .MaximumLength(UserValidationConstants.EMAIL_MAX_LENGTH)
                .Matches(UserValidationConstants.EMAIL_PATTERN);
        }
    }
}
