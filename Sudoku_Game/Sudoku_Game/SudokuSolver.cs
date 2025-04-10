using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Game
{
    public class SudokuSolver
    {
        private const int GRID_SIZE = 9; // Kích thước của bảng Sudoku 9x9
        private Random random = new Random(); // Đối tượng Random để tạo số ngẫu nhiên
        public bool SolveSudoku(int[,] board)
        {
            int row = -1; // Khởi tạo biến lưu chỉ số hàng
            int col = -1; // Khởi tạo biến lưu chỉ số cột
            bool isEmpty = false; // Biến kiểm tra xem có ô trống hay không

            // Tìm ô trống đầu tiên
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    if (board[i, j] == 0) // Kiểm tra xem ô có trống (giá trị 0) không
                    {
                        row = i;// Lưu chỉ số hàng
                        col = j;// Lưu chỉ số cột
                        isEmpty = true;// Đánh dấu là có ô trống
                        break;// Thoát khỏi vòng lặp
                    }
                }
                if (isEmpty) break;// Nếu đã tìm thấy ô trống, thoát khỏi vòng lặp bên ngoài
            }

            // Nếu không còn ô trống, trả về true (đã giải xong)
            if (!isEmpty) return true;

            // Thử điền các số từ 1 đến 9
            for (int num = 1; num <= 9; num++)
            {
                if (IsSafe(board, row, col, num)) // Kiểm tra xem việc điền số này có hợp lệ không
                {
                    board[row, col] = num; // Điền số vào ô trống

                    // Đệ quy gọi hàm để điền số vào ô tiếp theo
                    if (SolveSudoku(board))
                    {
                        return true; // Nếu thành công, trả về true
                    }

                    // Nếu không thể giải, đặt lại giá trị của ô về 0
                    board[row, col] = 0; // Quay lại nếu không tìm được lời giải
                }
            }

            return false; // Trả về false nếu không tìm thấy lời giải
        }

        // Kiểm tra xem số có thể được điền vào ô (row, col) không
        public bool IsSafe(int[,] board, int row, int col, int num)
        {
            // Kiểm tra hàng
            for (int x = 0; x < GRID_SIZE; x++)
            {
                if (board[row, x] == num) return false; // Nếu số đã tồn tại trong hàng, trả về false
            }

            // Kiểm tra cột
            for (int x = 0; x < GRID_SIZE; x++)
            {
                if (board[x, col] == num) return false; // Nếu số đã tồn tại trong cột, trả về false

            }

            // Kiểm tra khối 3x3
            int startRow = row / 3 * 3;// Tính chỉ số hàng bắt đầu của khối 3x3
            int startCol = col / 3 * 3;// Tính chỉ số cột bắt đầu của khối 3x3
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Nếu số đã tồn tại trong khối 3x3, trả về false
                    if (board[startRow + i, startCol + j] == num) return false;
                }
            }

            return true; // Nếu không có xung đột, trả về true
        }
        // Phương thức điền bảng bằng thuật toán backtracking
        public bool FillGrid(int[,] board)
        {
            for (int row = 0; row < GRID_SIZE; row++)
            {
                for (int col = 0; col < GRID_SIZE; col++)
                {
                    if (board[row, col] == 0) // Nếu ô trống
                    {
                        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                        Shuffle(numbers); // Xáo trộn các số

                        foreach (int number in numbers)
                        {
                            if (IsSafe(board, row, col, number)) // Kiểm tra số có hợp lệ không
                            {
                                board[row, col] = number; // Điền số

                                if (FillGrid(board)) // Đệ quy để điền các ô còn lại
                                    return true;

                                board[row, col] = 0; // Quay lại nếu không hợp lệ
                            }
                        }
                        return false; // Không thể điền số hợp lệ
                    }
                }
            }
            return true; // Bảng đã đầy
        }

        // Xáo trộn danh sách số từ 1 đến 9
        private void Shuffle(List<int> numbers)
        {
            //Duyệt từ chỉ số cuối cùng của danh sách numbers
            for (int i = numbers.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1); // Chọn ngẫu nhiên một chỉ số j từ 0 đến i
                int temp = numbers[i]; // Lưu giá trị của số tại vị trí i
                numbers[i] = numbers[j]; // Đặt giá trị tại vị trí j vào vị trí i
                numbers[j] = temp; // Đặt giá trị lưu trữ vào vị trí j
            }
        }
    }
}

