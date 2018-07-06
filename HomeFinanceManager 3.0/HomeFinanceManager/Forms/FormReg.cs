using System;
using System.Linq;
using System.Windows.Forms;

namespace HomeFinanceManager.Forms
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
            var listLogins = Conn.SelectAllLogins();
            if (listLogins.Any(t => tbLogin.Text == t))
            {
                MessageBox.Show("Выберите другой логин!");
                return;
            }
            Conn.InsertLoginPassword(tbLogin.Text, tbPassword.Text);
            Conn.UpdateOrInsertSumAtBills(tbLogin.Text, 0);
            Conn.AddAccount(tbLogin.Text, "Наличный счёт");
            MessageBox.Show("Вы зарегистрированы!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.btnCancel_Click(sender, e);
        }

        private void chbPassword_CheckedChanged(object sender, EventArgs e) // ++++++
        {
            tbPassword.UseSystemPasswordChar = (chbPassword.Checked != true);
        }
    }
}