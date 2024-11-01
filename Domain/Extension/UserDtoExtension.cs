﻿using Exemple.Identity.Domain.Contracts.Dtos;
using System.Security.Claims;


namespace Exemple.Identity.Domain.Extension
{
    public static class UserDtoExtension
    {
        public static Claim[] GetClaims(this UserDto userDto)
        {
            return [
                new Claim(nameof(userDto.UserId), userDto.UserId)
            ];
        }
    }
}
