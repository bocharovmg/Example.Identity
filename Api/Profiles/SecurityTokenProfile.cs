using AutoMapper;
using Api.Contracts.Requests.SecurityToken;
using Domain.Contracts.Queries;


namespace Api.Profiles
{
    public class SecurityTokenProfile : Profile
    {
        public SecurityTokenProfile()
        {
            CreateCommandMaps();

            CreateQueryMap();
        }

        private void CreateCommandMaps()
        {
            CreateMap<ValidateTokenRequest, GetTokenValidationStateQuery>()
                .ConstructUsing(src => new GetTokenValidationStateQuery(src.SecurityToken));
        }

        private void CreateQueryMap()
        {

        }
    }
}
