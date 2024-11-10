using Domain.Contracts.Queries;
using FluentValidation;


namespace Domain.Validators;

public class SignInQueryValidator : AbstractValidator<SignInQuery>
{
    public SignInQueryValidator()
    {
        RuleFor(request => request.Login)
            .Length(6, 320);

        RuleFor(request => request.Password)
            .Length(6, 100);
    }
}
