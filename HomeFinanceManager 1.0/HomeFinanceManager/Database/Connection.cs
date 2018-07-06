using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using HomeFinanceManager.DataModels;
using System.Configuration;

namespace HomeFinanceManager.Database
{
    public class Connection : IDatabaseClient
    {
        private static Connection member;
        MySqlConnection conn;
        MySqlCommand comm;
        MySqlDataReader r;
        int counter = 0;

        private Connection() // ++++++
        {

        }
        private Connection(string conn) // ++++++
        {
            member = new Connection();
            member.conn = new MySqlConnection(conn);
            member.conn.Open();
            member.comm = new MySqlCommand("", member.conn);
        }
        ~Connection() // ++++++
        {
            member.conn.Dispose();
        }

        public static Connection Create(string conn) // ++++++
        {
            try
            {
                if (member == null)
                {
                    return new Connection(conn);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return member;
        }

        public string DoubleToCorrectString(double d) // ++++++
        {
            string str = (Convert.ToString(d)).Replace(',', '.');
            return str;
        }

        public List<User> SelectAllUsers() // ++++++
        {
            try
            {
                member.comm.CommandText = "select * from Users";
                member.r = member.comm.ExecuteReader();
                member.r.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Create(ConfigurationManager.ConnectionStrings["mydbConString"].ConnectionString);
                if (counter > 4)
                {
                    throw new Exception("База данных недоступна!");
                }
                counter++;
                SelectAllUsers();
            }
            List<User> listUsers = new List<User>();
            if (!member.r.IsDBNull(0))
            {
                do
                {
                    if (!member.r.IsDBNull(3))
                    {
                        listUsers.Add(new User(Convert.ToInt32(member.r.GetString(0)), member.r.GetString(1), member.r.GetString(2), Convert.ToInt32(member.r.GetString(3))));
                        continue;
                    }
                    listUsers.Add(new User(Convert.ToInt32(member.r.GetString(0)), member.r.GetString(1), member.r.GetString(2)));
                } while (member.r.Read());
            }
            member.r.Dispose();
            return listUsers;
        }

        public List<Bill> SelectAllBills() // ++++++
        {
            try
            {
                member.comm.CommandText = "select * from Bills";
                member.r = member.comm.ExecuteReader();
                member.r.Read();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                Create(ConfigurationManager.ConnectionStrings["mydbConString"].ConnectionString);
                if ( counter > 4)
                {
                    throw new Exception("База данных недоступна!");
                }
                counter++;
                SelectAllBills();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Create(ConfigurationManager.ConnectionStrings["mydbConString"].ConnectionString);
                if (counter > 4)
                {
                    throw new Exception("База данных недоступна!");
                }
                counter++;
                SelectAllBills();
            }
            List<Bill> listBills = new List<Bill>();
            if (!member.r.IsDBNull(0))
            {
                do
                {
                    listBills.Add(new Bill(Convert.ToInt32(member.r.GetString(0)), Convert.ToInt32(member.r.GetString(1))));
                } while (member.r.Read());
            }
            member.r.Dispose();
            return listBills;
        }

        public List<string> SelectAllLogins() // ++++++
        {
            member.comm.CommandText = "select Login from Users";
            member.r = member.comm.ExecuteReader();
            member.r.Read();
            List<string> listLogins = new List<string>();
            if (!member.r.IsDBNull(0))
            {
                do
                {
                    listLogins.Add(member.r.GetString(0));
                } while (member.r.Read());
            }
            member.r.Dispose();
            return listLogins;
        }

        public User SelectUserByLogin(string login) // ++++++
        {
            member.comm.CommandText = "select * from Users where Login = '" + login + "'";
            member.r = member.comm.ExecuteReader();
            member.r.Read();
            User user = new User();
            user.Id = Convert.ToInt32(member.r.GetString(0));
            user.Login = member.r.GetString(1);
            user.Password = member.r.GetString(2);
            if (!member.r.IsDBNull(3))
            {
                user.IdBalance = Convert.ToInt32(member.r.GetString(3));
            }
            else
            {
                user.IdBalance = 0;
            }
            member.r.Dispose();
            return user;
        }

        public User SelectUserByPassword(string password) // ++++++
        {
            member.comm.CommandText = "select * from Users where Password = '" + password + "'";
            member.r = member.comm.ExecuteReader();
            member.r.Read();
            User user = new User();
            user.Id = Convert.ToInt32(member.r.GetString(0));
            user.Login = member.r.GetString(1);
            user.Password = member.r.GetString(2);
            if (!member.r.IsDBNull(3))
            {
                user.IdBalance = Convert.ToInt32(member.r.GetString(3));
            }
            else
            {
                user.IdBalance = 0;
            }
            member.r.Dispose();
            return user;
        }

        public User SelectUserByIDBalace(int idBalance) // ++++++
        {
            member.comm.CommandText = "select * from Users where IDBalance = " + idBalance;
            member.r = member.comm.ExecuteReader();
            member.r.Read();
            User user = new User();
            user.Id = Convert.ToInt32(member.r.GetString(0));
            user.Login = member.r.GetString(1);
            user.Password = member.r.GetString(2);
            user.IdBalance = Convert.ToInt32(member.r.GetString(3));
            member.r.Dispose();
            return user;
        }

        public Bill SelectBill(string login) // ++++++
        {
            member.comm.CommandText = "select IDBalance from Users where Login = '" + login + "'";
            member.r = member.comm.ExecuteReader();
            member.r.Read();
            int idbalance = Convert.ToInt32(member.r.GetString(0));
            member.r.Dispose();
            member.comm.CommandText = "select * from Bills where ID = " + idbalance;
            member.r = member.comm.ExecuteReader();
            member.r.Read();
            Bill Bills = new Bill();
            Bills.Id = Convert.ToInt32(member.r.GetString(0));
            Bills.Sum = Convert.ToInt32(member.r.GetString(1));
            member.r.Dispose();
            return Bills;
        }

        public double SelectSumByIdFromBills(int idBalance) // ++++++
        {
            double sum = 0;
            if (idBalance != 0)
            {
                member.comm.CommandText = "select Sum from Bills where ID = " + idBalance;
                member.r = member.comm.ExecuteReader();
                member.r.Read();
                sum = Convert.ToDouble(member.r.GetString(0));
                member.r.Dispose();
            }
            return sum;
        }

        public Bill UpdateOrInsertSumAtBills(string login, double sum) // ++++++
        {
            member.comm.CommandText = "select IDBalance from Users where Login = '" + login + "'";
            member.r = member.comm.ExecuteReader();
            member.r.Read();
            int idbalance = 0;
            string strSum = "";
            Bill bill = new Bill();
            if (!member.r.IsDBNull(0))
            {
                idbalance = Convert.ToInt32(member.r.GetString(0));
                member.r.Dispose();
                member.comm.CommandText = "select * from Bills where ID = " + idbalance;
                member.r = member.comm.ExecuteReader();
                member.r.Read();
                bill.Id = Convert.ToInt32(member.r.GetString(0));
                bill.Sum = Convert.ToDouble(member.r.GetString(1));
                member.r.Dispose();
                strSum = DoubleToCorrectString(sum);
                member.comm.CommandText = "update Bills set Sum = '" + strSum +"' where ID = " + bill.Id;
                member.comm.ExecuteNonQuery();
                return bill;
            }
            member.r.Dispose();
            strSum = DoubleToCorrectString(sum);
            member.comm.CommandText = "insert into Bills(Sum) values(" + strSum + ")";
            member.comm.ExecuteNonQuery();
            member.comm.CommandText = "select * from Bills";
            member.r = member.comm.ExecuteReader();
            while (member.r.Read())
            {
                bill.Id = Convert.ToInt32(member.r.GetString(0));
                bill.Sum = Convert.ToDouble(member.r.GetString(1));
            }
            member.r.Dispose();
            member.comm.CommandText = "update Users set IDBalance = " + bill.Id + " where Login = '" + login + "'";
            member.comm.ExecuteNonQuery();
            return bill;
        }

        public User UpdateIdBalanceAtUsers(string login, int idbalance) // ++++++
        {
            member.comm.CommandText = "update Users set IDBalance = " + idbalance + " where Login = '" + login + "'";
            member.comm.ExecuteNonQuery();
            member.comm.CommandText = "select * from Users where Login = '" + login + "'";
            member.r = member.comm.ExecuteReader();
            member.r.Read();
            User user = new User(Convert.ToInt32(member.r.GetString(0)), member.r.GetString(1), member.r.GetString(2), Convert.ToInt32(member.r.GetString(3)));
            member.r.Dispose();
            return user;
        }

        public double UpdateSumBySummingAtBills(string login, double sum) // ++++++
        {
            string strSum = DoubleToCorrectString(sum);
            member.comm.CommandText = "update Bills set Sum = ((select Sum from (select Sum from Bills where ID = (select IDBalance from (select IDBalance from Users where Login = '" + login + "' limit 1) as tmp) limit 1) as tmp) + " + strSum + ") where ID = (select IDBalance from (select IDBalance from Users where Login = '" + login + "' limit 1) as tmp)";
            member.comm.ExecuteNonQuery();
            member.comm.CommandText = "select Sum from Bills where ID = (select IDBalance from (select IDBalance from Users where Login = '" + login + "' limit 1) as tmp)";
            member.r = member.comm.ExecuteReader();
            member.r.Read();
            double newSum = Convert.ToDouble(member.r.GetString(0));
            member.r.Dispose();
            return newSum;
        }

        public double UpdateSumBySubtractionAtBills(string login, double sum) // ++++++
        {
            string strSum = DoubleToCorrectString(sum);
            member.comm.CommandText = "update Bills set Sum = ((select Sum from (select Sum from Bills where ID = (select IDBalance from (select IDBalance from Users where Login = '" + login + "' limit 1) as tmp) limit 1) as tmp) - " + strSum + ") where ID = (select IDBalance from (select IDBalance from Users where Login = '" + login + "' limit 1) as tmp)";
            member.comm.ExecuteNonQuery();
            member.comm.CommandText = "select Sum from Bills where ID = (select IDBalance from (select IDBalance from Users where Login = '" + login + "' limit 1) as tmp)";
            member.r = member.comm.ExecuteReader();
            member.r.Read();
            double newSum = Convert.ToDouble(member.r.GetString(0));
            member.r.Dispose();
            return newSum;
        }

        public int DeleteBill(int idbalance) // ++++++
        {
            member.comm.CommandText = "delete from Bills where ID = " + idbalance;
            member.comm.ExecuteNonQuery();
            return 1;
        }

        public User InsertLoginPassword(string login, string password) // ++++++
        {
            member.comm.CommandText = "insert into Users(Login, Password) values('" + login + "', '" + Convert.ToBase64String(Encoding.UTF8.GetBytes(password)) + "');";
            member.comm.ExecuteNonQuery();
            member.comm.CommandText = "select * from Users where Login = '" + login + "'";
            member.r = member.comm.ExecuteReader();
            member.r.Read();
            User user = new User(Convert.ToInt32(member.r.GetString(0)), member.r.GetString(1), member.r.GetString(2));
            member.r.Dispose();
            return user;
        }

    }
}