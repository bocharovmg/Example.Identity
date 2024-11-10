using Infrastructure.Contracts.Enums.User;


namespace Infrastructure.Extensions;

internal static class EnumConvertionExtension
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

    public static UserAttributeSection ToUserAttributeSection(
        this VerificationFieldType verificationFieldType
    )
    {
        return verificationFieldType switch
        {
            VerificationFieldType.Email => UserAttributeSection.Email,
            VerificationFieldType.AlternativeEmail => UserAttributeSection.AlternativeEmail,
            VerificationFieldType.Password => UserAttributeSection.Password,
            _ => 0
        };
    }
}
