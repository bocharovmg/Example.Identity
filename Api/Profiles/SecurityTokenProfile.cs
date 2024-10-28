using AutoMapper;
using Exemple.Identity.Api.Contracts.Requests.SecurityToken;
using Exemple.Identity.Domain.Contracts.Queries;


namespace Exemple.Identity.Api.Profiles
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
