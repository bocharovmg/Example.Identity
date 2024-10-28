using System.ComponentModel.DataAnnotations;


namespace Exemple.Identity.Api.Contracts.Requests.VerificationCode;

public class RestorePasswordVerificationCodeRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Email { get; init; } = string.Empty;
}
