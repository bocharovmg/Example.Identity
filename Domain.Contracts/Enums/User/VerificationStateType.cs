namespace Exemple.Identity.Domain.Contracts.Enums.User
{
    /// <summary>
    /// User verification state types
    /// </summary>
    public enum VerificationStateType
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Password
        /// </summary>
        Password = 1,

        /// <summary>
        /// Email
        /// </summary>
        Email = 2,

        /// <summary>
        /// AlternativeEmail
        /// </summary>
        AlternativeEmail = 3,
    }
}
