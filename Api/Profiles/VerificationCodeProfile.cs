using AutoMapper;
using Api.Contracts.Requests.VerificationCode;
using Domain.Contracts.Commands;
using Domain.Contracts.Queries;


namespace Api.Profiles
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
        }
    }
}
