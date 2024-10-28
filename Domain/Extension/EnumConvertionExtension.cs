using InfrastructureEnums = Exemple.Identity.Infrastructure.Contracts.Enums;


namespace Exemple.Identity.Domain.Extension;

public static class EnumConvertionExtension
{
    public static InfrastructureEnums.User.VerificationStateType ToVerificationStateType(
        this InfrastructureEnums.User.VerificationFieldType verificationFieldType
    )
    {
        return verificationFieldType switch
        {
            InfrastructureEnums.User.VerificationFieldType.Email => InfrastructureEnums.User.VerificationStateType.Email,
            InfrastructureEnums.User.VerificationFieldType.AlternativeEmail => InfrastructureEnums.User.VerificationStateType.AlternativeEmail,
            InfrastructureEnums.User.VerificationFieldType.Password => InfrastructureEnums.User.VerificationStateType.Password,
            _ => InfrastructureEnums.User.VerificationStateType.None
        };
    }
}
