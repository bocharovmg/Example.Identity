using System.ComponentModel.DataAnnotations;


namespace Exemple.Identity.Api.Contracts.Requests.SecurityToken;

public class ValidateTokenRequest
{
    [Required(AllowEmptyStrings = false)]
    public string SecurityToken { get; set; } = string.Empty;
}
