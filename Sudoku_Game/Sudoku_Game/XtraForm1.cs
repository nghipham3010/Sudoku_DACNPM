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
    public partial class frmWinner : DevExpress.XtraEditors.XtraForm
    {
        private int score;
        private string username; // Tên đăng nhập của người chơi
        private int timeBonusScore; // Điểm thưởng dựa trên thời gian
        private Model1 context;
        public frmWinner(int score, string username, TimeSpan elapsedTime)
        {
            InitializeComponent();
            this.score = score;
            this.username = username; // Nhận tên đăng nhập từ form trước
            context = new Model1();
            timeBonusScore = CalculateTimeBonus(elapsedTime); // Tính điểm thưởng
            UpdateScore(username, score + timeBonusScore); // Cập nhật điểm
            lblScore.Text = $"Điểm của bạn: {score + timeBonusScore}";
        }
        // Hàm tính điểm thưởng dựa trên thời gian
        private int CalculateTimeBonus(TimeSpan elapsedTime)
        {
            int timeBonus = 0;
            if (elapsedTime.TotalSeconds < 300) // Nếu hoàn thành dưới 5 phút
            {
                timeBonus = 500; // Thưởng thêm 500 điểm
            }
            else if (elapsedTime.TotalSeconds < 600) // Nếu hoàn thành dưới 10 phút
            {
                timeBonus = 250; // Thưởng thêm 250 điểm
            }
            return timeBonus;
        }

        private void UpdateScore(string username, int totalScore)
        {
            var user = context.LoginInformations.FirstOrDefault(u => u.TK == username);
            if (user != null)
            {
                // Cập nhật điểm tổng
                user.Score += totalScore; // Cộng điểm hiện tại và điểm thưởng
                context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
            else
            {
                MessageBox.Show("Người dùng không tồn tại!", "Thông Báo");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}