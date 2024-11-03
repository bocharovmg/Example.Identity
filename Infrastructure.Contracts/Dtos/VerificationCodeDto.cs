using Infrastructure.Contracts.Enums.User;


namespace Infrastructure.Contracts.Dtos;

public class VerificationCodeDto
{
    public string? VerificationCode { get; init; }

    public int Lifetime { get; init; }

    public VerificationStateType VerificationState { get; init; }
}
