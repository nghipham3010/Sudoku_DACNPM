using DevExpress.XtraBars;
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
    public partial class frmSudocku : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private TimeSpan elapsedTime = TimeSpan.Zero; //Khởi tạo thời gian từ 0
        private TextBox currentTextBox; // Biến chỉ số hiện tại
        private TextBox[,] textBoxes; // Mảng chứa các TextBox
        private Button[,] btnNumbers;  // Mảng chứa các Button 
        private int difficulty; // Biến để lưu độ khó
        //Khai báo Stack để lưu vị trí của ô mà người dùng vừa điền
        private Stack<(int Row, int Col, string Value)> recentEntries = new Stack<(int, int, string)>(); // Lưu trữ thông tin nhập gần nhất
        private int[,] solution; // Lưu bảng giải đúng
        private int incorrectAttempts = 0; // Biến để đếm số lần nhập sai
        private int score; // Biến để lưu điểm số
        private DateTime startTime; // Biến để lưu thời gian bắt đầu
        private string username; // Biến để lưu tên đăng nhập
        public frmSudocku(int[,] board, int[,] solvedBoard, int difficulty, string username)
        {
            InitializeComponent();
            InitializeTextBoxes();
            InitializeNumberButtons();
            this.difficulty = difficulty; // Gán độ khó được truyền từ Form1
            // Hiển thị bảng Sudoku lên các TextBox
            DisplaySudokuBoard(board);
            UpdateAttemptsLabel(); // Cập nhật giao diện ToolStrip
            this.solution = solvedBoard; // Lưu bảng giải đúng
            this.pnlSudocku.BackColor = Color.Transparent;
            this.pnlNumber.BackColor = Color.Transparent;
            this.statusStrip.BackColor = Color.Transparent;
            this.statusStrip1.BackColor = Color.Transparent;
            this.statusStrip2.BackColor = Color.Transparent;
            startTime = DateTime.Now; // Gán thời gian bắt đầu khi form tải
            // Gắn sự kiện cho nút "Start New Game"
            btnStart.ItemClick += new ItemClickEventHandler(btnStar_ItemClick);
            // Gắn sự kiện Clear cho nút btnClear
            btnClear.ItemClick += new ItemClickEventHandler(btnClear_ItemClick);
            this.username = username; // Lưu tên đăng nhập
        }
        private void InitializeTextBoxes()
        {
            textBoxes = new TextBox[9, 9]; // Mảng chứa các TextBox

            // Đảm bảo panel có hình vuông và tính toán kích thước cho mỗi TextBox
            // Lấy kích thước nhỏ hơn giữa chiều rộng và chiều cao của panel để đảm bảo hình vuông
            int panelSize = Math.Min(pnlSudocku.Width, pnlSudocku.Height);

            int padding = 5; // Khoảng cách nhỏ giữa các TextBox
            int textBoxSize = (panelSize - padding * 10) / 9; // Tính kích thước TextBox sao cho vừa khung, trừ padding
            // Khởi tạo và sắp xếp các TextBox
            for (int i = 0; i < 9; i++) // Duyệt qua từng hàng
            {
                for (int j = 0; j < 9; j++) // Duyệt qua từng cột
                {
                    textBoxes[i, j] = new TextBox // Tạo một TextBox mới
                    {
                        Width = textBoxSize, // Gán chiều rộng cho TextBox
                        Height = textBoxSize, // Gán chiều cao cho TextBox
                        // Tính toán vị trí của từng TextBox
                        Location = new Point(padding + j * (textBoxSize + padding), padding + i * (textBoxSize + padding)),
                        MaxLength = 1, // Giới hạn nhập 1 ký tự
                        TextAlign = HorizontalAlignment.Center, // Căn giữa văn bản
                        Font = new Font("Arial", 14), // Căn chỉnh font
                    };
                    // Gắn sự kiện click cho TextBox
                    textBoxes[i, j].Click += TextBox_Click;
                    // Gắn sự kiện TextChanged để xử lý khi người dùng nhập dữ liệu
                    textBoxes[i, j].TextChanged += TextBox_TextChanged;
                    this.pnlSudocku.Controls.Add(textBoxes[i, j]); // Thêm vào panel
                }
            }

            // Vẽ lưới Sudoku bằng sự kiện Paint của Panel
            pnlSudocku.Paint += new PaintEventHandler(DrawSudokuGrid);
        }
        private void TextBox_Click(object sender, EventArgs e)
        {
            // Gán TextBox hiện tại khi nhấp vào
            currentTextBox = sender as TextBox;
            pnlSudocku.Invalidate(); // Vẽ lại panel để cập nhật đường viền
        }
        // Vẽ lưới Sudoku
        private void DrawSudokuGrid(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;  // Lấy đối tượng Graphics từ sự kiện Paint
            int panelSize = Math.Min(pnlSudocku.Width, pnlSudocku.Height); // Lấy kích thước panel
            int textBoxSize = panelSize / 9; // Kích thước TextBox
            int gridSize = textBoxSize * 9; // Tổng kích thước lưới

            Pen thinPen = new Pen(Color.Gray, 1); // Đường viền mỏng màu xám
            Pen thickPen = new Pen(Color.Aqua, 3); // Đường viền dày màu xanh cho các ô 3x3
            Pen highlightPen = new Pen(Color.Blue, 2); // Đường viền để tô màu

            // Vẽ các đường ngang và dọc
            for (int i = 0; i <= 9; i++) // Duyệt qua từng đường (0 đến 9)
            {
                // Vẽ các đường dọc
                if (i % 3 == 0)
                {
                    // Vẽ đường viền dày
                    g.DrawLine(thickPen, i * textBoxSize, 0, i * textBoxSize, gridSize);

                }
                else
                {
                    // Vẽ đường viền mỏng
                    g.DrawLine(thinPen, i * textBoxSize, 0, i * textBoxSize, gridSize);
                }

                // Vẽ các đường ngang
                if (i % 3 == 0)
                {
                    // Vẽ đường viền dày
                    g.DrawLine(thickPen, 0, i * textBoxSize, gridSize, i * textBoxSize);
                }
                else
                {
                    // Vẽ đường viền mỏng
                    g.DrawLine(thinPen, 0, i * textBoxSize, gridSize, i * textBoxSize);
                }
            }

            // Tô đường viền cho ô 3x3 và hàng/cột
            if (currentTextBox != null)
            {
                int currentRow = currentTextBox.TabIndex / 9; // Lấy chỉ số hàng
                int currentCol = currentTextBox.TabIndex % 9; // Lấy chỉ số cột

                // Tô màu cho ô 3x3
                int startRow = (currentRow / 3) * 3; // Xác định hàng bắt đầu của ô 3x3
                int startCol = (currentCol / 3) * 3; // Xác định cột bắt đầu của ô 3x3
                // Duyệt qua từng hàng của ô 3x3
                for (int i = 0; i < 3; i++)
                {
                    // Duyệt qua từng cột của ô 3x3
                    for (int j = 0; j < 3; j++)
                    {
                        // Vẽ hình chữ nhật cho ô 3x3
                        g.DrawRectangle(highlightPen, (startCol + j) * textBoxSize, (startRow + i) * textBoxSize, textBoxSize, textBoxSize);
                    }
                }

                // Tô màu cho hàng và cột
                g.DrawLine(highlightPen, currentCol * textBoxSize, 0, currentCol * textBoxSize, gridSize); // Đường dọc
                g.DrawLine(highlightPen, 0, currentRow * textBoxSize, gridSize, currentRow * textBoxSize); // Đường ngang
            }

            // Giải phóng tài nguyên đồ họa
            thinPen.Dispose();
            thickPen.Dispose();
            highlightPen.Dispose();
        }

        // Hiển thị Sudoku puzzle lên các TextBox
        private void DisplaySudokuBoard(int[,] board)
        {
            for (int i = 0; i < 9; i++) // Duyệt qua từng hàng của bảng Sudoku
            {
                for (int j = 0; j < 9; j++) // Duyệt qua từng cột của bảng Sudoku
                {
                    if (board[i, j] != 0) // Nếu ô không trống (có giá trị khác 0)
                    {
                        textBoxes[i, j].Text = board[i, j].ToString(); // Điền giá trị vào ô tương ứng trên giao diện
                        textBoxes[i, j].Enabled = false; // Vô hiệu hóa ô để không cho người dùng nhập vào
                    }
                    else // Nếu ô trống (giá trị bằng 0)
                    {
                        textBoxes[i, j].Text = "";// Đặt lại nội dung ô thành rỗng
                        textBoxes[i, j].Enabled = true; // Cho phép người dùng nhập vào ô trống
                    }
                }
            }
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (currentTextBox == null) return; // Nếu chưa chọn ô nào

            TextBox textBox = currentTextBox; // Ô hiện tại
            int row = textBox.TabIndex / 9; // Lấy chỉ số hàng
            int col = textBox.TabIndex % 9; // Lấy chỉ số cột
                                            // Chỉ lưu giá trị trước đó nếu ô không rỗng
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                // Lưu thông tin ô và giá trị
                recentEntries.Push((row, col, textBox.Text));
            }
            // Kiểm tra nếu ô không rỗng
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                int number;
                if (int.TryParse(textBox.Text, out number))
                {
                    // So sánh giá trị nhập với giá trị đúng trong bảng giải
                    if (number == solution[row, col])
                    {
                        textBox.BackColor = Color.White; // Đúng, đổi thành màu trắng
                        // Tính điểm
                        UpdateScore(row, col); // Cập nhật điểm số
                    }
                    else
                    {
                        textBox.BackColor = Color.Red; // Sai, đổi thành màu đỏ
                        incorrectAttempts++; // Tăng số lần sai
                        UpdateAttemptsLabel(); // Cập nhật số lần sai

                        // Kiểm tra số lần sai
                        if (incorrectAttempts > 3)
                        {
                            // ==================== THÔNG BÁO THUA ====================
                            //ShowLoseForm(); // Hiển thị thông báo thua
                            //=========================================================
                        }
                    }
                }
                else
                {
                    textBox.BackColor = Color.Red; // Nếu không phải số, đổi màu thành đỏ
                    incorrectAttempts++; // Tăng số lần sai
                    UpdateAttemptsLabel(); // Cập nhật số lần sai

                    // Kiểm tra số lần sai
                    if (incorrectAttempts > 3)
                    {
                        // ==================== THÔNG BÁO THUA ====================
                        //ShowLoseForm(); // Hiển thị thông báo thua
                        //===========================================================
                    }
                }
            }
            else
            {
                textBox.BackColor = Color.White; // Nếu ô trống, đặt lại màu nền
            }

            // Kiểm tra xem Sudoku đã được giải hay chưa
            if (IsSudokuSolved())
            {
                ShowWinnerForm(); // Hiển thị thông báo chiến thắng
            }
        }

        // Hàm kiểm tra nếu giá trị tại vị trí (row, col) có hợp lệ
        private bool IsValidInput(int row, int col, string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return true; // Nếu ô trống, không cần kiểm tra.
            }

            int number;
            if (!int.TryParse(input, out number) || number < 1 || number > 9)
            {
                return false; // Nếu không phải số từ 1-9
            }

            // Kiểm tra hàng
            for (int j = 0; j < 9; j++)
            {
                if (j != col && textBoxes[row, j].Text == input)
                {
                    return false; // Trùng số trong hàng
                }
            }

            // Kiểm tra cột
            for (int i = 0; i < 9; i++)
            {
                if (i != row && textBoxes[i, col].Text == input)
                {
                    return false; // Trùng số trong cột
                }
            }

            // Kiểm tra khối 3x3
            int startRow = row / 3 * 3;// Lấy chỉ số hàng bắt đầu của khối 3x3
            int startCol = col / 3 * 3;// Lấy chỉ số cột bắt đầu của khối 3x3
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int r = startRow + i; // Tính chỉ số hàng hiện tại trong khối 3x3
                    int c = startCol + j; // Tính chỉ số cột hiện tại trong khối 3x3
                    if ((r != row || c != col) && textBoxes[r, c].Text == input)
                    {
                        return false; // Trùng số trong khối 3x3
                    }
                }
            }

            return true; // Nếu không có trùng lặp nào, trả về true
        }


        private void InitializeNumberButtons()
        {
            int buttonSize = 50; // Kích thước của nút (30 pixels)
            int gridSize = 3;  // Kích thước lưới (3x3)
            int totalButtons = gridSize * gridSize; // Tổng số nút (3x3 = 9 nút)

            // Giả định rằng pnlNumber là panel chứa các nút
            int containerWidth = pnlNumber.Width; // Chiều rộng của panel
            int containerHeight = pnlNumber.Height;// Chiều cao của panel

            // Tính toán khoảng cách giữa các nút dựa trên kích thước container và kích thước lưới
            // Khoảng cách ngang
            int horizontalSpacing = (containerWidth - (gridSize * buttonSize)) / (gridSize + 1);
            // Khoảng cách dọc
            int verticalSpacing = (containerHeight - (gridSize * buttonSize)) / (gridSize + 1);
            // Tải hình ảnh star.jpg một lần để sử dụng cho tất cả các nút
            Image starImage = Image.FromFile("image/button_background.jpg");

            for (int i = 0; i < totalButtons; i++)
            {
                Button btnNumber = new Button();// Tạo một nút mới
                btnNumber.Text = (i + 1).ToString();// Đặt văn bản nút từ 1 đến 9
                btnNumber.Width = buttonSize;// Gán chiều rộng cho nút
                btnNumber.Height = buttonSize;// Gán chiều cao cho nút

                // Gán hình ảnh star.jpg làm nền cho nút
                btnNumber.BackgroundImage = starImage; // Sử dụng hình ảnh ngôi sao
                btnNumber.BackgroundImageLayout = ImageLayout.Stretch; // Điều chỉnh hình ảnh để vừa khít nút
                btnNumber.TextAlign = ContentAlignment.MiddleCenter; // Căn giữa văn bản số trên hình ảnh

                // Làm số trên nút sáng hơn và lớn hơn
                btnNumber.ForeColor = Color.Salmon; // Đặt màu chữ thành màu vàng sáng (có thể đổi thành Color.White nếu cần)
                btnNumber.Font = new Font("Arial", 16, FontStyle.Bold); // Tăng kích thước font và in đậm
                btnNumber.TextAlign = ContentAlignment.MiddleCenter; // Căn giữa văn bản số trên hình ảnh

                // Tính toán hàng và cột dựa trên chỉ số nút
                int row = i / gridSize;// Xác định hàng
                int column = i % gridSize; // Xác định cột

                // Đặt vị trí cho nút dựa trên hàng và cột, với khoảng cách đã tính toán
                btnNumber.Location = new Point(
                    // Vị trí theo chiều ngang
                    horizontalSpacing + column * (buttonSize + horizontalSpacing),
                    // Vị trí theo chiều dọc
                    verticalSpacing + row * (buttonSize + verticalSpacing)
                );

                btnNumber.Click += BtnNumber_Click; // Gắn sự kiện click cho nút
                this.pnlNumber.Controls.Add(btnNumber); // Thêm nút vào panel
            }
        }
        private void BtnNumber_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có TextBox hiện tại không
            if (currentTextBox != null)
            {
                // Gán giá trị từ nút vào TextBox hiện tại
                Button clickedButton = sender as Button;
                currentTextBox.Text = clickedButton.Text;
            }
            else
            {
                MessageBox.Show("Hãy chọn một ô để điền số.");
            }
        }
        private void frmSudocku_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Tăng thời gian thêm 1 giây
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));

            // Cập nhật ToolStripStatusLabel với thời gian đã trôi qua
            this.toolStripStatusLabel1.Text = elapsedTime.ToString(@"hh\:mm\:ss");

        }

        private void btnClear_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Ngắt sự kiện TextChanged tạm thời để tránh màu đỏ
            foreach (TextBox tb in textBoxes)
            {
                tb.TextChanged -= TextBox_TextChanged; // Ngắt sự kiện TextChanged
            }

            // Duyệt qua toàn bộ các TextBox trên bảng Sudoku và xóa nội dung của chúng
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    textBoxes[i, j].Text = ""; // Xóa nội dung của tất cả các TextBox
                    textBoxes[i, j].BackColor = Color.White; // Đặt lại màu nền về mặc định
                    textBoxes[i, j].Enabled = true; // Cho phép nhập lại vào các ô
                }
            }

            // Đặt lại số lần sai về 0
            incorrectAttempts = 0;
            score = 0; // Đặt lại điểm số về 0
            UpdateAttemptsLabel(); // Cập nhật lượt sai
            toolStripStatusLabel3.Text = $"Điểm: {score}"; // Cập nhật điểm số

            // Đặt lại bảng đáp án
            solution = new int[9, 9]; // Khôi phục bảng đáp án về trạng thái ban đầu

            // Kích hoạt lại sự kiện TextChanged
            foreach (TextBox tb in textBoxes)
            {
                tb.TextChanged += TextBox_TextChanged; // Kích hoạt lại sự kiện TextChanged
            }
        }

        private void btnStar_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Gọi phương thức Clear để xóa bảng cũ và khôi phục lại giá trị mặc định
            btnClear_ItemClick(sender, e);
            // Đặt lại số lần sai về 0
            incorrectAttempts = 0; // Đặt lại số lần sai
            // Đặt lại số lần gợi ý về 0
            suggestionCount = 0; // Đặt lại số lần gợi ý
            UpdateAttemptsLabel(); // Cập nhật giao diện ToolStrip
            // Ngắt sự kiện TextChanged tạm thời để tránh màu đỏ
            foreach (TextBox tb in textBoxes)
            {
                tb.TextChanged -= TextBox_TextChanged; // Ngắt sự kiện TextChanged
            }

            // Tạo lại bảng Sudoku mới với cùng độ khó
            SudokuGenerator generator = new SudokuGenerator();
            int[,] newBoard = generator.GenerateCompleteBoard();
            generator.RemoveCells(newBoard, difficulty); // Xóa các ô dựa trên độ khó
            // Lưu bảng đáp án mới
            solution = generator.GetSolvedBoard(); // Lưu bảng giải đúng
            // Bắt đầu lại bộ đếm thời gian
            timer.Start(); // Đảm bảo timer đang chạy
            // Hiển thị lại bảng Sudoku mới
            DisplaySudokuBoard(newBoard);

            // Đặt lại thời gian nếu có chức năng đếm giờ
            elapsedTime = TimeSpan.Zero;
            toolStripStatusLabel1.Text = elapsedTime.ToString(@"hh\:mm\:ss");

            // Kích hoạt lại sự kiện TextChanged
            foreach (TextBox tb in textBoxes)
            {
                tb.TextChanged += TextBox_TextChanged; // Kích hoạt lại sự kiện TextChanged
            }
        }
        private int suggestionCount = 0; // Biến để đếm số lần gợi ý
        private const int MaxSuggestions = 3; // Số lần gợi ý tối đa
        private void btnSuggest_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Kiểm tra nếu người chơi đã dùng hết số lần gợi ý
            if (suggestionCount >= MaxSuggestions)
            {
                MessageBox.Show("Bạn đã sử dụng hết số lần gợi ý!", "Gợi ý");
                return;
            }

            // Tạo danh sách các ô trống
            List<(int, int)> emptyCells = new List<(int, int)>();

            // Duyệt qua tất cả các ô trong bảng Sudoku và thêm các ô trống vào danh sách
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (string.IsNullOrEmpty(textBoxes[i, j].Text)) // Nếu ô trống
                    {
                        emptyCells.Add((i, j));
                    }
                }
            }

            // Kiểm tra nếu không có ô trống
            if (emptyCells.Count == 0)
            {
                MessageBox.Show("Không còn ô trống nào để gợi ý!", "Gợi ý");
                return;
            }

            // Chọn ngẫu nhiên một ô trống từ danh sách
            Random random = new Random();
            var randomCell = emptyCells[random.Next(emptyCells.Count)]; // Chọn một ô trống ngẫu nhiên
            int row = randomCell.Item1; // Lấy chỉ số hàng
            int col = randomCell.Item2; // Lấy chỉ số cột

            // Lấy số hợp lệ từ bảng giải đúng
            int correctNumber = solution[row, col]; // Giả sử `solution` là bảng giải đúng

            // Điền số hợp lệ vào ô trống
            textBoxes[row, col].Text = correctNumber.ToString(); // Đặt giá trị số hợp lệ vào ô
            textBoxes[row, col].BackColor = Color.LightBlue; // Đổi màu để đánh dấu gợi ý
            suggestionCount++; // Tăng biến đếm số lần gợi ý
        }
        // Hàm kiểm tra nếu giá trị tại vị trí (row, col) có hợp lệ cho gợi ý
        private bool IsValidSuggestion(int row, int col, string input)
        {
            int number;
            if (!int.TryParse(input, out number) || number < 1 || number > 9)
            {
                return false; // Nếu không phải số từ 1-9
            }

            // Kiểm tra hàng
            for (int j = 0; j < 9; j++)
            {
                if (j != col && textBoxes[row, j].Text == input)
                {
                    return false; // Trùng số trong hàng
                }
            }

            // Kiểm tra cột
            for (int i = 0; i < 9; i++)
            {
                if (i != row && textBoxes[i, col].Text == input)
                {
                    return false; // Trùng số trong cột
                }
            }

            // Kiểm tra khối 3x3
            int startRow = row / 3 * 3;
            int startCol = col / 3 * 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int r = startRow + i;
                    int c = startCol + j;
                    if ((r != row || c != col) && textBoxes[r, c].Text == input)
                    {
                        return false; // Trùng số trong khối 3x3
                    }
                }
            }

            return true;
        }

        private void btnReturn_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Kiểm tra xem có ô nào vừa được điền không
            if (recentEntries.Count > 0)
            {
                // Lấy vị trí ô cuối cùng từ Stack và phục hồi giá trị
                var lastEntry = recentEntries.Pop();
                textBoxes[lastEntry.Row, lastEntry.Col].TextChanged -= TextBox_TextChanged; // Tạm ngắt sự kiện
                textBoxes[lastEntry.Row, lastEntry.Col].Text = ""; // Xóa giá trị ở ô đó 
                textBoxes[lastEntry.Row, lastEntry.Col].BackColor = Color.White; // Đặt lại màu nền về mặc định
                textBoxes[lastEntry.Row, lastEntry.Col].TextChanged += TextBox_TextChanged; // Kích hoạt lại sự kiện
            }
            else
            {
                MessageBox.Show("Không có ô nào để hoàn tác!", "Trở về");
            }
        }
        private bool IsSudokuSolved()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Kiểm tra xem ô có rỗng hay không hoặc giá trị trong ô không hợp lệ
                    if (string.IsNullOrEmpty(textBoxes[i, j].Text) || !IsValidInput(i, j, textBoxes[i, j].Text))
                    {
                        // Nếu có ô rỗng hoặc giá trị không hợp lệ, trả về false
                        return false;
                    }
                }
            }
            return true;// Nếu không có ô nào rỗng và tất cả giá trị đều hợp lệ, trả về true
        }

        private void ShowWinnerForm()
        {
            timer.Stop();
            // Tính toán thời gian và điểm số
            TimeSpan elapsedTime = DateTime.Now - startTime; // Giả sử bạn đã lưu thời gian bắt đầu
            int totalScore = score; // Căn cứ vào điểm hiện tại
            // Tạo một instance của frmWinner
            frmWinner winnerForm = new frmWinner(score, username, elapsedTime);
            // Hiển thị form Winner
            winnerForm.ShowDialog();
        }
        private void UpdateAttemptsLabel()
        {
            toolStripStatusLabel2.Text = $"Lượt sai: {incorrectAttempts}/3"; // Cập nhật lượt sai
        }
        //Kiểm Tra Hàng Hoàn Thành
        private bool IsRowCompleted(int row)
        {
            for (int col = 0; col < 9; col++)
            {
                if (string.IsNullOrEmpty(textBoxes[row, col].Text) || !IsValidInput(row, col, textBoxes[row, col].Text))
                {
                    return false; // Hàng không hoàn thành
                }
            }
            return true; // Hàng hoàn thành
        }
        //Kiểm Tra Cột Hoàn Thành
        private bool IsColumnCompleted(int col)
        {
            for (int row = 0; row < 9; row++)
            {
                if (string.IsNullOrEmpty(textBoxes[row, col].Text) || !IsValidInput(row, col, textBoxes[row, col].Text))
                {
                    return false; // Cột không hoàn thành
                }
            }
            return true; // Cột hoàn thành
        }
        //Kiểm Tra Ô 3x3 Hoàn Thành
        private bool IsBoxCompleted(int startRow, int startCol)
        {
            for (int row = startRow; row < startRow + 3; row++)
            {
                for (int col = startCol; col < startCol + 3; col++)
                {
                    if (string.IsNullOrEmpty(textBoxes[row, col].Text) || !IsValidInput(row, col, textBoxes[row, col].Text))
                    {
                        return false; // Ô 3x3 không hoàn thành
                    }
                }
            }
            return true; // Ô 3x3 hoàn thành
        }
        // Hàm cập nhật điểm số
        private void UpdateScore(int row, int col)
        {
            // Kiểm tra hàng
            if (IsRowCompleted(row))
            {
                score += 20; // Cộng 30 điểm cho mỗi hàng hoàn thành
            }

            // Kiểm tra cột
            if (IsColumnCompleted(col))
            {
                score += 20; // Cộng 20 điểm cho mỗi cột hoàn thành
            }

            // Kiểm tra ô 3x3
            int boxRow = (row / 3) * 3;
            int boxCol = (col / 3) * 3;
            if (IsBoxCompleted(boxRow, boxCol))
            {
                score += 50; // Cộng 50 điểm cho mỗi ô 3x3 hoàn thành
            }

            // Cập nhật điểm số lên ToolStripStatusLabel
            toolStripStatusLabel3.Text = $"Điểm: {score}";
        }

        // ==================== THÔNG BÁO THUA ====================
        //private void ShowLoseForm()
        //{
        //    timer.Stop();
        //    frmLose loseForm = new frmLose(score, username); // Tạo một instance của frmLose
        //    loseForm.ShowDialog(); // Hiển thị form thông báo thua
        //    this.Close(); // Đóng form chơi
        //}
        //==========================================================
    }
}