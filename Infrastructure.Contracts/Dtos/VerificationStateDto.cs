using Infrastructure.Contracts.Enums.User;


namespace Infrastructure.Contracts.Dtos;

public class VerificationStateDto
{
    public int? Lifetime { get; init; }

    public VerificationStateType VerificationState { get; init; }
}
