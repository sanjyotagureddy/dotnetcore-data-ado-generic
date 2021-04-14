using DataLayer_Factory.Factories.Interfaces;
using System.Data;
using System.Data.OracleClient;

namespace DataLayer_Factory.Factories
{
    public class OracleDataAccess : IDatabaseHandler
    {
        private string ConnectionString { get; set; }
        public OracleDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new OracleConnection();
        }

        public void CloseConnection(IDbConnection connection)
        {
            var oracleConnection = (OracleConnection)connection;
            oracleConnection.Close();
            oracleConnection.Dispose();
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new OracleCommand()
            {
                CommandText = commandText,
                Connection = (OracleConnection)connection,
                CommandType = commandType
            };
        }

        public IDataAdapter CreateAdapter(IDbCommand command)
        {
            return new OracleDataAdapter((OracleCommand)command);
        }

        public IDbDataParameter CreateParameter(IDbCommand command)
        {
            var oracleParameters = (OracleCommand) command;
            return oracleParameters.CreateParameter();

        }
    }
}