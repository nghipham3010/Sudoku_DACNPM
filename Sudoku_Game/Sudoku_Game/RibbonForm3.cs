using DevExpress.XtraBars;
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
    public partial class frmForm3 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Model1 context;
        public frmForm3()
        {
            InitializeComponent();
            context = new Model1();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView1.Rows[e.RowIndex];
                string username = selectedRow.Cells[0].Value?.ToString();

            }
        }
        private void BindGrid(List<LoginInformation> listLogin)
        {
            dataGridView1.Rows.Clear();
            foreach (LoginInformation item in listLogin)
            {
                int indext = dataGridView1.Rows.Add();
                dataGridView1.Rows[indext].Cells[0].Value = item.TK;
                dataGridView1.Rows[indext].Cells[1].Value = item.Score;
            }
        }

        private void frmForm3_Load(object sender, EventArgs e)
        {
            // Lấy danh sách người chơi đã sắp xếp theo điểm giảm dần
            var sort = context.LoginInformations.OrderByDescending(s => s.Score).ToList();

            // Hiển thị danh sách đã sắp xếp
            BindGrid(sort);
        }

        private void btnResetScore_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Lặp qua toàn bộ danh sách người chơi
            foreach (var user in context.LoginInformations)
            {
                user.Score = null; // Đặt điểm về null
            }

            // Lưu thay đổi xuống database
            context.SaveChanges();

            // Cập nhật lại DataGridView
            var updatedList = context.LoginInformations.OrderByDescending(s => s.Score).ToList();
            BindGrid(updatedList);

            // Thông báo cho người dùng
            MessageBox.Show("Đã đặt lại tất cả điểm của người chơi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}