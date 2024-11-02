using Abstractions.Infrastructure.ConnectionManager;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Infrastructure.ConnectionManager;

public class SqlConnectionManager : ISqlConnectionManager
{
    private readonly ILogger<SqlConnectionManager> _logger;

    private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

    private readonly SqlConnectionOptions _options;

    private int _references = 0;

    private SqlConnection? _sqlConnection = null;

    private SqlTransaction? _sqlTransaction = null;

    public bool IsTransactionStarted => Transaction != null;

    public SqlTransaction? Transaction
    {
        get
        {
            var semaphoreTask = _semaphoreSlim.WaitAsync();

            semaphoreTask.Wait();

            try
            {
                return _sqlTransaction;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }

    public SqlConnectionManager(
        ILogger<SqlConnectionManager> logger,
        IOptions<SqlConnectionOptions> options
    )
    {
        _logger = logger;

        _options = options.Value ?? throw new ArgumentNullException($"{nameof(options)} of type {typeof(IOptions<SqlConnectionOptions>)}");
    }

    public async Task<SqlConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        var sqlConnection = await OpenConnectionAsync(true, cancellationToken);

        return sqlConnection;
    }

    public async Task<SqlTransaction> BeginChangesAsync(
        System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default
    )
    {
        await _semaphoreSlim.WaitAsync(cancellationToken);

        try
        {
            _references++;

            if (_sqlTransaction != null)
            {
                return _sqlTransaction;
            }

            var sqlConnection = await OpenConnectionAsync(false, cancellationToken);

            _sqlTransaction = sqlConnection.BeginTransaction(isolationLevel);

            return _sqlTransaction;
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    public async Task ApplyChangesAsync(CancellationToken cancellationToken = default)
    {
        await _semaphoreSlim.WaitAsync(cancellationToken);

        try
        {
            if (_sqlTransaction != null)
            {
                _references--;

                if (_references == 0)
                {
#pragma warning disable CS8602
                    _sqlTransaction.Commit();
#pragma warning restore CS8602

                    _sqlTransaction.Dispose();

                    _sqlTransaction = null;
                }
            }
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    public async Task DiscardChangesAsync(CancellationToken cancellationToken = default)
    {
        await _semaphoreSlim.WaitAsync(cancellationToken);

        try
        {
            await DiscardChangesUnsafeAsync(cancellationToken);
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    public async Task CloseConnectionAsync(
        bool forceClose = false,
        CancellationToken cancellationToken = default
    )
    {
        await _semaphoreSlim.WaitAsync(cancellationToken);

        try
        {
            if (_sqlConnection != null)
            {
                if (
                    forceClose
                    && _sqlTransaction != null
                )
                {
                    await DiscardChangesUnsafeAsync(cancellationToken);
                }

                if (_sqlTransaction == null)
                {
                    _references = 0;

                    _sqlConnection.Close();
                    _sqlConnection.Disposed -= _sqlConnection_Disposed;
                    _sqlConnection.Dispose();
                    _sqlConnection = null;
                }
            }
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    private async Task<SqlConnection> OpenConnectionAsync(bool withSemaphoreLock, CancellationToken cancellationToken = default)
    {
        if (withSemaphoreLock)
        {
            await _semaphoreSlim.WaitAsync(cancellationToken);
        }

        try
        {
            if (_sqlConnection == null)
            {
                if (_options.Credential == null)
                {
                    _sqlConnection = new SqlConnection(_options.ConnectionString);
                }
                else
                {
                    _sqlConnection = new SqlConnection(_options.ConnectionString, _options.Credential);
                }

                _sqlConnection.Disposed += _sqlConnection_Disposed;

                await _sqlConnection.OpenAsync(cancellationToken);
            }

            return _sqlConnection;
        }
        finally
        {
            if (withSemaphoreLock)
            {
                _semaphoreSlim.Release();
            }
        }
    }

    private async Task DiscardChangesUnsafeAsync(CancellationToken cancellationToken = default)
    {
        if (_sqlTransaction != null)
        {
            _references--;

            if (_references == 0)
            {
#pragma warning disable CS8602
                await _sqlTransaction.RollbackAsync(cancellationToken);
#pragma warning restore CS8602

                _sqlTransaction.Dispose();

                _sqlTransaction = null;
            }
        }
    }

    private void _sqlConnection_Disposed(object? sender, EventArgs e)
    {
        var semaphoreTask = _semaphoreSlim.WaitAsync();

        semaphoreTask.Wait();

        try
        {
            _references = 0;

            _sqlConnection = null;

            _sqlTransaction = null;
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }
}
