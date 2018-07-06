using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using HomeFinanceManager.DataModels;
using System.Configuration;
using System.Globalization;

namespace HomeFinanceManager.Database
{
    public class Connection : IDatabaseClient
    {
        private static Connection _member;
        private MySqlConnection _conn;
        private MySqlCommand _comm;
        private MySqlDataReader _r;
        private int _counter;

        private Connection() // ++++++
        {

        }

        private Connection(string conn) // ++++++
        {
            _member = new Connection {_conn = new MySqlConnection(conn)};
            _member._conn.Open();
            _member._comm = new MySqlCommand("", _member._conn);
        }

        ~Connection() // ++++++
        {
            _member._conn.Dispose();
        }

        public static Connection Create(string conn) // ++++++
        {
            try
            {
                if (_member == null)
                {
                    return new Connection(conn);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return _member;
        }

        public string DoubleToCorrectString(double d) // ++++++
        {
            var str = (Convert.ToString(d, CultureInfo.CurrentCulture)).Replace(',', '.');
            return str;
        }

        public List<User> SelectAllUsers() // ++++++
        {
            try
            {
                _member._comm.CommandText = "select * from Users";
                _member._r = _member._comm.ExecuteReader();
                _member._r.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Create(ConfigurationManager.ConnectionStrings["mydbConString"].ConnectionString);
                if (_counter > 4)
                {
                    throw new Exception("База данных недоступна!");
                }
                _counter++;
                SelectAllUsers();
            }
            var listUsers = new List<User>();
            if (!_member._r.IsDBNull(0))
            {
                do
                {
                    if (!_member._r.IsDBNull(3))
                    {
                        listUsers.Add(new User(Convert.ToInt32(_member._r.GetString(0)), _member._r.GetString(1), _member._r.GetString(2), Convert.ToInt32(_member._r.GetString(3))));
                        continue;
                    }
                    listUsers.Add(new User(Convert.ToInt32(_member._r.GetString(0)), _member._r.GetString(1), _member._r.GetString(2)));
                } while (_member._r.Read());
            }
            _member._r.Dispose();
            return listUsers;
        }

        public List<Bill> SelectAllBills() // ++++++
        {
            try
            {
                _member._comm.CommandText = "select * from Bills";
                _member._r = _member._comm.ExecuteReader();
                _member._r.Read();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                Create(ConfigurationManager.ConnectionStrings["mydbConString"].ConnectionString);
                if ( _counter > 4)
                {
                    throw new Exception("База данных недоступна!");
                }
                _counter++;
                SelectAllBills();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Create(ConfigurationManager.ConnectionStrings["mydbConString"].ConnectionString);
                if (_counter > 4)
                {
                    throw new Exception("База данных недоступна!");
                }
                _counter++;
                SelectAllBills();
            }
            var listBills = new List<Bill>();
            if (!_member._r.IsDBNull(0))
            {
                do
                {
                    listBills.Add(new Bill(Convert.ToInt32(_member._r.GetString(0)), Convert.ToInt32(_member._r.GetString(1))));
                } while (_member._r.Read());
            }
            _member._r.Dispose();
            return listBills;
        }

        public List<Account> SelectAllAccounts() // ++++++
        {
            try
            {
                _member._comm.CommandText = "select * from Accounts";
                _member._r = _member._comm.ExecuteReader();
                _member._r.Read();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                Create(ConfigurationManager.ConnectionStrings["mydbConString"].ConnectionString);
                if (_counter > 4)
                {
                    throw new Exception("База данных недоступна!");
                }
                _counter++;
                SelectAllAccounts();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Create(ConfigurationManager.ConnectionStrings["mydbConString"].ConnectionString);
                if (_counter > 4)
                {
                    throw new Exception("База данных недоступна!");
                }
                _counter++;
                SelectAllAccounts();
            }
            var listAccounts = new List<Account>();
            if (!_member._r.IsDBNull(0))
            {
                do
                {
                    listAccounts.Add(new Account(Convert.ToInt32(_member._r.GetString(0)), _member._r.GetString(1),
                                                 Convert.ToDouble(_member._r.GetString(2)), Convert.ToInt32(_member._r.GetString(3))));
                } while (_member._r.Read());
            }
            _member._r.Dispose();
            return listAccounts;
        }

        public List<string> SelectAllLogins() // ++++++
        {
            _member._comm.CommandText = "select Login from Users";
            _member._r = _member._comm.ExecuteReader();
            _member._r.Read();
            var listLogins = new List<string>();
            if (!_member._r.IsDBNull(0))
            {
                do
                {
                    listLogins.Add(_member._r.GetString(0));
                } while (_member._r.Read());
            }
            _member._r.Dispose();
            return listLogins;
        }

        public User SelectUserByLogin(string login) // ++++++
        {
            _member._comm.CommandText = "select * from Users where Login = '" + login + "'";
            _member._r = _member._comm.ExecuteReader();
            _member._r.Read();
            var user = new User
            {
                Id = Convert.ToInt32(_member._r.GetString(0)),
                Login = _member._r.GetString(1),
                Password = _member._r.GetString(2)
            };

            user.IdBalance = !_member._r.IsDBNull(3) 
                ? Convert.ToInt32(_member._r.GetString(3)) 
                : 0;
            _member._r.Dispose();
            return user;
        }

        public double SelectSumByIdFromBills(int idBalance) // ++++++
        {
            double sum = 0;
            if (idBalance != 0)
            {
                _member._comm.CommandText = "select Sum from Bills where ID = " + idBalance;
                _member._r = _member._comm.ExecuteReader();
                _member._r.Read();
                sum = Convert.ToDouble(_member._r.GetString(0));
                _member._r.Dispose();
            }
            return sum;
        }

        public double SelectSumFromAccounts(string name, int idbalance)
        {
            double sum = 0;
            if (idbalance != 0)
            {
                _member._comm.CommandText = "select Sum from Accounts where IDBalance = " + idbalance + " and Name = '" + name + "'";
                _member._r = _member._comm.ExecuteReader();
                _member._r.Read();
                sum = Convert.ToDouble(_member._r.GetString(0));
                _member._r.Dispose();
            }
            return sum;
        }

        public List<Account> SelectAccountByLogin(string login) // ++++++
        {
            _member._comm.CommandText = "select * from Accounts where IDBalance = " + SelectIdBalanceAtUsers(login);
            _member._r = _member._comm.ExecuteReader();
            _member._r.Read();
            var listAcc = new List<Account>();
            if (!_member._r.IsDBNull(0))
            {
                do
                {
                    var acc = new Account(Convert.ToInt32(_member._r.GetString(0)), _member._r.GetString(1));
                    if (!_member._r.IsDBNull(2))
                    {
                        acc.Sum = Convert.ToDouble(_member._r.GetString(2));
                    }
                    if (!_member._r.IsDBNull(3))
                    {
                        acc.IdBalance = Convert.ToInt32(_member._r.GetString(3));
                    }
                    listAcc.Add(acc);
                } while (_member._r.Read());
            }
            _member._r.Dispose();
            return listAcc;
        }

        public int SelectIdBalanceAtUsers(string login) // ++++++
        {
            _member._comm.CommandText = "select IDBalance from Users where Login = '" + login + "'";
            _member._r = _member._comm.ExecuteReader();
            _member._r.Read();
            var idbalance = 0;
            if (!_member._r.IsDBNull(0))
            {
                idbalance = Convert.ToInt32(_member._r.GetString(0));
            }
            _member._r.Dispose();
            return idbalance;
        }

        public Bill UpdateOrInsertSumAtBills(string login, double sum) // ++++++
        {
            var idbalance = SelectIdBalanceAtUsers(login);
            var strSum = "";
            var bill = new Bill();
            if (idbalance != 0)
            {
                _member._comm.CommandText = "select * from Bills where ID = " + idbalance;
                _member._r = _member._comm.ExecuteReader();
                _member._r.Read();
                bill.Id = Convert.ToInt32(_member._r.GetString(0));
                _member._r.Dispose();
                strSum = DoubleToCorrectString(sum);
                bill.Sum = sum;
                _member._comm.CommandText = "update Bills set Sum = '" + strSum +"' where ID = " + bill.Id;
                _member._comm.ExecuteNonQuery();
                return bill;
            }
            strSum = DoubleToCorrectString(sum);
            _member._comm.CommandText = "insert into Bills(Sum) values(" + strSum + ")";
            _member._comm.ExecuteNonQuery();
            _member._comm.CommandText = "select * from Bills";
            _member._r = _member._comm.ExecuteReader();
            while (_member._r.Read())
            {
                bill.Id = Convert.ToInt32(_member._r.GetString(0));
                bill.Sum = Convert.ToDouble(_member._r.GetString(1));
            }
            _member._r.Dispose();
            _member._comm.CommandText = "update Users set IDBalance = " + bill.Id + " where Login = '" + login + "'";
            _member._comm.ExecuteNonQuery();
            return bill;
        }

        public User UpdateIdBalanceAtUsers(string login, int idbalance) // ++++++
        {
            _member._comm.CommandText = "update Users set IDBalance = " + idbalance + " where Login = '" + login + "'";
            _member._comm.ExecuteNonQuery();
            _member._comm.CommandText = "select * from Users where Login = '" + login + "'";
            _member._r = _member._comm.ExecuteReader();
            _member._r.Read();
            var user = new User(Convert.ToInt32(_member._r.GetString(0)), _member._r.GetString(1), _member._r.GetString(2), Convert.ToInt32(_member._r.GetString(3)));
            _member._r.Dispose();
            return user;
        }

        public Account UpdateSumAtAccounts(string login, string name, double sum) // ++++++
        {
            var strSum = "";
            var acc = new Account();
            _member._comm.CommandText = "select * from Accounts where IDBalance = " + SelectIdBalanceAtUsers(login) + " and Name = '" + name + "'";
            _member._r = _member._comm.ExecuteReader();
            _member._r.Read();
            acc.Id = Convert.ToInt32(_member._r.GetString(0));
            acc.Name = _member._r.GetString(1);
            acc.Sum = sum;
            acc.IdBalance = Convert.ToInt32(_member._r.GetString(3));
            _member._r.Dispose();
            strSum = DoubleToCorrectString(acc.Sum);
            _member._comm.CommandText = "update Accounts set Sum = '" + strSum + "' where ID = " + acc.Id;
            _member._comm.ExecuteNonQuery();
            return acc;
        }

        public double UpdateSumBySummingAtBills(string login, double sum) // ++++++
        {
            var strSum = DoubleToCorrectString(sum);
            var idbalance = SelectIdBalanceAtUsers(login);
            _member._comm.CommandText = "update Bills set Sum = (" + SelectSumByIdFromBills(idbalance) + " + " + strSum + ") where ID = " + idbalance;
            _member._comm.ExecuteNonQuery();
            _member._comm.CommandText = "select Sum from Bills where ID = " + idbalance;
            _member._r = _member._comm.ExecuteReader();
            _member._r.Read();
            var newSum = Convert.ToDouble(_member._r.GetString(0));
            _member._r.Dispose();
            return newSum;
        }

        public double UpdateSumBySubtractionAtBills(string login, double sum) // ++++++
        {
            var strSum = DoubleToCorrectString(sum);
            var idbalance = SelectIdBalanceAtUsers(login);
            _member._comm.CommandText = "update Bills set Sum = (" + SelectSumByIdFromBills(idbalance) + " - " + strSum + ") where ID = " + idbalance;
            _member._comm.ExecuteNonQuery();
            _member._comm.CommandText = "select Sum from Bills where ID = " + idbalance;
            _member._r = _member._comm.ExecuteReader();
            _member._r.Read();
            var newSum = Convert.ToDouble(_member._r.GetString(0));
            _member._r.Dispose();
            return newSum;
        }

        public double UpdateSumBySummingAtAccounts(string login, string name, double sum) // ++++++
        {
            var strSum = DoubleToCorrectString(sum);
            var idbalance = SelectIdBalanceAtUsers(login);
            _member._comm.CommandText = "update Accounts set Sum = (" + SelectSumFromAccounts(name, idbalance) + " + " + strSum + ")  where IDBalance = " + idbalance + " and Name = '" + name + "'";
            _member._comm.ExecuteNonQuery();
            _member._comm.CommandText = "select Sum from Accounts where IDBalance = " + idbalance + " and Name = '" + name + "'";
            _member._r = _member._comm.ExecuteReader();
            _member._r.Read();
            var newSum = Convert.ToDouble(_member._r.GetString(0));
            _member._r.Dispose();
            return newSum;
        }

        public double UpdateSumBySubtractionAtAccounts(string login, string name, double sum) // ++++++
        {
            var strSum = DoubleToCorrectString(sum);
            var idbalance = SelectIdBalanceAtUsers(login);
            _member._comm.CommandText = "update Accounts set Sum = (" + SelectSumFromAccounts(name, idbalance) + " - " + strSum + ")  where IDBalance = " + idbalance + " and Name = '" + name + "'";
            _member._comm.ExecuteNonQuery();
            _member._comm.CommandText = "select Sum from Accounts where IDBalance = " + idbalance + " and Name = '" + name + "'";
            _member._r = _member._comm.ExecuteReader();
            _member._r.Read();
            var newSum = Convert.ToDouble(_member._r.GetString(0));
            _member._r.Dispose();
            return newSum;
        }

        public int DeleteBill(int idbalance) // ++++++
        {
            _member._comm.CommandText = "delete from Bills where ID = " + idbalance;
            _member._comm.ExecuteNonQuery();
            return 1;
        }

        public User InsertLoginPassword(string login, string password) // ++++++
        {
            _member._comm.CommandText = "insert into Users(Login, Password) values('" + login + "', '" + Convert.ToBase64String(Encoding.UTF8.GetBytes(password)) + "');";
            _member._comm.ExecuteNonQuery();
            _member._comm.CommandText = "select * from Users where Login = '" + login + "'";
            _member._r = _member._comm.ExecuteReader();
            _member._r.Read();
            var user = new User(Convert.ToInt32(_member._r.GetString(0)), _member._r.GetString(1), _member._r.GetString(2));
            _member._r.Dispose();
            return user;
        }

        public Account AddAccount(string login, string name) // ++++++
        {
            var idbalance = SelectIdBalanceAtUsers(login);
            Account acc;
            _member._comm.CommandText = "select * from Accounts where IDBalance = " + idbalance;
            _member._r = _member._comm.ExecuteReader();
            while (_member._r.Read())
            {
                acc = new Account(Convert.ToInt32(_member._r.GetString(0)), _member._r.GetString(1),
                                        Convert.ToDouble(_member._r.GetString(2)),
                                        Convert.ToInt32(_member._r.GetString(3)));
                if (_member._r.GetString(1) == name)
                {
                    _member._r.Dispose();
                    return acc;
                }
            }
            _member._r.Dispose();
            //_member._comm.CommandText = $"insert into Accounts(Name, Sum, IDBalance) values('{name}', 0, '{idbalance}')";
            _member._comm.CommandText = "insert into Accounts(Name, Sum, IDBalance) values('" + name + "', 0, " + idbalance + ")";
            _member._comm.ExecuteNonQuery();
            _member._comm.CommandText = "select * from Accounts where IDBalance = " + idbalance;
            _member._r = _member._comm.ExecuteReader();
            _member._r.Read();
            do
            {
                acc = new Account(Convert.ToInt32(_member._r.GetString(0)), _member._r.GetString(1),
                                          Convert.ToDouble(_member._r.GetString(2)),
                                          Convert.ToInt32(_member._r.GetString(3)));
            } while (_member._r.Read());
            _member._r.Dispose();
            return acc;
        }

    }
}