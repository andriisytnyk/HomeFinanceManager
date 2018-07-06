using System.Collections.Generic;

namespace HomeFinanceManager.DataModels
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Sum { get; set; }

        public int IdBalance { get; set; }

        public static List<Account> AccList = new List<Account>();

        public Account() // ++++++
        {

        }

        public Account(int id, string name) // ++++++
        {
            Id = id;
            Name = name;
            Sum = 0;
            IdBalance = 0;
        }

        public Account(int id, string name, double sum, int idbalance) // ++++++
        {
            Id = id;
            Name = name;
            Sum = sum;
            IdBalance = idbalance;
        }
    }
}
