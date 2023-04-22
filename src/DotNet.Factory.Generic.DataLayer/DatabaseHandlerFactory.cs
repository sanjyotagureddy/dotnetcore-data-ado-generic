using System.Configuration;
using System.Globalization;

using DotNet.Factory.Generic.DataLayer.Factories;
using DotNet.Factory.Generic.DataLayer.Factories.Interfaces;

using Microsoft.Extensions.Configuration;

namespace DotNet.Factory.Generic.DataLayer
{
  public class DatabaseHandlerFactory
  {
    private readonly string _connectionStringSettings;
    private readonly IConfiguration _configuration;

    public DatabaseHandlerFactory(string connectionStringName, IConfiguration configuration)
    {
      _configuration = configuration;
      _connectionStringSettings = _configuration.GetConnectionString(connectionStringName)!.ToString();
    }

    public IDatabaseHandler CreateDatabase()
    {
      var client = GetProviderName().ToLowerInvariant();
      IDatabaseHandler database = (client switch
      {
        "system.data.sqlclient" => new SqlDataAccess(_connectionStringSettings),
        "system.data.oracleclient" => new OracleDataAccess(_connectionStringSettings),
        "system.data.oleDb" => new OleDbDataAccess(_connectionStringSettings),
        "system.data.odbc" => new OdbcDataAccess(_connectionStringSettings),
        _ => null
      })!;

      return database;
    }

    public string GetProviderName()
    {
      return _configuration.GetConnectionString("SqlProvider")!.ToString()!; 
    }
  }
}