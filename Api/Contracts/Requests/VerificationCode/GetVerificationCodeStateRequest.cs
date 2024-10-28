using System.ComponentModel.DataAnnotations;


namespace Exemple.Identity.Api.Contracts.Requests.VerificationCode;

public class GetVerificationCodeStateRequest
{
    [Required(AllowEmptyStrings = false)]
    public virtual string? Email { get; private init; }
}
