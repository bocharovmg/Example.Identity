using Microsoft.Data.SqlClient;


namespace Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;

public interface ISqlConnectionManager : IConnectionManager<SqlConnection, SqlTransaction>;
