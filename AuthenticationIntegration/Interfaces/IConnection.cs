using System.Data.Common;
using System.Data.SQLite;

namespace xp.auth.core.integration.Interfaces
{
    public interface IConnection
    {
        DbConnection Connect();

      //  SQLiteConnection Connection { get; }

        void CloseConnection(SQLiteConnection conn);
                
    }
}
