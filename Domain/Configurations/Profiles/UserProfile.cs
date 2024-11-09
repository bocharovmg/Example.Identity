using AutoMapper;
using DomainCommands = Domain.Contracts.Commands;
using DomainQueries = Domain.Contracts.Queries;
using DomainDtos = Domain.Contracts.Dtos;
using InfrastructureCommands = Infrastructure.Contracts.Commands;
using InfrastructureQueries = Infrastructure.Contracts.Queries;
using InfrastructureDtos = Infrastructure.Contracts.Dtos;
using Domain.Contracts.Enums.User;


namespace Domain.Configurations.Profiles;

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

        CreateMap< DomainDtos.UserDto, DomainQueries.GetSecurityTokenQuery>()
            .ConstructUsing(src => new DomainQueries.GetSecurityTokenQuery(src.UserId, src.Email));
    }

    private void CreateDtoMaps()
    {
        CreateMap<InfrastructureDtos.UserDto, DomainDtos.UserDto>();

        CreateMap<InfrastructureDtos.VerificationStateDto, DomainDtos.VerificationStateDto>()
            .ForMember(dst => dst.Countdown, opt => opt.MapFrom(src => src.Lifetime))
            .ForMember(dst => dst.VerificationState, opt => opt.MapFrom(src => (VerificationStateType)src.VerificationState));
    }
}
