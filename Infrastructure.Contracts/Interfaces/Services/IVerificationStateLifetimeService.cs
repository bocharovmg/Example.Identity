using Infrastructure.Contracts.Enums.User;


namespace Infrastructure.Contracts.Interfaces.Services
{
    public interface IVerificationStateLifetimeService
    {
        Task AddAsync(Guid userId, VerificationStateType verificationState, int lifetime, CancellationToken cancellationToken = default);

        Task<int?> GetLifetimeAsync(Guid userId, VerificationStateType verificationState, CancellationToken cancellationToken = default);

        Task RemoveAsync(Guid userId, VerificationStateType verificationState, CancellationToken cancellationToken = default);
    }
}
