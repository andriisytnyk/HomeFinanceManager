using System.Windows.Forms;
using HomeFinanceManager.Database;
using HomeFinanceManager.DataModels;

namespace HomeFinanceManager.Forms
{
    public class FormBase : Form
    {
        private static User _currentUser = new User();
        private const string _sConn = "server=localhost;user id=root;password=1234;database=mydb";
        private static readonly Connection _conn = Connection.Create(SConn);

        protected static User CurrentUser // ++++++
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
            }
        }
        protected static string SConn // ++++++
        {
            get
            {
                return _sConn;
            }
        }
        protected static Connection Conn // ++++++
        {
            get
            {
                return _conn;
            }
        }

        /// <summary>
        /// Removing first zeros from string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TrimZeroes(string s) // ++++++
        {
            var i = 0;
            for (; s[i] == '0'; i++) { }
            return s.Substring(i);
        }
    }
}
