#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HomeFinanceManager.DataModels;
using System.IO;
using XmlWriterReader;
using TxtWriterReader;
using HomeFinanceManager.Extensions;
using System.Globalization;

#endregion

namespace HomeFinanceManager
{
    public partial class FormMain : FormBase
    {
        #region fields

        private readonly Form _fl; // Pointer to a FormLog
        private int _selRowNumLeft; // Number of selected row at dgvLeft
        private int _selRowNumRight; // Number of selected row at dgvRight
        private int _selRowNumShow; // Number of selected row at dgvShow
        private string _dgvContext;

        #endregion

        #region constructors

        public FormMain() // ++++++
        {
            InitializeComponent();
        }
        public FormMain(Form fl) /*++++++*/: this()
        {
            _fl = fl;
            _fl.Hide();
        }

        #endregion

        #region additional methods
        public bool CheckForDot(string s) // ++++++
        {
            //Проверка на правильность формата строки с точкой или запятой
            return (s[0] != ',') && (s[s.Length - 1] != ',') && (s[0] != '.') && (s[s.Length - 1] != '.') && (s.IndexOf(',') == s.LastIndexOf(',')) && (s.IndexOf('.') == s.LastIndexOf('.'));
        }

        public string StringToCorrectDoubleFormat(string s) // ++++++
        {
            //Изменение строки в правильный формат для типа double
            if (s.IndexOf(',') == -1)
            {
                s = s.Replace('.', ',');
                return s;
            }
            return s;
        }

        public string DoubleToCorrectString(double d) // ++++++
        {
            //Изменение строки из правильного формата для типа double в правильный формат для типа string
            var str = (Convert.ToString(d, CultureInfo.CurrentCulture)).Replace('.', ',');
            return str;
        }

        public string FirstWordFromString(string s) // ++++++
        {
            return s.TakeWhile(t => t != ' ').Aggregate("", (current, t) => current + t);
        }

        public bool ClearControlsForCombiningBills(bool b) // ++++++
        {
            try
            {
                if (b)
                {
                    gbAdd.Enabled = false;
                    gbChange.Enabled = false;
                    btnLogout.Enabled = false;
                    tpInfo.Parent = null;
                    return true;
                }
                gbAdd.Enabled = true;
                gbChange.Enabled = true;
                btnLogout.Enabled = true;
                tpInfo.Parent = tabControl;
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\nОшибка в методе ClearControlsForGbConfirm()");
                return false;
            }
        }

        public bool ClearControlsForAddingGoods() // ++++++
        {
            try
            {
                tbAddName.Clear();
                tbAddCount.Clear();
                tbAddCost.Clear();
                cbAddType.SelectedIndex = -1;
                cbAddImportance.SelectedIndex = -1;
                dgvLeft.CurrentCell = null;
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\nОшибка в методе ClearControlsForAddingGoods()");
                return false;
            }
        }

        public bool ClearControlsForFilterDgvShow() // ++++++
        {
            cbDay.SelectedIndex = 0;
            cbMonth.SelectedIndex = 0;
            cbYear.SelectedIndex = 0;
            cbLastTime.SelectedIndex = 0;
            chboxImportanceInfo.Checked = true;
            chboxTypeInfo.Checked = true;
            chboxAccountInfo.Checked = true;
            nudMax.Maximum = (int)Good.MaxSum + 1;
            nudMax.Value = (int)Good.MaxSum + 1;
            nudMin.Value = 0;
            nudMin.Maximum = nudMax.Maximum;
            FillingListbFilterForFullDgvShow();
            dgvShow.CurrentCell = null;
            return true;
        }

        public bool FillingListbFilterForFullDgvShow() // ++++++
        {
            listbFilter.Items.Clear();
            listbFilter.Items.Add("За последнее время: " + cbLastTime.SelectedItem.ToString() + ";");
            listbFilter.Items.Add("По важности: ");
            foreach (var item in clbImportanceInfo.CheckedItems)
            {
                listbFilter.Items[listbFilter.Items.Count - 1] += FirstWordFromString(item.ToString()) + "; ";
            }
            listbFilter.Items.Add("По типу: ");
            foreach (var item in clbTypeInfo.CheckedItems)
            {
                listbFilter.Items[listbFilter.Items.Count - 1] += item.ToString() + "; ";
            }
            listbFilter.Items.Add("По счёту: ");
            foreach (var item in clbAccountInfo.CheckedItems)
            {
                listbFilter.Items[listbFilter.Items.Count - 1] += item.ToString() + "; ";
            }
            listbFilter.Items.Add("По сумме: от 0 грн. до " + nudMax.Value + " грн.");
            return true;
        }
        #endregion

        #region form loading/closing

