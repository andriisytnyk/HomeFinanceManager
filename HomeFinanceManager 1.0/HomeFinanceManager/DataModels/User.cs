using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceManager.DataModels
{
    public class User
    {
        #region fields
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int IdBalance { get; set; }
        #endregion

        public User() // ++++++
        {
            Id = 0;
            Login = null;
            Password = null;
            IdBalance = 0;
        }
        public User(int id, string login, string password) // ++++++
        {
            Id = id;
            Login = login;
            Password = password;
            IdBalance = 0;
        }

        public User(int id, string login, string password, int idbalance) // ++++++
        {
            Id = id;
            Login = login;
            Password = password;
            IdBalance = idbalance;
        }
    }
}
