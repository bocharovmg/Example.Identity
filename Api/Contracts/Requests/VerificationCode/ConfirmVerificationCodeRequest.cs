using System.ComponentModel.DataAnnotations;


namespace Exemple.Identity.Api.Contracts.Requests.VerificationCode;

public class ConfirmVerificationCodeRequest
{
    [Required(AllowEmptyStrings = false)]
    public virtual string VerificationCode { get; init; } = string.Empty;
}
