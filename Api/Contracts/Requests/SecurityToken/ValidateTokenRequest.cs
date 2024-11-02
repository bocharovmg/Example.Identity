using System.ComponentModel.DataAnnotations;


namespace Api.Contracts.Requests.SecurityToken;

public class ValidateTokenRequest
{
    [Required(AllowEmptyStrings = false)]
    public string SecurityToken { get; set; } = string.Empty;
}
