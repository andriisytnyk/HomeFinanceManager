#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HomeFinanceManager.DataModels;
using HomeFinanceManager.Extensions;
using TxtWriterReader;
using XmlWriterReader;

#endregion

namespace HomeFinanceManager.Forms
{
    public partial class FormMain : FormBase
    {
        #region fields

        private readonly Form _fl; // Pointer to a FormLog
        private int _selRowNumLeft; // Number of selected row at dgvLeft
        private int _selRowNumRight; // Number of selected row at dgvRight
        private int _selRowNumShow; // Number of selected row at dgvShow
        private string _dgvContext; // Variable for ContextMenu that keep name of DataGridView
        private DateTime _dtMin = DateTime.Now; // Min date of date interval for filtration
        private DateTime _dtMax = DateTime.Now; // Max date of date interval for filtration

        #endregion

        #region constructors

        public FormMain() // ++++++
        {
            InitializeComponent();
        }
        public FormMain(Form fl) // ++++++
            : this()
        {
            _fl = fl;
            _fl.Hide();
        }

        #endregion

        #region additional methods

        public bool CheckForDot(string s) // ++++++
        {
            // Check for correct format of string with '.' or ','
            return (s[0] != ',') && (s[s.Length - 1] != ',') && (s[0] != '.') && (s[s.Length - 1] != '.') && (s.IndexOf(',') == s.LastIndexOf(',')) && (s.IndexOf('.') == s.LastIndexOf('.'));
        }

        public string StringToCorrectDoubleFormat(string s) // ++++++
        {
            // Change string to correct double format
            if (s.IndexOf(',') == -1)
            {
                s = s.Replace('.', ',');
                return s;
            }
            return s;
        }

        public string DoubleToCorrectString(double d) // ++++++
        {
            // Change string from correct format for double to correct format for string
            var str = (Convert.ToString(d, CultureInfo.CurrentCulture)).Replace('.', ',');
            return str;
        }

        public string FirstWordFromString(string s) // ++++++
        {
            return s.TakeWhile(t => t != ' ').Aggregate("", (current, t) => current + t);
        }

        public void ClearControlsForCombiningBills(bool b) // ++++++
        {
            try
            {
                if (b)
                {
                    gbAdd.Enabled = false;
                    gbChange.Enabled = false;
                    btnLogout.Enabled = false;
                    tpInfo.Parent = null;
                    return;
                }
                gbAdd.Enabled = true;
                gbChange.Enabled = true;
                btnLogout.Enabled = true;
                tpInfo.Parent = tabControl;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\nОшибка в методе ClearControlsForGbConfirm()");
            }
        }

        public void ClearControlsForAddingGoods() // ++++++
        {
            try
            {
                tbAddName.Clear();
                tbAddCount.Clear();
                tbAddCost.Clear();
                cbAddType.SelectedIndex = -1;
                cbAddImportance.SelectedIndex = -1;
                dgvLeft.CurrentCell = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\nОшибка в методе ClearControlsForAddingGoods()");
            }
        }

        public void ClearControlsForFilterDgvShow() // ++++++
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
        }

