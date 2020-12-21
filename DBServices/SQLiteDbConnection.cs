using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using xp.auth.core.integration.Interfaces;

namespace xp.auth.core.services.DBServices
{

    public class SQLiteDbConnection : IConnection
    {
        public static string ConnectionString = @"URI=file:C:\amish\test.db";
        private bool _isValidDbConnection;

        public bool IsValidDbConnection
        {
            get { return _isValidDbConnection; }
            set { _isValidDbConnection = value; }
        }

        private System.Data.Common.DbConnection _dbconnection;

        public System.Data.Common.DbConnection DBConnection
        {
            get { return _dbconnection; }
            set { _dbconnection = value; }
        }


        public System.Data.Common.DbConnection Connect()
        {
            try
            {
                if (!IsValidDbConnection)
                {   
                    if (ConnectionString != null)
                    {
                        DBConnection = new SQLiteConnection(ConnectionString);
                        IsValidDbConnection = true;                    
                    }
                    else
                    {
                        IsValidDbConnection = false;
                        throw new SystemException();
                    }                        
                }
                
                    return DBConnection;
            }
            catch (Exception ex)
            {
                IsValidDbConnection = false;
                throw ex;
            }
            
        
        }

        public void CloseConnection(SQLiteConnection conn)
        {
            try
            {
                if(conn.State == System.Data.ConnectionState.Open)
                { 
                    conn.Close();
                    conn = null;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


    }
}
