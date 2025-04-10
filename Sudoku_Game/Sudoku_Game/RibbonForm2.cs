﻿using DevExpress.XtraBars;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
namespace Sudoku_Game
{
    public partial class frmAdmin : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Model1 context;
        public frmAdmin()
        {
            InitializeComponent();
            context = new Model1(); // Khởi tạo context

            // Đăng ký sự kiện CellClick cho dgvLogin
            this.dgvLogin.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLogin_CellContentClick);
        }

        private void btnDeleteUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (dgvLogin.SelectedRows.Count > 0)
            {
                var selectedRow = dgvLogin.SelectedRows[0];

                // Lấy tên đăng nhập và quyền
                string username = selectedRow.Cells[0].Value.ToString(); // TK
                string role = selectedRow.Cells[3].Value.ToString().Trim().ToLower(); // Quyen

                // Không cho xóa nếu là admin
                if (role == "admin")
                {
                    MessageBox.Show("Không thể xóa tài khoản có quyền admin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var userToDelete = context.LoginInformations.FirstOrDefault(u => u.TK == username);

                if (userToDelete != null)
                {
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa người dùng '{username}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        context.LoginInformations.Remove(userToDelete);
                        context.SaveChanges();
                        MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một người dùng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void LoadData()
        {
            context = new Model1(); // Làm mới để đảm bảo dữ liệu mới nhất
            var users = context.LoginInformations.ToList();
            BrindGrid(users);
        }
        // Phương thức để truyền dữ liệu vào DataGridView
        private void BrindGrid(List<LoginInformation> listUsers)
        {
            dgvLogin.Rows.Clear(); // Xóa các hàng hiện có

            foreach (var user in listUsers)
            {
                int index = dgvLogin.Rows.Add(); // Thêm hàng mới
                dgvLogin.Rows[index].Cells[0].Value = user.TK;     // Tên đăng nhập
                dgvLogin.Rows[index].Cells[1].Value = user.MK;     // Mật khẩu
                dgvLogin.Rows[index].Cells[2].Value = user.Email;   // Email
                dgvLogin.Rows[index].Cells[3].Value = user.Quyen;    // Quyền
                dgvLogin.Rows[index].Cells[4].Value = user.Score;    // Điểm số
            }
        }

        private void dgvLogin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra chỉ số hàng hợp lệ
            {
                // Lấy thông tin từ các ô trong hàng được chọn
                var selectedRow = dgvLogin.Rows[e.RowIndex];
                string username = selectedRow.Cells[0].Value?.ToString();
                string password = selectedRow.Cells[1].Value?.ToString();
                string email = selectedRow.Cells[2].Value?.ToString();

                // Hiển thị thông tin người dùng trong MessageBox
                string message = $"Tên Đăng Nhập: {username}\nMật Khẩu: {password}\nEmail: {email}";
                MessageBox.Show(message, "Thông Tin Người Dùng");
            }
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            var nonAdminUsers = context.LoginInformations
      .Where(u => u.Quyen.ToLower() != "admin")
      .ToList();

            if (nonAdminUsers.Count == 0)
            {
                MessageBox.Show("Không có tài khoản nào để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa tất cả tài khoản người dùng (trừ admin)?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                context.LoginInformations.RemoveRange(nonAdminUsers);
                context.SaveChanges();

                MessageBox.Show("Đã xóa tất cả tài khoản người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); // Làm mới DataGridView
            }
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            List<LoginInformation> listUsers = context.LoginInformations.ToList(); // Lấy danh sách người dùng
            BrindGrid(listUsers); // Truyền dữ liệu vào DataGridView
            LoadData(); // Gọi LoadData để làm mới dữ liệu
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmForm3 frmForm = new frmForm3();
            frmForm.Show();
        }

        private void btnAddAdmin_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmSignIn signInForm = new frmSignIn();
            signInForm.RoleToAssign = "admin"; // Truyền quyền admin vào form
            signInForm.FormClosed += (s, args) => {
                LoadData(); // Sau khi đăng ký xong, làm mới lại DataGridView
            };
            signInForm.ShowDialog();
        }

        private void btnToFromLogin_Click(object sender, EventArgs e)
        {

        }

        private void btnExportPdf_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveDialog.FileName = $"BaoCaoNguoiDung_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                ExportDataGridViewToPdf(saveDialog.FileName);
                MessageBox.Show("Xuất PDF thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ExportDataGridViewToPdf(string filePath)
        {
            Document document = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 10f);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            // Font tiếng Việt có dấu
            BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);


            PdfPTable table = new PdfPTable(dgvLogin.Columns.Count);
            table.WidthPercentage = 100;

            // Header
            foreach (DataGridViewColumn column in dgvLogin.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, font));
                cell.BackgroundColor = new BaseColor(240, 240, 240);
                table.AddCell(cell);
            }

            // Dữ liệu
            foreach (DataGridViewRow row in dgvLogin.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string text = cell.Value?.ToString() ?? "";
                        table.AddCell(new Phrase(text, font));
                    }
                }
            }

            document.Add(table);
            document.Close();
        }
    }
}