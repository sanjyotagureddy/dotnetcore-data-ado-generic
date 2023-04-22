using System.Data;

using DotNet.Factory.Generic.DataLayer.Factories.Interfaces;

using Microsoft.Extensions.Configuration;

namespace DotNet.Factory.Generic.DataLayer
{
  public class DbManager
  {
    private readonly IDatabaseHandler _database;
    private readonly IConfiguration _configuration;

    private readonly string _providerName;

    public DbManager(string connectionStringName, IConfiguration configuration)
    {
      _configuration = configuration;
      var dbFactory = new DatabaseHandlerFactory(connectionStringName, _configuration);
      _database = dbFactory.CreateDatabase();
      _providerName = dbFactory.GetProviderName();
    }

    public IDbConnection GetDatabaseConnection()
    {
      return _database.CreateConnection();
    }

    public void CloseConnection(IDbConnection connection)
    {
      _database.CloseConnection(connection);
    }

    public IDbDataParameter CreateParameter(string name, object value, DbType dbType)
    {
      return DataParameterManager.CreateParameter(_providerName, name, value, dbType, ParameterDirection.Input);
    }

    public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType)
    {
      return DataParameterManager.CreateParameter(_providerName, name, size, value, dbType, ParameterDirection.Input);
    }

    public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
    {
      return DataParameterManager.CreateParameter(_providerName, name, size, value, dbType, direction);
    }

    public DataTable GetDataTable(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
    {
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      var dataSet = new DataSet();
      var dataAdapter = _database.CreateAdapter(command);
      dataAdapter.Fill(dataSet);

      return dataSet.Tables[0];
    }

    public DataSet GetDataSet(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
    {
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      var dataSet = new DataSet();
      var dataAdapter = _database.CreateAdapter(command);
      dataAdapter.Fill(dataSet);

      return dataSet;
    }

    public IDataReader GetDataReader(string commandText, CommandType commandType, IDbDataParameter[] parameters, out IDbConnection connection)
    {
      connection = _database.CreateConnection();
      connection.Open();

      var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      var reader = command.ExecuteReader();

      return reader;
    }

    public void Delete(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
    {
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      command.ExecuteNonQuery();
    }

    public void Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters)
    {
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      command.ExecuteNonQuery();
    }

    public int Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters, out int lastId)
    {
      lastId = 0;
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      var newId = command.ExecuteScalar();
      lastId = Convert.ToInt32(newId);

      return lastId;
    }

    public long Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters, out long lastId)
    {
      lastId = 0;
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      var newId = command.ExecuteScalar();
      lastId = Convert.ToInt64(newId);

      return lastId;
    }

    public void InsertWithTransaction(string commandText, CommandType commandType, IDbDataParameter[] parameters)
    {
      using var connection = _database.CreateConnection();
      connection.Open();
      var transactionScope = connection.BeginTransaction();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      try
      {
        command.ExecuteNonQuery();
        transactionScope.Commit();
      }
      catch (Exception)
      {
        transactionScope.Rollback();
      }
      finally
      {
        connection.Close();
      }
    }

    public void InsertWithTransaction(string commandText, CommandType commandType, IsolationLevel isolationLevel, IDbDataParameter[] parameters)
    {
      using var connection = _database.CreateConnection();
      connection.Open();
      var transactionScope = connection.BeginTransaction(isolationLevel);

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      try
      {
        command.ExecuteNonQuery();
        transactionScope.Commit();
      }
      catch (Exception)
      {
        transactionScope.Rollback();
      }
      finally
      {
        connection.Close();
      }
    }

    public void Update(string commandText, CommandType commandType, IDbDataParameter[] parameters)
    {
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      command.ExecuteNonQuery();
    }

    public void UpdateWithTransaction(string commandText, CommandType commandType, IDbDataParameter[] parameters)
    {
      using var connection = _database.CreateConnection();
      connection.Open();
      var transactionScope = connection.BeginTransaction();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      try
      {
        command.ExecuteNonQuery();
        transactionScope.Commit();
      }
      catch (Exception)
      {
        transactionScope.Rollback();
      }
      finally
      {
        connection.Close();
      }
    }

    public void UpdateWithTransaction(string commandText, CommandType commandType, IsolationLevel isolationLevel, IDbDataParameter[] parameters)
    {
      using var connection = _database.CreateConnection();
      connection.Open();
      var transactionScope = connection.BeginTransaction(isolationLevel);

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          command.Parameters.Add(parameter);
        }
      }

      try
      {
        command.ExecuteNonQuery();
        transactionScope.Commit();
      }
      catch (Exception)
      {
        transactionScope.Rollback();
      }
      finally
      {
        connection.Close();
      }
    }

    public object GetScalarValue(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
    {
      using var connection = _database.CreateConnection();
      connection.Open();

      using var command = _database.CreateCommand(commandText, commandType, connection);
      if (parameters == null) return command.ExecuteScalar();
      foreach (var parameter in parameters)
      {
        command.Parameters.Add(parameter);
      }

      return command.ExecuteScalar();
    }
  }
}