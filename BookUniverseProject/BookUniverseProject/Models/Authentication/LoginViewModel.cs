using BookUniverse.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace BookUniverse.Web.Models.Authentication
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(UserValidationConstants.PASSWORD_DTO_MAX_LENGTH, ErrorMessage = UserValidationConstants.NOT_VALID_PASSWORD, MinimumLength = UserValidationConstants.PASSWORD_DTO_MIN_LENGTH)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
