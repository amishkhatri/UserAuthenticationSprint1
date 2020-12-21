
//using Microsoft.Data.Sqlite;
using System;


namespace xp.auth.core.services
{
    public class Program
    {
        public void InMemory()
        {

            // Using a name and a shared cache allows multiple connections to access the same
            // in-memory database

            // var str = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            string cs = "Data Source=:memory:";
            string stm = "SELECT SQLITE_VERSION()";

            var con = new SqliteConnection(cs);
            con.Open();

            //var cmd = new SQLiteCommand(stm, con);
            //string version = cmd.ExecuteScalar().ToString();

           // Console.WriteLine($"SQLite version: {version}");



            const string connectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";

           var  masterConnection = new SqliteConnection("Data Source=InMemorySample;Mode=Memory;Cache=Shared");
            masterConnection.Open();

            

            // The in-memory database only persists while a connection is open to it. To manage
            // its lifetime, keep one open connection around for as long as you need it.
            var createCommand = masterConnection.CreateCommand();
            createCommand.CommandText =
            @"
                CREATE TABLE data (
                    value TEXT
                )
            ";
            createCommand.ExecuteNonQuery();

            using (var firstConnection = new SqliteConnection(connectionString))
            {
                firstConnection.Open();

                var updateCommand = firstConnection.CreateCommand();
                updateCommand.CommandText =
                @"
                    INSERT INTO data
                    VALUES ('Hello, memory!')
                ";
                updateCommand.ExecuteNonQuery();
            }

            using (var secondConnection = new SqliteConnection(connectionString))
            {
                secondConnection.Open();
                var queryCommand = secondConnection.CreateCommand();
                queryCommand.CommandText =
                @"
                    SELECT *
                    FROM data
                ";
                var value = (string)queryCommand.ExecuteScalar();
                Console.WriteLine(value);
            }

            // After all the connections are closed, the database is deleted.
            masterConnection.Close();
        }
    }
}
