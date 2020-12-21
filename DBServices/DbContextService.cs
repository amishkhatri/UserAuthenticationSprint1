using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using xp.auth.core.integration.Domain;
using xp.auth.core.integration.Interfaces;
using xp.auth.core.services.DBServices;

namespace xp.auth.core.services
{
    public class DbContextService : IDbContext<DbContextService>
    {
        //private readonly SQLiteDbConnection _dbconnection;

        private IConnection _IConnection;

        public IConnection DatabaseConnection
        {
            get { return _IConnection; }
            set { _IConnection = value; }
        }

        public bool AddUser(User obj)
        {
            SQLiteDbConnection dbConnection = new SQLiteDbConnection();
            SQLiteCommand cmd;
            bool result;
            
            try
            {   
                
                using (var conn = (SQLiteConnection)dbConnection.Connect())
                {
                    cmd = new SQLiteCommand(conn);
                    conn.Open();

                    cmd.CommandText = "INSERT INTO Users(username,password,usertype) VALUES(@username,@password,@usertype)";
                    cmd.Parameters.AddWithValue("@username", obj.username);
                    cmd.Parameters.AddWithValue("@password", obj.password);
                    cmd.Parameters.AddWithValue("@usertype", obj.usertype);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    
                    result = true;

                }
        
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;

            }
         
            return result;
        }

        public void CreateDatabase()
        {

            SQLiteDbConnection dbConnection = new SQLiteDbConnection();
            //SQLiteDbConnection dbConnection = (SQLiteDbConnection) DatabaseConnection;

            SQLiteCommand cmd;            
            try
            {

                if (!dbConnection.IsValidDbConnection)
                {

                    using (var conn = (SQLiteConnection)dbConnection.Connect())
                    {
                        cmd = new SQLiteCommand(conn);
                        conn.Open();
                        cmd.CommandText = "DROP TABLE IF EXISTS Users";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"
                            CREATE TABLE Users (id INTEGER PRIMARY KEY,
                            username TEXT, password TEXT, usertype INTEGER) ";
                        cmd.ExecuteNonQuery();

                    }

                }
            }
            catch(Exception ex)
            {
                throw ex;

            }
            

        }

        public User GetUser(string user)
        {
         
            SQLiteDbConnection dbConnection = new SQLiteDbConnection();
            SQLiteCommand cmd;
            User ReturnUser = new User();

            try
            {

                if (!dbConnection.IsValidDbConnection)
                {
                    using (var conn = (SQLiteConnection)dbConnection.Connect())
                    {
                        cmd = new SQLiteCommand(conn);
                        conn.Open();

                        cmd.CommandText = "SELECT * FROM Users Where username=@username";
                        cmd.Parameters.AddWithValue("@username", user);
                        //cmd.Prepare();
                        SQLiteDataReader resultset = cmd.ExecuteReader();

                        while (resultset.Read())
                        {
                            ReturnUser.username = resultset.GetString(1);
                            ReturnUser.password = resultset.GetString(2);
                            ReturnUser.usertype = resultset.GetInt16(3);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return ReturnUser;

        }
                
    }
}
