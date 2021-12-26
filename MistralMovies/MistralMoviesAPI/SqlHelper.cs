using System.Data;
using System.Data.SqlClient;

namespace MistralMoviesAPI
{
    public class SqlHelper
    {
        public string DataSource = "";
        public string UserId = "";
        public string Password = "";
        public string Catalog = "";
        public string ConnectionString = "";

        private SqlConnection connection;

        public SqlHelper()
        {

        }
        public SqlHelper(string dataSource,string userId, string password, string catalog)
        {
            this.DataSource = dataSource;
            this.UserId = userId;
            this.Catalog = catalog; 
            this.Password = password;
        }
        public DataTable ExecSql(string sql)
        {
            DataTable ret = new DataTable();

            if(this.connection == null)
            {
                Connect();
            }
            SqlCommand cmd = new SqlCommand(sql, this.connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ret);

            Disconnect();
            da.Dispose();

            return ret;
        }
        private bool Connect()
        {
            try
            {
                this.connection = new SqlConnection(this.ConnectionString);
                this.connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool Disconnect()
        {
            try
            {
                if(this.connection != null && this.connection.State == ConnectionState.Open)
                {
                    this.connection.Close();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
