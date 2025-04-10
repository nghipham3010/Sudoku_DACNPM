using DevExpress.XtraEditors;
using Sudoku_Game.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_Game
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        private string username; // Biến toàn cục để lưu tên người dùng
        public frmLogin()
        {
            InitializeComponent();
            this.panel1.BackColor = Color.Transparent;
            this.label4.BackColor = Color.Transparent;
            this.lblSignIn.BackColor = Color.Transparent;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (var context = new Model1())
            {
                string username = txtUserName.Text;
                string password = txtPass.Text;

                var user = context.LoginInformations.FirstOrDefault(u => u.TK == username);

                if (user == null)
                {
                    MessageBox.Show("Tài khoản không tồn tại.");
                    return;
                }

                // Nếu tài khoản đang bị khóa
                if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.Now)
                {
                    TimeSpan remaining = user.LockoutEnd.Value - DateTime.Now;
                    MessageBox.Show($"Tài khoản đang bị khóa. Vui lòng thử lại sau {remaining.Minutes} phút {remaining.Seconds} giây.");
                    return;
                }

                // So sánh mật khẩu dạng plain-text (không mã hóa)
                if (user.MK == password)
                {
                    // Reset lại đếm sai nếu đăng nhập thành công
                    user.FailedAttempts = 0;
                    user.LockoutEnd = null;
                    context.SaveChanges();

                    this.username = username;
                    if (user.Quyen == "admin")
                    {
                        /* */
                    }
                    else
                    {
                        Form1 userForm = new Form1(username);
                        userForm.Show();
                    }
                    this.Hide();
                }
                else
                {
                    user.FailedAttempts++;

                    if (user.FailedAttempts >= 5)
                    {
                        user.LockoutEnd = DateTime.Now.AddMinutes(3); // Khóa 3 phút
                        MessageBox.Show("Bạn đã nhập sai quá nhiều lần. Tài khoản bị khóa trong 3 phút.");
                    }
                    else
                    {
                        MessageBox.Show($"Mật khẩu không đúng. Bạn còn {5 - user.FailedAttempts} lần thử.");
                    }

                    context.SaveChanges();
                }
            }
        }

        private void lblSignIn_Click(object sender, EventArgs e)
        {
            frmSignIn signInForm = new frmSignIn();
            signInForm.ShowDialog(); // Hiển thị form đăng ký
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            /* */
        }
    }
}