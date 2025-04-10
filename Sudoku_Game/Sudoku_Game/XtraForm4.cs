using DevExpress.XtraEditors;
using Sudoku_Game.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_Game
{
    public partial class frmSignIn : DevExpress.XtraEditors.XtraForm
    {
        // Thuộc tính nhận quyền từ form cha
        public string RoleToAssign { get; set; } = "user";
        public frmSignIn()
        {
            InitializeComponent();
            this.panel1.BackColor = Color.Transparent;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            using (var context = new Model1())
            {
                string username = txtUserName.Text;
                string password = txtPass.Text;
                string email = txtEmail.Text;

                var existingUser = context.LoginInformations.FirstOrDefault(u => u.TK == username);
                if (existingUser != null)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.");
                    return;
                }

                var newUser = new LoginInformation
                {
                    TK = username,
                    MK = password,
                    Email = email,
                    Quyen = RoleToAssign, // Quyền mặc định là user
                    FailedAttempts = 0,
                    LockoutEnd = null
                };

                context.LoginInformations.Add(newUser);

                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Đăng ký thành công!");
                    this.Close(); // Đóng form đăng ký
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show("Đăng ký không thành công: " + ex.Message);
                }
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}