﻿using Api.Extensions;
using AutoMapper;
using Exemple.Identity.Api.Contracts.Requests.User;
using Exemple.Identity.Domain.Contracts.Commands;
using Exemple.Identity.Domain.Contracts.Queries;
using System.Security.Cryptography;


namespace Exemple.Identity.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateCommandMaps();

            CreateQueryMap();
        }

        private void CreateCommandMaps()
        {
            CreateMap<SignInRequest, SignInQuery>()
                .ConstructUsing(src => new SignInQuery(src.UserName, src.Password.GetHash(SHA512.Create()) ?? string.Empty));

            CreateMap<SignUpRequest, SignUpCommand>()
                .ConstructUsing(src => new SignUpCommand(src.Name, src.Email, src.AlternativeEmail, src.Password.GetHash(SHA512.Create()) ?? string.Empty));

            CreateMap<SetupPasswordRequest, SetupPasswordCommand>()
                .ConstructUsing(src => new SetupPasswordCommand(src.VerificationCode, src.Password.GetHash(SHA512.Create()) ?? string.Empty));
        }

        private void CreateQueryMap()
        {
        }
    }
}
