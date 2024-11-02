using System.ComponentModel.DataAnnotations;


namespace Api.Contracts.Requests.VerificationCode;

public class GetVerificationCodeStateRequest
{
    [Required(AllowEmptyStrings = false)]
    public virtual string? Email { get; private init; }
}
