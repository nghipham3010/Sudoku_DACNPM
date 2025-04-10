using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Game
{
    public class SudokuGenerator
    {
        private const int GRID_SIZE = 9;
        private Random random = new Random();
        private SudokuSolver solver = new SudokuSolver(); // Tạo instance của SudokuSolver
        private int[,] solvedBoard; // Lưu bảng giải đúng
        //Danh sách lưu trữ các bảng đã tạo trước đó để tránh việc tạo ra bảng giống nhau.
        private List<int[,]> previousBoards = new List<int[,]>(); // Danh sách lưu bảng đã tạo

        // Tạo bảng Sudoku hoàn chỉnh
        public int[,] GenerateCompleteBoard()
        {
            solvedBoard = new int[GRID_SIZE, GRID_SIZE]; // Khởi tạo bảng giải đúng

            solver.FillGrid(solvedBoard); // Gọi hàm điền bảng Sudoku

            // Kiểm tra xem bảng đã tồn tại trong danh sách chưa
            while (previousBoards.Any(board => AreBoardsEqual(board, solvedBoard)))
            {
                solvedBoard = new int[GRID_SIZE, GRID_SIZE]; // Khởi tạo lại
                solver.FillGrid(solvedBoard); // Tạo lại bảng ngẫu nhiên
            }

            previousBoards.Add((int[,])solvedBoard.Clone()); // Lưu bảng mới vào danh sách
            return (int[,])solvedBoard.Clone(); // Trả về bản sao của bảng hoàn chỉnh
        }

        // Xóa các ô dựa trên độ khó
        public void RemoveCells(int[,] board, int numberOfCellsToRemove)
        {
            int cellsRemoved = 0; // Khởi tạo biến đếm số ô đã xóa
            // Khi số ô đã xóa nhỏ hơn số ô cần xóa
            while (cellsRemoved < numberOfCellsToRemove)
            {
                // Tạo ra chỉ số hàng và cột ngẫu nhiên
                int row = random.Next(0, GRID_SIZE);
                int col = random.Next(0, GRID_SIZE);

                // Xóa ô chỉ khi ô đó chưa bị xóa trước đó
                if (board[row, col] != 0) // Kiểm tra xem ô không rỗng
                {
                    int originalValue = board[row, col]; // Lưu giá trị ban đầu
                    board[row, col] = 0; // Tạm thời xóa ô

                    // Kiểm tra xem bảng có còn giải được sau khi xóa không
                    if (!HasUniqueSolution(board)) // Kiểm tra xem còn lời giải duy nhất không
                    {
                        board[row, col] = originalValue; // Phục hồi ô nếu không còn duy nhất một lời giải
                    }
                    else
                    {
                        cellsRemoved++; // Tăng biến đếm số ô đã xóa
                    }
                }
            }
        }

        // Kiểm tra bảng Sudoku có duy nhất một lời giải không
        private bool HasUniqueSolution(int[,] board)
        {
            int solutionCount = 0; // Khởi tạo biến đếm số lượng lời giải
            CountSolutions(board, ref solutionCount); // Gọi phương thức đếm số lượng lời giải
            return solutionCount == 1; // Trả về true nếu chỉ có một lời giải
        }

        // Đếm số lượng lời giải của bảng Sudoku
        private void CountSolutions(int[,] board, ref int solutionCount)
        {
            int row = -1;// Khởi tạo biến hàng
            int col = -1;// Khởi tạo biến cột
            bool isEmpty = false;// Biến để kiểm tra xem ô có trống hay không

            // Tìm ô trống đầu tiên
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    if (board[i, j] == 0)// Nếu ô đang trống
                    {
                        row = i;// Ghi lại chỉ số hàng
                        col = j;// Ghi lại chỉ số cột
                        isEmpty = true;// Đánh dấu rằng đã tìm thấy ô trống
                        break;// Thoát khỏi vòng lặp
                    }
                }
                if (isEmpty) break;  // Thoát khỏi vòng lặp bên ngoài nếu tìm thấy ô trống
            }

            // Nếu không còn ô trống, nghĩa là đã tìm được một lời giải
            if (!isEmpty)
            {
                solutionCount++; // Tăng biến đếm số lượng lời giải
                return; // Kết thúc phương thức
            }

            // Thử điền các số từ 1 đến 9
            for (int num = 1; num <= 9; num++)
            {
                if (solver.IsSafe(board, row, col, num)) // Kiểm tra số có hợp lệ không
                {
                    board[row, col] = num; // Điền số vào ô

                    // Đệ quy gọi hàm để đếm các lời giải tiếp theo
                    CountSolutions(board, ref solutionCount);

                    // Nếu đã tìm được hơn một lời giải, kết thúc kiểm tra
                    if (solutionCount > 1)
                    {
                        board[row, col] = 0; // Đặt lại giá trị của ô về 0
                        return;// Kết thúc kiểm tra
                    }

                    board[row, col] = 0; // Đặt lại giá trị của ô về 0
                }
            }
        }
        // Phương thức kiểm tra hai bảng có giống nhau không
        private bool AreBoardsEqual(int[,] board1, int[,] board2)
        {
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    if (board1[i, j] != board2[i, j]) // Nếu giá trị tại ô không bằng nhau
                    {
                        return false; // Trả về false (các bảng không bằng nhau)
                    }
                }
            }
            return true; // Trả về true (các bảng bằng nhau)
        }
        // Phương thức để lấy bảng giải đúng
        public int[,] GetSolvedBoard()
        {
            return solvedBoard;
        }
    }
}
