using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HomeFinanceManager.DataModels;

namespace HomeFinanceManager.Forms
{
    public partial class FormLog : FormBase
    {
        public FormLog() // ++++++
        {
            InitializeComponent();
        }

        private void linklbReg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // ++++++
        {
            var formReg = new FormReg();
            formReg.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e) // ++++++
        {
            this.Close();
        }

        private void btnLog_Click(object sender, EventArgs e) // ++++++
        {
            if (string.IsNullOrEmpty(this.tbLogin.Text))
            {
                if (string.IsNullOrEmpty(this.tbPassword.Text))
                {
                    MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Логин не введен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbPassword.Clear();
                return;
            }
            if (string.IsNullOrEmpty(this.tbPassword.Text))
            {
                MessageBox.Show("Пароль не введен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbLogin.Clear();
                return;
            }
            List<User> listUsers;
            try
            {
                listUsers = Conn.SelectAllUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            foreach (var user in listUsers)
            {
                if (user.Login == tbLogin.Text)
                {
                    if (user.Password == Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(tbPassword.Text)))
                    {
                        CurrentUser.Login = user.Login;
                        CurrentUser.Password = user.Password;
                        CurrentUser.Id = user.Id;
                        CurrentUser.IdBalance = user.IdBalance;
                        var formMain = new FormMain(this);
                        formMain.Show();
                        tbLogin.Clear();
                        tbPassword.Clear();
                        return;
                    }
                    MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbPassword.Clear();
                    return;
                }
            }
            MessageBox.Show("Пользователь с таким логин не существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            tbLogin.Clear();
            tbPassword.Clear();
        }

        private void chbPassword_CheckedChanged(object sender, EventArgs e) // ++++++
        {
            tbPassword.UseSystemPasswordChar = (chbPassword.Checked != true);
        }
    }
}
