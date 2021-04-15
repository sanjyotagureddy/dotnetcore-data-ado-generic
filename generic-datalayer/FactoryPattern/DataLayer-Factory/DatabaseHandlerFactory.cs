using DataLayer_Factory.Factories;
using System.Configuration;
using DataLayer_Factory.Factories.Interfaces;

namespace DataLayer_Factory
{
    public class DatabaseHandlerFactory
    {
        private readonly ConnectionStringSettings _connectionStringSettings;

        public DatabaseHandlerFactory(string connectionStringName)
        {
            _connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
        }

        public IDatabaseHandler CreateDatabase()
        {
            IDatabaseHandler database = _connectionStringSettings.ProviderName.ToLower() switch
            {
                "system.data.sqlclient" => new SqlDataAccess(_connectionStringSettings.ConnectionString),
                "system.data.oracleclient" => new OracleDataAccess(_connectionStringSettings.ConnectionString),
                "system.data.oleDb" => new OleDbDataAccess(_connectionStringSettings.ConnectionString),
                "system.data.odbc" => new OdbcDataAccess(_connectionStringSettings.ConnectionString),
                _ => null
            };

            return database;
        }

        public string GetProviderName()
        {
            return _connectionStringSettings.ProviderName;
        }
    }
}