        private void FormMain_Load(object sender, EventArgs e) // ++++++
        {

            #region checking directory existing

            if (!Directory.Exists(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login))
            {
                Directory.CreateDirectory(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login);
            }
            if (!Directory.Exists(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\_Backup_\\" + CurrentUser.Login))
            {
                Directory.CreateDirectory(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\_Backup_\\" + CurrentUser.Login);
            }
            
            #endregion

            #region reading from XML-file All

            if (File.Exists(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\" + CurrentUser.Login + ".xml"))
            {
                try
                {
                    Good.listAll = XmlReader<Good>.Read(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\" + CurrentUser.Login + ".xml");
                }
                catch (Exception ex)
                {
                    XmlReader<Good>.condition = false;
                    MessageBox.Show("Ошибка при чтении файла: " + CurrentUser.Login + ".xml" + ".\n\n" + ex.Message);
                    Close();
                }
            }
            
            #endregion

            #region reading from TXT-file

            if (File.Exists(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\TypeOf'" + CurrentUser.Login + "'.txt"))
            {
                var list = TxtReader.Read(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\TypeOf'" + CurrentUser.Login + "'.txt");
                for (byte i = 0; i < list.Count; i++)
                {
                    Good.typeList.Add(new KeyValuePair<string, byte>(list[i], i));
                }
            }
            else
            {
                File.CreateText(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\TypeOf'" + CurrentUser.Login + "'.txt");
            }

            foreach (var item in Good.typeList)
            {
                cbAddType.Items.Insert(cbAddType.Items.Count - 1, item.Key);
                clbTypeInfo.Items.Insert(clbTypeInfo.Items.Count, item.Key);
            }
            
            #endregion

            #region reading from XML-file Variants

            if (File.Exists(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\VariantOf'" + CurrentUser.Login + "'.xml"))
            {
                try
                {
                    Good.listVariant = XmlReader<Good>.Read(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\VariantOf'" + CurrentUser.Login + "'.xml");
                }
                catch (Exception ex)
                {
                    XmlReader<Good>.condition = false;
                    MessageBox.Show("Ошибка при чтении файла: VariantOf'" + CurrentUser.Login + "'.xml" + ".\n\n" + ex.Message);
                    Close();
                }
            }

            #endregion

            #region copying files for backup

            if (File.Exists(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\" + CurrentUser.Login + ".xml"))
            {
                try
                {
                    File.Copy(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\" + CurrentUser.Login + ".xml", Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\_Backup_\\" + CurrentUser.Login + "\\" + CurrentUser.Login + ".xml", true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при копировании файла: " + CurrentUser.Login + ".xml" + ".\n\n" + ex.Message);
                    Close();
                }
            }
            if (File.Exists(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\" + CurrentUser.Login + ".xml"))
            {
                try
                {
                    File.Copy(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\TypeOf'" + CurrentUser.Login + "'.txt", Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\_Backup_\\" + CurrentUser.Login + "\\TypeOf'" + CurrentUser.Login + "'.txt", true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при копировании файла: " + CurrentUser.Login + ".txt" + ".\n\n" + ex.Message);
                    Close();
                }
            }
            if (File.Exists(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\" + CurrentUser.Login + ".xml"))
            {
                try
                {
                    File.Copy(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\VariantOf'" + CurrentUser.Login + "'.xml", Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\_Backup_\\" + CurrentUser.Login + "\\VariantOf'" + CurrentUser.Login + "'.xml", true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при копировании файла: VariantOf'" + CurrentUser.Login + "'.xml" + ".\n\n" + ex.Message);
                    Close();
                }
            }
            
            #endregion

            #region loading Accounts

            Account.accList = Conn.SelectAccountByLogin(CurrentUser.Login);
            foreach (var item in Account.accList)
            {
                cbAddAccounts.Items.Add(item.Name);
                cbCurrentAccount.Items.Insert(cbCurrentAccount.Items.Count - 1, item.Name);
                cbAccountForTransfer.Items.Insert(cbAccountForTransfer.Items.Count, item.Name);
                clbAccountInfo.Items.Add(item.Name, true);
            }
            cbAddAccounts.SelectedIndex = 0;
            cbCurrentAccount.SelectedIndex = 0;
            cbAccountForTransfer.SelectedIndex = 0;

            #endregion

            #region loading SumOfCurrentUser

            tbCurrentSum.Text = Conn.SelectSumByIdFromBills(CurrentUser.IdBalance).ToString("N");
            
            #endregion

            #region showing XML-file All at dgvShow

            Good.MaxSum = 0;
            foreach (var item in Good.listAll)
            {
                if ((item.Price * item.Count) > Good.MaxSum)
                {
                    Good.MaxSum = item.Price * item.Count;
                }
                Good.Sum += item.Price * item.Count;
                dgvShow.Rows.Add(item.Name, item.Price, item.Count, item.Price * item.Count, 
                                 item.Type, item.Importance, item.Acc, item.Time);
            }
            Good.listAll.Clear();
            dgvShow.Sort(dgvShow.Columns[clShowDate.Name], ListSortDirection.Descending);
            tbSum.Text = Convert.ToString(Math.Round(Good.Sum, 2), CultureInfo.CurrentCulture);
            
            #endregion

            #region showing XML-file Variants at dgvLeft

            foreach (var item in Good.listVariant)
            {
                dgvLeft.Rows.Add(item.Name, item.Price, item.Count, item.Price * item.Count, 
                                 item.Type, item.Importance, item.Acc);
            }
            Good.listVariant.Clear();
            dgvLeft.Sort(dgvLeft.Columns[clLeftName.Name], ListSortDirection.Ascending);
            dgvLeft.CurrentCell = null;
            
            #endregion

            #region filling cbDate

            for (var i = 0; i < 30; i++)
            {
                cbAddDate.Items.Add(DateTime.Now.AddDays(-i).Date);
            }
            cbAddDate.SelectedIndex = 0;

            #endregion

            #region preparation controls of filter

            ClearControlsForFilterDgvShow();

            var dt = DateTime.Now;
            for (var i = 0; i < 20; i++)
            {
                cbYear.Items.Add(dt.Year - i);
            }
            
            #endregion

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) // ++++++
        {
            if (XmlReader<Good>.condition)
            {
                #region saving typeList
                var list = Good.typeList.Select(t => t.Key).ToList();
                TxtWriter.Write(list, Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\TypeOf'" + CurrentUser.Login + "'.txt");
                #endregion

                #region saving listVariant
                foreach (DataGridViewRow row in dgvLeft.Rows)
                {
                    Good.listVariant.Add(new Good(Convert.ToString(row.Cells[0].Value),
                                                   Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[1].Value))),
                                                   Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[2].Value))),
                                                   Convert.ToString(row.Cells[4].Value), Convert.ToString(row.Cells[5].Value),
                                                   Convert.ToString(row.Cells[6].Value)));
                }
                XmlWriter<Good>.Write(Good.listVariant, Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\VariantOf'" + CurrentUser.Login + "'.xml");
                #endregion

                #region saving listAll
                dgvShow.Sort(dgvShow.Columns[clShowDate.Name], ListSortDirection.Ascending);
                foreach (DataGridViewRow row in dgvShow.Rows)
                {
                    Good.listAll.Add((new Good(Convert.ToString(row.Cells[0].Value),
                                                   Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[1].Value))),
                                                   Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[2].Value))),
                                                   Convert.ToString(row.Cells[4].Value), Convert.ToString(row.Cells[5].Value),
                                                   Convert.ToString(row.Cells[6].Value), Convert.ToDateTime(row.Cells[7].Value))));
                }
                XmlWriter<Good>.Write(Good.listAll, Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\" + CurrentUser.Login + ".xml");
                #endregion

                #region clearing all lists
                Good.Sum = 0;
                Good.typeList.Clear();
                Good.listAll.Clear();
                Good.listVariant.Clear();
                Account.accList.Clear();
                #endregion

                #region removing notifyIcon from tray

                if (IconTray != null)
                {
                    IconTray.Visible = false;
                    IconTray.Dispose();
                }

                #endregion

                if (_fl.Visible == false)
                {
                    _fl.Close();
                }
            }
            else
            {
                Good.Sum = 0;
                XmlReader<Good>.condition = true;
                _fl.Show();
            }
        }

        #endregion

        #region Icon

        private void Icon_MouseDoubleClick(object sender, MouseEventArgs e) // ++++++
        {
            WindowState = FormWindowState.Normal;
        }

        #endregion

        #region tabControl

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            if (tabControl.SelectedIndex == 0)
            {
                ClearControlsForAddingGoods();
            }
            else if (tabControl.SelectedIndex == 1)
            {
                btnCancelInfo_Click(sender, e);
            }
        }

        #endregion

        #region contextMenu

        private void cmsDgv_ItemClicked(object sender, ToolStripItemClickedEventArgs e) // ++++++
        {
            if (e.ClickedItem == tsmiDgvEdit)
            {
                DataGridViewRow row;
                var dt = new DateTime();
                if (_dgvContext == dgvLeft.Name)
                {
                    row = dgvLeft.Rows[_selRowNumLeft];
                }
                else if (_dgvContext == dgvRight.Name)
                {
                    row = dgvRight.Rows[_selRowNumRight];
                    dt = Convert.ToDateTime(row.Cells[7].Value);
                }
                else if (_dgvContext == dgvShow.Name)
                {
                    if (dgvShow.SelectedRows.Count == 1)
                    {
                        row = dgvShow.Rows[_selRowNumShow];
                        dt = Convert.ToDateTime(row.Cells[7].Value);
                    }
                    else
                    {
                        dt = DateTime.Now;
                        if (EditMessageBox.InputBox(ref dt) != DialogResult.OK)
                        {
                            return;
                        }
                        foreach (DataGridViewRow item in dgvShow.SelectedRows)
                        {
                            item.Cells[7].Value = dt;
                        }
                        return;
                    }
                }
                else
                {
                    row = new DataGridViewRow();
                }
                var name = Convert.ToString(row.Cells[0].Value);
                var cost = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[1].Value)));
                var count = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[2].Value)));
                var type = Convert.ToString(row.Cells[4].Value);
                var imp = Convert.ToString(row.Cells[5].Value);
                var acc = Convert.ToString(row.Cells[6].Value);
                var listType = new List<string>();
                foreach (var item in cbAddType.Items)
                {
                    listType.Add(item.ToString());
                }
                var listImp = new List<string>();
                foreach (var item in cbAddImportance.Items)
                {
                    listImp.Add(item.ToString());
                }
                var listAcc = new List<string>();
                foreach (var item in cbCurrentAccount.Items)
                {
                    listAcc.Add(item.ToString());
                }

                if (_dgvContext == dgvLeft.Name)
                {
                    if (EditMessageBox.InputBox(ref name, ref cost, ref count, ref type, ref imp, listType, listImp) != DialogResult.OK)
                    {
                        return;
                    }
                }
                else if ((_dgvContext == dgvShow.Name) || (_dgvContext == dgvRight.Name))
                {
                    if (EditMessageBox.InputBox(ref name, ref cost, ref count, ref type, ref imp, ref acc, ref dt, listType, listImp, listAcc) != DialogResult.OK)
                    {
                        return;
                    }
                }
                row.Cells[0].Value = name;
                row.Cells[1].Value = cost;
                row.Cells[2].Value = count;
                row.Cells[3].Value = cost * count;
                row.Cells[4].Value = type;
                row.Cells[5].Value = imp;
                row.Cells[6].Value = acc;
                if ((_dgvContext == dgvShow.Name) || (_dgvContext == dgvRight.Name))
                {
                    row.Cells[7].Value = dt;
                }
            }
            if (e.ClickedItem == tsmiDgvDelete)
            {
                if (_dgvContext == dgvLeft.Name)
                {
                    dgvLeft.Rows.Remove(dgvLeft.SelectedRows[0]);
                }
                else if (_dgvContext == dgvRight.Name)
                {
                    Good.listAdd.RemoveAt(_selRowNumRight);
                    dgvRight.Rows.Remove(dgvRight.SelectedRows[0]);
                }
                else if (_dgvContext == dgvShow.Name)
                {
                    dgvShow.Rows.Remove(dgvShow.SelectedRows[0]);
                }
            }
        }

        #endregion

        #region button Logout

        private void btnLogout_Click(object sender, EventArgs e) // ++++++
        {
            _fl.Show();
            Close();
        }

        #endregion

        #region tbAdd

        #region changeSum

        private void cbCurrentAccount_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            if (cbCurrentAccount.SelectedIndex == cbCurrentAccount.Items.Count - 1)
            {
                tbCurrentAccountSum.Text = "";
                var name = "";
                if (EditMessageBox.InputBox("Добавление счёта", "Новый счёт:", ref name) != DialogResult.OK)
                {
                    cbCurrentAccount.SelectedIndex = 0;
                    return;
                }
                var b = true;
                var acc = Conn.AddAccount(CurrentUser.Login, name);
                foreach (var item in Account.accList)
                {
                    if (item.Id == acc.Id)
                    {
                        b = false;
                    }
                }
                if (b)
                {
                    Account.accList.Add(acc);
                    cbAddAccounts.Items.Add(name);
                    cbAddAccounts.SelectedIndex = cbAddAccounts.Items.Count - 1;
                    cbCurrentAccount.Items.Insert(cbCurrentAccount.Items.Count - 1, name);
                    cbCurrentAccount.SelectedIndex = cbCurrentAccount.Items.Count - 2;
                    return;
                }
                for (var i = 0; i < cbCurrentAccount.Items.Count - 1; i++)
                {
                    if (cbCurrentAccount.Items[i].ToString() == name)
                    {
                        cbCurrentAccount.SelectedIndex = i;
                    }
                }
                return;
            }
            else
            {
                tbCurrentAccountSum.Text = Account.accList[cbCurrentAccount.SelectedIndex].Sum.ToString("N");
            }
        }

        private void cbAccountForTransfer_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            tbAccountForTransferSum.Text = Account.accList[cbAccountForTransfer.SelectedIndex].Sum.ToString("N");
        }

        private void btnChangeSum_Click(object sender, EventArgs e) // ++++++
        {
            if (tbChangeSum.Text != "")
            {
                if ((!tbChangeSum.Text.Contains('+')) && (!tbChangeSum.Text.Contains('-')))
                {
                    if (CheckForDot(tbChangeSum.Text))
                    {
                        Conn.UpdateSumBySubtractionAtBills(CurrentUser.Login, Account.accList[cbCurrentAccount.SelectedIndex].Sum);
                        Account.accList[cbCurrentAccount.SelectedIndex].Sum = Conn.UpdateSumAtAccounts(CurrentUser.Login, cbCurrentAccount.SelectedItem.ToString(), Math.Round(Convert.ToDouble(StringToCorrectDoubleFormat(tbChangeSum.Text)), 2)).Sum;
                        tbCurrentSum.Text = Conn.UpdateSumBySummingAtBills(CurrentUser.Login, Account.accList[cbCurrentAccount.SelectedIndex].Sum).ToString("N");
                        tbCurrentAccountSum.Text = Convert.ToDouble(StringToCorrectDoubleFormat(tbChangeSum.Text)).ToString("N");
                        if (cbAddAccounts.SelectedIndex == cbCurrentAccount.SelectedIndex)
                        {
                            tbAddAccountSum.Text = tbCurrentAccountSum.Text;
                        }
                        if (cbAccountForTransfer.SelectedIndex == cbCurrentAccount.SelectedIndex)
                        {
                            tbAccountForTransferSum.Text = tbCurrentAccountSum.Text;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправильный ввод суммы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if ((tbChangeSum.Text[0] == '+') && (tbChangeSum.Text.IndexOf('+') == tbChangeSum.Text.LastIndexOf('+')) && (!tbChangeSum.Text.Contains('-')))
                    {
                        var sum = Math.Round(Convert.ToDouble(StringToCorrectDoubleFormat(tbChangeSum.Text.Remove(0, 1))), 2, MidpointRounding.AwayFromZero);
                        tbCurrentSum.Text = Conn.UpdateSumBySummingAtBills(CurrentUser.Login, sum).ToString("N");
                        Account.accList[cbCurrentAccount.SelectedIndex].Sum = Conn.UpdateSumBySummingAtAccounts(CurrentUser.Login, cbCurrentAccount.SelectedItem.ToString(), sum);
                        tbCurrentAccountSum.Text = Account.accList[cbCurrentAccount.SelectedIndex].Sum.ToString("N");
                        if (cbAddAccounts.SelectedIndex == cbCurrentAccount.SelectedIndex)
                        {
                            tbAddAccountSum.Text = tbCurrentAccountSum.Text;
                        }
                        if (cbAccountForTransfer.SelectedIndex == cbCurrentAccount.SelectedIndex)
                        {
                            tbAccountForTransferSum.Text = tbCurrentAccountSum.Text;
                        }
                    }
                    else if ((tbChangeSum.Text[0] == '-') && (tbChangeSum.Text.IndexOf('-') == tbChangeSum.Text.LastIndexOf('-')) && (!tbChangeSum.Text.Contains('+')))
                    {
                        var sum = Math.Round(Convert.ToDouble(StringToCorrectDoubleFormat(tbChangeSum.Text.Remove(0, 1))), 2, MidpointRounding.AwayFromZero);
                        tbCurrentSum.Text = Conn.UpdateSumBySubtractionAtBills(CurrentUser.Login, sum).ToString(CultureInfo.CurrentCulture);
                        Account.accList[cbCurrentAccount.SelectedIndex].Sum = Conn.UpdateSumBySubtractionAtAccounts(CurrentUser.Login, cbCurrentAccount.SelectedItem.ToString(), sum);
                        tbCurrentAccountSum.Text = Account.accList[cbCurrentAccount.SelectedIndex].Sum.ToString("N");
                        if (cbAddAccounts.SelectedIndex == cbCurrentAccount.SelectedIndex)
                        {
                            tbAddAccountSum.Text = tbCurrentAccountSum.Text;
                        }
                        if (cbAccountForTransfer.SelectedIndex == cbCurrentAccount.SelectedIndex)
                        {
                            tbAccountForTransferSum.Text = tbCurrentAccountSum.Text;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправильный ввод суммы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                tbChangeSum.Text = "";
            }
        }

        private void tbChangeSum_KeyPress(object sender, KeyPressEventArgs e) // ++++++
        {
            //Allows to enter only number or using ".", ",", "backspace", "+", "-"
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == '+' || e.KeyChar == '-' || e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                return;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e) //++++++
        {
            if (tbSumForTransfer.Text != "")
            {
                if (cbCurrentAccount.SelectedIndex != cbAccountForTransfer.SelectedIndex)
                {
                    if (CheckForDot(tbSumForTransfer.Text))
                    {
                        if (Account.accList[cbCurrentAccount.SelectedIndex].Sum < Math.Round(Convert.ToDouble(StringToCorrectDoubleFormat(tbSumForTransfer.Text)), 2))
                        {
                            MessageBox.Show("Недостаточно средств для перевода на текущем счету!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            Account.accList[cbCurrentAccount.SelectedIndex].Sum = Conn.UpdateSumBySubtractionAtAccounts(CurrentUser.Login, cbCurrentAccount.SelectedItem.ToString(), Math.Round(Convert.ToDouble(StringToCorrectDoubleFormat(tbSumForTransfer.Text)), 2));
                            Account.accList[cbAccountForTransfer.SelectedIndex].Sum = Conn.UpdateSumBySummingAtAccounts(CurrentUser.Login, cbAccountForTransfer.SelectedItem.ToString(), Math.Round(Convert.ToDouble(StringToCorrectDoubleFormat(tbSumForTransfer.Text)), 2));
                            tbCurrentAccountSum.Text = Account.accList[cbCurrentAccount.SelectedIndex].Sum.ToString("N");
                            tbAccountForTransferSum.Text = Account.accList[cbAccountForTransfer.SelectedIndex].Sum.ToString("N");
                            if (cbAddAccounts.SelectedIndex == cbCurrentAccount.SelectedIndex)
                            {
                                tbAddAccountSum.Text = tbCurrentAccountSum.Text;
                            }
                            else if (cbAddAccounts.SelectedIndex == cbAccountForTransfer.SelectedIndex)
                            {
                                tbAddAccountSum.Text = tbAccountForTransferSum.Text;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправильный ввод суммы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    tbSumForTransfer.Text = "";
                }
                else
                {
                    MessageBox.Show("Текущий счёт и счёт для перевода совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region addingGood

        private void tbCost_KeyPress(object sender, KeyPressEventArgs e) // ++++++
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                return;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void tbCount_KeyPress(object sender, KeyPressEventArgs e) // ++++++
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                return;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            if (cbAddType.SelectedIndex == cbAddType.Items.Count - 1)
            {
                var type = "";
                if (EditMessageBox.InputBox("Добавление типа", "Новый тип:", ref type) != DialogResult.OK)
                {
                    return;
                }
                cbAddType.Items.Insert(cbAddType.Items.Count - 1, type);
                cbAddType.SelectedIndex = cbAddType.Items.Count - 2;
                clbTypeInfo.Items.Add(type, true);
                Good.typeList.Add(new KeyValuePair<string, byte>(type, (byte)(Good.typeList.Count)));
            }
        }

        private void cbAccountsAdd_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            tbAddAccountSum.Text = Account.accList[cbAddAccounts.SelectedIndex].Sum.ToString("N");
        }

        private void btnAdd_Click(object sender, EventArgs e) // ++++++
        {
            if ((tbAddName.Text == "") || (tbAddCost.Text == "") || (tbAddCount.Text == "") || (string.IsNullOrEmpty(Convert.ToString(cbAddType.SelectedItem))) || (string.IsNullOrEmpty(Convert.ToString(cbAddImportance.SelectedItem))))
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Убирание лишних нулей
                var number = -1;
                var d = Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCost.Text));
                if ((d - Math.Floor(d)) == 0.0)
                {
                    tbAddCost.Text = ((int)d).ToString();
                }

                //Проверка на совпадения в dgvRight
                foreach (DataGridViewRow item in dgvRight.Rows)
                {
                    if ((item.Cells[0].Value.Equals(tbAddName.Text)) && (StringToCorrectDoubleFormat((item.Cells[1].Value).ToString()).Equals(StringToCorrectDoubleFormat(tbAddCost.Text))))
                    {
                        number = item.Index;
                        break;
                    }
                }

                //
                var count = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(tbAddCount.Text)));
                if (number == -1)
                {
                    if ((count - Math.Floor(count)) == 0)
                    {
                        //+
                        Good.listAdd.Add(new Good(tbAddName.Text, Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCost.Text)),
                                                   count, cbAddType.SelectedItem.ToString(),
                                                   cbAddImportance.SelectedItem.ToString(), 
                                                   cbAddAccounts.SelectedItem.ToString(),
                                                   Convert.ToDateTime(cbAddDate.SelectedItem)));
                        //+
                        dgvRight.Rows.Add(tbAddName.Text, Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCost.Text)), 
                                          Convert.ToString((int)count), 
                                          Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCost.Text)) * count, 
                                          cbAddType.SelectedItem, cbAddImportance.SelectedItem, cbAddAccounts.SelectedItem,
                                          cbAddDate.SelectedItem);
                        ClearControlsForAddingGoods();
                        return;
                    }
                    var s = (Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCost.Text)) * Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCount.Text))).ToString("N");
                    //+
                    Good.listAdd.Add(new Good(tbAddName.Text, Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCost.Text)),
                                               count, cbAddType.SelectedItem.ToString(), 
                                               cbAddImportance.SelectedItem.ToString(),
                                               cbAddAccounts.SelectedItem.ToString(),
                                               Convert.ToDateTime(cbAddDate.SelectedItem)));
                    //+
                    dgvRight.Rows.Add(tbAddName.Text, tbAddCost.Text, tbAddCount.Text, s, 
                                      cbAddType.SelectedItem, cbAddImportance.SelectedItem, cbAddAccounts.SelectedItem,
                                      cbAddDate.SelectedItem);
                    ClearControlsForAddingGoods();
                    return;
                }

