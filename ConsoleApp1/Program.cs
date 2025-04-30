using ConsoleApp1;
using System;
using System.Threading.Tasks;
class Program
{


    static String[] show(int[,] board)
    {
        String[] boardS = new string[8];
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(0); j++)
            {
                boardS[i] += getPice(board[i, j]) + " ";
                Console.Write(getPice(board[i, j]) + " ");
                if (j != 7)
                {
                    Console.Write("|");
                    boardS[i] += "|";
                }
                else
                {
                    Console.Write("  "+i);
                }
            }
            Console.WriteLine();
            if (i != 7)
            {
                for (int j = 0; j < 3 * board.GetLength(0); j++)
                    Console.Write("-");
                Console.WriteLine();
            }
        }

        for(int i = 0; i < board.GetLength(0); i++)
        {
            Console.Write(i+"  ");
        }
        Console.WriteLine();
        return boardS;
    }
    static String getPice(int m)
    {
        switch (m)
        {
            case 1:
                return "P";
            case 2:
                return "N";
            case 3:
                return "B";
            case 4:
                return "R";
            case 5:
                return "Q";
            case 6:
                return "K";
            case -1:
                return "p";
            case -2:
                return "n";
            case -3:
                return "b";
            case -4:
                return "r";
            case -5:
                return "q";
            case -6:
                return "k";
            default:
                return " ";
        }
    }
    static int[,] moveTo(int[,] board, int startX, int startY, int endX, int endY)
    {
        int[,] newBoard = new int[board.GetLength(0), board.GetLength(0)];
        newBoard[endX, endY] = board[startX, startY];
        newBoard[startX, startY] = 0;
        for (int i = 0; i < newBoard.GetLength(0); i++)
            for (int j = 0; j < newBoard.GetLength(0); j++)
            {
                if ((i == startX && j == startY) || (i == endX && j == endY))
                    continue;
                newBoard[i, j] = board[i, j];
            }
        return newBoard;
    }
    static LinkedList<int[,]> getAllMoves(int[,] board, bool whiteMove)
    {
        LinkedList<int[,]> moves = new LinkedList<int[,]>();
        int len = board.GetLength(0) - 1;

        //castling
        if (len == 8 && whiteMove)
        {
            if (board[7, 7] == -4 && board[7, 4] == -6 && board[7, 6] == 0 && board[7, 5] == 0)
            {
                int[,] newBoard = moveTo(board, 7, 7, 7, 5);
                moves.AddLast(moveTo(newBoard, 7, 4, 7, 6));
            }
            if (board[7, 0] == -4 && board[7, 4] == -6 && board[7, 1] == 0 && board[7, 2] == 0 && board[7, 3] == 0)
            {
                int[,] newBoard = moveTo(board, 7, 0, 7, 3);
                moves.AddLast(moveTo(newBoard, 7, 4, 7, 2));
            }
        }
        else if (len == 8)
        {
            if (board[0, 7] == 4 && board[0, 4] == 6 && board[0, 6] == 0 && board[0, 5] == 0)
            {
                int[,] newBoard = moveTo(board, 0, 7, 0, 5);
                moves.AddLast(moveTo(newBoard, 0, 4, 0, 6));
            }
            if (board[0, 0] == 4 && board[0, 4] == 6 && board[0, 1] == 0 && board[0, 2] == 0 && board[0, 4] == 0)
            {
                int[,] newBoard = moveTo(board, 0, 0, 0, 3);
                moves.AddLast(moveTo(newBoard, 0, 4, 0, 2));
            }
        }
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(0); j++)
            {
                if (board[i, j] < 0 && whiteMove)
                {
                    if (board[i, j] == -1) //pawn
                    {
                        if (board[i - 1, j] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i - 1, j));
                            if (i - 1 == 0)
                            {
                                moves.Last.Value[i - 1, j] = -2;
                                int[,] newBoard = moveTo(board, i, j, i - 1, j);
                                newBoard[i - 1, j] = -3;
                                moves.AddLast(newBoard);
                                newBoard = moveTo(board, i, j, i - 1, j);
                                newBoard[i - 1, j] = -4;
                                moves.AddLast(newBoard);
                                newBoard = moveTo(board, i, j, i - 1, j);
                                newBoard[i - 1, j] = -5;
                                moves.AddLast(newBoard);
                            }
                            if (i == 6 && board[i - 2, j] == 0)
                                moves.AddLast(moveTo(board, i, j, i - 2, j));

                        }
                        if (j != 0 && board[i - 1, j - 1] > 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i - 1, j - 1));
                            if (i - 1 == 0)
                            {
                                moves.Last.Value[i - 1, j - 1] = -2;
                                int[,] newBoard = moveTo(board, i, j, i - 1, j - 1);
                                newBoard[i - 1, j - 1] = -3;
                                moves.AddLast(newBoard);
                                newBoard = moveTo(board, i, j, i - 1, j - 1);
                                newBoard[i - 1, j - 1] = -4;
                                moves.AddLast(newBoard);
                                newBoard = moveTo(board, i, j, i - 1, j - 1);
                                newBoard[i - 1, j - 1] = -5;
                                moves.AddLast(newBoard);
                            }
                        }
                        if (j != len && board[i - 1, j + 1] > 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i - 1, j + 1));
                            if (i - 1 == 0)
                            {
                                moves.Last.Value[i - 1, j + 1] = -2;
                                int[,] newBoard = moveTo(board, i, j, i - 1, j + 1);
                                newBoard[i - 1, j + 1] = -3;
                                moves.AddLast(newBoard);
                                newBoard = moveTo(board, i, j, i - 1, j + 1);
                                newBoard[i - 1, j + 1] = -4;
                                moves.AddLast(newBoard);
                                newBoard = moveTo(board, i, j, i - 1, j + 1);
                                newBoard[i - 1, j + 1] = -5;
                                moves.AddLast(newBoard);
                            }
                        }
                    }
                    if (board[i, j] == -2)//knight
                    {
                        if (i > 1 && j != len && board[i - 2, j + 1] >= 0)//^^>
                            moves.AddLast(moveTo(board, i, j, i - 2, j + 1));
                        if (i > 1 && j != 0 && board[i - 2, j - 1] >= 0)//^^<
                            moves.AddLast(moveTo(board, i, j, i - 2, j - 1));
                        if (i != 0 && j < len - 1 && board[i - 1, j + 2] >= 0)//>>^
                            moves.AddLast(moveTo(board, i, j, i - 1, j + 2));
                        if (i != len && j < len - 1 && board[i + 1, j + 2] >= 0)//>>v
                            moves.AddLast(moveTo(board, i, j, i + 1, j + 2));
                        if (i < len - 1 && j != len && board[i + 2, j + 1] >= 0)//vv>
                            moves.AddLast(moveTo(board, i, j, i + 2, j + 1));
                        if (i < len - 1 && j != 0 && board[i + 2, j - 1] >= 0)//vv<
                            moves.AddLast(moveTo(board, i, j, i + 2, j - 1));
                        if (i != 0 && j > 1 && board[i - 1, j - 2] >= 0)//<<^
                            moves.AddLast(moveTo(board, i, j, i - 1, j - 2));
                        if (i != len && j > 1 && board[i + 1, j - 2] >= 0)//<<v
                            moves.AddLast(moveTo(board, i, j, i + 1, j - 2));
                    }
                    if (board[i, j] == -3)//bishop
                    {
                        int k = i - 1;
                        int l = j - 1;
                        while (k >= 0 && l >= 0 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k--;
                            l--;
                        }
                        if (k >= 0 && l >= 0 && board[k, l] > 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i + 1;
                        l = j + 1;
                        while (k < len + 1 && l < len + 1 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k++;
                            l++;
                        }
                        if (k < len + 1 && l < len + 1 && board[k, l] > 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i + 1;
                        l = j - 1;
                        while (k < len + 1 && l >= 0 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k++;
                            l--;
                        }
                        if (k < len + 1 && l >= 0 && board[k, l] > 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i - 1;
                        l = j + 1;
                        while (k >= 0 && l < len + 1 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k--;
                            l++;
                        }
                        if (k >= 0 && l < len + 1 && board[k, l] > 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                    }
                    if (board[i, j] == -4)//rook
                    {
                        int k = i - 1;
                        int l = j - 1;
                        while (k >= 0 && board[k, j] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, j));
                            k--;
                        }
                        if (k >= 0 && board[k, j] > 0)
                            moves.AddLast(moveTo(board, i, j, k, j));
                        k = i + 1;
                        while (k <= len && board[k, j] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, j));
                            k++;

                        }
                        if (k <= len && board[k, j] > 0)
                            moves.AddLast(moveTo(board, i, j, k, j));
                        while (l >= 0 && board[i, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i, l));
                            l--;
                        }
                        if (l >= 0 && board[i, l] > 0)
                            moves.AddLast(moveTo(board, i, j, i, l));
                        l = j + 1;
                        while (l <= len && board[i, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i, l));
                            l++;

                        }
                        if (l <= len && board[i, l] > 0)
                            moves.AddLast(moveTo(board, i, j, i, l));
                    }
                    if (board[i, j] == -5)//queen
                    {
                        int k = i - 1;
                        int l = j - 1;
                        while (k >= 0 && l >= 0 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k--;
                            l--;
                        }
                        if (k >= 0 && l >= 0 && board[k, l] > 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i + 1;
                        l = j + 1;
                        while (k < len + 1 && l < len + 1 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k++;
                            l++;
                        }
                        if (k < len + 1 && l < len + 1 && board[k, l] > 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i + 1;
                        l = j - 1;
                        while (k < len + 1 && l >= 0 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k++;
                            l--;
                        }
                        if (k < len + 1 && l >= 0 && board[k, l] > 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i - 1;
                        l = j + 1;
                        while (k >= 0 && l < len + 1 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k--;
                            l++;
                        }
                        if (k >= 0 && l < len + 1 && board[k, l] > 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i - 1;
                        l = j - 1;
                        while (k >= 0 && board[k, j] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, j));
                            k--;
                        }
                        if (k >= 0 && board[k, j] > 0)
                            moves.AddLast(moveTo(board, i, j, k, j));
                        k = i + 1;
                        while (k <= len && board[k, j] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, j));
                            k++;

                        }
                        if (k <= len && board[k, j] > 0)
                            moves.AddLast(moveTo(board, i, j, k, j));
                        while (l >= 0 && board[i, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i, l));
                            l--;
                        }
                        if (l >= 0 && board[i, l] > 0)
                            moves.AddLast(moveTo(board, i, j, i, l));
                        l = j + 1;
                        while (l <= len && board[i, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i, l));
                            l++;

                        }
                        if (l <= len && board[i, l] > 0)
                            moves.AddLast(moveTo(board, i, j, i, l));
                    }
                    if (board[i, j] == -6)//king
                    {
                        if (i != 0 && j != 0 && board[i - 1, j - 1] >= 0)//^<
                            moves.AddLast(moveTo(board, i, j, i - 1, j - 1));
                        if (i != 0 && board[i - 1, j] >= 0)//^
                            moves.AddLast(moveTo(board, i, j, i - 1, j));
                        if (i != 0 && j != len && board[i - 1, j + 1] >= 0)//^>
                            moves.AddLast(moveTo(board, i, j, i - 1, j + 1));
                        if (j != len && board[i, j + 1] >= 0)//>
                            moves.AddLast(moveTo(board, i, j, i, j + 1));
                        if (i != len && j != len && board[i + 1, j + 1] >= 0)//v>
                            moves.AddLast(moveTo(board, i, j, i + 1, j + 1));
                        if (i != 0 && board[i - 1, j] >= 0)//v
                            moves.AddLast(moveTo(board, i, j, i - 1, j));
                        if (i != len && j != 0 && board[i + 1, j - 1] >= 0)//v<
                            moves.AddLast(moveTo(board, i, j, i + 1, j - 1));
                        if (j != 0 && board[i, j - 1] >= 0)//<
                            moves.AddLast(moveTo(board, i, j, i, j - 1));
                    }

                }
                else if (board[i, j] > 0 && !whiteMove)
                {
                    if (board[i, j] == 1)//pawn
                    {
                        if (board[i + 1, j] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i + 1, j));
                            if (i + 1 == 0)
                            {
                                moves.Last.Value[i + 1, j] = 2;
                                int[,] newBoard = moveTo(board, i, j, i + 1, j);
                                newBoard[i + 1, j] = 3;
                                moves.AddLast(newBoard);
                                newBoard = moveTo(board, i, j, i + 1, j);
                                newBoard[i + 1, j] = 4;
                                moves.AddLast(newBoard);
                                newBoard = moveTo(board, i, j, i + 1, j);
                                newBoard[i + 1, j] = 5;
                                moves.AddLast(newBoard);
                            }
                            if (i == 1 && board[i + 2, j] == 0)
                                moves.AddLast(moveTo(board, i, j, i + 2, j));
                        }
                        if (j != 0 && board[i + 1, j - 1] < 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i + 1, j - 1));
                            if (i + 1 == 0)
                            {
                                moves.Last.Value[i + 1, j - 1] = 2;
                                int[,] newBoard = moveTo(board, i, j, i + 1, j - 1);
                                newBoard[i + 1, j - 1] = 3;
                                moves.AddLast(newBoard);
                                newBoard = moveTo(board, i, j, i + 1, j - 1);
                                newBoard[i + 1, j - 1] = 4;
                                moves.AddLast(newBoard);
                                newBoard = moveTo(board, i, j, i + 1, j - 1);
                                newBoard[i + 1, j - 1] = 5;
                                moves.AddLast(newBoard);
                            }
                        }
                        if (j != len && board[i + 1, j + 1] < 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i + 1, j + 1));
                            if (i + 1 == 0)
                            {
                                moves.Last.Value[i + 1, j + 1] = 2;
                                int[,] newBoard = moveTo(board, i, j, i + 1, j + 1);
                                newBoard[i + 1, j + 1] = 3;
                                moves.AddLast(newBoard);
                                newBoard = moveTo(board, i, j, i + 1, j + 1);
                                newBoard[i + 1, j + 1] = 4;
                                moves.AddLast(newBoard);
                                newBoard = moveTo(board, i, j, i + 1, j + 1);
                                newBoard[i + 1, j + 1] = 5;
                                moves.AddLast(newBoard);
                            }
                        }
                    }
                    if (board[i, j] == 2)//knight
                    {
                        if (i > 1 && j != len && board[i - 2, j + 1] <= 0)//^^>
                            moves.AddLast(moveTo(board, i, j, i - 2, j + 1));
                        if (i > 1 && j != 0 && board[i - 2, j - 1] <= 0)//^^<
                            moves.AddLast(moveTo(board, i, j, i - 2, j - 1));
                        if (i != 0 && j < len - 1 && board[i - 1, j + 2] <= 0)//>>^
                            moves.AddLast(moveTo(board, i, j, i - 1, j + 2));
                        if (i != len && j < len - 1 && board[i + 1, j + 2] <= 0)//>>v
                            moves.AddLast(moveTo(board, i, j, i + 1, j + 2));
                        if (i < len - 1 && j != len && board[i + 2, j + 1] <= 0)//vv>
                            moves.AddLast(moveTo(board, i, j, i + 2, j + 1));
                        if (i < len - 1 && j != 0 && board[i + 2, j - 1] <= 0)//vv<
                            moves.AddLast(moveTo(board, i, j, i + 2, j - 1));
                        if (i != 0 && j > 1 && board[i - 1, j - 2] <= 0)//<<^
                            moves.AddLast(moveTo(board, i, j, i - 1, j - 2));
                        if (i != len && j > 1 && board[i + 1, j - 2] <= 0)//<<v
                            moves.AddLast(moveTo(board, i, j, i + 1, j - 2));
                    }
                    if (board[i, j] == 3)//bishop
                    {
                        int k = i - 1;
                        int l = j - 1;
                        while (k >= 0 && l >= 0 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k--;
                            l--;
                        }
                        if (k >= 0 && l >= 0 && board[k, l] < 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i + 1;
                        l = j + 1;
                        while (k < len + 1 && l < len + 1 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k++;
                            l++;
                        }
                        if (k < len + 1 && l < len + 1 && board[k, l] < 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i + 1;
                        l = j - 1;
                        while (k < len + 1 && l >= 0 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k++;
                            l--;
                        }
                        if (k < len + 1 && l >= 0 && board[k, l] < 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i - 1;
                        l = j + 1;
                        while (k >= 0 && l < len + 1 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k--;
                            l++;
                        }
                        if (k >= 0 && l < len + 1 && board[k, l] < 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                    }
                    if (board[i, j] == 4)//rook
                    {
                        int k = i - 1;
                        int l = j - 1;
                        while (k >= 0 && board[k, j] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, j));
                            k--;
                        }
                        if (k >= 0 && board[k, j] < 0)
                            moves.AddLast(moveTo(board, i, j, k, j));
                        k = i + 1;
                        while (k <= len && board[k, j] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, j));
                            k++;

                        }
                        if (k <= len && board[k, j] < 0)
                            moves.AddLast(moveTo(board, i, j, k, j));
                        while (l >= 0 && board[i, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i, l));
                            l--;
                        }
                        if (l >= 0 && board[i, l] < 0)
                            moves.AddLast(moveTo(board, i, j, i, l));
                        l = j + 1;
                        while (l <= len && board[i, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i, l));
                            l++;

                        }
                        if (l <= len && board[i, l] < 0)
                            moves.AddLast(moveTo(board, i, j, i, l));
                    }
                    if (board[i, j] == 5)//queen
                    {
                        int k = i - 1;
                        int l = j - 1;
                        while (k >= 0 && l >= 0 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k--;
                            l--;
                        }
                        if (k >= 0 && l >= 0 && board[k, l] < 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i + 1;
                        l = j + 1;
                        while (k < len + 1 && l < len + 1 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k++;
                            l++;
                        }
                        if (k < len + 1 && l < len + 1 && board[k, l] < 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i + 1;
                        l = j - 1;
                        while (k < len + 1 && l >= 0 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k++;
                            l--;
                        }
                        if (k < len + 1 && l >= 0 && board[k, l] < 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i - 1;
                        l = j + 1;
                        while (k >= 0 && l < len + 1 && board[k, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, l));
                            k--;
                            l++;
                        }
                        if (k >= 0 && l < len + 1 && board[k, l] < 0)
                            moves.AddLast(moveTo(board, i, j, k, l));
                        k = i - 1;
                        l = j - 1;
                        while (k >= 0 && board[k, j] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, j));
                            k--;
                        }
                        if (k >= 0 && board[k, j] < 0)
                            moves.AddLast(moveTo(board, i, j, k, j));
                        k = i + 1;
                        while (k <= len && board[k, j] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, k, j));
                            k++;

                        }
                        if (k <= len && board[k, j] < 0)
                            moves.AddLast(moveTo(board, i, j, k, j));
                        while (l >= 0 && board[i, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i, l));
                            l--;
                        }
                        if (l >= 0 && board[i, l] < 0)
                            moves.AddLast(moveTo(board, i, j, i, l));
                        l = j + 1;
                        while (l <= len && board[i, l] == 0)
                        {
                            moves.AddLast(moveTo(board, i, j, i, l));
                            l++;

                        }
                        if (l <= len && board[i, l] < 0)
                            moves.AddLast(moveTo(board, i, j, i, l));
                    }
                    if (board[i, j] == 6)//king
                    {
                        if (i != 0 && j != 0 && board[i - 1, j - 1] <= 0)//^<
                            moves.AddLast(moveTo(board, i, j, i - 1, j - 1));
                        if (i != 0 && board[i - 1, j] <= 0)//^
                            moves.AddLast(moveTo(board, i, j, i - 1, j));
                        if (i != 0 && j != len && board[i - 1, j + 1] <= 0)//^>
                            moves.AddLast(moveTo(board, i, j, i - 1, j + 1));
                        if (j != len && board[i, j + 1] <= 0)//>
                            moves.AddLast(moveTo(board, i, j, i, j + 1));
                        if (i != len && j != len && board[i + 1, j + 1] <= 0)//v>
                            moves.AddLast(moveTo(board, i, j, i + 1, j + 1));
                        if (i != 0 && board[i - 1, j] <= 0)//v
                            moves.AddLast(moveTo(board, i, j, i - 1, j));
                        if (i != len && j != 0 && board[i + 1, j - 1] <= 0)//v<
                            moves.AddLast(moveTo(board, i, j, i + 1, j - 1));
                        if (j != 0 && board[i, j - 1] <= 0)//<
                            moves.AddLast(moveTo(board, i, j, i, j - 1));
                    }
                }
            }
        }
        return moves;
    }
    static (int, int) findPieace(int[,] board, int pieaceVal)
    {
        for (int i = 0; i < board.GetLength(0); i++)
            for (int j = 0; j < board.GetLength(0); j++)
                if (board[i, j] == pieaceVal)
                    return (i, j);
        return (-1, -1);
    }
    static bool inCheck(int[,] board, bool whiteMove)
    {
        int kingValue = whiteMove ? -6 : 6;
        (int kingRow, int kingCol) = findPieace(board, kingValue);

        if (kingRow == -1) return true;

        LinkedList<int[,]> opponentMoves = getAllMoves(board, !whiteMove);

        foreach (int[,] moveBoard in opponentMoves)
        {
            if (findPieace(moveBoard, kingValue) == (-1, -1))
                return true; // king was captured
        }

        return false;
    }
    static LinkedList<int[,]> getAllLeagalMoves(int[,] board, bool whiteMove)
    {
        int baseEvo = evaluate(board);
        LinkedList<int[,]> allMoves = getAllMoves(board, whiteMove);
        LinkedList<int[,]> leagalMoves = new LinkedList<int[,]>();
        foreach (int[,] move in allMoves)
        {
            int evo = evaluate(move);
            if (!inCheck(move, whiteMove))
            {
                if ((evo > baseEvo && !whiteMove) || (evo < baseEvo && whiteMove))
                    leagalMoves.AddFirst(move);
                else
                    leagalMoves.AddLast(move);
            }
        }
        return leagalMoves;
    }
    static int evaluate(int[,] board)
    {
        int[,] heatMapPawn =
        {
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,2,1,1,2,1,1 },
            {1,1,1,2,2,1,1,1 },
            {1,1,1,2,2,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 }
        };
        int[,] heatMapKnight =
        {
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,2,1,1,2,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 }
        };
        int[,] heatMapBishop =
        {
            {1,1,2,1,1,2,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,2,1,1,1,1,2,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 }
        };
        int[,] heatMapRook =
        {
            {2,1,1,1,1,1,1,2 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 }
        };
        int[,] heatMapQueen =
        {
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 }
        };
        int[,] heatMapKing =
        {
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1 }
        };
        int evo = 0;
        for (int i = 0; i < board.GetLength(0); i++)
            for (int j = 0; j < board.GetLength(0); j++)
            {
                int pieace = board[i, j];
                if (pieace == 0) continue;
                else if (Math.Abs(board[i, j]) == 6)
                    evo += pieace * 1500 * heatMapKing[i,j];
                else if (Math.Abs(pieace) == 1)
                    evo += pieace * 10 * heatMapPawn[i, j];
                else if (Math.Abs(pieace) == 2)
                    evo += pieace * 15 * heatMapKnight[i, j];
                else if (Math.Abs(pieace) == 3)
                    evo += pieace * 10 * heatMapBishop[i, j];
                else if (Math.Abs(pieace) == 4)
                    evo += pieace * 13 * heatMapRook[i, j];
                else if (Math.Abs(pieace) == 5)
                    evo += pieace * 18 * heatMapQueen[i, j];
            }
        return evo;
    }
    static int Search(int[,] board, int depth, int alpha, int beta, bool whiteMove)
    {
        if (depth == 0) return evaluate(board);
        LinkedList<int[,]> moves = getAllLeagalMoves(board, whiteMove);
        if (moves.Count == 0)
        {
            if (inCheck(board, whiteMove))
                return -10000000 - depth;
            else
                return 0;
        }
        foreach (int[,] move in moves)
        {
            int evo = -Search(move, depth - 1, -beta, -alpha, !whiteMove);
            if (evo >= beta)
                return beta;
            alpha = Math.Max(alpha, evo);
        }

        return alpha;
    }
    static int[,] getBestMove(int[,] board, int depth, bool whiteMove)
    {
        int alpha = -100000;
        int beta = 100000;
        LinkedList<int[,]> moves = getAllLeagalMoves(board, whiteMove);
        int bestMoveEvo = Search(moves.First.Value, depth, alpha, beta, !whiteMove);
        int[,] bestBoard = moves.First.Value;
        bool first = true;
        foreach (int[,] move in moves)
        {
            if (first)
            {
                first = false;
                continue;
            }
            int evo = Search(move, depth, alpha, beta, !whiteMove);
            if (evo < bestMoveEvo)
            {
                bestMoveEvo = evo;
                bestBoard = move;
            }
        }
        Console.WriteLine(bestMoveEvo);
        return bestBoard;
    }
    static int[,] getBestMoveFaster(int[,] board, int depth, bool whiteMove)
    {
        int alpha = -100000;
        int beta = 100000;
        int[][,] moves = getAllLeagalMoves(board, whiteMove).ToArray();
        Task<int>[] tasks = new Task<int>[moves.GetLength(0)];
        for (int i = 0; i < moves.GetLength(0); i++)
        {
            int index = i;
            tasks[index] = Task.Run(() => Search(moves[index], depth, alpha, beta, !whiteMove));
        }
        Task.WaitAll(tasks);
        int bestEvo = tasks[0].Result;
        int[,] bestMove = moves[0];
        for (int i = 1; i < moves.GetLength(0); i++)
        {
            if (tasks[i].Result < bestEvo)
            {
                bestEvo = tasks[i].Result;
                bestMove = moves[i];
            }
        }
        return bestMove;
    }
    
    static String toChessNotation(Board board, String move)
    {
        String not = getPice(board.board[move[0] - '0', move[1] - '0']).ToUpper();
        if (move[3] == '0')
            not += "a";
        else if (move[3] == '1')
            not += "b";
        else if (move[3] == '2')
            not += "c";
        else if (move[3] == '3')
            not += "d";
        else if (move[3] == '4')
            not += "e";
        else if (move[3] == '5')
            not += "f";
        else if (move[3] == '6')
            not += "g";
        else if (move[3] == '7')
            not += "h";
        else return move;
        not += (8 - (move[2] - '0')).ToString();
        return not;
    }
    static bool ContainsMatrixByValue(LinkedList<int[,]> list, int[,] target)
    {
        foreach (var item in list)
        {
            if (AreEqual(item, target))
                return true;
        }
        return false;
    }
    static bool AreEqual(int[,] a, int[,] b)
    {
        if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1))
            return false;

        for (int i = 0; i < a.GetLength(0); i++)
            for (int j = 0; j < a.GetLength(1); j++)
                if (a[i, j] != b[i, j])
                    return false;

        return true;
    }
    static int[,] NormalBoard =
        {
    { 4, 2, 3, 5, 6, 3, 2, 4 },
    { 1, 1, 1, 1, 1, 1, 1, 1 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    {-1,-1,-1,-1,-1,-1,-1,-1 },
    {-4,-2,-3,-5,-6,-3,-2,-4 }
};
    static int[,] otherBoard =
        {
    { 0, 0, 0, 0, 0, 0, 0, 6 },
    { 0, 0, 0, 0, 0, 1, 1, 1 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 4, 0, 0, 0, 0, 0 },
    { 0,-5, 0,-1, 0, 0, 0, 0 },
    { 0, 0,-1, 0, 0, 0, 0, 0 },
    {-1, 0, 0, 0, 0,-1,-1,-1 },
    {-4, 0,-3,-4, 0, 0,-6, 0 }
};
    static int[,] otherBoard2 =
        {
    { 4, 0, 3, 5, 6, 3, 0, 4 },
    { 1, 1, 1, 1, 0, 1, 1, 1 },
    { 0, 0, 2, 0, 0, 2, 0, 0 },
    { 0, 0, 0, 0, 1, 0, 0, 0 },
    { 0, 0, 0, 0,-1, 0, 0, 0 },
    { 0, 0, 0,-3, 0,-2, 0, 0 },
    {-1,-1,-1,-1, 0,-1,-1,-1 },
    {-4,-2,-3,-5, 0,-4,-6, 0 }
};
    static int[,] smallBoard =
    {
    { 4, 0, 0 },
    { 2, 0, 3 },
    { 0, -3, 0 }
    };
    static void Main()
    {
        int[,] board = otherBoard2;
        bool whiteMove = true;
        int depth = 5;

        Console.WriteLine("How to make move:");
        Console.WriteLine("Row+Collum Numbers of the pieace you want to move");
        Console.WriteLine("then you add the Row and Collum numbers of the square you want to move to");
        Console.WriteLine("for example in the starting board the move 6444 is like e4\n");
        show(board);
        Console.WriteLine("Waiting for your move:");
        String input = Console.ReadLine();
        if (input.Equals("O-O\n"))
        {
            board[7, 4] = 0;
            board[7, 7] = 0;
            board[7, 5] = -4;
            board[7, 6] = -6;
            
        }
        else if (input.Equals("O-O-O\n"))
        {
            board[7, 4] = 0;
            board[7, 0] = 0;
            board[7, 3] = -4;
            board[7, 2] = -6;
            
        }
        else
        {
            LinkedList<int[,]> allLeagalMoves = getAllLeagalMoves(board, whiteMove);
            int tmp = board[input[2] - '0', input[3] - '0'];
            board[input[2] - '0', input[3] - '0'] = board[input[0] - '0', input[1] - '0'];
            board[input[0] - '0', input[1] - '0'] = 0;
            while (!(ContainsMatrixByValue(allLeagalMoves, board)))
            {
                board[input[0] - '0', input[1] - '0'] = board[input[2] - '0', input[3] - '0'];
                board[input[2] - '0', input[3] - '0'] = tmp;
                Console.WriteLine("try again");
                input = Console.ReadLine();
                tmp = board[input[1] - '0', input[2] - '0'];
                board[input[2] - '0', input[3] - '0'] = board[input[0] - '0', input[1] - '0'];
                board[input[0] - '0', input[1] - '0'] = 0;
            }
        }
        while (getAllLeagalMoves(board,!whiteMove).Count!=0)
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            show(board);
            Console.WriteLine("calculating the best move...");
            board = getBestMoveFaster(board, depth, !whiteMove);
            show(board);
            if (getAllLeagalMoves(board, whiteMove).Count == 0)
                break;
            TimeSpan then = DateTime.Now.TimeOfDay;
            Console.WriteLine(then - now);
            Console.WriteLine("Waiting for your move:");
            input = Console.ReadLine();
            if (input.Equals("O-O\n"))
            {
                board[7, 4] = 0;
                board[7, 7] = 0;
                board[7, 5] = -4;
                board[7, 6] = -6;
                continue;
            }
            else if (input.Equals("O-O-O\n"))
            {
                board[7, 4] = 0;
                board[7, 0] = 0;
                board[7, 3] = -4;
                board[7, 2] = -6;
                continue;
            }
            LinkedList<int[,]> allLeagalMoves = getAllLeagalMoves(board, whiteMove);
            int tmp = board[input[2] - '0', input[3] - '0'];
            board[input[2] - '0', input[3] - '0'] = board[input[0] - '0', input[1] - '0'];
            board[input[0] - '0', input[1] - '0'] = 0;
            while (!(ContainsMatrixByValue(allLeagalMoves,board)))
            {
                board[input[0] - '0', input[1] - '0'] = board[input[2] - '0', input[3] - '0'];
                board[input[2] - '0', input[3] - '0'] = tmp;
                Console.WriteLine("try again");
                input = Console.ReadLine();
                tmp = board[input[1] - '0', input[2] - '0'];
                board[input[2] - '0', input[3] - '0'] = board[input[0] - '0', input[1] - '0'];
                board[input[0] - '0', input[1] - '0'] = 0;
            }
        }
        Console.WriteLine("Check Mate!");
    }
    

}


/* 1 pawn
 * 2 knight
 * 3 bishop
 * 4 rook
 * 5 queen
 * 6 king
 * if numbers are positive then its a black pieace, and negative is white
 * 
 * Still work in progres
 * I have been working on it for just 2 days now
 * 
 * 
 */

