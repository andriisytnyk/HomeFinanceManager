using HomeFinanceManager.Database;
using HomeFinanceManager.DataModels;
using System.Windows.Forms;

namespace HomeFinanceManager
{
    public class FormBase : Form
    {
        private static User currentUser = new User();
        private static string sConn = "server=localhost;user id=root;password=1234;database=mydb";
        private static Connection conn = Connection.Create(SConn);

        protected static User CurrentUser // ++++++
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }
        protected static string SConn // ++++++
        {
            get
            {
                return sConn;
            }
        }
        protected static Connection Conn // ++++++
        {
            get
            {
                return conn;
            }
        }

        /// <summary>
        /// Removing first zeros from string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TrimZeroes(string s) // ++++++
        {
            int i = 0;
            for (; s[i] == '0'; i++) { }
            return s.Substring(i);
        }
    }
}