                //Для форматирования значений с плавающей точкой
                var nfi = new CultureInfo("en-US", false).NumberFormat;
                nfi.NumberDecimalDigits = 3;

                if ((count - Math.Floor(count)) == 0)
                {
                    var dgvRightCount = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value)));
                    if ((dgvRightCount - Math.Floor(dgvRightCount)) == 0)
                    {
                        dgvRight.Rows[number].Cells[2].Value = (int)dgvRightCount + (int)count;
                    }
                    else
                    {
                        dgvRight.Rows[number].Cells[2].Value = (Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value))) + Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(tbAddCount.Text)))).ToString("N", nfi);
                    }
                }
                else
                {
                    dgvRight.Rows[number].Cells[2].Value = (Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value))) + Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(tbAddCount.Text)))).ToString("N", nfi);
                }
                dgvRight.Rows[number].Cells[3].Value = (Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[1].Value))) * Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value)))).ToString("N");
                Good.listAdd[number].Count += count;
                ClearControlsForAddingGoods();
            }
        }

        #endregion

        #region search

        private void tbSearchAdd_TextChanged(object sender, EventArgs e) // ++++++
        {
            // Отображение всех строк
            var counter = 0;
            do
            {
                if (dgvLeft.Rows[counter].Visible == false)
                {
                    dgvLeft.Rows[counter].Visible = true;
                }
                counter++;
            } while (counter < dgvLeft.Rows.Count);

            if (tbSearchAdd.Text != "")
            {
                for (var i = 0; i < dgvLeft.Rows.Count; i++)
                {
                    var isVisible = dgvLeft.Rows[i].Cells[0].Value.ToString().ToLower().IndexOf(tbSearchAdd.Text.ToLower(), StringComparison.Ordinal) != -1;
                    dgvLeft.Rows[i].Visible = isVisible;
                }
            }
        }

        #endregion

        #region buttons Left, Right, Save

        private void btnLeft_Click(object sender, EventArgs e) // ++++++
        {
            var b = false;
            var row = dgvRight.SelectedRows[0];
            row.Cells[2].Value = 1;
            row.Cells[3].Value = row.Cells[1].Value;

            for (var i = 0; i < dgvLeft.Rows.Count; i++)
            {
                if (dgvLeft.Rows[i].Cells[0].Value.Equals(row.Cells[0].Value))
                {
                    b = true;
                }
            }
            dgvRight.Rows.Remove(row);
            if (!b)
            {
                dgvLeft.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value,
                                 row.Cells[4].Value, row.Cells[5].Value, "Наличный счёт");
                dgvLeft.Sort(dgvLeft.Columns[clLeftName.Name], ListSortDirection.Ascending);
            }
        }

        private void btnRight_Click(object sender, EventArgs e) // ++++++
        {
            var row = dgvLeft.SelectedRows[0];
            var number = -1;
            var count = 1d;
            var price = Convert.ToDouble(StringToCorrectDoubleFormat(row.Cells[1].Value.ToString()));
            var acc = row.Cells[dgvLeft.Columns["clLeftAccount"].Index].Value.ToString();
            var date = DateTime.Now;
            var listAcc = new List<string>();
            foreach (string item in cbAddAccounts.Items)
            {
                listAcc.Add(item);
            }
            if (EditMessageBox.InputBox(ref price, ref count, ref acc, ref date, listAcc) != DialogResult.OK)
            {
                return;
            }
            foreach (DataGridViewRow item in dgvRight.Rows)
            { //Good.listAdd[item.Index].Time.Equals(date)
                if ((item.Cells[0].Value.Equals(row.Cells[0].Value)) &&
                    (item.Cells[1].Value.Equals(price)) &&
                    (item.Cells[6].Value.Equals(acc)) &&
                    (item.Cells[7].Value.Equals(date)))
                {
                    number = item.Index;
                }
            }
            if (number == -1)
            {
                Good.listAdd.Add(new Good(row.Cells[0].Value.ToString(),
                                           price, count, row.Cells[4].Value.ToString(),
                                           row.Cells[5].Value.ToString(), acc, date));
                dgvRight.Rows.Add(row.Cells[0].Value, price, count, price * count,
                                  row.Cells[4].Value, row.Cells[5].Value, acc, date);
            }
            else
            {
                Good.listAdd[number].Count = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value))) + count;
                dgvRight.Rows[number].Cells[2].Value = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value))) + count;
                dgvRight.Rows[number].Cells[3].Value = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[3].Value))) * Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value)));
            }
        }

        private void btnSave_Click(object sender, EventArgs e) // ++++++
        {
            DataGridViewRow row;
            double sum = 0;
            for (var i = 0; i < dgvRight.Rows.Count;)
            {
                row = dgvRight.Rows[i];

                //Удаление строки из dgvRight
                dgvRight.Rows.Remove(row);

                //
                foreach (var item in Account.accList)
                {
                    if (row.Cells[6].Value.ToString().Equals(item.Name))
                    {
                        item.Sum -= Convert.ToDouble(row.Cells[3].Value);
                    }
                }

                // Подсчет суммы для дальнейшего изменения в БД
                sum += Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[3].Value)));

                //Изменение максимального значения суммы для nudMax в tabInfo
                if (Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[3].Value))) > Good.MaxSum)
                {
                    Good.MaxSum = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[3].Value)));
                }

                row.Cells[2].Value = 1;
                row.Cells[3].Value = row.Cells[1].Value;

                // Проверка на наличие в левой таблице строк
                if (dgvLeft.Rows.Count == 0)
                {
                    dgvLeft.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value,
                                 row.Cells[4].Value, row.Cells[5].Value, "Наличный счёт");
                    dgvLeft.Sort(dgvLeft.Columns[clLeftName.Name], ListSortDirection.Ascending);
                }
                else
                {
                    var count = dgvLeft.Rows.Count;
                    var b = false;
                    for (var j = 0; j < count; j++)
                    {
                        // Проверка на совпадение строк
                        if (row.Cells[0].Value.Equals(dgvLeft.Rows[j].Cells[0].Value))
                        {
                            b = true;
                            break;
                        }
                    }

                    // Добавление строки в левую таблицу
                    if (!b)
                    {
                        dgvLeft.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value,
                                 row.Cells[4].Value, row.Cells[5].Value, "Наличный счёт");
                        dgvLeft.Sort(dgvLeft.Columns[clLeftName.Name], ListSortDirection.Ascending);
                    }
                }
            }
            //Добавление строк в dgvShow
            foreach (var item in Good.listAdd)
            {
                dgvShow.Rows.Add(item.Name, item.Price, item.Count, item.Price * item.Count, 
                                 item.Type, item.Importance, item.Acc, item.Time);
            }

            //Очистка Good.listAdd
            Good.listAdd.Clear();

            //Убирание новых значений из отображения в левой таблице при поиске в tbSearch
            tbSearchAdd_TextChanged(sender, e);

            //Сортировка dgvShow по дате и отмена выделения
            dgvShow.Sort(dgvShow.Columns[clShowDate.Name], ListSortDirection.Descending);

            //
            foreach (var item in Account.accList)
            {
                Conn.UpdateSumAtAccounts(CurrentUser.Login, item.Name, item.Sum);
                if (cbCurrentAccount.SelectedItem.ToString().Equals(item.Name))
                {
                    tbCurrentAccountSum.Text = item.Sum.ToString("N");
                }
                if (cbAddAccounts.SelectedItem.ToString().Equals(item.Name))
                {
                    tbAddAccountSum.Text = item.Sum.ToString("N");
                }
            }

            // Изменение суммы в БД и в поле текущей суммы
            tbCurrentSum.Text = Conn.UpdateSumBySubtractionAtBills(CurrentUser.Login, sum).ToString("N");

            //Изменение суммы в tabShow tbSum
            Good.Sum += sum;
            tbSum.Text = Convert.ToString(Math.Round(Good.Sum, 2), CultureInfo.CurrentCulture);

            //Изменение максимального значения nudMax в tabInfo
            nudMax.Maximum = (int)Good.MaxSum + 1;
            nudMax.Value = nudMax.Maximum;
        }

        #endregion

        #region dgvLeft

        private void dgvLeft_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) //++++++
        {
            var row = dgvLeft.Rows[e.RowIndex];

            //Определение цвета по типу важности
            var c = Good.GetColorOfImportance(row.Cells[dgvLeft.Columns["clLeftImportance"].Index].Value.ToString());

            //Изменение цвета каждой ячейки строки
            for (var j = 0; j < dgvLeft.Columns.Count; j++)
            {
                row.Cells[j].Style.BackColor = c;
            }
        }

        private void dgvLeft_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) // ++++++
        {
            if (e.RowIndex < 0)
                return;
            if (e.Button == MouseButtons.Left)
            {
                _selRowNumLeft = e.RowIndex;
                return;
            }
            if (_selRowNumLeft < dgvLeft.Rows.Count)
                dgvLeft.Rows[_selRowNumLeft].Selected = false;
            dgvLeft.Rows[e.RowIndex].Selected = true;
            _selRowNumLeft = e.RowIndex;

            _dgvContext = dgvLeft.Name;
            var point = MousePosition;
            cmsDgv.Show(point.X, point.Y);
        }

        #endregion

        #region dgvRight

        private void dgvRight_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) // ++++++
        {
            var row = dgvRight.Rows[e.RowIndex];
            //Определение цвета по типу важности
            var c = Good.GetColorOfImportance(row.Cells[dgvRight.Columns["clRightImportance"].Index].Value.ToString());
            //Изменение цвета каждой ячейки строки
            for (var j = 0; j < dgvRight.Columns.Count; j++)
            {
                row.Cells[j].Style.BackColor = c;
            }
        }

        private void dgvRight_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) // ++++++
        {
            if (e.RowIndex < 0)
                return;
            if (e.Button == MouseButtons.Left)
            {
                _selRowNumRight = e.RowIndex;
                return;
            }
            if (_selRowNumRight < dgvRight.Rows.Count)
                dgvRight.Rows[_selRowNumRight].Selected = false;
            dgvRight.Rows[e.RowIndex].Selected = true;
            _selRowNumRight = e.RowIndex;

            _dgvContext = dgvRight.Name;
            var point = MousePosition;
            cmsDgv.Show(point.X, point.Y);
        }

        #endregion

        #endregion

        #region tbInfo

        #region dgvShow

        private void dgvShow_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) // ++++++
        {
            var row = dgvShow.Rows[e.RowIndex];
            //Определение цвета по типу важности
            var c = Good.GetColorOfImportance(row.Cells[dgvShow.Columns["clShowImportance"].Index].Value.ToString());
            //Изменение цвета каждой ячейки строки
            for (var j = 0; j < dgvShow.Columns.Count; j++)
            {
                row.Cells[j].Style.BackColor = c;
            }
        }

        private void dgvShow_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) // ++++++
        {
            if (e.RowIndex < 0)
                return;
            if (e.Button == MouseButtons.Left)
            {
                _selRowNumShow = e.RowIndex;
                return;
            }
            if (_selRowNumShow < dgvShow.Rows.Count)
                dgvShow.Rows[_selRowNumShow].Selected = false;
            dgvShow.Rows[e.RowIndex].Selected = true;
            _selRowNumShow = e.RowIndex;

            _dgvContext = dgvShow.Name;
            var point = MousePosition;
            cmsDgv.Show(point.X, point.Y);
        }

        private void tbSearchInfo_TextChanged(object sender, EventArgs e) // ++++++
        {
            // Отображение всех строк
            var counter = 0;
            do
            {
                if (dgvShow.Rows[counter].Visible == false)
                {
                    dgvShow.Rows[counter].Visible = true;
                }
                counter++;
            } while (counter < dgvShow.Rows.Count);

            if (tbSearchInfo.Text != "")
            {
                for (var i = 0; i < dgvShow.Rows.Count; i++)
                {
                    var isVisible = dgvShow.Rows[i].Cells[0].Value.ToString().ToLower().IndexOf(tbSearchInfo.Text.ToLower()) != -1;
                    dgvShow.Rows[i].Visible = isVisible;
                }
            }
        }

        private void tbSum_MouseEnter(object sender, EventArgs e) // ++++++
        {
            if (Hint.Active == false)
            {
                Hint.Active = true;
                Hint.Show("Общая сумма", tbSum);
            }
        }

        private void tbSum_MouseLeave(object sender, EventArgs e) // ++++++
        {
            Hint.Hide(tbSum);
            Hint.Active = false;
        }

        #endregion

        #region listbFilter

        private void listbFilter_MeasureItem(object sender, MeasureItemEventArgs e) // ++++++
        {
            e.ItemHeight = (int)e.Graphics.MeasureString(listbFilter.Items[e.Index].ToString(), listbFilter.Font, listbFilter.Width).Height;
        }

        private void listbFilter_DrawItem(object sender, DrawItemEventArgs e) // ++++++
        {
            if (listbFilter.Items.Count > 0)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.DrawString(listbFilter.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
            }
        }

        #endregion

        #region date

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            if (cbYear.SelectedIndex != 0)
            {
                if (cbMonth.Enabled == false)
                {
                    cbMonth.Enabled = true;
                }

                //Для того, чтобы обновить количество дней для нововыбранного года (высокосный/не высокосный)
                cbMonth_SelectedIndexChanged(sender, e);
            }
            else
            {
                cbMonth.SelectedIndex = 0;
                cbDay.SelectedIndex = 0;
                cbMonth.Enabled = false;
                cbDay.Enabled = false;
            }
        }

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            //Возвращение количества дней к 31
            while (cbDay.Items.Count != 32)
            {
                cbDay.Items.Add(cbDay.Items.Count);
            }

            if (cbMonth.SelectedIndex != 0)
            {
                cbDay.Enabled = true;

                //Изменение количества дней в зависимости от месяца и года
                switch (cbMonth.SelectedIndex)
                {
                    case 2:
                        {
                            cbDay.Items.RemoveAt(31);
                            cbDay.Items.RemoveAt(30);
                            var year = Convert.ToInt32(cbYear.SelectedItem);
                            if (((year % 100 == 0) && (year % 400 != 0)) || (year % 4 != 0))
                            {
                                cbDay.Items.RemoveAt(29);
                                break;
                            }
                            break;
                        }
                    case 4:
                        {
                            cbDay.Items.RemoveAt(31);
                            break;
                        }
                    case 6:
                        {
                            cbDay.Items.RemoveAt(31);
                            break;
                        }
                    case 9:
                        {
                            cbDay.Items.RemoveAt(31);
                            break;
                        }
                    case 11:
                        {
                            cbDay.Items.RemoveAt(31);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                cbDay.SelectedIndex = 0;
                cbDay.Enabled = false;
            }
        }

        #endregion

        #region lastTime

        private void cbLastTime_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            if (cbLastTime.SelectedIndex != 0)
            {
                cbYear.Enabled = false;
                cbYear.SelectedIndex = 0;
                cbMonth.Enabled = false;
                cbMonth.SelectedIndex = 0;
                cbDay.Enabled = false;
                cbDay.SelectedIndex = 0;
            }
            else
            {
                cbYear.Enabled = true;
            }
        }

        #endregion

        #region importance

        private void chboxImportanceInfo_CheckedChanged(object sender, EventArgs e) // ++++++
        {
            if (chboxImportanceInfo.Checked == false)
            {
                if (clbImportanceInfo.CheckedItems.Count != clbImportanceInfo.Items.Count)
                {
                    return;
                }
            }
            for (var i = 0; i < clbImportanceInfo.Items.Count; i++)
            {
                clbImportanceInfo.SetItemChecked(i, chboxImportanceInfo.Checked);
            }
        }

        private void clbImportanceInfo_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            chboxImportanceInfo.Checked = (clbImportanceInfo.CheckedItems.Count == clbImportanceInfo.Items.Count);
        }

        #endregion

        #region type

        private void chboxTypeInfo_CheckedChanged(object sender, EventArgs e) // ++++++
        {
            if (chboxTypeInfo.Checked == false)
            {
                if (clbTypeInfo.CheckedItems.Count != clbTypeInfo.Items.Count)
                {
                    return;
                }
            }
            for (var i = 0; i < clbTypeInfo.Items.Count; i++)
            {
                clbTypeInfo.SetItemChecked(i, chboxTypeInfo.Checked);
            }
        }

        private void clbTypeInfo_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            chboxTypeInfo.Checked = (clbTypeInfo.CheckedItems.Count == clbTypeInfo.Items.Count);
        }

        #endregion

        #region account

        private void chboxAccountInfo_CheckedChanged(object sender, EventArgs e) // ++++++
        {
            if (chboxAccountInfo.Checked == false)
            {
                if (clbAccountInfo.CheckedItems.Count != clbAccountInfo.Items.Count)
                {
                    return;
                }
            }
            for (var i = 0; i < clbAccountInfo.Items.Count; i++)
            {
                clbAccountInfo.SetItemChecked(i, chboxAccountInfo.Checked);
            }
        }

        private void clbAccountInfo_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            chboxAccountInfo.Checked = (clbAccountInfo.CheckedItems.Count == clbAccountInfo.Items.Count);
        }

        #endregion

        #region buttons

        private void btnFilter_Click(object sender, EventArgs e) // ++++++
        {
            listbFilter.Items.Clear();

            #region arguments
            var sumMin = Convert.ToInt32(nudMin.Value);
            var sumMax = Convert.ToInt32(nudMax.Value);
            var numLastTime = cbLastTime.SelectedIndex;
            var dt = new Date();
            if (cbYear.SelectedIndex != 0)
            {
                dt.Year = Convert.ToInt32(cbYear.SelectedItem.ToString());
                if (cbMonth.SelectedIndex != 0)
                {
                    dt.Month = cbMonth.SelectedIndex;
                    if (cbDay.SelectedIndex != 0)
                    {
                        dt.Day = cbDay.SelectedIndex;
                    }
                }
            }
            #endregion

            #region filling listbFilter

            #region last time
            if (dt.Year == 0)
            {
                listbFilter.Items.Add("За последнее время: " + cbLastTime.SelectedItem.ToString() + ";");
            }
            #endregion

            #region date
            else
            {
                if (dt.Month != 0)
                {
                    if (dt.Day != 0)
                    {
                        listbFilter.Items.Add("По конкретной дате: " + dt.Day + "." + dt.Month + "." + dt.Year + ";");
                    }
                    else
                    {
                        listbFilter.Items.Add("По конкретной дате: " + cbMonth.SelectedItem.ToString() + " " + dt.Year + " года;");
                    }
                }
                else
                {
                    listbFilter.Items.Add("По конкретной дате: " + dt.Year + " год;");
                }
            }
            #endregion

            #region importance
            if (clbImportanceInfo.CheckedItems.Count != 0)
            {
                listbFilter.Items.Add("По важности: ");
                foreach (var item in clbImportanceInfo.CheckedItems)
                {
                    listbFilter.Items[listbFilter.Items.Count - 1] += FirstWordFromString(item.ToString()) + "; ";
                }
            }
            #endregion

            #region type
            if (clbTypeInfo.CheckedItems.Count != 0)
            {
                listbFilter.Items.Add("По типу: ");
                foreach (var item in clbTypeInfo.CheckedItems)
                {
                    listbFilter.Items[listbFilter.Items.Count - 1] += item.ToString() + "; ";
                }
            }
            #endregion

            #region account

            if (clbAccountInfo.CheckedItems.Count != 0)
            {
                listbFilter.Items.Add("По счёту: ");
                foreach (var item in clbAccountInfo.CheckedItems)
                {
                    listbFilter.Items[listbFilter.Items.Count - 1] += item.ToString() + "; ";
                }
            }

            #endregion

            #region sum

            listbFilter.Items.Add("По сумме: от " + nudMin.Value + " грн. до " + nudMax.Value + " грн.");

            #endregion

            #endregion

            #region filtration
            double sum = 0;
            for (var i = 0; i < dgvShow.Rows.Count; i++)
            {
                var row = dgvShow.Rows[i];
                var count = 1;
                var countUp = 0;

                #region sum
                if ((Convert.ToDouble(StringToCorrectDoubleFormat(row.Cells[3].Value.ToString())) >= sumMin) &&
                    (Convert.ToDouble(StringToCorrectDoubleFormat(row.Cells[3].Value.ToString())) <= sumMax))
                {
                    countUp++;
                }
                #endregion

                if (count != countUp)
                {
                    dgvShow.Rows[i].Visible = false;
                    continue;
                }

                #region last time
                if (dt.Year == 0)
                {
                    count++;
                    switch (numLastTime)
                    {
                        case 0:
                            {
                                countUp++;
                                break;
                            }
                        case 1:
                            {
                                if ((Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value) > DateTime.Today.AddDays(-1)) && 
                                    (Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value) < DateTime.Today))
                                {
                                    countUp++;
                                }
                                break;
                            }
                        case 2:
                            {
                                if ((Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value) > DateTime.Today.AddDays(-6)) && 
                                    (Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value) < DateTime.Now))
                                {
                                    countUp++;
                                }
                                break;
                            }
                        case 3:
                            {
                                if ((Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value) > DateTime.Today.AddMonths(-1)) && 
                                    (Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value) < DateTime.Now))
                                {
                                    countUp++;
                                }
                                break;
                            }
                        case 4:
                            {
                                if ((Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value) > DateTime.Today.AddMonths(-3)) && 
                                    (Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value) < DateTime.Now))
                                {
                                    countUp++;
                                }
                                break;
                            }
                        case 5:
                            {
                                if ((Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value) > DateTime.Today.AddMonths(-6)) && 
                                    (Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value) < DateTime.Now))
                                {
                                    countUp++;
                                }
                                break;
                            }
                        case 6:
                            {
                                if ((Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value) > DateTime.Today.AddYears(-1)) && 
                                    (Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value) < DateTime.Now))
                                {
                                    countUp++;
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                #endregion

                #region date
                else
                {
                    count++;
                    if (dt.Day == 0)
                    {
                        if (dt.Month == 0)
                        {
                            if (Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value).Year == dt.Year)
                            {
                                countUp++;
                            }
                        }
                        else
                        {
                            if ((Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value).Year == dt.Year) 
                                && (Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value).Month == dt.Month))
                            {
                                countUp++;
                            }
                        }
                    }
                    else
                    {
                        if ((Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value).Year == dt.Year) && 
                            (Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value).Month == dt.Month) && 
                            (Convert.ToDateTime(row.Cells[dgvShow.Columns["clShowDate"].Index].Value).Day == dt.Day))
                        {
                            countUp++;
                        }
                    }
                }
                #endregion

                if (count != countUp)
                {
                    dgvShow.Rows[i].Visible = false;
                    continue;
                }

                count++;

                #region importance
                if (clbImportanceInfo.CheckedItems.Cast<object>().Any(t => t.ToString() == row.Cells[5].Value.ToString()))
                {
                    countUp++;
                }
                #endregion

                if (count != countUp)
                {
                    dgvShow.Rows[i].Visible = false;
                    continue;
                }

                count++;

                #region type
                if (clbTypeInfo.CheckedItems.Cast<object>().Any(t => t.ToString() == row.Cells[4].Value.ToString()))
                {
                    countUp++;
                }
                #endregion

                if (count != countUp)
                {
                    dgvShow.Rows[i].Visible = false;
                    continue;
                }

                count++;

                #region account

                if (clbAccountInfo.CheckedItems.Cast<object>().Any(t => t.ToString() == row.Cells[6].Value.ToString()))
                {
                    countUp++;
                }

                #endregion

                if (count != countUp)
                {
                    dgvShow.Rows[i].Visible = false;
                    continue;
                }

                dgvShow.Rows[i].Visible = true;
                sum += Convert.ToDouble(dgvShow.Rows[i].Cells[3].Value);
            }
            #endregion

            tbSum.Text = Convert.ToString(Math.Round(sum, 2), CultureInfo.CurrentCulture);
        }

        private void btnCancelInfo_Click(object sender, EventArgs e) // ++++++
        {
            var counter = 0;
            if (dgvShow.Rows.Count != 0)
            {
                do
                {
                    if (dgvShow.Rows[counter].Visible == false)
                    {
                        dgvShow.Rows[counter].Visible = true;
                    }
                    counter++;
                } while (counter < dgvShow.Rows.Count);
            }
            tbSum.Text = Convert.ToString(Math.Round(Good.Sum, 2), CultureInfo.CurrentCulture);
            ClearControlsForFilterDgvShow();
        }

        #endregion

        #region NumericUpDown

        private void nudMin_ValueChanged(object sender, EventArgs e) // ++++++
        {
            if (nudMin.Value > nudMax.Value)
            {
                nudMax.Value = nudMin.Value;
            }
        }

        private void nudMax_ValueChanged(object sender, EventArgs e) // ++++++
        {
            if (nudMax.Value < nudMin.Value)
            {
                nudMin.Value = nudMax.Value;
            }
        }


        #endregion

        #endregion
    }
}