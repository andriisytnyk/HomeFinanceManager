using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceManager.DataModels
{
    public class Bill
    {
        public int Id { get; set; }
        public double Sum { get; set; }

        public Bill() // ++++++
        {

        }
        public Bill(int id, double sum) // ++++++
        {
            Id = id;
            Sum = sum;
        }
    }
}
