using System;
using System.Collections.Generic;
using System.Drawing;

namespace HomeFinanceManager.DataModels
{
    [Serializable]
    public class Good
    {
        #region fields
        public string Name { get; set; }
        public double Price { get; set; }
        public double Count { get; set; }
        public string Type { get; set; }
        public string Importance { get; set; }
        public string Acc { get; set; }
        public DateTime Time { get; set; }

        public static double MaxSum { get; set; }
        public static double Sum { get; set; }

        [NonSerialized]
        public static List<Good> listAdd = new List<Good>();
        
        [NonSerialized]
        public static List<Good> listAll = new List<Good>();

        [NonSerialized]
        public static List<Good> listVariant = new List<Good>();

        [NonSerialized]
        public static List<string> listType = new List<string>();

        #endregion

        public Good() // ++++++
        {
            Acc = "Наличный счёт";
            Time = DateTime.Now;
        }

        public Good(string n, double p, double c, string t, string i) : this() // ++++++
        {
            Name = n;
            Price = p;
            Count = c;
            Type = t;
            Importance = i;
        }

        public Good(string n, double p, double c, string t, string i, DateTime d) : this()
        {
            Name = n;
            Price = p;
            Count = c;
            Type = t;
            Importance = i;
            Time = d;
        }

        public Good(string n, double p, double c, string t, string i, string a) : this()
        {
            Name = n;
            Price = p;
            Count = c;
            Type = t;
            Importance = i;
            Acc = a;
        }

        public Good(string n, double p, double c, string t, string i, string a, DateTime d) : this()
        {
            Name = n;
            Price = p;
            Count = c;
            Type = t;
            Importance = i;
            Acc = a;
            Time = d;
        }

        public static Color GetColorOfImportance(string imp)
        {
            if (imp == "Первостепенной важности")
            {
                return Color.Red;
            }
            else if (imp == "Средней важности")
            {
                return Color.Orange;
            }
            else if (imp == "Малой важности")
            {
                return Color.Yellow;
            }
            else if (imp == "Неважно")
            {
                return Color.Green;
            }
            else
            {
                return Color.White;
            }
        }
    }
}
