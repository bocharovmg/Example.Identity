﻿using Domain.Contracts.Enums.User;


namespace Domain.Contracts.Dtos;

public class UserDto
{
    /// <summary>
    /// User Id
    /// </summary>
    public Guid UserId { get; set; }

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
