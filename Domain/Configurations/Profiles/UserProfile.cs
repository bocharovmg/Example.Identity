using AutoMapper;
using Exemple.Identity.Domain.Extension;
using DomainCommands = Exemple.Identity.Domain.Contracts.Commands;
using DomainQueries = Exemple.Identity.Domain.Contracts.Queries;
using DomainDtos = Exemple.Identity.Domain.Contracts.Dtos;
using InfrastructureCommands = Exemple.Identity.Infrastructure.Contracts.Commands;
using InfrastructureQueries = Exemple.Identity.Infrastructure.Contracts.Queries;
using InfrastructureDtos = Exemple.Identity.Infrastructure.Contracts.Dtos;


namespace Exemple.Identity.Domain.Configurations.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateCommandMaps();

        CreateQueryMaps();

        CreateDtoMaps();
    }

    private void CreateCommandMaps()
    {
        CreateMap<DomainCommands.SignUpCommand, InfrastructureCommands.CreateUserCommand>()
            .ConstructUsing(src => new InfrastructureCommands.CreateUserCommand(src.Name, src.Email, src.AlternativeEmail, src.Password));

        CreateMap<DomainCommands.ConfirmVerificationCodeCommand, InfrastructureCommands.ConfirmVerificationCodeCommand>()
            .ConstructUsing(src => new InfrastructureCommands.ConfirmVerificationCodeCommand(src.VerificationCode));
    }

    private void CreateQueryMaps()
    {
        CreateMap<DomainQueries.SignInQuery, InfrastructureQueries.AuthQuery>()
            .ConstructUsing(src => new InfrastructureQueries.AuthQuery(src.Login, src.Password));

        CreateMap<DomainDtos.UserDto, DomainQueries.GetSecurityTokenQuery>()
            .ConstructUsing(src => new DomainQueries.GetSecurityTokenQuery(src.GetClaims()));
    }

    private void CreateDtoMaps()
    {
        CreateMap<InfrastructureDtos.UserDto, DomainDtos.UserDto>();
    }
}
