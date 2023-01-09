using System.Data.SqlClient;
using System.Data;

namespace demo.Service
{
    public class DataService : IDataService
    {
        private IConfiguration _config;

        public DataService(IConfiguration config)
        {
            _config = config;
        }

        public bool InsertProduct(string xml)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("InsertProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter xmlParam = new SqlParameter
                    {
                        ParameterName = "@xml",
                        SqlDbType = SqlDbType.Xml,
                        Value = xml
                    };
                    command.Parameters.Add(xmlParam);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            return true;
        }
    }
}