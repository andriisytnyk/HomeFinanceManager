using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DateTime Time { get; set; }

        public static double MaxSum { get; set; }
        public static double Sum { get; set; }

        [NonSerialized]
        public static List<Good> goodsAdd = new List<Good>();
        
        [NonSerialized]
        public static List<Good> listAll = new List<Good>();

        [NonSerialized]
        public static List<Good> listVariant = new List<Good>();

        [NonSerialized]
        public static List<KeyValuePair<string, byte>> typeList = new List<KeyValuePair<string, byte>>();

        [NonSerialized]
        public static List<KeyValuePair<string, byte>> importanceList = new List<KeyValuePair<string, byte>>() 
        { new KeyValuePair<string, byte>("Первостепенной важности", 0), 
          new KeyValuePair<string, byte>("Средней важности", 1), 
          new KeyValuePair<string, byte>("Малой важности", 2),
          new KeyValuePair<string, byte>("Неважно", 3)
        };
        #endregion

        public Good() // ++++++
        {
            Time = DateTime.Now;
        }

        public Good(string n, double p, double c, string t, string i) : this() // ++++++
        {
            Name = n;
            Price = p;
            Count = c;
            Type = t;
            Importance = i;
            Time = DateTime.Now;
        }

        public Good(string n, double p, double c, string t, string i, DateTime d)
        {
            Name = n;
            Price = p;
            Count = c;
            Type = t;
            Importance = i;
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
                return Color.Black;
            }
        }
    }
}
