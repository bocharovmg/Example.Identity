using Exemple.Identity.Domain.Contracts.Enums.User;


namespace Exemple.Identity.Domain.Contracts.Dtos;

public class VerificationStateDto
{
    /// <summary>
    /// Countdown
    /// </summary>
    public virtual int? Countdown { get; set; }

    /// <summary>
    /// VerificationState
    /// </summary>
    public virtual VerificationStateType VerificationState { get; set; }
}