        public void FillingListbFilterForFullDgvShow() // ++++++
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
        }

        public void FillingChartInfo()
        {
            
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
                    Good.listType.Add(list[i]);
                }
            }
            else
            {
                File.CreateText(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\TypeOf'" + CurrentUser.Login + "'.txt");
            }

            foreach (var item in Good.listType)
            {
                cbAddType.Items.Insert(cbAddType.Items.Count - 1, item);
                clbTypeInfo.Items.Insert(clbTypeInfo.Items.Count, item);
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

            Account.AccList = Conn.SelectAccountByLogin(CurrentUser.Login);
            foreach (var item in Account.AccList)
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

            #region filling cbDate

            for (var i = 0; i < 30; i++)
            {
                cbAddDate.Items.Add(DateTime.Now.AddDays(-i).Date);
            }
            cbAddDate.SelectedIndex = 0;

            #endregion

            #region preparing controls of filter

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
                #region saving listType

                TxtWriter.Write(Good.listType, Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\TypeOf'" + CurrentUser.Login + "'.txt");
                
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
                Good.listType.Clear();
                Good.listAll.Clear();
                Good.listVariant.Clear();
                Account.AccList.Clear();

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
                        if (EditMessageBox.InputBox(out dt) != DialogResult.OK)
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
                for (var i = 0; i < cbAddType.Items.Count - 2; i++)
                {
                    listType.Add(cbAddType.Items[i].ToString());
                }

                var listImp = new List<string>();
                foreach (var item in cbAddImportance.Items)
                {
                    listImp.Add(item.ToString());
                }

                var listAcc = new List<string>();
                foreach (var item in Account.AccList)
                {
                    listAcc.Add(item.Name);
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

                dgvShow.Sort(dgvShow.Columns[clShowDate.Name], ListSortDirection.Descending);
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

        #region changeAccounts

        private void cbCurrentAccount_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            if (cbCurrentAccount.SelectedIndex == cbCurrentAccount.Items.Count - 1)
            {
                tbCurrentAccountSum.Text = "";
                string name;
                if (EditMessageBox.InputBox(out name) != DialogResult.OK)
                {
                    cbCurrentAccount.SelectedIndex = 0;
                    return;
                }
                var acc = Conn.AddAccount(CurrentUser.Login, name);
                Account.AccList.Add(acc);
                cbAccountForTransfer.Items.Add(name);
                cbAccountForTransfer.SelectedIndex = cbAccountForTransfer.Items.Count - 1;
                cbAddAccounts.Items.Add(name);
                cbAddAccounts.SelectedIndex = cbAddAccounts.Items.Count - 1;
                cbCurrentAccount.Items.Insert(cbCurrentAccount.Items.Count - 1, name);
                cbCurrentAccount.SelectedIndex = cbCurrentAccount.Items.Count - 2;
            }
            else
            {
                tbCurrentAccountSum.Text = Account.AccList[cbCurrentAccount.SelectedIndex].Sum.ToString("N");
            }
        }

        private void cbAccountForTransfer_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            tbAccountForTransferSum.Text = Account.AccList[cbAccountForTransfer.SelectedIndex].Sum.ToString("N");
        }

        private void btnChangeSum_Click(object sender, EventArgs e) // ++++++
        {
            if (tbChangeSum.Text != "")
            {
                if ((!tbChangeSum.Text.Contains('+')) && (!tbChangeSum.Text.Contains('-')))
                {
                    if (CheckForDot(tbChangeSum.Text))
                    {
                        Conn.UpdateSumBySubtractionAtBills(CurrentUser.Login, Account.AccList[cbCurrentAccount.SelectedIndex].Sum);
                        Account.AccList[cbCurrentAccount.SelectedIndex].Sum = Conn.UpdateSumAtAccounts(CurrentUser.Login, cbCurrentAccount.SelectedItem.ToString(), Math.Round(Convert.ToDouble(StringToCorrectDoubleFormat(tbChangeSum.Text)), 2)).Sum;
                        tbCurrentSum.Text = Conn.UpdateSumBySummingAtBills(CurrentUser.Login, Account.AccList[cbCurrentAccount.SelectedIndex].Sum).ToString("N");
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
                        Account.AccList[cbCurrentAccount.SelectedIndex].Sum = Conn.UpdateSumBySummingAtAccounts(CurrentUser.Login, cbCurrentAccount.SelectedItem.ToString(), sum);
                        tbCurrentAccountSum.Text = Account.AccList[cbCurrentAccount.SelectedIndex].Sum.ToString("N");
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
                        Account.AccList[cbCurrentAccount.SelectedIndex].Sum = Conn.UpdateSumBySubtractionAtAccounts(CurrentUser.Login, cbCurrentAccount.SelectedItem.ToString(), sum);
                        tbCurrentAccountSum.Text = Account.AccList[cbCurrentAccount.SelectedIndex].Sum.ToString("N");
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
            // Allows to enter only number or using ".", ",", "backspace", "+", "-"
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == '+' || e.KeyChar == '-' || e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
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
                        if (Account.AccList[cbCurrentAccount.SelectedIndex].Sum < Math.Round(Convert.ToDouble(StringToCorrectDoubleFormat(tbSumForTransfer.Text)), 2))
                        {
                            MessageBox.Show("Недостаточно средств для перевода на текущем счету!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            Account.AccList[cbCurrentAccount.SelectedIndex].Sum = Conn.UpdateSumBySubtractionAtAccounts(CurrentUser.Login, cbCurrentAccount.SelectedItem.ToString(), Math.Round(Convert.ToDouble(StringToCorrectDoubleFormat(tbSumForTransfer.Text)), 2));
                            Account.AccList[cbAccountForTransfer.SelectedIndex].Sum = Conn.UpdateSumBySummingAtAccounts(CurrentUser.Login, cbAccountForTransfer.SelectedItem.ToString(), Math.Round(Convert.ToDouble(StringToCorrectDoubleFormat(tbSumForTransfer.Text)), 2));
                            tbCurrentAccountSum.Text = Account.AccList[cbCurrentAccount.SelectedIndex].Sum.ToString("N");
                            tbAccountForTransferSum.Text = Account.AccList[cbAccountForTransfer.SelectedIndex].Sum.ToString("N");
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
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
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
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar.Equals('.') || e.KeyChar.Equals(',')))
            {
                e.Handled = true;
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            if (cbAddType.SelectedIndex == cbAddType.Items.Count - 1)
            {
                string type;
                if (EditMessageBox.InputBox(out type) != DialogResult.OK)
                {
                    cbAddType.SelectedIndex = 0;
                    return;
                }
                cbAddType.Items.Insert(cbAddType.Items.Count - 1, type);
                cbAddType.SelectedIndex = cbAddType.Items.Count - 2;
                clbTypeInfo.Items.Add(type, true);
                Good.listType.Add(type);
            }
        }

        private void cbAccountsAdd_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            tbAddAccountSum.Text = Account.AccList[cbAddAccounts.SelectedIndex].Sum.ToString("N");
        }

        private void btnAdd_Click(object sender, EventArgs e) // ++++++
        {
            if ((tbAddName.Text == "") || (tbAddCost.Text == "") || (tbAddCount.Text == "") || (string.IsNullOrEmpty(Convert.ToString(cbAddType.SelectedItem))) || (string.IsNullOrEmpty(Convert.ToString(cbAddImportance.SelectedItem))))
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Remove redundant zeros
                var number = -1;
                var d = Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCost.Text));
                if ((d - Math.Floor(d)) == 0.0)
                {
                    tbAddCost.Text = ((int)d).ToString();
                }

                // Check for equal in dgvRight
                foreach (DataGridViewRow item in dgvRight.Rows)
                {
                    if ((item.Cells[0].Value.Equals(tbAddName.Text)) && (StringToCorrectDoubleFormat((item.Cells[1].Value).ToString()).Equals(StringToCorrectDoubleFormat(tbAddCost.Text))))
                    {
                        number = item.Index;
                        break;
                    }
                }

                var count = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(tbAddCount.Text)));
                if (number == -1)
                {
                    if ((count - Math.Floor(count)) == 0)
                    {
                        Good.listAdd.Add(new Good(tbAddName.Text, Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCost.Text)),
                                                   count, cbAddType.SelectedItem.ToString(),
                                                   cbAddImportance.SelectedItem.ToString(), 
                                                   cbAddAccounts.SelectedItem.ToString(),
                                                   Convert.ToDateTime(cbAddDate.SelectedItem)));

                        dgvRight.Rows.Add(tbAddName.Text, Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCost.Text)), 
                                          Convert.ToString((int)count), 
                                          Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCost.Text)) * count, 
                                          cbAddType.SelectedItem, cbAddImportance.SelectedItem, cbAddAccounts.SelectedItem,
                                          cbAddDate.SelectedItem);
                        ClearControlsForAddingGoods();
                        return;
                    }
                    var s = (Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCost.Text)) * Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCount.Text))).ToString("N");

                    Good.listAdd.Add(new Good(tbAddName.Text, Convert.ToDouble(StringToCorrectDoubleFormat(tbAddCost.Text)),
                                               count, cbAddType.SelectedItem.ToString(), 
                                               cbAddImportance.SelectedItem.ToString(),
                                               cbAddAccounts.SelectedItem.ToString(),
                                               Convert.ToDateTime(cbAddDate.SelectedItem)));

                    dgvRight.Rows.Add(tbAddName.Text, tbAddCost.Text, tbAddCount.Text, s, 
                                      cbAddType.SelectedItem, cbAddImportance.SelectedItem, cbAddAccounts.SelectedItem,
                                      cbAddDate.SelectedItem);
                    ClearControlsForAddingGoods();
                    return;
                }

                // Variable for formating double values
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
            // Display all rows
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
            DateTime date;
            var listAcc = new List<string>();
            foreach (var item in Account.AccList)
            {
                listAcc.Add(item.Name);
            }
            if (EditMessageBox.InputBox(ref price, ref count, ref acc, out date, listAcc) != DialogResult.OK)
            {
                return;
            }
            foreach (DataGridViewRow item in dgvRight.Rows)
            { 
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
            double sum = 0;
            for (var i = 0; i < dgvRight.Rows.Count;)
            {
                var row = dgvRight.Rows[i];

                // Delete row from dgvRight
                dgvRight.Rows.Remove(row);

                // Change Sum in Account by row's Sum substraction
                foreach (var item in Account.AccList)
                {
                    if (row.Cells[6].Value.ToString().Equals(item.Name))
                    {
                        item.Sum -= Convert.ToDouble(row.Cells[3].Value);
                    }
                }

                // Calculation of the Sum for further change in DB
                sum += Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[3].Value)));

                // Change maximum value of Sum for nudMax in tabInfo
                if (Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[3].Value))) > Good.MaxSum)
                {
                    Good.MaxSum = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[3].Value)));
                }

                row.Cells[2].Value = 1;
                row.Cells[3].Value = row.Cells[1].Value;

                // Check for availability rows in dgvLeft
                if (dgvLeft.Rows.Count == 0)
                {
                    dgvLeft.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value,
                                 row.Cells[4].Value, row.Cells[5].Value, "Наличный счёт");
                    dgvLeft.Sort(dgvLeft.Columns[clLeftName.Name], ListSortDirection.Ascending);
                }
                else
                {
                    var rowsEqual = false;
                    for (var j = 0; j < dgvLeft.Rows.Count; j++)
                    {
                        // Check for equal of rows
                        if (row.Cells[0].Value.Equals(dgvLeft.Rows[j].Cells[0].Value))
                        {
                            rowsEqual = true;
                            break;
                        }
                    }

                    // Add row to dgvLeft
                    if (!rowsEqual)
                    {
                        dgvLeft.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value,
                                 row.Cells[4].Value, row.Cells[5].Value, "Наличный счёт");
                        dgvLeft.Sort(dgvLeft.Columns[clLeftName.Name], ListSortDirection.Ascending);
                    }
                }
            }

            // Add row to dgvShow
            foreach (var item in Good.listAdd)
            {
                dgvShow.Rows.Add(item.Name, item.Price, item.Count, item.Price * item.Count, 
                                 item.Type, item.Importance, item.Acc, item.Time);
            }

            // Clear Good.listAdd
            Good.listAdd.Clear();

            // Change Enable property of row in dgvLeft during the search via tbSearch to false value
            tbSearchAdd_TextChanged(sender, e);

            // Sort rows in dgvShow by date
            dgvShow.Sort(dgvShow.Columns[clShowDate.Name], ListSortDirection.Descending);

            // Change Sum at Accounts in DB and in textBoxes
            foreach (var item in Account.AccList)
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

            // Change general Sum at Bills in DB and in tbCurrentSum
            tbCurrentSum.Text = Conn.UpdateSumBySubtractionAtBills(CurrentUser.Login, sum).ToString("N");

            // Change Sum in tbSum in tpShow
            Good.Sum += sum;
            tbSum.Text = Convert.ToString(Math.Round(Good.Sum, 2), CultureInfo.CurrentCulture);

            // Change maximum value in nudMax in tpInfo
            nudMax.Maximum = (int)Good.MaxSum + 1;
            nudMax.Value = nudMax.Maximum;
        }

        #endregion

        #region dgvLeft

        private void dgvLeft_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) // ++++++
        {
            var row = dgvLeft.Rows[e.RowIndex];

            // Determination of color by ImportanceType
            var c = Good.GetColorOfImportance(row.Cells[dgvLeft.Columns["clLeftImportance"].Index].Value.ToString());

            // Change the color of each cell of the line
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

            // Determination of color by ImportanceType
            var c = Good.GetColorOfImportance(row.Cells[dgvRight.Columns["clRightImportance"].Index].Value.ToString());
            
            // Change the color of each cell of the line
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

            // Determination of color by ImportanceType
            var c = Good.GetColorOfImportance(row.Cells[dgvShow.Columns["clShowImportance"].Index].Value.ToString());

            // Change the color of each cell of the line
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
            // Display all rows
            foreach (var row in dgvShow.Rows)
            {
                ((DataGridViewRow) row).Visible = true;
            }

            if (tbSearchInfo.Text != "")
            {
                for (var i = 0; i < dgvShow.Rows.Count; i++)
                {
                    var isVisible = dgvShow.Rows[i].Cells[0].Value.ToString().ToLower().IndexOf(tbSearchInfo.Text.ToLower(), StringComparison.Ordinal) != -1;
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

        #region lastTime

        private void cbLastTime_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            if (cbLastTime.SelectedIndex != 0)
            {
                if (cbLastTime.SelectedIndex == cbLastTime.Items.Count - 1)
                {
                    if (EditMessageBox.InputBox(ref _dtMin, ref _dtMax) != DialogResult.OK)
                    {
                        return;
                    }
                    if (_dtMin == _dtMax)
                    {
                        cbLastTime.SelectedIndex = 0;
                        cbYear.SelectedIndex = DateTime.Now.Year - _dtMin.Year + 1;
                        cbMonth.SelectedIndex = _dtMin.Month;
                        cbDay.SelectedIndex = _dtMin.Day;
                        return;
                    }
                }
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

        #region date

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            if (cbYear.SelectedIndex != 0)
            {
                if (cbMonth.Enabled == false)
                {
                    cbMonth.Enabled = true;
                }

                // For refreshing day's amount for new selected year (leap/not leap year)
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
            // Return amount of days to 31
            while (cbDay.Items.Count != 32)
            {
                cbDay.Items.Add(cbDay.Items.Count);
            }

            if (cbMonth.SelectedIndex != 0)
            {
                cbDay.Enabled = true;

                // Change amount of days depending on the month and year
                for (var i = 31; i > DateTime.DaysInMonth(Convert.ToInt32(cbYear.SelectedItem.ToString()), cbMonth.SelectedIndex); i--)
                {
                    cbDay.Items.RemoveAt(i);
                }
            }
            else
            {
                cbDay.SelectedIndex = 0;
                cbDay.Enabled = false;
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
            var selectedDate = new DateTime();

            if (cbLastTime.SelectedIndex != cbLastTime.Items.Count - 1)
            {
                if (cbYear.SelectedIndex != 0)
                {
                    selectedDate = selectedDate.AddYears(Convert.ToInt32(cbYear.SelectedItem.ToString()) - 1);
                    if (cbMonth.SelectedIndex != 0)
                    {
                        selectedDate = selectedDate.AddMonths(cbMonth.SelectedIndex - 1);
                        if (cbDay.SelectedIndex != 0)
                        {
                            selectedDate = selectedDate.AddDays(cbDay.SelectedIndex - 1);
                        }
                    }
                }
            }

            #endregion

            #region filling listbFilter

            #region last time

            if (cbLastTime.SelectedIndex == cbLastTime.Items.Count - 1)
            {
                listbFilter.Items.Add("За интервал: от " + _dtMin.ToString("dd MMMM yyyy") + " до " + _dtMax.ToString("dd MMMM yyyy") + ";");
            }
            else if (cbYear.SelectedIndex == 0)
            {
                listbFilter.Items.Add("За последнее время: " + cbLastTime.SelectedItem.ToString() + ";");
            }

            #endregion

            #region date

            else
            {
                if (selectedDate.Month != 0)
                {
                    if (selectedDate.Day != 0)
                    {
                        listbFilter.Items.Add("По конкретной дате: " + selectedDate.Day + "." + selectedDate.Month + "." + selectedDate.Year + ";");
                    }
                    else
                    {
                        listbFilter.Items.Add("По конкретной дате: " + cbMonth.SelectedItem.ToString() + " " + selectedDate.Year + " года;");
                    }
                }
                else
                {
                    listbFilter.Items.Add("По конкретной дате: " + selectedDate.Year + " год;");
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

            dgvShow.Sort(dgvShow.Columns[clShowDate.Name], ListSortDirection.Ascending);

            double sum = 0;
            var dictionaryOfPointsImportance = new Dictionary<DateTime, double>();

            for (var i = 0; i < dgvShow.Rows.Count; i++)
            {
                var row = dgvShow.Rows[i];
                var rowDate = Convert.ToDateTime(row.Cells[row.Cells.Count - 1].Value.ToString());
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
                    row.Visible = false;
                    continue;
                }

                #region last time

                if (cbYear.SelectedIndex == 0)
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
                            if (rowDate > DateTime.Today.AddDays(-1) &&
                                rowDate < DateTime.Today)
                            {
                                countUp++;
                            }

                            break;
                        }
                        case 2:
                        {
                            if (rowDate > DateTime.Today.AddDays(-6) &&
                                rowDate < DateTime.Now)
                            {
                                countUp++;
                            }

                            break;
                        }
                        case 3:
                            {
                                if (rowDate > DateTime.Today.AddMonths(-1) &&
                                    rowDate < DateTime.Now)
                                {
                                    countUp++;
                                }
                                break;
                            }
                        case 4:
                        {
                            if (rowDate > DateTime.Today.AddMonths(-3) &&
                                rowDate < DateTime.Now)
                            {
                                countUp++;
                            }

                            break;
                        }
                        case 5:
                        {
                            if (rowDate > DateTime.Today.AddMonths(-6) &&
                                rowDate < DateTime.Now)
                            {
                                countUp++;
                            }

                            break;
                        }
                        case 6:
                        {
                            if (rowDate > DateTime.Today.AddYears(-1) &&
                                rowDate < DateTime.Now)
                            {
                                countUp++;
                            }

                            break;
                        }
                        case 7:
                        {
                            if (rowDate >= _dtMin &&
                                rowDate <= _dtMax)
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
                    if (selectedDate.Day == 0)
                    {
                        if (selectedDate.Month == 0)
                        {
                            if (rowDate.Year == selectedDate.Year)
                            {
                                countUp++;
                            }
                        }
                        else
                        {
                            if (rowDate.Year == selectedDate.Year && 
                                rowDate.Month == selectedDate.Month)
                            {
                                countUp++;
                            }
                        }
                    }
                    else
                    {
                        if (rowDate.Year == selectedDate.Year &&
                            rowDate.Month == selectedDate.Month &&
                            rowDate.Day == selectedDate.Day)
                        {
                            countUp++;
                        }
                    }
                }

                #endregion

                if (count != countUp)
                {
                    row.Visible = false;
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
                    row.Visible = false;
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
                    row.Visible = false;
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
                    row.Visible = false;
                    continue;
                }

                dgvShow.Rows[i].Visible = true;

                #region 

                // TODO

                if (row.Cells[5].Value.ToString() == "Первостепенной важности")
                {
                    if (!dictionaryOfPointsImportance.ContainsKey(rowDate.Date))
                    {
                        dictionaryOfPointsImportance.Add(rowDate.Date, Convert.ToDouble(row.Cells[3].Value));
                    }
                    else
                    {
                        dictionaryOfPointsImportance[rowDate.Date] += Convert.ToDouble(row.Cells[3].Value);
                    }
                }

                #endregion

                sum += Convert.ToDouble(row.Cells[3].Value);
            }

            #endregion

            foreach (var kvpair in dictionaryOfPointsImportance)
            {
                chart1.Series[0].Points.AddXY(0, kvpair.Value);
            }

            chart1.ChartAreas[0].AxisY.IsLogarithmic = true;

            dgvShow.Sort(dgvShow.Columns[clShowDate.Name], ListSortDirection.Descending);

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