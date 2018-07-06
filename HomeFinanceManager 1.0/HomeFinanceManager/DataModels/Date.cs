using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceManager.DataModels
{
    public class Date
    {
        #region fields
        private int year;
        private int month;
        private int day;

        public int Year 
        { 
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }
        public int Month
        {
            get
            {
                return month;
            }
            set
            {
                month = value;
            }
        }
        public int Day
        {
            get
            {
                return day;
            }
            set
            {
                day = value;
            }
        }
        #endregion

        public Date()
        {
            Year = 0;
            Month = 0;
            Day = 0;
        }
        public Date(int year)
        {
            Year = year;
            Month = 0;
            Day = 0;
        }
        public Date(int year, int month)
        {
            Year = year;
            Month = month;
            Day = 0;
        }
        public Date(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public DateTime DateToDateTime()
        {
            return new DateTime(this.year, this.month, this.day);
        }
    }
}
