using Infrastructure.Contracts.Enums.Language;
using MediatR;


namespace Infrastructure.Contracts.Commands;

public class SetupUserLanguageCommand(
    Guid userId,
    LanguageType language
) :
    IRequest<bool>
{
    public Guid UserId { get; private init; } = userId;

    public LanguageType Language { get; private init; } = language;
}
