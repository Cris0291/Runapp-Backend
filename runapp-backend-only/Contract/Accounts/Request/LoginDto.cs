using System.ComponentModel.DataAnnotations;

namespace Contracts.Accounts.Request
{
    public record LoginDto(
        [Required(ErrorMessage = "{0} can't be null or empty")]
        [EmailAddress] 
        string Email,
        [Required(ErrorMessage = "{0} can't be null or empty")] 
        string Password);
    
}
