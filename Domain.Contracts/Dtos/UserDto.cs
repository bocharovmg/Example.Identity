using Exemple.Identity.Domain.Contracts.Enums.User;


namespace Exemple.Identity.Domain.Contracts.Dtos;

public class UserDto
{
    /// <summary>
    /// User Id
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// User create date
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// VerificationState
    /// </summary>
    public VerificationStateDto VerificationState { get; set; } = new VerificationStateDto();

    /// <summary>
    /// Type of block
    /// </summary>
    public BlockType? BlockType { get; set; }
}
