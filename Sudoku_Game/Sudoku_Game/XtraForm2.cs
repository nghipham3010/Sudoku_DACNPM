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
    public partial class frmLose : DevExpress.XtraEditors.XtraForm
    {
        private int score;
        private string username; // Biến lưu tên người dùng
        private Model1 context;
        public frmLose(int score, string username)
        {
            InitializeComponent();
            this.score = score; // Nhận điểm từ frmSudocku
            this.username = username;
            context = new Model1();
            lblScore.Text = $"Điểm của bạn: {score}"; // Hiển thị điểm thua
            UpdateScore(username, score); // Cập nhật điểm
            lblScore.BackColor = Color.Transparent;
        }
        private void UpdateScore(string username, int score)
        {
            var user = context.LoginInformations.FirstOrDefault(u => u.TK == username);
            if (user != null)
            {
                user.Score = score; // Cập nhật điểm
                context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
            else
            {
                MessageBox.Show("Người dùng không tồn tại!", "Thông Báo");
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}