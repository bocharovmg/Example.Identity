using Microsoft.Data.SqlClient;


namespace Abstractions.Infrastructure.ConnectionManager;

public interface ISqlConnectionManager : IConnectionManager<SqlConnection, SqlTransaction>;
