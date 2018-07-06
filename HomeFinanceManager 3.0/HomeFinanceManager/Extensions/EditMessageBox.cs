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
        private static string StringToCorrectDoubleFormat(string s) // ++++++
        {
            //Изменение строки в правильный формат для типа double
            if (s.IndexOf(',') == -1)
            {
                s = s.Replace('.', ',');
                return s;
            }
            return s;
        }

        public static DialogResult InputBox(out string value) // ++++++
        {
            #region creating controls

            var form = new Form();

            var lbAccount = new Label();
            var tbAccount = new TextBox();

            var buttonOk = new Button();
            var buttonCancel = new Button();

            buttonOk.Name = "buttonOk";
            buttonCancel.Name = "buttonCancel";

            tbAccount.TextChanged += tbAccount_TextChanged;

            #endregion

            #region adding controls

            form.Controls.AddRange(new Control[] { lbAccount, tbAccount, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            #endregion

            #region text and items of controls

            form.Text = "Добавление счёта";

            lbAccount.Text = "Новый счёт:";
            //tbAccount.Text = value;

            buttonOk.Enabled = false;
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            #endregion

            #region bounds

            lbAccount.SetBounds(9, 20, 100, 13);
            tbAccount.SetBounds(12, 36, 200, 20);
            buttonOk.SetBounds(58, 72, 75, 23);
            buttonCancel.SetBounds(139, 72, 75, 23);

            #endregion

            #region form parameterizing

            form.ClientSize = new Size(form.Controls[form.Controls.Count - 1].Right + 10, form.Controls[form.Controls.Count - 1].Bottom + 10);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            #endregion

            var dialogResult = form.ShowDialog();

            value = tbAccount.Text;

            return dialogResult;
        }

        public static DialogResult InputBox(out DateTime date) // ++++++
        {
            #region creating controls

            var form = new Form();

            var dController = new DateController();

            var buttonOk = new Button();
            var buttonCancel = new Button();

            dController.Name = "dController";
            buttonOk.Name = "buttonOk";
            buttonCancel.Name = "buttonCancel";

            #endregion

            #region adding controls

            form.Controls.AddRange(new Control[] { dController, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            #endregion

            #region text and items of controls

            form.Text = "Изменение даты";

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            #endregion

            #region bounds

            dController.SetBounds(32, 20, 223, 37);

            dController.LbDay.SetBounds(0, 0, 50, 13);
            dController.CbDay.SetBounds(3, 16, 40, 21);
            dController.LbMonth.SetBounds(60, 0, 50, 13);
            dController.CbMonth.SetBounds(63, 16, 90, 21);
            dController.LbYear.SetBounds(170, 0, 50, 13);
            dController.CbYear.SetBounds(173, 16, 50, 21);

            buttonOk.SetBounds(121, 78, 75, 23);
            buttonCancel.SetBounds(202, 78, 75, 23);

            #endregion

            #region form parameterizing

            form.ClientSize = new Size(form.Controls[form.Controls.Count - 1].Right + 10, form.Controls[form.Controls.Count - 1].Bottom + 10);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            #endregion

            var dialogResult = form.ShowDialog();
            date = dController.GetDate();
            return dialogResult;
        }

        public static DialogResult InputBox(ref double price, ref double count, ref string acc, out DateTime date, List<string> listAcc) // ++++++
        {
            #region creating controls

            var form = new Form();

            var lbPrice = new Label();
            var tbPrice = new TextBox();

            var lbCount = new Label();
            var tbCount = new TextBox();

            var lbAcc = new Label();
            var cbAcc = new ComboBox();

            var dController = new DateController();

            var buttonOk = new Button();
            var buttonCancel = new Button();

            dController.Name = "dController";
            buttonOk.Name = "buttonOk";
            buttonCancel.Name = "buttonCancel";

            #endregion

            #region adding controls

            form.Controls.AddRange(new Control[] { lbPrice, tbPrice, lbCount, tbCount, lbAcc, cbAcc,
                dController, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            #endregion

            #region text and items of controls

            form.Text = "Изменение";

            lbPrice.Text = "Цена:";
            tbPrice.Text = price.ToString();

            lbCount.Text = "Количество:";
            tbCount.Text = count.ToString();

            lbAcc.Text = "Счёт:";
            var c = 0;
            for (var i = 0; i < listAcc.Count; i++)
            {
                cbAcc.Items.Add(listAcc[i]);
                if(listAcc[i] == acc)
                {
                    c = i;
                }
            }
            cbAcc.SelectedIndex = c;
            cbAcc.DropDownStyle = ComboBoxStyle.DropDownList;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            #endregion

            #region bounds

            lbPrice.SetBounds(9, 20, 260, 13);
            tbPrice.SetBounds(12, 36, 260, 20);

            lbCount.SetBounds(9, 59, 260, 13);
            tbCount.SetBounds(12, 75, 260, 20);

            lbAcc.SetBounds(9, 98, 260, 13);
            cbAcc.SetBounds(12, 114, 260, 20);

            dController.SetBounds(32, 144, 223, 37);

            dController.LbDay.SetBounds(0, 0, 50, 13);
            dController.CbDay.SetBounds(3, 16, 40, 21);
            dController.LbMonth.SetBounds(60, 0, 50, 13);
            dController.CbMonth.SetBounds(63, 16, 90, 21);
            dController.LbYear.SetBounds(170, 0, 50, 13);
            dController.CbYear.SetBounds(173, 16, 50, 21);

            buttonOk.SetBounds(121, 202, 75, 23);
            buttonCancel.SetBounds(202, 202, 75, 23);

            #endregion

            #region form parameterizing

            form.ClientSize = new Size(form.Controls[form.Controls.Count - 1].Right + 10, form.Controls[form.Controls.Count - 1].Bottom + 10);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            #endregion

            var dialogResult = form.ShowDialog();
            price = Convert.ToDouble(StringToCorrectDoubleFormat(tbPrice.Text));
            count = Convert.ToDouble(StringToCorrectDoubleFormat(tbCount.Text));
            acc = cbAcc.SelectedItem.ToString();
            date = dController.GetDate();
            return dialogResult;
        }

        public static DialogResult InputBox(ref string name, ref double cost, ref double count, ref string type, ref string importance, 
                                            List<string> listType, List<string> listImportance) // ++++++
        {
            #region creating controls

            var form = new Form();

            var lbName = new Label();
            var tbName = new TextBox();

            var lbCost = new Label();
            var tbCost = new TextBox();

            var lbCount = new Label();
            var tbCount = new TextBox();

            var lbType = new Label();
            var cbType = new ComboBox();

            var lbImportance = new Label();
            var cbImportance = new ComboBox();

            var buttonOk = new Button();
            var buttonCancel = new Button();

            buttonOk.Name = "buttonOk";
            buttonCancel.Name = "buttonCancel";

            #endregion

            #region adding controls

            form.Controls.AddRange(new Control[] { lbName, tbName, lbCost, tbCost, lbCount, tbCount, lbType, cbType, lbImportance, cbImportance, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            #endregion

            #region text and items of controls

            form.Text = "Изменение";

            lbName.Text = "Название:";
            tbName.Text = name;

            lbCost.Text = "Стоимость:";
            tbCost.Text = Convert.ToString(cost, CultureInfo.CurrentCulture);

            lbCount.Text = "Количество:";
            tbCount.Text = Convert.ToString(count, CultureInfo.CurrentCulture);

            lbType.Text = "Тип:";
            foreach (var item in listType)
            {
                cbType.Items.Add(item);
            }
            cbType.SelectedIndex = cbType.Items.IndexOf(type);
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;

            lbImportance.Text = "Важность:";
            foreach (var item in listImportance)
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

            lbName.SetBounds(9, 20, 100, 13);
            tbName.SetBounds(12, 36, 200, 20);

            lbCost.SetBounds(9, 59, 100, 13);
            tbCost.SetBounds(12, 75, 200, 20);

            lbCount.SetBounds(9, 98, 100, 13);
            tbCount.SetBounds(12, 114, 200, 20);

            lbType.SetBounds(9, 137, 100, 13);
            cbType.SetBounds(12, 153, 200, 21);

            lbImportance.SetBounds(9, 177, 100, 13);
            cbImportance.SetBounds(12, 193, 200, 21);

            buttonOk.SetBounds(58, 235, 75, 23);
            buttonCancel.SetBounds(139, 235, 75, 23);

            #endregion

            #region form parameterizing

            form.ClientSize = new Size(form.Controls[form.Controls.Count - 1].Right + 10, form.Controls[form.Controls.Count - 1].Bottom + 10);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            #endregion

            var dialogResult = form.ShowDialog();

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

            var form = new Form();

            var lbName = new Label();
            var tbName = new TextBox();

            var lbCost = new Label();
            var tbCost = new TextBox();

            var lbCount = new Label();
            var tbCount = new TextBox();

            var lbType = new Label();
            var cbType = new ComboBox();

            var lbImportance = new Label();
            var cbImportance = new ComboBox();

            var lbAccount = new Label();
            var cbAccount = new ComboBox();

            var dController = new DateController();

            var buttonOk = new Button();
            var buttonCancel = new Button();

            dController.Name = "dController";
            buttonOk.Name = "buttonOk";
            buttonCancel.Name = "buttonCancel";

            #endregion

            #region adding controls

            form.Controls.AddRange(new Control[] { lbName, tbName, lbCost, tbCost, lbCount, tbCount, lbType, cbType, lbImportance, cbImportance,
                lbAccount, cbAccount, dController, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            #endregion

            #region text and items of controls

            form.Text = "Изменение";

            lbName.Text = "Название:";
            tbName.Text = name;

            lbCost.Text = "Стоимость:";
            tbCost.Text = Convert.ToString(cost, CultureInfo.CurrentCulture);

            lbCount.Text = "Количество:";
            tbCount.Text = Convert.ToString(count, CultureInfo.CurrentCulture);

            lbType.Text = "Тип:";
            foreach (var item in listType)
            {
                cbType.Items.Add(item);
            }
            cbType.SelectedIndex = cbType.Items.IndexOf(type);
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;

            lbImportance.Text = "Важность:";
            foreach (var item in listImportance)
            {
                cbImportance.Items.Add(item);
            }
            cbImportance.SelectedIndex = cbImportance.Items.IndexOf(importance);
            cbImportance.DropDownStyle = ComboBoxStyle.DropDownList;

            lbAccount.Text = "Счёт:";
            foreach (var item in listAccount)
            {
                cbAccount.Items.Add(item);
            }
            cbAccount.SelectedIndex = cbAccount.Items.IndexOf(account);
            cbAccount.DropDownStyle = ComboBoxStyle.DropDownList;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

            dController.SetDate(date);

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            #endregion

            #region bounds

            lbName.SetBounds(9, 20, 260, 13);
            tbName.SetBounds(12, 36, 260, 20);

            lbCost.SetBounds(9, 59, 260, 13);
            tbCost.SetBounds(12, 75, 260, 20);

            lbCount.SetBounds(9, 98, 260, 13);
            tbCount.SetBounds(12, 114, 260, 20);

            lbType.SetBounds(9, 137, 260, 13);
            cbType.SetBounds(12, 153, 260, 21);

            lbImportance.SetBounds(9, 177, 260, 13);
            cbImportance.SetBounds(12, 192, 260, 21);

            lbAccount.SetBounds(9, 215, 260, 13);
            cbAccount.SetBounds(12, 231, 260, 21);

            dController.SetBounds(32, 254, 223, 37);

            dController.LbDay.SetBounds(0, 0, 50, 13);
            dController.CbDay.SetBounds(3, 16, 40, 21);
            dController.LbMonth.SetBounds(60, 0, 50, 13);
            dController.CbMonth.SetBounds(63, 16, 90, 21);
            dController.LbYear.SetBounds(170, 0, 50, 13);
            dController.CbYear.SetBounds(173, 16, 50, 21);

            buttonOk.SetBounds(121, 312, 75, 23);
            buttonCancel.SetBounds(202, 312, 75, 23);

            #endregion

            #region form parameterizing

            form.ClientSize = new Size(form.Controls[form.Controls.Count - 1].Right + 10, form.Controls[form.Controls.Count - 1].Bottom + 10);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            #endregion

            var dialogResult = form.ShowDialog();

            name = tbName.Text;
            cost = Convert.ToDouble(StringToCorrectDoubleFormat(tbCost.Text));
            count = Convert.ToDouble(StringToCorrectDoubleFormat(tbCount.Text));
            type = cbType.SelectedItem.ToString();
            importance = cbImportance.SelectedItem.ToString();
            account = cbAccount.SelectedItem.ToString();
            date = dController.GetDate();

            return dialogResult;
        }

        public static DialogResult InputBox(ref DateTime dateMin, ref DateTime dateMax) // ++++++
        {
            #region creating controls

            var form = new Form();
            var lbMin = new Label();
            var dControllerMin = new DateController(dateMin);
            var lbMax = new Label();
            var dControllerMax = new DateController(dateMax);
            var buttonOk = new Button();
            var buttonCancel = new Button();

            dControllerMin.Name = "dControllerMin";
            dControllerMax.Name = "dControllerMax";
            buttonOk.Name = "buttonOk";
            buttonCancel.Name = "buttonCancel";

            #endregion

            #region text and items of controls

            form.Text = "Задать интервал";

            lbMin.Text = "От:";
            lbMax.Text = "До:";

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            #endregion

            #region adding controls

            form.Controls.AddRange(new Control[] { lbMin, dControllerMin, lbMax, dControllerMax, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            #endregion

            #region bounds

            for (var i = 0; i < form.Controls.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        form.Controls[i].SetBounds(32, 10, 50, 13);
                        break;
                    case 1:
                        ((DateController)form.Controls[i]).SetBounds(32, 26, 223, 37);

                        ((DateController)form.Controls[i]).LbDay.SetBounds(0, 0, 50, 13);
                        ((DateController)form.Controls[i]).CbDay.SetBounds(3, 16, 40, 21);
                        ((DateController)form.Controls[i]).LbMonth.SetBounds(60, 0, 50, 13);
                        ((DateController)form.Controls[i]).CbMonth.SetBounds(63, 16, 90, 21);
                        ((DateController)form.Controls[i]).LbYear.SetBounds(170, 0, 50, 13);
                        ((DateController)form.Controls[i]).CbYear.SetBounds(173, 16, 50, 21);
                        break;
                    case 2:
                        form.Controls[i].SetBounds(32, 76, 50, 13);
                        break;
                    case 3:
                        ((DateController)form.Controls[i]).SetBounds(32, 92, 223, 37);

                        ((DateController)form.Controls[i]).LbDay.SetBounds(0, 0, 50, 13);
                        ((DateController)form.Controls[i]).CbDay.SetBounds(3, 16, 40, 21);
                        ((DateController)form.Controls[i]).LbMonth.SetBounds(60, 0, 50, 13);
                        ((DateController)form.Controls[i]).CbMonth.SetBounds(63, 16, 90, 21);
                        ((DateController)form.Controls[i]).LbYear.SetBounds(170, 0, 50, 13);
                        ((DateController)form.Controls[i]).CbYear.SetBounds(173, 16, 50, 21);
                        break;
                    case 4:
                        form.Controls[i].SetBounds(128, 144, 75, 23);
                        break;
                    case 5:
                        form.Controls[i].SetBounds(209, 144, 75, 23);
                        break;
                    default:
                        break;
                }
            }

            #endregion

            #region anchor

            #endregion

            #region form parameterizing

            form.ClientSize = new Size(form.Controls[form.Controls.Count - 1].Right + 10, form.Controls[form.Controls.Count - 1].Bottom + 10);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            #endregion

            var dialogResult = form.ShowDialog();

            dateMin = dControllerMin.GetDate();
            dateMax = dControllerMax.GetDate();

            return dialogResult;
        }

        private static void tbAccount_TextChanged(object sender, EventArgs e) // ++++++
        {
            if (((TextBox) sender).Text == "")
            {
                ((Control) sender).FindForm().Controls["buttonOk"].Enabled = false;
            }
            else
            {
                ((Control)sender).FindForm().Controls["buttonOk"].Enabled = true;
            }
        }
    }
}