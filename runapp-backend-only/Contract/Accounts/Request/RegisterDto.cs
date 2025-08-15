using System.ComponentModel.DataAnnotations;

namespace Contracts.Accounts.Request
{
    public record RegisterDto
    {
        [Required(ErrorMessage = "{0} can't be null or empty")]
        [EmailAddress]
        public string Email { get; init; }

        [Required(ErrorMessage = "{0} can't be null or empty")]
        public string Password { get; init; }

        [Required(ErrorMessage = "{0} can't be null or empty")]
        [Compare("Password", ErrorMessage = "{0} and {1} don't math")]
        public string ConfirmPassword { get; init; }

        [Required(ErrorMessage = "{0} can't be null or empty")]
        [StringLength(10, MinimumLength = 4, ErrorMessage ="{0} must be between 4 and 10 characters long")]
        public string UserName { get; init; }

        [Required(ErrorMessage = "{0} can't be null or empty")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "{0} must be between 4 and 10 characters long")]
        public string NickName { get; init; }
    }
}
