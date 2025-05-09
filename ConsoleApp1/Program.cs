using System;
using System.Reflection;
using System.Threading.Tasks;

//Maby do it with GUI in the future
class Program
{
    static bool whiteMove = true;
    static int depth = 5;

    static String[] show(int[,] board)
    {
        String[] boardS = new string[8];
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(0); j++)
                Console.Write("+---");
            Console.WriteLine("+");
            for (int j = 0; j < board.GetLength(0); j++)
            {
                Console.Write("| " + getPieace(board[i, j]) + " ");
                boardS[i] += "| " + getPieace(board[i, j]) + " ";
            }
            Console.WriteLine("| "+(8-i));

        }

        for (int j = 0; j < board.GetLength(0); j++)
            Console.Write("+---");
        Console.WriteLine("+");
        for (int j = 0; j < board.GetLength(0); j++)
            Console.Write("  "+numToAlphaBet(j)+" ");
        Console.WriteLine();
        return boardS;
    }
    static char numToAlphaBet(int num)
    {
        switch (num)
        {
            case 0: return 'a';
            case 1: return 'b';
            case 2: return 'c';
            case 3: return 'd';
            case 4: return 'e';
            case 5: return 'f';
            case 6: return 'g';
            case 7: return 'h';
            default: return '-';
        }
    }
    static char alphaBetTonum(char c)
    {
        switch (c)
        {
            case 'a': return '0';
            case 'b': return '1';
            case 'c': return '2';
            case 'd': return '3';
            case 'e': return '4';
            case 'f': return '5';
            case 'g': return '6';
            case 'h': return '7';
            default: return 'z';
        }
    }
    static String getPieace(int m)
    {
        switch (m)
        {
            case 1: return "p";
            case 2: return "n";
            case 3: return "b";
            case 4: return "r";
            case 5: return "q";
            case 6: return "k";
            case -1: return "P";
            case -2: return "N";
            case -3: return "B";
            case -4: return "R";
            case -5: return "Q";
            case -6: return "K";
            default: return " ";
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
                        if (i != len && board[i + 1, j] >= 0)//v
                            moves.AddLast(moveTo(board, i, j, i + 1, j));
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
                            if (i + 1 == 7)
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
                            if (i + 1 == 7)
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
                            if (i + 1 == 7)
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
                        if (i != len && board[i + 1, j] <= 0)//v
                            moves.AddLast(moveTo(board, i, j, i + 1, j));
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
    
    //returns a number that indicades who is in a better position: white or black and how much are they in a better position
    // higher number means black is winning and lower means white is winning
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
    
    //usess the previous function to find what move gives the best board for black
    static int[,] getBestMove(int[,] board, int depth, bool whiteMove)
    {
        //usess multitasking for better preformance
        int alpha = -100000;
        int beta = 100000;
        int[][,] moves = getAllLeagalMoves(board, whiteMove).ToArray();
        Task<int>[] tasks = new Task<int>[moves.Length];
        for (int i = 0; i < moves.Length; i++)
        {
            int index = i;
            tasks[index] = Task.Run(() => Search(moves[index], depth, alpha, beta, !whiteMove));
        }
        try
        {
            Task.WaitAll(tasks);
        }
        catch (AggregateException ex)
        {
            foreach (var inner in ex.InnerExceptions)
                Console.WriteLine("you will need to wait a little longer");  
        }
        int bestEvo = tasks[0].Result;
        int[,] bestMove = moves[0];
        for (int i = 1; i < moves.GetLength(0); i++)
        {
            if (!tasks[i].Status.Equals(TaskStatus.Faulted))
            {
                if (tasks[i].Result < bestEvo)
                {
                    bestEvo = tasks[i].Result;
                    bestMove = moves[i];
                }
            }
            else
            {
                int searched = Search(moves[i], depth, alpha, beta, !whiteMove);
                if (searched < bestEvo)
                {
                    bestEvo = searched;
                    bestMove = moves[i];
                }
            }
        }
        return bestMove;
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
    static String FEN(int[,] board,bool whiteMove)
    {
        String fen = "";
        for(int i = 0; i < 8; i++)
        {
            for(int j=0; j < 8; j++)
            {
                int air = 0;
                while (j!=8&&board[i, j] == 0)
                {
                    air++;
                    j++;
                }
                if (air != 0)
                    fen += "" + air;
                if (j != 8)
                    fen += getPieace(board[i,j]);
            }
            if(i!=7)
                fen += "/";
        }
        if (whiteMove)
            fen += " w";
        else
            fen += " b";
        fen += " - - 0 1";
        return fen;
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
    { 6, 0, 3, 0, 0, 3, 0, 4 },
    { 0, 0, 1, 0, 0, 1, 1, 0 },
    { 1, 0, 0, 1, 0, 0, 0, 1 },
    { 0, 0, 0,-2, 0, 0,-2, 0 },
    { 0, 0,-3, 0, 0, 0,-1,-4 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0,-1, 0,-3, 0,-1,-6,-1 },
    { 0, 0, 0, 0, 5, 0, 0, 0 }
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
    {-4,-2,-3, 0, 0,-4,-6, 0 }
}; 
    static int[,] smallBoard =
    {
    { 4, 0, 0 },
    { 2, 0, 3 },
    { 0, -3, 0 }
    };


    static int[,] board = NormalBoard;
    
    static void Main()
    {
        Console.WriteLine("How to make move:");
        Console.WriteLine("Row+Collum of the pieace you want to move");
        Console.WriteLine("then you add the Row+Collum of the square you want to move to");
        Console.WriteLine("for example in the starting board the move e2e4 is like pawn to e4 because the pawn starting position is e2\n");
        
        int movesCount=0;
        if (whiteMove)
        {
            
            show(board);
            Console.WriteLine("Waiting for your move: ");
            
            LinkedList<int[,]> allLeagalMoves = getAllLeagalMoves(board, whiteMove);
            int tmp = board[0, 0];
            int[,] boardSave = moveTo(board,0,0,0,0);
            boardSave[0, 0] = tmp;
            do
            {

                String input = Console.ReadLine();

                if (input.Equals("O-O"))
                {
                    board[7, 4] = 0;
                    board[7, 7] = 0;
                    board[7, 5] = -4;
                    board[7, 6] = -6;

                }
                else if (input.Equals("O-O-O"))
                {
                    board[7, 4] = 0;
                    board[7, 0] = 0;
                    board[7, 3] = -4;
                    board[7, 2] = -6;

                }
                while (input.Length != 4 && input.Length != 5)
                {
                    Console.WriteLine("try again: ");
                    input = Console.ReadLine();
                }
                try
                {
                    if (input.Length == 4)
                    {
                        input = (8 - (input[1] - '0')) + "" + alphaBetTonum(input[0]) + (8 - (input[3] - '0')) + alphaBetTonum(input[2]);
                        board[input[2] - '0', input[3] - '0'] = board[input[0] - '0', input[1] - '0'];
                        board[input[0] - '0', input[1] - '0'] = 0;
                    }
                    else if (input.Length == 5)
                    {
                        input = (8 - (input[1] - '0')) + "" + alphaBetTonum(input[0]) + (8 - (input[3] - '0')) + alphaBetTonum(input[2]) + input[4];
                        board[input[0] - '0', input[1] - '0'] = 0;
                        if (input[4] == 'n')
                            board[input[2] - '0', input[3] - '0'] = -2;
                        else if (input[4] == 'b')
                            board[input[2] - '0', input[3] - '0'] = -3;
                        else if (input[4] == 'r')
                            board[input[2] - '0', input[3] - '0'] = -4;
                        else if (input[4] == 'q')
                            board[input[2] - '0', input[3] - '0'] = -5;
                    }
                }
                catch(IndexOutOfRangeException e)
                {
                    Console.WriteLine("try again: ");
                    continue;
                }
                if (!ContainsMatrixByValue(allLeagalMoves, board))
                {
                    board = boardSave;
                    tmp = board[0, 0];
                    boardSave = moveTo(board,0,0,0,0);
                    boardSave[0, 0] = tmp;
                    Console.WriteLine("try again: ");
                }
            } while (!ContainsMatrixByValue(allLeagalMoves, board));
        }
        whiteMove = false;
        movesCount = 1;
        
        while (getAllLeagalMoves(board,whiteMove).Count!=0)
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            show(board);
            Console.WriteLine("calculating the best move...");
            board = getBestMove(board, depth, whiteMove);
            show(board);
            whiteMove = !whiteMove;
            if (getAllLeagalMoves(board, whiteMove).Count == 0)
                break;
            TimeSpan then = DateTime.Now.TimeOfDay;
            Console.WriteLine(then - now);

            
            Console.WriteLine("Waiting for your move:");
            int tmp = board[0, 0];
            int[,] boardSave = moveTo(board, 0, 0, 0, 0);
            boardSave[0, 0] = tmp;
            LinkedList<int[,]> allLeagalMoves = getAllLeagalMoves(board, whiteMove);
            do
            {

                String input = Console.ReadLine();

                if (input.Equals("O-O"))
                {
                    board[7, 4] = 0;
                    board[7, 7] = 0;
                    board[7, 5] = -4;
                    board[7, 6] = -6;

                }
                else if (input.Equals("O-O-O"))
                {
                    board[7, 4] = 0;
                    board[7, 0] = 0;
                    board[7, 3] = -4;
                    board[7, 2] = -6;

                }
                while (input.Length != 4 && input.Length != 5)
                {
                    Console.WriteLine("try again: ");
                    input = Console.ReadLine();
                }
                try
                {
                    if (input.Length == 4)
                    {
                        input = (8 - (input[1] - '0')) + "" + alphaBetTonum(input[0]) + (8 - (input[3] - '0')) + alphaBetTonum(input[2]);
                        board[input[2] - '0', input[3] - '0'] = board[input[0] - '0', input[1] - '0'];
                        board[input[0] - '0', input[1] - '0'] = 0;
                    }
                    else if (input.Length == 5)
                    {
                        input = (8 - (input[1] - '0')) + "" + alphaBetTonum(input[0]) + (8 - (input[3] - '0')) + alphaBetTonum(input[2]) + input[4];
                        board[input[0] - '0', input[1] - '0'] = 0;
                        if (input[4] == 'n')
                            board[input[2] - '0', input[3] - '0'] = -2;
                        else if (input[4] == 'b')
                            board[input[2] - '0', input[3] - '0'] = -3;
                        else if (input[4] == 'r')
                            board[input[2] - '0', input[3] - '0'] = -4;
                        else if (input[4] == 'q')
                            board[input[2] - '0', input[3] - '0'] = -5;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("try again: ");
                    continue;
                }
                if (!ContainsMatrixByValue(allLeagalMoves, board))
                {
                    board = boardSave;
                    tmp = board[0, 0];
                    boardSave = moveTo(board, 0, 0, 0, 0);
                    board[0, 0] = tmp;
                    Console.WriteLine("try again: ");
                }
            } while (!ContainsMatrixByValue(allLeagalMoves, board));
            whiteMove = !whiteMove;
            movesCount++;
        }
        
        show(board);
        
        if (!inCheck(board, whiteMove))
            Console.WriteLine("Stale Mate! moves count: "+movesCount);
        else
        {
            Console.WriteLine("Check Mate! movesCount: "+movesCount);
            if (whiteMove)
                Console.WriteLine("Black wins");
            else
                Console.WriteLine("White wins");
        }
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
 * 
 * I have been working on it for just 3 days now
 * 
 * 
 */

