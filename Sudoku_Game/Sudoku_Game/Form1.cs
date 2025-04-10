using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace Sudoku_Game
{
    public partial class Form1 : Form
    {
        private string username; // Khai báo biến lưu tên người dùng
        public Form1()
        {
            InitializeComponent();
            this.lblName.BackColor = Color.Transparent;
            this.lblNewGame.BackColor = Color.Transparent;
            this.lblLevel.BackColor = Color.Transparent;
            this.lblOut.BackColor = Color.Transparent;
            this.label1.BackColor = Color.Transparent;
            this.label2.BackColor = Color.Transparent;
            this.username = username; // Lưu tên người dùng
        }

        private void lblNewGame_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu người dùng đã chọn mức độ
            if (cmbLevel.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn mức độ để bắt đầu game!", "Thông báo");
                return;
            }

            // Lấy giá trị từ ComboBox (Mức độ khó)
            string selectedLevel = cmbLevel.SelectedItem.ToString();

            int difficulty = 0; // Số lượng ô cần xóa dựa vào mức độ

            // Xác định độ khó dựa trên lựa chọn của người dùng
            if (selectedLevel == "Dễ")
            {
                difficulty = 30; // Xóa 30 ô cho mức độ dễ
            }
            else if (selectedLevel == "Vừa")
            {
                difficulty = 45; // Xóa 45 ô cho mức độ vừa
            }
            else if (selectedLevel == "Khó")
            {
                difficulty = 55; // Xóa 55 ô cho mức độ khó
            }

            // Tạo bảng Sudoku mới
            SudokuGenerator generator = new SudokuGenerator();
            int[,] board = generator.GenerateCompleteBoard(); // Bảng hiện tại
            int[,] solvedBoard = generator.GetSolvedBoard(); // Bảng giải đúng
            generator.RemoveCells(board, difficulty); // Xóa các ô dựa trên độ khó

            // Hiển thị bảng Sudoku lên form frmSudocku
            frmSudocku sudokuForm = new frmSudocku(board, solvedBoard, difficulty, username);
            sudokuForm.Owner = this;
            sudokuForm.Show();
        }

        private void lblOut_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận thoát\
            DialogResult result = MessageBox.Show("Bạn có muốn thoát trò chơi không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kiểm tra người dùng chọn Yes hay No
            if (result == DialogResult.Yes)
            {
                // Thoát khỏi ứng dụng
                Application.Exit();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
        }
    }
}
