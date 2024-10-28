using Microsoft.Data.SqlClient;


namespace Exemple.Identity.Infrastructure.ConnectionManager
{
    public class SqlConnectionOptions
    {
        public string ConnectionString { get; init; }

        public SqlCredential? Credential { get; init; }
    }
}
