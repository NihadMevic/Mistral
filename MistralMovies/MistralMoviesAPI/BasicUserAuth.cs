using System.Data;
using Custom;

namespace MistralMoviesAPI
{
    public class BasicUserAuth
    {
        public static bool Login(string username, string password)
        {
            SqlHelper sqlHelper = new SqlHelper();
            sqlHelper.ConnectionString = ConfigManager.AppSetting["ConnectionStrings:LocalDb"];
            DataTable result = sqlHelper.ExecSql("exec MistralMovies.dbo.qLogin '" + username + "', '" + password + "'");
            if (result.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
}
