using Microsoft.Data.SqlClient;
using System.Data;

namespace Infraestructure.Implementations
{
    public class SqlBase
    {
        public string ConnectionString;
        private SqlDataAdapter _adapter;
        public SqlBase(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public async Task<DataSet> ExecuteSpResults(string nameStoreProcedure, params SqlParameter[] parameters)
        {
            try
            {
                DataSet _dataSet = new();
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    // Do work here; connection closed on following line.
                    using (SqlCommand cmd = new SqlCommand(nameStoreProcedure, connection))
                    {
                        // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect
                        // type is only for OLE DB.
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters);
                        _adapter = new SqlDataAdapter(cmd);

                        // created the dataset object
                        _dataSet = new DataSet();

                        _adapter.Fill(_dataSet);
                    }
                }
                return _dataSet;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DataSet> ExecuteSpResults(string nameStoreProcedure)
        {
            try
            {
                DataSet _dataSet = new();
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    // Do work here; connection closed on following line.
                    using (SqlCommand cmd = new SqlCommand(nameStoreProcedure, connection))
                    {
                        // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect
                        // type is only for OLE DB.
                        cmd.CommandType = CommandType.StoredProcedure;
                        _adapter = new SqlDataAdapter(cmd);

                        // created the dataset object
                        _dataSet = new DataSet();

                        _adapter.Fill(_dataSet);
                    }
                }
                return _dataSet;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<DataSet> ExecuteQuery(string query)
        {
            try
            {
                DataSet _dataSet = new();
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    // Do work here; connection closed on following line.
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect
                        // type is only for OLE DB.
                        cmd.CommandType = CommandType.Text;
                        _adapter = new SqlDataAdapter(cmd);

                        // created the dataset object
                        _dataSet = new DataSet();

                        _adapter.Fill(_dataSet);
                    }
                }
                return _dataSet;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
