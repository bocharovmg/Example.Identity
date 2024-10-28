using AutoMapper;
using Exemple.Identity.Api.Contracts.Requests.VerificationCode;
using Exemple.Identity.Domain.Contracts.Commands;
using Exemple.Identity.Domain.Contracts.Queries;


namespace Exemple.Identity.Api.Profiles
{
    public class VerificationCodeProfile : Profile
    {
        public VerificationCodeProfile()
        {
            CreateCommandMaps();

            CreateQueryMap();
        }
        private void CreateCommandMaps()
        {
            CreateMap<ConfirmVerificationCodeRequest, ConfirmVerificationCodeCommand>()
                .ConstructUsing(src => new ConfirmVerificationCodeCommand(src.VerificationCode));
        }

        private void CreateQueryMap()
        {
            CreateMap<GetVerificationCodeStateRequest, GetVerificationStateQuery>()
                .ConstructUsing(src => new GetVerificationStateQuery(src.Email));
        }
    }
}
