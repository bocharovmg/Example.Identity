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
            CreateMap<ValidateTokenRequest, GetSecurityTokenStateQuery>()
                .ConstructUsing(src => new GetSecurityTokenStateQuery(src.SecurityToken));
        }

        private void CreateQueryMap()
        {

        }
    }
}
