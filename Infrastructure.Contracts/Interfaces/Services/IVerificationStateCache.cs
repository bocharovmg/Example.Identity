using Exemple.Identity.Infrastructure.Contracts.Enums.User;


namespace Exemple.Identity.Infrastructure.Contracts.Interfaces.Services
{
    public interface IVerificationStateCache
    {
        Task AddAsync(Guid userId, VerificationStateType verificationState, int lifetime, CancellationToken cancellationToken = default);

        Task<int?> GetLifetimeAsync(Guid userId, VerificationStateType verificationState, CancellationToken cancellationToken = default);

        Task RemoveAsync(Guid userId, VerificationStateType verificationState, CancellationToken cancellationToken = default);
    }
}
