namespace Domain.Contracts.Dtos
{
    public class AuthDto
    {
        public UserDto User { get; init; } = new UserDto();

        public SecurityTokenDto SecurityToken { get; init; } = new SecurityTokenDto();
    }
}
