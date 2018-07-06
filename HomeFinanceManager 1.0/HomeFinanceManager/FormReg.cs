using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HomeFinanceManager
{
    public partial class FormReg : FormBase
    {
        public FormReg() // ++++++
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e) // ++++++
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e) // ++++++
        {
            if (string.IsNullOrEmpty(this.tbLogin.Text))
            {
                if (string.IsNullOrEmpty(this.tbPassword.Text))
                {
                    MessageBox.Show("Выберите логин и пароль!");
                    return;
                }
                MessageBox.Show("Выберите логин!");
                return;
            }
            if (string.IsNullOrEmpty(this.tbPassword.Text))
            {
                MessageBox.Show("Выберите пароль!");
                return;
            }
            List<string> listLogins = Conn.SelectAllLogins();
            for (int i = 0; i < listLogins.Count; i++)
            {
                if (tbLogin.Text == listLogins[i])
                {
                    MessageBox.Show("Выберите другой логин!");
                    return;
                }
            }
            Conn.InsertLoginPassword(tbLogin.Text, tbPassword.Text);
            Conn.UpdateOrInsertSumAtBills(tbLogin.Text, 0);
            MessageBox.Show("Вы зарегистрированы!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.btnCancel_Click(sender, e);
        }

        private void chbPassword_CheckedChanged(object sender, EventArgs e) // ++++++
        {
            tbPassword.UseSystemPasswordChar = (chbPassword.Checked == true) ? false : true;
        }
    }
}