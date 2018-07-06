using System;
using System.Globalization;
using System.Windows.Forms;

namespace HomeFinanceManager.Extensions
{
    internal class DateController : Panel
    {
        public Label LbDay { get; set; }
        public ComboBox CbDay { get; set; }
        public Label LbMonth { get; set; }
        public ComboBox CbMonth { get; set; }
        public Label LbYear { get; set; }
        public ComboBox CbYear { get; set; }

        public DateController() // ++++++
        {
            LbDay = new Label {AutoSize = true};
            CbDay = new ComboBox
            {
                AutoSize = true,
                DropDownStyle = ComboBoxStyle.DropDownList,
                IntegralHeight = false,
                MaxDropDownItems = 6
            };

            LbMonth = new Label {AutoSize = true};
            CbMonth = new ComboBox
            {
                AutoSize = true,
                DropDownStyle = ComboBoxStyle.DropDownList,
                IntegralHeight = false,
                MaxDropDownItems = 6
            };

            LbYear = new Label {AutoSize = true};
            CbYear = new ComboBox
            {
                AutoSize = true,
                DropDownStyle = ComboBoxStyle.DropDownList,
                IntegralHeight = false,
                MaxDropDownItems = 6
            };

            Controls.AddRange(new Control[] { LbDay, CbDay, LbMonth, CbMonth, LbYear, CbYear });

            CbMonth.SelectedIndexChanged += CbMonth_SelectedIndexChanged;

            FillYear();
            FillDay();
            FillMonth();

            OnChanged(DateControllerControlsOnSelectedIndexChanged);
        }

        public DateController(DateTime dt) // ++++++
        {
            LbDay = new Label { AutoSize = true };
            CbDay = new ComboBox
            {
                AutoSize = true,
                DropDownStyle = ComboBoxStyle.DropDownList,
                IntegralHeight = false,
                MaxDropDownItems = 6
            };

            LbMonth = new Label { AutoSize = true };
            CbMonth = new ComboBox
            {
                AutoSize = true,
                DropDownStyle = ComboBoxStyle.DropDownList,
                IntegralHeight = false,
                MaxDropDownItems = 6
            };

            LbYear = new Label { AutoSize = true };
            CbYear = new ComboBox
            {
                AutoSize = true,
                DropDownStyle = ComboBoxStyle.DropDownList,
                IntegralHeight = false,
                MaxDropDownItems = 6
            };

            Controls.AddRange(new Control[] { LbDay, CbDay, LbMonth, CbMonth, LbYear, CbYear });

            CbMonth.SelectedIndexChanged += CbMonth_SelectedIndexChanged;

            FillYear();
            FillDay();
            FillMonth();

            CbYear.SelectedIndex = DateTime.Now.Year - dt.Year;
            CbMonth.SelectedIndex = dt.Month - 1;
            CbDay.SelectedIndex = dt.Day - 1;

            OnChanged(DateControllerControlsOnSelectedIndexChanged);
        }

        public void FillDay() // ++++++
        {
            LbDay.Text = "День:";
            for (var i = 1; i <= 31; i++)
            {
                CbDay.Items.Add(i);
            }
            CbDay.SelectedIndex = DateTime.Now.Day - 1;
        }

        public void FillMonth() // ++++++
        {
            LbMonth.Text = "Месяц:";
            CbMonth.Items.AddRange(DateTimeFormatInfo.CurrentInfo.MonthNames);
            CbMonth.Items.RemoveAt(CbMonth.Items.Count - 1);
            CbMonth.SelectedIndex = DateTime.Now.Month - 1;
        }

        public void FillYear() // ++++++
        {
            LbYear.Text = "Год:";
            for (var i = 0; i < 20; i++)
            {
                CbYear.Items.Add(DateTime.Now.Year - i);
            }
            CbYear.SelectedIndex = 0;
        }

        public void SetDate(DateTime dt) // ++++++
        {
            CbDay.SelectedIndex = dt.Day - 1;
            CbMonth.SelectedIndex = dt.Month - 1;
            CbYear.SelectedIndex = DateTime.Now.Year - dt.Year;
        }

        public DateTime GetDate() // ++++++
        {
            return new DateTime(Convert.ToInt32(CbYear.SelectedItem.ToString()), CbMonth.SelectedIndex + 1, Convert.ToInt32(CbDay.SelectedItem.ToString()));
        }

        private void CbMonth_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            while (CbDay.Items.Count != 31)
            {
                CbDay.Items.Add(CbDay.Items.Count + 1);
            }

            //Изменение количества дней в зависимости от месяца и года
            for (var i = 31; i > DateTime.DaysInMonth(Convert.ToInt32(CbYear.SelectedItem.ToString()), CbMonth.SelectedIndex + 1); i--)
            {
                if (CbDay.SelectedIndex == i - 1)
                {
                    CbDay.SelectedIndex = DateTime.DaysInMonth(Convert.ToInt32(CbYear.SelectedItem.ToString()), CbMonth.SelectedIndex + 1) - 1;
                }
                CbDay.Items.RemoveAt(i - 1);
            }
        }

        public void OnChanged(EventHandler del) // ++++++
        {
            CbDay.SelectedIndexChanged += del;
            CbMonth.SelectedIndexChanged += del;
            CbYear.SelectedIndexChanged += del;
        }

        private static void DateControllerControlsOnSelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            if (((DateController)((ComboBox)sender).Parent).Name == "dControllerMin")
            {
                ((ComboBox)sender).FindForm().Controls["buttonOk"].Enabled = ((DateController)((ComboBox)sender).Parent).GetDate() <=
                                                                             ((DateController)((ComboBox)sender).FindForm().Controls["dControllerMax"]).GetDate();
            }
            if (((DateController)((ComboBox)sender).Parent).Name == "dControllerMax")
            {
                ((ComboBox)sender).FindForm().Controls["buttonOk"].Enabled = ((DateController)((ComboBox)sender).Parent).GetDate() >
                                                                             ((DateController)((ComboBox)sender).FindForm().Controls["dControllerMin"]).GetDate();
            }
        }
    }
}