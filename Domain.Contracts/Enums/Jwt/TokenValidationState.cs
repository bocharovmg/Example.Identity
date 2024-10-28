namespace Exemple.Identity.Domain.Contracts.Enums.Jwt
{
    public enum TokenValidationState
    {
        Invalid = 0,

        Valid = 1,

        Expired = 2,
    }
}
