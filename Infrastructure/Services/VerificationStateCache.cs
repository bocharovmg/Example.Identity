using Infrastructure.Contracts.Enums.User;
using Infrastructure.Contracts.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;


namespace Infrastructure.Services;

public class VerificationStateCache : IVerificationStateCache
{
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

    private readonly IMemoryCache _cache;

    public VerificationStateCache(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task AddAsync(Guid userId, VerificationStateType verificationState, int lifetime, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);

        try
        {
            var key = GetKey(userId, verificationState);

            _cache.Remove(key);

            var now = DateTime.UtcNow;

            _cache.Set(key, now.AddSeconds(lifetime).AddMicroseconds(-now.Microsecond), now.AddSeconds(lifetime + 30));
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<int?> GetLifetimeAsync(Guid userId, VerificationStateType verificationState, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);

        try
        {
            var key = GetKey(userId, verificationState);

            var lifetimeDateTime = _cache.Get<DateTime?>(key);

            if (lifetimeDateTime == null)
            {
                return default;
            }

            var lifetime = (int)lifetimeDateTime.Value.Subtract(DateTime.UtcNow).TotalSeconds;

            if (lifetime < 0)
            {
                lifetime = 0;
            }
            else
            {
                lifetime++;
            }

            return lifetime;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task RemoveAsync(Guid userId, VerificationStateType verificationState, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);

        try
        {
            var key = GetKey(userId, verificationState);

            _cache.Remove(key);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private object GetKey(Guid userId, VerificationStateType verificationState)
    {
        return userId.GetHashCode() ^ verificationState.GetHashCode();
    }
}
