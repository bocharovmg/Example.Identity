using Infrastructure.Contracts.Enums.User;


namespace Infrastructure.Contracts.Dtos;

public class UserDto
{
    /// <summary>
    /// User Id
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// User create date
    /// </summary>
    public DateTime? CreateDate { get; init; }

    /// <summary>
    /// User name
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// User name
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Type of block
    /// </summary>
    public BlockType? BlockType { get; init; }
}
