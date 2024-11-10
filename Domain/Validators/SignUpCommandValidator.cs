using MediatR;
using FluentValidation;
using Domain.Contracts.Commands;
using Infrastructure.Contracts.Queries;
using Infrastructure.Contracts.Exceptions;


namespace Domain.Validators;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    private readonly IMediator _mediator;

    public SignUpCommandValidator(
        IMediator mediator
    )
    {
        _mediator = mediator;

        RuleFor(request => request.Name)
            .Length(6, 320)
            .MustAsync(IsUniqueLogin).WithMessage("the user with same name is exists");

        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(IsUniqueLogin).WithMessage("the user with same email is exists");

        RuleFor(request => request.AlternativeEmail)
            .EmailAddress()
            .NotEqual(request => request.Email).WithMessage("must not match the email")
            .MustAsync(IsUniqueLogin).WithMessage("the user with same email is exists");

        RuleFor(request => request.Password)
            .Length(6, 100);
    }

    private async Task<bool> IsUniqueLogin(string login, CancellationToken cancellationToken)
    {
        try
        {
            #region get user id
            var getUserIdRequest = new GetUserIdByLoginQuery(login);

            await _mediator.Send(getUserIdRequest, cancellationToken);
            #endregion
        }
        catch (UserNotExistsException)
        {
            return true;
        }

        return false;
    }
}
