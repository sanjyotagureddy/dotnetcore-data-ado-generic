using System.Data;
using System.Data.OleDb;
using DotNet.Factory.Generic.DataLayer.Factories.Interfaces;

namespace DotNet.Factory.Generic.DataLayer.Factories
{
    public class OleDbDataAccess: IDatabaseHandler
    {
        private string ConnectionString { get; set; } 
        public OleDbDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }


        public IDbConnection CreateConnection()
        {
            return new OleDbConnection();
        }

        public void CloseConnection(IDbConnection connection)
        {
            var oleDbConnection = (OleDbConnection)connection;
            oleDbConnection.Close();
            oleDbConnection.Dispose();
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new OleDbCommand()
            {
                CommandText = commandText,
                Connection = (OleDbConnection)connection,
                CommandType = commandType
            };
        }

        public IDataAdapter CreateAdapter(IDbCommand command)
        {
            return new OleDbDataAdapter((OleDbCommand) command);
        }

        public IDbDataParameter CreateParameter(IDbCommand command)
        {
            var sqlCommand = (OleDbCommand)command;
            return sqlCommand.CreateParameter();
        }
    }
}
