using System.ComponentModel.DataAnnotations;


namespace Exemple.Identity.Api.Contracts.Requests.User;

public class SignUpRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; init; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Email { get; init; } = string.Empty;

    public string? AlternativeEmail { get; init; }

    [Required(AllowEmptyStrings = false)]
    public string Password { get; init; } = string.Empty;
}
