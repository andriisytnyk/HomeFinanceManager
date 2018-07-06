using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceManager.DataModels
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Sum { get; set; }

        public int IDBalance { get; set; }

        public static List<Account> accList = new List<Account>();

        public Account()
        {

        }

        public Account(int id, string name)
        {
            Id = id;
            Name = name;
            Sum = 0;
            IDBalance = 0;
        }

        public Account(int id, string name, double sum)
        {
            Id = id;
            Name = name;
            Sum = sum;
            IDBalance = 0;
        }

        public Account(int id, string name, double sum, int idbalance)
        {
            Id = id;
            Name = name;
            Sum = sum;
            IDBalance = idbalance;
        }
    }
}
