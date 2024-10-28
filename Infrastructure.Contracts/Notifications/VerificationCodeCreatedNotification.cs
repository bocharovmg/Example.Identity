using Exemple.Identity.Infrastructure.Contracts.Enums.User;
using MediatR;


namespace Exemple.Identity.Infrastructure.Contracts.Notifications;

public class VerificationCodeCreatedNotification(
    string email,
    string verificationcode,
    VerificationFieldType verificationField
) :
    INotification
{
    public string Email { get; private init; } = email;

    public string VerificationCode { get; private init; } = verificationcode;

    public VerificationFieldType VerificationField { get; private init; } = verificationField;
}
