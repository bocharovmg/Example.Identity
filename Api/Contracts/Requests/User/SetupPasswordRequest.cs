﻿using System.ComponentModel.DataAnnotations;


namespace Exemple.Identity.Api.Contracts.Requests.User;

public class SetupPasswordRequest
{
    [Required(AllowEmptyStrings = false)]
    public string VerificationCode { get; init; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Password { get; init; } = string.Empty;
}