#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private Form Fl; // Pointer to a FormLog
        private int selRowNumLeft; // Number of selected row at dgvLeft
        private int selRowNumRight; // Number of selected row at dgvRight
        private int selRowNumShow; // Number of selected row at dgvShow
        private string dgvContext;
        private User CombUser;

        #endregion

        #region constructors

        public FormMain() // ++++++
        {
            InitializeComponent();
        }
        public FormMain(Form Fl) : this() // ++++++
        {
            this.Fl = Fl;
            this.Fl.Hide();
        }

        #endregion

        #region additional methods
        public bool CheckForDot(string s) // ++++++
        {
            //Проверка на правильность формата строки с точкой или запятой
            if ((s[0] == ',') || (s[s.Length - 1] == ',') || (s[0] == '.') || (s[s.Length - 1] == '.') || (s.IndexOf(',') != s.LastIndexOf(',')) || (s.IndexOf('.') != s.LastIndexOf('.')))
            {
                return false;
            }
            return true;
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
            string str = (Convert.ToString(d)).Replace('.', ',');
            return str;
        }

        public string FirstWordFromString(string s) // ++++++
        {
            string res = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {
                    break;
                }
                res += s[i];
            }
            return res;
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
                    gbConfirm.Visible = true;
                    return true;
                }
                gbAdd.Enabled = true;
                gbChange.Enabled = true;
                btnLogout.Enabled = true;
                tpInfo.Parent = tabControl;
                gbConfirm.Visible = false;
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
                tbName.Clear();
                tbCount.Clear();
                tbCost.Clear();
                cbTypeAdd.SelectedIndex = -1;
                cbImportanceAdd.SelectedIndex = -1;
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
            nudMax.Maximum = (int)Good.MaxSum + 1;
            nudMax.Value = (int)Good.MaxSum + 1;
            nudMin.Value = 0;
            nudMin.Maximum = nudMax.Maximum;
            FillingListbFilterForFullDgvShow();
            dgvShow.CurrentCell = null;
            return true;
        }

        public bool FillingListbFilterForFullDgvShow()
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
                    this.Close();
                }
            }
            #endregion

            #region reading from TXT-file
            if (File.Exists(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\TypeOf'" + CurrentUser.Login + "'.txt"))
            {
                List<string> list = TxtReader.Read(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\TypeOf'" + CurrentUser.Login + "'.txt");
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
                cbTypeAdd.Items.Insert(cbTypeAdd.Items.Count - 1, item.Key);
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
                    this.Close();
                }
            }
            #endregion

            #region copying files for backup
            File.Copy(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\" + CurrentUser.Login + ".xml", Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\_Backup_\\" + CurrentUser.Login + "\\" + CurrentUser.Login + ".xml", true);
            File.Copy(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\TypeOf'" + CurrentUser.Login + "'.txt", Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\_Backup_\\" + CurrentUser.Login + "\\TypeOf'" + CurrentUser.Login + "'.txt", true);
            File.Copy(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\VariantOf'" + CurrentUser.Login + "'.xml", Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\_Backup_\\" + CurrentUser.Login + "\\VariantOf'" + CurrentUser.Login + "'.xml", true);
            #endregion

            #region loading CombUsers and SumOfCurrentUser
            List<User> listUsers = Conn.SelectAllUsers();
            int idbalance = 0;
            for (int i = 0; i < listUsers.Count; i++)
            {
                if (listUsers[i].Login != CurrentUser.Login)
                {
                    cbCombine.Items.Add(listUsers[i].Login);
                }
                if (listUsers[i].Login == CurrentUser.Login)
                {
                    idbalance = listUsers[i].IdBalance;
                }
            }
            tbCurrentSum.Text = Conn.SelectSumByIdFromBills(idbalance).ToString("N");
            #endregion

            #region showing XML-file All at dgvShow
            Good.MaxSum = 0;
            foreach (Good item in Good.listAll)
            {
                if ((item.Price * item.Count) > Good.MaxSum)
                {
                    Good.MaxSum = item.Price * item.Count;
                }
                Good.Sum += item.Price * item.Count;
                dgvShow.Rows.Add(item.Name, item.Price, item.Count, item.Price * item.Count, item.Type, item.Importance, item.Time);
            }
            Good.listAll.Clear();
            dgvShow.Sort(dgvShow.Columns[clShowDate.Name], ListSortDirection.Descending);
            tbSum.Text = Convert.ToString(Math.Round(Good.Sum, 2));
            #endregion

            #region showing XML-file Variants at dgvLeft
            foreach (Good item in Good.listVariant)
            {
                dgvLeft.Rows.Add(item.Name, item.Price, item.Count, item.Price * item.Count, item.Type, item.Importance);
            }
            Good.listVariant.Clear();
            dgvLeft.Sort(dgvLeft.Columns[clLeftName.Name], ListSortDirection.Ascending);
            dgvLeft.CurrentCell = null;
            #endregion

            #region filling cbDate
            for (int i = 0; i < 30; i++)
            {
                cbDate.Items.Add(DateTime.Now.AddDays(-i).Date);
            }
            cbDate.SelectedIndex = 0;
            
            #endregion

            #region preparation controls of filter
            ClearControlsForFilterDgvShow();
            DateTime dt = DateTime.Now;
            for (int i = 0; i < 20; i++)
            {
                cbYear.Items.Add(dt.Year - i);
            }
            #endregion

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) // ++++++
        {
            if (XmlReader<Good>.condition == true)
            {
                #region saving typeList
                List<string> list = new List<string>();
                for (int i = 0; i < Good.typeList.Count; i++)
                {
                    list.Add(Good.typeList[i].Key);
                }
                TxtWriter.Write(list, Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\TypeOf'" + CurrentUser.Login + "'.txt");
                #endregion

                #region saving listVariant
                foreach (DataGridViewRow row in dgvLeft.Rows)
                {
                    Good.listVariant.Add((new Good(Convert.ToString(row.Cells[0].Value),
                                                   Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[1].Value))),
                                                   Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[2].Value))),
                                                   Convert.ToString(row.Cells[4].Value), Convert.ToString(row.Cells[5].Value))));
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
                                                   Convert.ToDateTime(row.Cells[6].Value))));
                }
                XmlWriter<Good>.Write(Good.listAll, Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\AppData\\" + CurrentUser.Login + "\\" + CurrentUser.Login + ".xml");
                #endregion

                #region clearing all lists
                Good.Sum = 0;
                Good.typeList.Clear();
                Good.listAll.Clear();
                Good.listVariant.Clear();
                #endregion

                if (this.Fl.Visible == false)
                {
                    this.Fl.Close();
                }
            }
            else
            {
                Good.Sum = 0;
                XmlReader<Good>.condition = true;
                this.Fl.Show();
            }
        }

        #endregion

        #region Icon

        private void Icon_MouseDoubleClick(object sender, MouseEventArgs e) // ++++++
        {
            this.WindowState = FormWindowState.Normal;
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
                DateTime dt = new DateTime();
                if (dgvContext == dgvLeft.Name)
                {
                    row = dgvLeft.Rows[selRowNumLeft];
                }
                else if (dgvContext == dgvRight.Name)
                {
                    row = dgvRight.Rows[selRowNumRight];
                }
                else if (dgvContext == dgvShow.Name)
                {
                    if (dgvShow.SelectedRows.Count == 1)
                    {
                        row = dgvShow.Rows[selRowNumShow];
                        dt = Convert.ToDateTime(row.Cells[6].Value);
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
                            item.Cells[6].Value = dt;
                        }
                        return;
                    }
                }
                else
                {
                    row = new DataGridViewRow();
                }
                string name = Convert.ToString(row.Cells[0].Value);
                double cost = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[1].Value)));
                double count = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[2].Value)));
                string type = Convert.ToString(row.Cells[4].Value);
                string imp = Convert.ToString(row.Cells[5].Value);
                List<string> listType = new List<string>();
                foreach (var item in cbTypeAdd.Items)
                {
                    listType.Add(item.ToString());
                }
                List<string> listImp = new List<string>();
                foreach (var item in cbImportanceAdd.Items)
                {
                    listImp.Add(item.ToString());
                }
                if ((dgvContext == dgvLeft.Name) || (dgvContext == dgvRight.Name))
                {
                    if (EditMessageBox.InputBox(ref name, ref cost, ref count, ref type, ref imp, listType, listImp) != DialogResult.OK)
                    {
                        return;
                    }
                }
                else if (dgvContext == dgvShow.Name)
                {
                    if (EditMessageBox.InputBox(ref name, ref cost, ref count, ref type, ref imp, ref dt, listType, listImp) != DialogResult.OK)
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
                if (dgvContext == dgvShow.Name)
                {
                    row.Cells[6].Value = dt;
                }
            }
            if (e.ClickedItem == tsmiDgvDelete)
            {
                if (dgvContext == dgvLeft.Name)
                {
                    dgvLeft.Rows.Remove(dgvLeft.SelectedRows[0]);
                }
                else if (dgvContext == dgvRight.Name)
                {
                    Good.goodsAdd.RemoveAt(selRowNumRight);
                    dgvRight.Rows.Remove(dgvRight.SelectedRows[0]);
                }
                else if (dgvContext == dgvShow.Name)
                {
                    dgvShow.Rows.Remove(dgvShow.SelectedRows[0]);
                }
            }
        }

        #endregion

        #region button Logout

        private void btnLogout_Click(object sender, EventArgs e) // ++++++
        {
            this.Fl.Show();
            this.Close();
        }

        #endregion

        #region tbAdd

        #region changeSum

        private void btnChangeSum_Click(object sender, EventArgs e) // ++++++
        {
            if (tbChangeSum.Text != "")
            {
                if ((!tbChangeSum.Text.Contains('+')) && (!tbChangeSum.Text.Contains('-')))
                {
                    //string s = tbChangeSum.Text.Substring(1);
                    if (CheckForDot(tbChangeSum.Text))
                    {
                        Conn.UpdateOrInsertSumAtBills(CurrentUser.Login, Math.Round(Convert.ToDouble(StringToCorrectDoubleFormat(tbChangeSum.Text)), 2));
                        tbCurrentSum.Text = Convert.ToDouble(StringToCorrectDoubleFormat(tbChangeSum.Text)).ToString("N");
                        tbChangeSum.Text = "";
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
                        double sum = Math.Round(Convert.ToDouble(StringToCorrectDoubleFormat(tbChangeSum.Text.Remove(0, 1))), 2, MidpointRounding.AwayFromZero);
                        sum = Conn.UpdateSumBySummingAtBills(CurrentUser.Login, sum);
                        tbCurrentSum.Text = sum.ToString("N");
                        tbChangeSum.Text = "";
                    }
                    else if ((tbChangeSum.Text[0] == '-') && (tbChangeSum.Text.IndexOf('-') == tbChangeSum.Text.LastIndexOf('-')) && (!tbChangeSum.Text.Contains('+')))
                    {
                        double sum = Math.Round(Convert.ToDouble(StringToCorrectDoubleFormat(tbChangeSum.Text.Remove(0, 1))), 2, MidpointRounding.AwayFromZero);
                        sum = Conn.UpdateSumBySubtractionAtBills(CurrentUser.Login, sum);
                        tbCurrentSum.Text = sum.ToString("N");
                        tbChangeSum.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Неправильный ввод суммы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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

        #endregion

        #region combineUsers

        private void btnCombine_Click(object sender, EventArgs e) // ++++++
        {
            if (string.IsNullOrEmpty(Convert.ToString(cbCombine.SelectedItem)))
                return;
            CombUser = Conn.SelectUserByLogin(Convert.ToString(cbCombine.SelectedItem));
            ClearControlsForCombiningBills(true);
        }

        private void tbPassword_TextChanged(object sender, EventArgs e) // ++++++
        {
            /*Encoding Password to Base64*/
            if (Convert.ToBase64String(Encoding.UTF8.GetBytes(tbPassword.Text)) == CombUser.Password)
            {
                tbPassword.ForeColor = System.Drawing.Color.Green;
                btnCancel.Visible = false;
                btnApply.Visible = true;
            }
            else
            {
                if (btnCancel.Visible == false)
                {
                    btnCancel.Visible = true;
                    btnApply.Visible = false;
                }
                tbPassword.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnApply_Click(object sender, EventArgs e) // ++++++
        {
            List<User> listUser = Conn.SelectAllUsers();
            List<Bill> listBill = Conn.SelectAllBills();
            int idbalance1 = 0;
            int number1 = -1;
            int idbalance2 = 0;
            int number2 = -1;
            for (int i = 0; i < listUser.Count; i++)
            {
                if (listUser[i].Login == CurrentUser.Login)
                {
                    if (listUser[i].IdBalance != 0)
                    {
                        idbalance1 = listUser[i].IdBalance;
                    }
                    number1 = i;
                }
                if (listUser[i].Login == CombUser.Login)
                {
                    if (listUser[i].IdBalance != 0)
                    {
                        idbalance2 = listUser[i].IdBalance;
                    }
                    number2 = i;
                }
            }
            if ((idbalance1 == idbalance2) && (idbalance1 != 0))
            {
                MessageBox.Show("Счета данных пользователей уже объединены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCancel_Click(sender, e);
            }
            else
            {
                if (idbalance1 == 0)
                {
                    if (idbalance2 == 0)
                    {
                        Bill bill = Conn.UpdateOrInsertSumAtBills(listUser[number1].Login, 0);
                        Conn.UpdateIdBalanceAtUsers(listUser[number2].Login, bill.Id);
                    }
                    else
                    {
                        Conn.UpdateIdBalanceAtUsers(listUser[number1].Login, listUser[number2].IdBalance);
                    }
                }
                else
                {
                    if (idbalance2 != 0)
                    {
                        double sum = Conn.SelectSumByIdFromBills(idbalance2);
                        Conn.UpdateSumBySummingAtBills(listUser[number1].Login, sum);
                        Conn.UpdateIdBalanceAtUsers(listUser[number2].Login, listUser[number1].IdBalance);
                        Conn.DeleteBill(idbalance2);
                    }
                    else
                    {
                        Conn.UpdateIdBalanceAtUsers(listUser[number2].Login, listUser[number1].IdBalance);
                    }
                }
                MessageBox.Show("Счета успешно объеденены!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                btnCancel_Click(sender, e);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) // ++++++
        {
            tbPassword.Clear();
            ClearControlsForCombiningBills(false);
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

        private void cbGoods_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            if (cbTypeAdd.SelectedIndex == cbTypeAdd.Items.Count - 1)
            {
                string type = "";
                if (EditMessageBox.InputBox("Добавление типа", "Новый тип:", ref type) != DialogResult.OK)
                {
                    return;
                }
                cbTypeAdd.Items.Insert(cbTypeAdd.Items.Count - 1, type);
                cbTypeAdd.SelectedIndex = cbTypeAdd.Items.Count - 2;
                Good.typeList.Add(new KeyValuePair<string, byte>(type, (byte)(Good.typeList.Count)));
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) // ++++++
        {
            if ((tbName.Text == "") || (tbCost.Text == "") || (tbCount.Text == "") || (string.IsNullOrEmpty(Convert.ToString(cbTypeAdd.SelectedItem))) || (string.IsNullOrEmpty(Convert.ToString(cbImportanceAdd.SelectedItem))))
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Убирание лишних нулей
                int number = -1;
                double d = Convert.ToDouble(StringToCorrectDoubleFormat(tbCost.Text));
                if ((d - Math.Floor(d)) == 0.0)
                {
                    tbCost.Text = ((int)d).ToString();
                }

                //Проверка на совпадения в dgvRight
                foreach (DataGridViewRow item in dgvRight.Rows)
                {
                    if ((item.Cells[0].Value.Equals(tbName.Text)) && (StringToCorrectDoubleFormat((item.Cells[1].Value).ToString()).Equals(StringToCorrectDoubleFormat(tbCost.Text))))
                    {
                        number = item.Index;
                        break;
                    }
                }

                //
                double count = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(tbCount.Text)));
                if (number == -1)
                {
                    if ((count - Math.Floor(count)) == 0)
                    {
                        Good.goodsAdd.Add(new Good(tbName.Text, Convert.ToDouble(StringToCorrectDoubleFormat(tbCost.Text)),
                                                   count, cbTypeAdd.SelectedItem.ToString(), 
                                                   cbImportanceAdd.SelectedItem.ToString(),
                                                   Convert.ToDateTime(cbDate.SelectedItem)));
                        dgvRight.Rows.Add(tbName.Text, Convert.ToDouble(StringToCorrectDoubleFormat(tbCost.Text)), 
                                          Convert.ToString((int)count), 
                                          Convert.ToDouble(StringToCorrectDoubleFormat(tbCost.Text)) * count, 
                                          cbTypeAdd.SelectedItem, cbImportanceAdd.SelectedItem);
                        ClearControlsForAddingGoods();
                        return;
                    }
                    string s = (Convert.ToDouble(StringToCorrectDoubleFormat(tbCost.Text)) * Convert.ToDouble(StringToCorrectDoubleFormat(tbCount.Text))).ToString("N");
                    Good.goodsAdd.Add(new Good(tbName.Text, Convert.ToDouble(StringToCorrectDoubleFormat(tbCost.Text)),
                                               count, cbTypeAdd.SelectedItem.ToString(), 
                                               cbImportanceAdd.SelectedItem.ToString(),
                                               Convert.ToDateTime(cbDate.SelectedItem)));
                    dgvRight.Rows.Add(tbName.Text, tbCost.Text, tbCount.Text, s, 
                                      cbTypeAdd.SelectedItem, cbImportanceAdd.SelectedItem);
                    ClearControlsForAddingGoods();
                    return;
                }

                //Для форматирования значений с плавающей точкой
                NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                nfi.NumberDecimalDigits = 3;

                if ((count - Math.Floor(count)) == 0)
                {
                    double dgvRightCount = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value)));
                    if ((dgvRightCount - Math.Floor(dgvRightCount)) == 0)
                    {
                        dgvRight.Rows[number].Cells[2].Value = (int)dgvRightCount + (int)count;
                    }
                    else
                    {
                        dgvRight.Rows[number].Cells[2].Value = (Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value))) + Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(tbCount.Text)))).ToString("N", nfi);
                    }
                }
                else
                {
                    dgvRight.Rows[number].Cells[2].Value = (Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value))) + Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(tbCount.Text)))).ToString("N", nfi);
                }
                dgvRight.Rows[number].Cells[3].Value = (Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[1].Value))) * Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value)))).ToString("N");
                Good.goodsAdd[number].Count += count;
                ClearControlsForAddingGoods();
            }
        }

        #endregion

        #region search

        private void tbSearchAdd_TextChanged(object sender, EventArgs e) // ++++++
        {
            // Отображение всех строк
            int counter = 0;
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
                for (int i = 0; i < dgvLeft.Rows.Count; i++)
                {
                    bool isVisible = false;
                    if (dgvLeft.Rows[i].Cells[0].Value.ToString().ToLower().IndexOf(tbSearchAdd.Text.ToLower()) != -1)
                    {
                        isVisible = true;
                    }
                    dgvLeft.Rows[i].Visible = isVisible;
                }
            }
        }

        #endregion

        #region buttons Left, Right, Save

        private void btnLeft_Click(object sender, EventArgs e) // ++++++
        {
            bool b = false;
            DataGridViewRow row = dgvRight.SelectedRows[0];
            row.Cells[2].Value = 1;
            row.Cells[3].Value = row.Cells[1].Value;

            for (int i = 0; i < dgvLeft.Rows.Count; i++)
            {
                if (dgvLeft.Rows[i].Cells[0].Value.Equals(row.Cells[0].Value))
                {
                    b = true;
                }
            }
            dgvRight.Rows.Remove(row);
            if (!b)
            {
                dgvLeft.Rows.Add(row);
                dgvLeft.Sort(dgvLeft.Columns[clLeftName.Name], ListSortDirection.Ascending);
            }
        }

        private void btnRight_Click(object sender, EventArgs e) // ++++++
        {
            DataGridViewRow row;
            row = dgvLeft.SelectedRows[0];
            int number = -1;
            double count = 1;
            double price = Convert.ToDouble(StringToCorrectDoubleFormat(row.Cells[1].Value.ToString()));
            DateTime date = DateTime.Now;
            if (EditMessageBox.InputBox(ref price, ref count, ref date) != DialogResult.OK)
            {
                return;
            }
            foreach (DataGridViewRow item in dgvRight.Rows)
            {
                if ((item.Cells[0].Value.Equals(row.Cells[0].Value)) &&
                    (item.Cells[1].Value.Equals(price)) &&
                    (Good.goodsAdd[item.Index].Time.Equals(date)))
                {
                    number = item.Index;
                }
            }
            if (number == -1)
            {
                Good.goodsAdd.Add(new Good(row.Cells[0].Value.ToString(),
                                           price, count, row.Cells[4].Value.ToString(),
                                           row.Cells[5].Value.ToString(), date));
                dgvRight.Rows.Add(row.Cells[0].Value, price, count, price * count,
                                  row.Cells[4].Value, row.Cells[5].Value);
            }
            else
            {
                Good.goodsAdd[number].Count = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value))) + count;
                dgvRight.Rows[number].Cells[2].Value = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value))) + count;
                dgvRight.Rows[number].Cells[3].Value = Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(row.Cells[3].Value))) * Convert.ToDouble(StringToCorrectDoubleFormat(Convert.ToString(dgvRight.Rows[number].Cells[2].Value)));
            }
        }

        private void btnSave_Click(object sender, EventArgs e) // ++++++
        {
            DataGridViewRow row;
            double sum = 0;
            for (int i = 0; i < dgvRight.Rows.Count;)
            {
                row = dgvRight.Rows[i];

                //Удаление строки из правой таблицы
                dgvRight.Rows.Remove(row);

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
                    dgvLeft.Rows.Add(row);
                    dgvLeft.Sort(dgvLeft.Columns[clLeftName.Name], ListSortDirection.Ascending);
                }
                else
                {
                    int count = dgvLeft.Rows.Count;
                    bool b = false;
                    for (int j = 0; j < count; j++)
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
                        dgvLeft.Rows.Add(row);
                        dgvLeft.Sort(dgvLeft.Columns[clLeftName.Name], ListSortDirection.Ascending);
                    }
                }
            }
            //Добавление строк в dgvShow
            foreach (Good item in Good.goodsAdd)
            {
                dgvShow.Rows.Add(item.Name, item.Price, item.Count, item.Price * item.Count, 
                                 item.Type, item.Importance, item.Time);
            }

            //Очистка Good.goodsAdd
            Good.goodsAdd.Clear();

            //Убирание новых значений из отображения в левой таблице при поиске в tbSearch
            tbSearchAdd_TextChanged(sender, e);

            //Сортировка dgvShow по дате и отмена выделения
            dgvShow.Sort(dgvShow.Columns[clShowDate.Name], ListSortDirection.Descending);

            // Изменение суммы в БД и в поле текущей суммы
            tbCurrentSum.Text = Conn.UpdateSumBySubtractionAtBills(CurrentUser.Login, sum).ToString("N");

            //Изменение суммы в tabShow tbSum
            Good.Sum += sum;
            tbSum.Text = Convert.ToString(Math.Round(Good.Sum, 2));

            //Изменение максимального значения nudMax в tabInfo
            nudMax.Maximum = (int)Good.MaxSum + 1;
            nudMax.Value = nudMax.Maximum;
        }

        #endregion

        #region dgvLeft

        private void dgvLeft_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) //++++++
        {
            DataGridViewRow row = dgvLeft.Rows[e.RowIndex];

            //Определение цвета по типу важности
            Color c = Good.GetColorOfImportance(row.Cells[dgvLeft.Columns.Count - 1].Value.ToString());

            //Изменение цвета каждой ячейки строки
            for (int j = 0; j < dgvLeft.Columns.Count; j++)
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
                selRowNumLeft = e.RowIndex;
                return;
            }
            if (selRowNumLeft < dgvLeft.Rows.Count)
                dgvLeft.Rows[selRowNumLeft].Selected = false;
            dgvLeft.Rows[e.RowIndex].Selected = true;
            selRowNumLeft = e.RowIndex;

            dgvContext = dgvLeft.Name;
            Point point = MousePosition;
            cmsDgv.Show(point.X, point.Y);
        }

        #endregion

        #region dgvRight

        private void dgvRight_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) // ++++++
        {
            DataGridViewRow row = dgvRight.Rows[e.RowIndex];
            //Определение цвета по типу важности
            Color c = Good.GetColorOfImportance(row.Cells[dgvRight.Columns.Count - 1].Value.ToString());
            //Изменение цвета каждой ячейки строки
            for (int j = 0; j < dgvRight.Columns.Count; j++)
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
                selRowNumRight = e.RowIndex;
                return;
            }
            if (selRowNumRight < dgvRight.Rows.Count)
                dgvRight.Rows[selRowNumRight].Selected = false;
            dgvRight.Rows[e.RowIndex].Selected = true;
            selRowNumRight = e.RowIndex;

            dgvContext = dgvRight.Name;
            Point point = MousePosition;
            cmsDgv.Show(point.X, point.Y);
        }

        #endregion

        #endregion

        #region tbInfo

        #region dgvShow

        private void dgvShow_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) // ++++++
        {
            DataGridViewRow row = dgvShow.Rows[e.RowIndex];
            //Определение цвета по типу важности
            Color c = Good.GetColorOfImportance(row.Cells[dgvShow.Columns.Count - 2].Value.ToString());
            //Изменение цвета каждой ячейки строки
            for (int j = 0; j < dgvShow.Columns.Count; j++)
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
                selRowNumShow = e.RowIndex;
                return;
            }
            if (selRowNumShow < dgvShow.Rows.Count)
                dgvShow.Rows[selRowNumShow].Selected = false;
            dgvShow.Rows[e.RowIndex].Selected = true;
            selRowNumShow = e.RowIndex;

            dgvContext = dgvShow.Name;
            Point point = MousePosition;
            cmsDgv.Show(point.X, point.Y);
        }

        private void tbSearchInfo_TextChanged(object sender, EventArgs e) // ++++++
        {
            // Отображение всех строк
            int counter = 0;
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
                for (int i = 0; i < dgvShow.Rows.Count; i++)
                {
                    bool isVisible = false;
                    if (dgvShow.Rows[i].Cells[0].Value.ToString().ToLower().IndexOf(tbSearchInfo.Text.ToLower()) != -1)
                    {
                        isVisible = true;
                    }
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

        private void listbFilter_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)e.Graphics.MeasureString(listbFilter.Items[e.Index].ToString(), listbFilter.Font, listbFilter.Width).Height;
        }

        private void listbFilter_DrawItem(object sender, DrawItemEventArgs e)
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

                if (gbDateLastTime.Enabled == true)
                {
                    gbDateLastTime.Enabled = false;
                }
            }
            else
            {
                if ((cbDay.SelectedIndex == 0) && (cbMonth.SelectedIndex == 0))
                {
                    if (gbDateLastTime.Enabled == false)
                    {
                        gbDateLastTime.Enabled = true;
                    }
                }
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
                            if (Convert.ToInt32(cbYear.SelectedItem) % 4 != 0)
                            {
                                cbDay.Items.RemoveAt(29);
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
                if (gbDateTime.Enabled == true)
                {
                    gbDateTime.Enabled = false;
                }
            }
            else
            {
                if (gbDateTime.Enabled == false)
                {
                    gbDateTime.Enabled = true;
                }
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
            for (int i = 0; i < clbImportanceInfo.Items.Count; i++)
            {
                clbImportanceInfo.SetItemChecked(i, chboxImportanceInfo.Checked);
            }
        }

        private void clbImportanceInfo_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            chboxImportanceInfo.Checked = (clbImportanceInfo.CheckedItems.Count != clbImportanceInfo.Items.Count) ? false : true;
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
            for (int i = 0; i < clbTypeInfo.Items.Count; i++)
            {
                clbTypeInfo.SetItemChecked(i, chboxTypeInfo.Checked);
            }
        }

        private void clbTypeInfo_SelectedIndexChanged(object sender, EventArgs e) // ++++++
        {
            chboxTypeInfo.Checked = (clbTypeInfo.CheckedItems.Count != clbTypeInfo.Items.Count) ? false : true;
        }

        #endregion

        #region buttons

        private void btnFilter_Click(object sender, EventArgs e) // ++++++
        {
            listbFilter.Items.Clear();

            #region arguments
            int sumMin = Convert.ToInt32(nudMin.Value);
            int sumMax = Convert.ToInt32(nudMax.Value);
            int numLastTime = cbLastTime.SelectedIndex;
            Date dt = new Date();
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

            #region sum
            listbFilter.Items.Add("По сумме: от " + nudMin.Value + " грн. до " + nudMax.Value + " грн.");
            #endregion

            #endregion

            #region filtration
            double sum = 0;
            for (int i = 0; i < dgvShow.Rows.Count; i++)
            {
                DataGridViewRow row = dgvShow.Rows[i];
                int count = 1;
                int countUp = 0;

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
                                if ((Convert.ToDateTime(row.Cells[6].Value) > DateTime.Today.AddDays(-1)) && (Convert.ToDateTime(row.Cells[6].Value) < DateTime.Today))
                                {
                                    countUp++;
                                }
                                break;
                            }
                        case 2:
                            {
                                if ((Convert.ToDateTime(row.Cells[6].Value) > DateTime.Today.AddDays(-6)) && (Convert.ToDateTime(row.Cells[6].Value) < DateTime.Now))
                                {
                                    countUp++;
                                }
                                break;
                            }
                        case 3:
                            {
                                if ((Convert.ToDateTime(row.Cells[6].Value) > DateTime.Today.AddMonths(-1)) && (Convert.ToDateTime(row.Cells[6].Value) < DateTime.Now))
                                {
                                    countUp++;
                                }
                                break;
                            }
                        case 4:
                            {
                                if ((Convert.ToDateTime(row.Cells[6].Value) > DateTime.Today.AddMonths(-3)) && (Convert.ToDateTime(row.Cells[6].Value) < DateTime.Now))
                                {
                                    countUp++;
                                }
                                break;
                            }
                        case 5:
                            {
                                if ((Convert.ToDateTime(row.Cells[6].Value) > DateTime.Today.AddMonths(-6)) && (Convert.ToDateTime(row.Cells[6].Value) < DateTime.Now))
                                {
                                    countUp++;
                                }
                                break;
                            }
                        case 6:
                            {
                                if ((Convert.ToDateTime(row.Cells[6].Value) > DateTime.Today.AddYears(-1)) && (Convert.ToDateTime(row.Cells[6].Value) < DateTime.Now))
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
                            if (Convert.ToDateTime(row.Cells[6].Value).Year == dt.Year)
                            {
                                countUp++;
                            }
                        }
                        else
                        {
                            if ((Convert.ToDateTime(row.Cells[6].Value).Year == dt.Year) && (Convert.ToDateTime(row.Cells[6].Value).Month == dt.Month))
                            {
                                countUp++;
                            }
                        }
                    }
                    else
                    {
                        if ((Convert.ToDateTime(row.Cells[6].Value).Year == dt.Year) && (Convert.ToDateTime(row.Cells[6].Value).Month == dt.Month) && (Convert.ToDateTime(row.Cells[6].Value).Day == dt.Day))
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
                for (int j = 0; j < clbImportanceInfo.CheckedItems.Count; j++)
                {
                    if (clbImportanceInfo.CheckedItems[j].ToString() == row.Cells[5].Value.ToString())
                    {
                        countUp++;
                        break;
                    }
                }
                #endregion

                if (count != countUp)
                {
                    dgvShow.Rows[i].Visible = false;
                    continue;
                }

                count++;

                #region type
                for (int j = 0; j < clbTypeInfo.CheckedItems.Count; j++)
                {
                    if (clbTypeInfo.CheckedItems[j].ToString() == row.Cells[4].Value.ToString())
                    {
                        countUp++;
                        break;
                    }
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

            tbSum.Text = Convert.ToString(Math.Round(sum, 2));
        }

        private void btnCancelInfo_Click(object sender, EventArgs e) // ++++++
        {
            int counter = 0;
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
            tbSum.Text = Convert.ToString(Math.Round(Good.Sum, 2));
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