using System.ComponentModel.DataAnnotations;


namespace Exemple.Identity.Api.Contracts.Requests.User;

public class SignInRequest
{
    [Required(AllowEmptyStrings = false)]
    public string UserName { get; init; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Password { get; init; } = string.Empty;
}
