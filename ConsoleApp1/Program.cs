String[] show(int[,] board)
{
    String[] boardS = new string[8];
    for (int i = 0; i < board.GetLength(0); i++)
    {
        for (int j = 0; j < board.GetLength(0); j++)
        {
            boardS[i] += getPice(board[i, j]) + " ";
            Console.Write(getPice(board[i,j])+" ");
            if (j != 7)
            {
                Console.Write("|");
                boardS[i] += "|";
            }
        }
        Console.WriteLine();
        if(i != 7)
            for (int j = 0; j < 3*board.GetLength(0); j++)
                Console.Write("-");
        Console.WriteLine();
    }
    return boardS;
}
String getPice(int m)
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
int[,] moveTo(int[,] board, int startX,int startY, int endX, int endY)
{
    int[,] newBoard = new int[board.GetLength(0), board.GetLength(0)];
    newBoard[endX, endY] = board[startX, startY];
    newBoard[startX, startY] = 0;
    for (int i=0;i<newBoard.GetLength(0);i++)
        for(int j=0;j<newBoard.GetLength(0);j++)
        {
            if ((i == startX && j == startY) || (i == endX && j == endY))
                continue;
            newBoard[i,j]=board[i,j];
        }
    return newBoard;
}
LinkedList<int[,]> getAllMoves(int[,] board,bool whiteMove)
{
    LinkedList<int[,]> moves = new LinkedList<int[,]>();
    int len=board.GetLength(0)-1;

    //castling
    if (len==8&&whiteMove)
    {
        if (board[7, 7] == -4 && board[7, 4] == -6 && board[7, 6] == 0 && board[7, 5] == 0)
        {
            int[,] newBoard = moveTo(board, 7, 7, 7, 5);
            moves.AddLast(moveTo(newBoard, 7, 4, 7, 6));
        }
        if (board[7, 0] == -4 && board[7, 4] == -6 && board[7, 1] == 0 && board[7, 2] == 0&& board[7,3]==0)
        {
            int[,] newBoard = moveTo(board, 7, 0, 7, 3);
            moves.AddLast(moveTo(newBoard, 7, 4, 7, 2));
        }
    }
    else if(len==8)
    {
        if (board[0, 7] == 4 && board[0, 4] == 6 && board[0, 6] == 0 && board[0, 5] == 0)
        {
            int[,] newBoard = moveTo(board, 0, 7, 0, 5);
            moves.AddLast(moveTo(newBoard, 0, 4, 0, 6));
        }
        if (board[0, 0] == 4 && board[0, 4] == 6 && board[0, 1] == 0 && board[0, 2] == 0 && board[0,4]==0)
        {
            int[,] newBoard = moveTo(board, 0, 0, 0, 3);
            moves.AddLast(moveTo(newBoard, 0, 4, 0, 2));
        }
    }
    for (int i = 0; i < board.GetLength(0); i++)
    {
        for(int j = 0; j < board.GetLength(0); j++)
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
                            int[,] newBoard = moveTo(board,i,j,i-1,j);
                            newBoard[i - 1, j] = -3;
                            moves.AddLast(newBoard);
                            newBoard = moveTo(board, i, j, i - 1, j);
                            newBoard[i - 1, j] = -4;
                            moves.AddLast(newBoard);
                            newBoard = moveTo(board, i, j, i - 1, j);
                            newBoard[i - 1, j] = -5;
                            moves.AddLast(newBoard);
                        }
                        if(i==6&&board[i - 2, j] == 0)
                            moves.AddLast(moveTo(board, i, j, i - 2, j));
                        
                    }
                    if (j != 0 && board[i - 1, j - 1] > 0)
                    {
                        moves.AddLast(moveTo(board, i, j, i - 1, j - 1));
                        if (i - 1 == 0)
                        {
                            moves.Last.Value[i - 1, j-1] = -2;
                            int[,] newBoard = moveTo(board, i, j, i - 1, j-1);
                            newBoard[i - 1, j-1] = -3;
                            moves.AddLast(newBoard);
                            newBoard = moveTo(board, i, j, i - 1, j-1);
                            newBoard[i - 1, j-1] = -4;
                            moves.AddLast(newBoard);
                            newBoard = moveTo(board, i, j, i - 1, j-1);
                            newBoard[i - 1, j-1] = -5;
                            moves.AddLast(newBoard);
                        }
                    }
                    if (j != len && board[i - 1, j + 1] > 0)
                    {
                        moves.AddLast(moveTo(board, i, j, i - 1, j + 1));
                        if (i - 1 == 0)
                        {
                            moves.Last.Value[i - 1, j+1] = -2;
                            int[,] newBoard = moveTo(board, i, j, i - 1, j+1);
                            newBoard[i - 1, j+1] = -3;
                            moves.AddLast(newBoard);
                            newBoard = moveTo(board, i, j, i - 1, j+1);
                            newBoard[i - 1, j+1] = -4;
                            moves.AddLast(newBoard);
                            newBoard = moveTo(board, i, j, i - 1, j+1);
                            newBoard[i - 1, j+1] = -5;
                            moves.AddLast(newBoard);
                        }
                    }
                }
                if(board[i, j] == -2)//knight
                {
                    if (i > 1 && j != len && board[i-2,j+1]>=0)//^^>
                        moves.AddLast(moveTo(board, i, j, i - 2, j + 1));
                    if (i > 1 && j != 0 && board[i - 2, j - 1] >= 0)//^^<
                        moves.AddLast(moveTo(board, i, j, i - 2, j - 1));
                    if (i !=0 && j < len-1 && board[i - 1, j + 2] >= 0)//>>^
                        moves.AddLast(moveTo(board, i, j, i - 1, j + 2));
                    if (i != len && j < len - 1 && board[i + 1, j + 2] >= 0)//>>v
                        moves.AddLast(moveTo(board, i, j, i + 1, j + 2));
                    if (i < len-1 && j != len && board[i + 2, j + 1] >= 0)//vv>
                        moves.AddLast(moveTo(board, i, j, i + 2, j + 1));
                    if (i < len-1 && j != 0 && board[i + 2, j - 1] >= 0)//vv<
                        moves.AddLast(moveTo(board, i, j, i + 2, j - 1));
                    if (i != 0 && j > 1 && board[i - 1, j - 2] >= 0)//<<^
                        moves.AddLast(moveTo(board, i, j, i - 1, j - 2));
                    if (i != len && j > 1 && board[i + 1, j - 2] >= 0)//<<v
                        moves.AddLast(moveTo(board, i, j, i + 1, j - 2));
                }
                if (board[i, j] == -3)//bishop
                {
                    int k = i-1;
                    int l = j-1;
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
                    while (k < len+1 && l < len+1 && board[k, l] == 0)
                    {
                        moves.AddLast(moveTo(board, i, j, k, l));
                        k++;
                        l++;
                    }
                    if (k < len+1 && l < len+1 && board[k, l] > 0)
                        moves.AddLast(moveTo(board, i, j, k, l));
                    k = i + 1;
                    l = j - 1;
                    while (k < len + 1 && l >=0 && board[k, l] == 0)
                    {
                        moves.AddLast(moveTo(board, i, j, k, l));
                        k++;
                        l--;
                    }
                    if (k < len+1 && l >=0 && board[k, l] > 0)
                        moves.AddLast(moveTo(board, i, j, k, l));
                    k = i - 1;
                    l = j + 1;
                    while (k >=0 && l < len+1 && board[k, l] == 0)
                    {
                        moves.AddLast(moveTo(board, i, j, k, l));
                        k--;
                        l++;
                    }
                    if (k >= 0 && l < len + 1 && board[k, l] > 0)
                        moves.AddLast(moveTo(board, i, j, k, l));
                }
                if(board[i, j] == -4)//rook
                {
                    int k = i-1;
                    int l = j-1;
                    while (k >= 0 && board[k, j] == 0)
                    {
                        moves.AddLast(moveTo(board, i, j, k, j));
                        k--;
                    }
                    if(k>=0&&board[k, j] > 0)
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
                if(board[i, j] == -5)//queen
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
                if (board[i,j] == -6)//king
                {
                    if (i != 0 && j != 0 && board[i-1, j-1] >= 0)//^<
                        moves.AddLast(moveTo(board, i, j, i - 1, j - 1));
                    if (i != 0 && board[i-1, j] >= 0)//^
                        moves.AddLast(moveTo(board, i, j, i - 1, j));
                    if (i != 0 && j != len && board[i-1, j+1] >= 0)//^>
                        moves.AddLast(moveTo(board, i, j, i - 1, j + 1));
                    if (j != len && board[i, j+1] >= 0)//>
                        moves.AddLast(moveTo(board, i, j, i, j + 1));
                    if (i != len && j != len && board[i+1, j+1] >= 0)//v>
                        moves.AddLast(moveTo(board, i, j, i + 1, j + 1));
                    if (i != 0 && board[i-1, j] >= 0)//v
                        moves.AddLast(moveTo(board, i, j, i - 1, j));
                    if (i != len && j != 0 && board[i+1, j-1] >= 0)//v<
                        moves.AddLast(moveTo(board, i, j, i + 1, j - 1));
                    if (j != 0 && board[i, j-1] >= 0)//<
                        moves.AddLast(moveTo(board, i, j, i, j - 1));
                }
                
            }
            if (board[i,j] > 0 && !whiteMove)
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
                        if (i == 1 && board[i+2,j] == 0)
                            moves.AddLast(moveTo(board, i, j, i + 2, j));
                    }
                    if (j != 0 && board[i + 1, j - 1] < 0)
                    {
                        moves.AddLast(moveTo(board, i, j, i + 1, j - 1));
                        if (i + 1 == 0)
                        {
                            moves.Last.Value[i + 1, j-1] = 2;
                            int[,] newBoard = moveTo(board, i, j, i + 1, j-1);
                            newBoard[i + 1, j-1] = 3;
                            moves.AddLast(newBoard);
                            newBoard = moveTo(board, i, j, i + 1, j-1);
                            newBoard[i + 1, j-1] = 4;
                            moves.AddLast(newBoard);
                            newBoard = moveTo(board, i, j, i + 1, j-1);
                            newBoard[i + 1, j-1] = 5;
                            moves.AddLast(newBoard);
                        }
                    }
                    if (j != len && board[i + 1, j + 1] < 0)
                    {
                        moves.AddLast(moveTo(board, i, j, i + 1, j + 1));
                        if (i + 1 == 0)
                        {
                            moves.Last.Value[i + 1, j+1] = 2;
                            int[,] newBoard = moveTo(board, i, j, i + 1, j+1);
                            newBoard[i + 1, j+1] = 3;
                            moves.AddLast(newBoard);
                            newBoard = moveTo(board, i, j, i + 1, j+1);
                            newBoard[i + 1, j+1] = 4;
                            moves.AddLast(newBoard);
                            newBoard = moveTo(board, i, j, i + 1, j+1);
                            newBoard[i + 1, j+1] = 5;
                            moves.AddLast(newBoard);
                        }
                    }
                }
                if(board[i,j] == 2)//knight
                {
                    if (i > 1 && j != len && board[i - 2, j + 1] <= 0)//^^>
                        moves.AddLast(moveTo(board, i, j, i - 2, j + 1));
                    if (i > 1 && j != 0 && board[i - 2, j - 1] <= 0)//^^<
                        moves.AddLast(moveTo(board, i, j, i - 2, j - 1));
                    if (i != 0 && j < len - 1 && board[i - 1, j + 2] <= 0)//>>^
                        moves.AddLast(moveTo(board, i, j, i - 1, j + 2));
                    if (i != len && j < len-1 && board[i + 1, j + 2] <= 0)//>>v
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
                if (board[i,j] == 4)//rook
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
                if(board[i, j] == 5)//queen
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
                if(board[i, j] == 6)//king
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
(int,int) findPieace(int[,] board,int pieaceVal)
{
    for (int i = 0; i < board.GetLength(0); i++)
        for (int j = 0; j < board.GetLength(0); j++)
            if (board[i, j] == pieaceVal)
                return (i,j);
    return (-1, -1);
}
bool inCheck(int[,] board, bool whiteMove)
{
    int kingValue = whiteMove ? -6 : 6;
    (int kingRow, int kingCol) = findPieace(board, kingValue);

    if (kingRow == -1) return true;

    LinkedList<int[,]> opponentMoves = getAllMoves(board, !whiteMove);

    foreach (int[,] moveBoard in opponentMoves)
    {
        if (findPieace(moveBoard, kingValue) == (-1,-1))
            return true; // king was captured
    }

    return false;
}
LinkedList<int[,]> getAllLeagalMoves(int[,] board, bool whiteMove)
{
    LinkedList<int[,]> allMoves = getAllMoves(board, whiteMove);
    LinkedList<int[,]> leagalMoves = new LinkedList<int[,]>();
    foreach (int[,] move in allMoves)
    {
        if (!inCheck(move, whiteMove))
        {
            leagalMoves.AddLast(move);
        }
    }
    return leagalMoves;
}
int evaluate(int[,] board)
{
    int evo = 0;
    for(int i=0;i<board.GetLength(0);i++)
        for(int j = 0; j < board.GetLength(0); j++)
        {
            if (Math.Abs(board[i, j]) == 6)
                evo += board[i, j] * 1500;
            if (Math.Abs(board[i, j]) == 1)
                evo += board[i, j] * 10;
            if (Math.Abs(board[i, j]) == 2)
                evo += board[i, j] * 15;
            if (Math.Abs(board[i, j]) == 3)
                evo += board[i, j] * 10;
            if (Math.Abs(board[i, j]) == 4)
                evo += board[i, j] * 13;
            if (Math.Abs(board[i, j]) == 5)
                evo += board[i, j] * 18;
        }
    return evo;
}

int Search(int[,] board,int depth, int alpha, int beta,bool whiteMove)
{
    if (depth == 0) return evaluate(board);
    LinkedList<int[,]> moves = getAllLeagalMoves(board, whiteMove);
    if (moves.Count == 0)
    {
        if (inCheck(board, whiteMove))
        {
            if(whiteMove)
                return -10000000 - depth;
            return 10000000;
        }
        else
            return 0;
    }
    foreach (int[,] move in moves)
    {
   
        int evo = -Search(move,depth - 1, -beta, -alpha,!whiteMove);
        if (evo >= beta)
            return beta;
        alpha = Math.Max(alpha, evo);
    }

    return alpha;
}
int[,] getBestMove(int[,] board, int depth)
{
    int alpha = -10000000;
    int beta = 10000000;
    LinkedList<int[,]> moves = getAllLeagalMoves(board, false);
    int bestMoveEvo = Search(moves.First.Value,depth,alpha,beta,true);
    int[,] bestBoard = moves.First.Value;
    bool first = true;
    foreach (int[,] move in moves)
    {
        if (first)
        {
            first = false;
            continue;
        }
        int evo = Search(move, depth, alpha, beta,true);
        
        if (evo > bestMoveEvo)
        {
            bestMoveEvo = evo;
            bestBoard = move;
        }
    }
    Console.WriteLine(bestMoveEvo);
    return bestBoard;
}
int[,] NormalBoard =
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
int[,] otherBoard =
    {
    { 0, 0, 0, 0, 0, 0, 6, 0 },
    { 0, 0, 0, 4, 0, 1, 1, 1 },
    { 0, 0, 0, 0, 0, 3, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0,-5, 0,-1, 0, 0, 0, 0 },
    { 0, 0,-1, 0, 0, 0, 0, 0 },
    {-1, 0, 0, 0, 0,-1,-1,-1 },
    {-4, 0,-3,-4, 0, 0,-6, 0 }
};
int[,] smallBoard =
{
    { 4, 0, 0 },
    { 2, 0, 3 },
    { 0, -3, 0 }
};
//Console.WriteLine(Search(otherBoard,4,-100000,1000000,true));
int[,] nextMove = getBestMove(otherBoard,4);
show(nextMove);

/* 1 pawn
 * 2 knight
 * 3 bishop
 * 4 rook
 * 5 queen
 * 6 king
 * if numbers are positive then its a black pieace, and negative is white
 * 
 * Still work in progres, this program can not calculate the best move yet
 * I have been working on it for just 2 days now
 * 
 * 
 */

