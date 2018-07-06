using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace HomeFinanceManager.Extensions
{
    public static class EditMessageBox
    {
        static string StringToCorrectDoubleFormat(string s) // ++++++
        {
            //Изменение строки в правильный формат для типа double
            if (s.IndexOf(',') == -1)
            {
                s = s.Replace('.', ',');
                return s;
            }
            return s;
        }

        public static DialogResult InputBox(string formName, string lbText, ref string value) // ++++++
        {
            #region creating controls

            Form form = new Form();
            Label lbType = new Label();
            TextBox tbType = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            #endregion

            #region text and items of controls

            form.Text = formName;

            lbType.Text = lbText;
            tbType.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            #endregion

            #region bounds

            lbType.SetBounds(9, 20, 372, 13);
            tbType.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            #endregion

            #region anchor

            lbType.AutoSize = true;
            tbType.Anchor = tbType.Anchor | AnchorStyles.Right;

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            #endregion

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { lbType, tbType, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, lbType.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = tbType.Text;
            return dialogResult;
        }

        public static DialogResult InputBox(ref DateTime date) // ++++++
        {
            #region creating controls

            Form form = new Form();
            Label lbDay = new Label();
            ComboBox cbDay = new ComboBox();
            Label lbMonth = new Label();
            ComboBox cbMonth = new ComboBox();
            Label lbYear = new Label();
            ComboBox cbYear = new ComboBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            #endregion

            #region text and items of controls

            form.Text = "Добавление типа";

            lbDay.Text = "День:";
            for (int i = 1; i <= 31; i++)
            {
                cbDay.Items.Add(i);
            }
            cbDay.SelectedIndex = date.Day - 1;

            lbMonth.Text = "Месяц:";
            cbMonth.Items.AddRange(DateTimeFormatInfo.CurrentInfo.MonthNames);
            cbMonth.Items.RemoveAt(cbMonth.Items.Count - 1);
            cbMonth.SelectedIndex = date.Month - 1;

            lbYear.Text = "Год:";
            for (int i = 0; i < 20; i++)
            {
                cbYear.Items.Add(DateTime.Now.Year - i);
            }
            cbYear.SelectedIndex = DateTime.Now.Year - date.Year;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            #endregion

            #region bounds

            lbDay.SetBounds(32, 20, 124, 13);
            cbDay.SetBounds(35, 36, 40, 21);

            lbMonth.SetBounds(92, 20, 124, 13);
            cbMonth.SetBounds(95, 36, 90, 21);

            lbYear.SetBounds(202, 20, 124, 13);
            cbYear.SetBounds(205, 36, 50, 21);

            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            #endregion

            #region anchor

            lbDay.AutoSize = true;
            //cbDay.Anchor = cbDay.Anchor | AnchorStyles.Right;

            lbMonth.AutoSize = true;
            //cbMonth.Anchor = cbMonth.Anchor | AnchorStyles.Right;

            lbYear.AutoSize = true;
            //cbYear.Anchor = cbYear.Anchor | AnchorStyles.Right;

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            #endregion

            #region properties of date

            cbDay.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDay.IntegralHeight = false;
            cbDay.MaxDropDownItems = 6;

            cbMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMonth.IntegralHeight = false;
            cbMonth.MaxDropDownItems = 6;

            cbYear.DropDownStyle = ComboBoxStyle.DropDownList;
            cbYear.IntegralHeight = false;
            cbYear.MaxDropDownItems = 6;

            #endregion

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { lbDay, cbDay, lbMonth, cbMonth, lbYear, cbYear, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, lbYear.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            date = new DateTime(Convert.ToInt32(cbYear.SelectedItem.ToString()), cbMonth.SelectedIndex + 1, cbDay.SelectedIndex + 1);
            return dialogResult;
        }

        public static DialogResult InputBox(ref double price, ref double count, ref string acc, ref DateTime date, List<string> listAcc) // ++++++
        {
            #region creating controls

            Form form = new Form();
            Label lbPrice = new Label();
            TextBox tbPrice = new TextBox();
            Label lbCount = new Label();
            TextBox tbCount = new TextBox();
            Label lbAcc = new Label();
            ComboBox cbAcc = new ComboBox();
            Label lbDate = new Label();
            ComboBox cbDate = new ComboBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            #endregion

            #region text and items of controls

            form.Text = "Изменение";

            lbPrice.Text = "Цена:";
            tbPrice.Text = price.ToString();

            lbCount.Text = "Количество:";
            tbCount.Text = count.ToString();

            lbAcc.Text = "Счёт:";
            int c = 0;
            for (int i = 0; i < listAcc.Count; i++)
            {
                cbAcc.Items.Add(listAcc[i]);
                if(listAcc[i] == acc)
                {
                    c = i;
                }
            }
            cbAcc.SelectedIndex = c;

            lbDate.Text = "Дата:";
            for (int i = 0; i < 30; i++)
            {
                cbDate.Items.Add(DateTime.Now.AddDays(-i).ToShortDateString());
            }
            cbDate.SelectedIndex = 0;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            #endregion

            #region bounds

            lbPrice.SetBounds(9, 20, 372, 13);
            tbPrice.SetBounds(12, 36, 372, 20);

            lbCount.SetBounds(9, 59, 372, 13);
            tbCount.SetBounds(12, 75, 372, 20);

            lbAcc.SetBounds(9, 98, 372, 13);
            cbAcc.SetBounds(12, 114, 372, 20);

            lbDate.SetBounds(9, 137, 372, 13);
            cbDate.SetBounds(12, 153, 372, 20);

            buttonOk.SetBounds(228, 189, 75, 23);
            buttonCancel.SetBounds(309, 189, 75, 23);

            #endregion

            #region anchor

            lbPrice.AutoSize = true;
            tbPrice.Anchor = tbPrice.Anchor | AnchorStyles.Right;

            lbCount.AutoSize = true;
            tbCount.Anchor = tbCount.Anchor | AnchorStyles.Right;

            lbCount.AutoSize = true;
            cbAcc.Anchor = cbAcc.Anchor | AnchorStyles.Right;

            lbDate.AutoSize = true;
            cbDate.Anchor = cbDate.Anchor | AnchorStyles.Right;

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            #endregion

            #region properties of account and date

            cbAcc.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAcc.IntegralHeight = false;
            cbAcc.MaxDropDownItems = 6;

            cbDate.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDate.IntegralHeight = false;
            cbDate.MaxDropDownItems = 6;

            #endregion

            form.ClientSize = new Size(396, 224);
            form.Controls.AddRange(new Control[] { lbPrice, tbPrice, lbCount, tbCount, lbAcc, cbAcc,
                                                   lbDate, cbDate, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, lbPrice.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            price = Convert.ToDouble(StringToCorrectDoubleFormat(tbPrice.Text));
            count = Convert.ToDouble(StringToCorrectDoubleFormat(tbCount.Text));
            acc = cbAcc.SelectedItem.ToString();
            date = Convert.ToDateTime(cbDate.SelectedItem.ToString());
            return dialogResult;
        }

        public static DialogResult InputBox(ref string name, ref double cost, ref double count, ref string type, ref string importance, 
                                            List<string> listType, List<string> listImportance) // ++++++
        {
            #region creating controls

            Form form = new Form();
            Label lbName = new Label();
            TextBox tbName = new TextBox();
            Label lbCost = new Label();
            TextBox tbCost = new TextBox();
            Label lbCount = new Label();
            TextBox tbCount = new TextBox();
            Label lbType = new Label();
            ComboBox cbType = new ComboBox();
            Label lbImportance = new Label();
            ComboBox cbImportance = new ComboBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            #endregion

            #region text and items of controls

            form.Text = "Изменение";

            lbName.Text = "Название:";
            tbName.Text = name;

            lbCost.Text = "Стоимость:";
            tbCost.Text = Convert.ToString(cost);

            lbCount.Text = "Количество:";
            tbCount.Text = Convert.ToString(count);

            lbType.Text = "Тип:";
            foreach (string item in listType)
            {
                cbType.Items.Add(item);
            }
            cbType.SelectedIndex = cbType.Items.IndexOf(type);
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;

            lbImportance.Text = "Важность:";
            foreach (string item in listImportance)
            {
                cbImportance.Items.Add(item);
            }
            cbImportance.SelectedIndex = cbImportance.Items.IndexOf(importance);
            cbImportance.DropDownStyle = ComboBoxStyle.DropDownList;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            #endregion

            #region bounds

            lbName.SetBounds(9, 20, 372, 13);
            tbName.SetBounds(12, 36, 372, 20);

            lbCost.SetBounds(9, 59, 372, 13);
            tbCost.SetBounds(12, 75, 372, 20);

            lbCount.SetBounds(9, 98, 372, 13);
            tbCount.SetBounds(12, 114, 372, 20);

            lbType.SetBounds(9, 137, 372, 13);
            cbType.SetBounds(12, 153, 372, 21);

            lbImportance.SetBounds(9, 177, 372, 13);
            cbImportance.SetBounds(12, 193, 372, 21);

            buttonOk.SetBounds(228, 235, 75, 23);
            buttonCancel.SetBounds(309, 235, 75, 23);

            #endregion

            #region anchor

            lbName.AutoSize = true;
            tbName.Anchor = tbName.Anchor | AnchorStyles.Right;

            lbCost.AutoSize = true;
            tbCost.Anchor = tbName.Anchor | AnchorStyles.Right;

            lbCount.AutoSize = true;
            tbCount.Anchor = tbName.Anchor | AnchorStyles.Right;

            lbType.AutoSize = true;
            cbType.Anchor = cbType.Anchor | AnchorStyles.Right;

            lbImportance.AutoSize = true;
            cbImportance.Anchor = cbImportance.Anchor | AnchorStyles.Right;

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            #endregion

            form.ClientSize = new Size(396, 270);
            form.Controls.AddRange(new Control[] { lbName, tbName, lbCost, tbCost, lbCount, tbCount, lbType, cbType, lbImportance, cbImportance, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, lbName.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();

            name = tbName.Text;
            cost = Convert.ToDouble(StringToCorrectDoubleFormat(tbCost.Text));
            count = Convert.ToDouble(StringToCorrectDoubleFormat(tbCount.Text));
            type = cbType.SelectedItem.ToString();
            importance = cbImportance.SelectedItem.ToString();

            return dialogResult;
        }

        public static DialogResult InputBox(ref string name, ref double cost, ref double count, ref string type, ref string importance, ref string account, 
                                            ref DateTime date, List<string> listType, List<string> listImportance, List<string> listAccount) // ++++++
        {
            #region creating controls

            Form form = new Form();
            Label lbName = new Label();
            TextBox tbName = new TextBox();
            Label lbCost = new Label();
            TextBox tbCost = new TextBox();
            Label lbCount = new Label();
            TextBox tbCount = new TextBox();
            Label lbType = new Label();
            ComboBox cbType = new ComboBox();
            Label lbImportance = new Label();
            ComboBox cbImportance = new ComboBox();
            Label lbAccount = new Label();
            ComboBox cbAccount = new ComboBox();
            Label lbDay = new Label();
            ComboBox cbDay = new ComboBox();
            Label lbMonth = new Label();
            ComboBox cbMonth = new ComboBox();
            Label lbYear = new Label();
            ComboBox cbYear = new ComboBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            #endregion

            #region text and items of controls

            form.Text = "Изменение";

            lbName.Text = "Название:";
            tbName.Text = name;

            lbCost.Text = "Стоимость:";
            tbCost.Text = Convert.ToString(cost);

            lbCount.Text = "Количество:";
            tbCount.Text = Convert.ToString(count);

            lbType.Text = "Тип:";
            foreach (string item in listType)
            {
                cbType.Items.Add(item);
            }
            cbType.SelectedIndex = cbType.Items.IndexOf(type);
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;

            lbImportance.Text = "Важность:";
            foreach (string item in listImportance)
            {
                cbImportance.Items.Add(item);
            }
            cbImportance.SelectedIndex = cbImportance.Items.IndexOf(importance);
            cbImportance.DropDownStyle = ComboBoxStyle.DropDownList;

            lbAccount.Text = "Счёт:";
            foreach (string item in listAccount)
            {
                cbAccount.Items.Add(item);
            }
            cbAccount.SelectedIndex = cbAccount.Items.IndexOf(account);
            cbAccount.DropDownStyle = ComboBoxStyle.DropDownList;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

            lbDay.Text = "День:";
            for (int i = 1; i <= 31; i++)
            {
                cbDay.Items.Add(i);
            }
            cbDay.SelectedIndex = date.Day - 1;

            lbMonth.Text = "Месяц:";
            cbMonth.Items.AddRange(DateTimeFormatInfo.CurrentInfo.MonthNames);
            cbMonth.Items.RemoveAt(cbMonth.Items.Count - 1);
            cbMonth.SelectedIndex = date.Month - 1;

            lbYear.Text = "Год:";
            for (int i = 0; i < 20; i++)
            {
                cbYear.Items.Add(DateTime.Now.Year - i);
            }
            cbYear.SelectedIndex = DateTime.Now.Year - date.Year;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            #endregion

            #region bounds

            lbName.SetBounds(9, 20, 372, 13);
            tbName.SetBounds(12, 36, 372, 20);

            lbCost.SetBounds(9, 59, 372, 13);
            tbCost.SetBounds(12, 75, 372, 20);

            lbCount.SetBounds(9, 98, 372, 13);
            tbCount.SetBounds(12, 114, 372, 20);

            lbType.SetBounds(9, 137, 372, 13);
            cbType.SetBounds(12, 153, 372, 21);

            lbImportance.SetBounds(9, 177, 372, 13);
            cbImportance.SetBounds(12, 192, 372, 21);

            lbAccount.SetBounds(9, 215, 372, 13);
            cbAccount.SetBounds(12, 231, 372, 21);

            lbDay.SetBounds(32, 254, 124, 13);
            cbDay.SetBounds(35, 270, 40, 21);

            lbMonth.SetBounds(92, 254, 124, 13);
            cbMonth.SetBounds(95, 270, 90, 21);

            lbYear.SetBounds(202, 254, 124, 13);
            cbYear.SetBounds(205, 270, 50, 21);

            buttonOk.SetBounds(228, 312, 75, 23);
            buttonCancel.SetBounds(309, 312, 75, 23);

            #endregion

            #region anchor

            lbName.AutoSize = true;
            tbName.Anchor = tbName.Anchor | AnchorStyles.Right;

            lbCost.AutoSize = true;
            tbCost.Anchor = tbName.Anchor | AnchorStyles.Right;

            lbCount.AutoSize = true;
            tbCount.Anchor = tbName.Anchor | AnchorStyles.Right;

            lbType.AutoSize = true;
            cbType.Anchor = cbType.Anchor | AnchorStyles.Right;

            lbImportance.AutoSize = true;
            cbImportance.Anchor = cbImportance.Anchor | AnchorStyles.Right;

            lbAccount.AutoSize = true;
            cbAccount.Anchor = cbAccount.Anchor | AnchorStyles.Right;

            lbDay.AutoSize = true;
            //cbDay.Anchor = cbDay.Anchor | AnchorStyles.Right;

            lbMonth.AutoSize = true;
            //cbMonth.Anchor = cbMonth.Anchor | AnchorStyles.Right;

            lbYear.AutoSize = true;
            //cbYear.Anchor = cbYear.Anchor | AnchorStyles.Right;

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            #endregion

            #region properties of date

            cbDay.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDay.IntegralHeight = false;
            cbDay.MaxDropDownItems = 6;

            cbMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMonth.IntegralHeight = false;
            cbMonth.MaxDropDownItems = 6;

            cbYear.DropDownStyle = ComboBoxStyle.DropDownList;
            cbYear.IntegralHeight = false;
            cbYear.MaxDropDownItems = 6;

            #endregion

            form.ClientSize = new Size(396, 347);
            form.Controls.AddRange(new Control[] { lbName, tbName, lbCost, tbCost, lbCount, tbCount, lbType, cbType, lbImportance, cbImportance,
                lbAccount, cbAccount, lbDay, cbDay, lbMonth, cbMonth, lbYear, cbYear, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, lbName.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();

            name = tbName.Text;
            cost = Convert.ToDouble(StringToCorrectDoubleFormat(tbCost.Text));
            count = Convert.ToDouble(StringToCorrectDoubleFormat(tbCount.Text));
            type = cbType.SelectedItem.ToString();
            importance = cbImportance.SelectedItem.ToString();
            account = cbAccount.SelectedItem.ToString();
            date = new DateTime(Convert.ToInt32(cbYear.SelectedItem.ToString()), cbMonth.SelectedIndex + 1, cbDay.SelectedIndex + 1);

            return dialogResult;
        }
    }
}
