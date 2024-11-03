using Infrastructure.Contracts.Enums.User;


namespace Infrastructure.Extensions;

public static class EnumConvertionExtension
{
    public static VerificationStateType ToVerificationStateType(
        this VerificationFieldType verificationFieldType
    )
    {
        return verificationFieldType switch
        {
            VerificationFieldType.Email => VerificationStateType.Email,
            VerificationFieldType.AlternativeEmail => VerificationStateType.AlternativeEmail,
            VerificationFieldType.Password => VerificationStateType.Password,
            _ => VerificationStateType.None
        };
    }
}
