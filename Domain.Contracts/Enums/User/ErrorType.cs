namespace Domain.Contracts.Enums.User
{
    /// <summary>
    /// User error types:
    /// <br/> 0x03**
    /// <br/> 0x04**
    /// </summary>
    public enum ErrorType
    {
        #region User 0x04**
        /// <summary>
        /// UserNotExists
        /// </summary>
        UserNotExists = AllErrors.UserNotExists,

        /// <summary>
        /// UserNotExistsOrPasswordIncorrect
        /// </summary>
        UserNotExistsOrPasswordIncorrect = AllErrors.UserNotExistsOrPasswordIncorrect,

        /// <summary>
        /// AccessDenied
        /// </summary>
        AccessDenied = AllErrors.AccessDenied,

        /// <summary>
        /// TokenIsEmpty
        /// </summary>
        TokenIsEmpty = AllErrors.TokenIsEmpty,

        /// <summary>
        /// RefreshTokenIsEmpty
        /// </summary>
        RefreshTokenIsEmpty = AllErrors.RefreshTokenIsEmpty,

        /// <summary>
        /// InvalidReferalLink
        /// </summary>
        InvalidReferalLink = AllErrors.InvalidReferalLink,

        /// <summary>
        /// ReferalOwnerNotExists
        /// </summary>
        ReferalOwnerNotExists = AllErrors.ReferalOwnerNotExists,

        /// <summary>
        /// DuplicateUser
        /// </summary>
        DuplicateUser = AllErrors.DuplicateUser,

        /// <summary>
        /// TooManyUsers
        /// </summary>
        TooManyUsers = AllErrors.TooManyUsers,

        /// <summary>
        /// InvalidRoleGroup
        /// </summary>
        InvalidRoleGroup = AllErrors.InvalidRoleGroup,

        /// <summary>
        /// InvalidUserId
        /// </summary>
        InvalidUserId = AllErrors.InvalidUserId,

        /// <summary>
        /// RunOutOfViews
        /// </summary>
        RunOutOfViews = AllErrors.RunOutOfViews,

        /// <summary>
        /// VerificationCodeNotExists
        /// </summary>
        VerificationCodeNotExists = AllErrors.VerificationCodeNotExists,

        /// <summary>
        /// VerificationCodeNotConfirmed
        /// </summary>
        VerificationCodeNotConfirmed = AllErrors.VerificationCodeNotConfirmed,

        /// <summary>
        /// VerificationCodeIncorrect
        /// </summary>
        VerificationCodeIncorrect = AllErrors.VerificationCodeIncorrect,

        /// <summary>
        /// VerificationCodeIsOutdated
        /// </summary>
        VerificationCodeOutdated = AllErrors.VerificationCodeOutdated,

        /// <summary>
        /// UserNotVerified
        /// </summary>
        UserNotVerified = AllErrors.UserNotVerified,
        #endregion
    }
}
