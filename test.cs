using System.Configuration;
using System.Data.SQLite;


namespace xp.auth.core.services
{
    public class test
    {
        public void rgktest()
        {

            var currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
            var currentDir1 = System.Environment.CurrentDirectory;

            var path = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            string cs = "Data Source=:memory:";
            string stm = "SELECT SQLITE_VERSION()";

            var con = new SQLiteConnection(cs);
            con.Open();

            var cmd = new SQLiteCommand(stm, con);
            string version = cmd.ExecuteScalar().ToString();

        }
    }
}
