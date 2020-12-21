using System;
using System.Collections.Generic;
using System.Text;
using xp.auth.core.integration.Domain;

namespace xp.auth.core.integration.Interfaces
{

    public interface IDbContext <T> where T : class
    {

        IConnection DatabaseConnection { get; set; }

        void CreateDatabase();

        bool AddUser(User obj);

        User GetUser(string user);

    }
}
