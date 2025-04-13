void show(int[,] board)
{
    for (int i = 0; i < board.GetLength(0); i++)
    {
        for (int j = 0; j < board.GetLength(0); j++)
        {
            Console.Write(getPice(board[i,j])+" ");
            if (j != 7)
                Console.Write("|");
        }
        Console.WriteLine();
        if(i != 7)
            for (int j = 0; j < 3*board.GetLength(0); j++)
                Console.Write("-");
        Console.WriteLine();
    }
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
int evaluate(int[,] board)
{
    int evo = 0;
    for(int i=0;i<board.GetLength(0);i++)
        for(int j = 0; j < board.GetLength(0); j++)
        {
            if (Math.Abs(board[i, j]) == 6)
                evo += board[i, j] * 150;
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
Move getBestMoveWhite(int[,] board,int deep,int currEvo)
{
    if (deep == 0)
    {
        LinkedList<int[,]> movesNoSeek = getAllMoves(board, true);
        Move bestMoveNoSeek = new Move();
        bestMoveNoSeek.evo = 1000000;
        foreach (int[,] move in movesNoSeek)
        {
            int thisEvo = evaluate(move);
            if(thisEvo < bestMoveNoSeek.evo)
            {
                bestMoveNoSeek.evo = thisEvo;
                bestMoveNoSeek.board = move;
            }
        }
        return bestMoveNoSeek;
    }
    LinkedList<int[,]> moves = getAllMoves(board,true);
    Move bestMove=new Move();
    bestMove.evo = currEvo;
    foreach (int[,] move in moves)
    {
        Move hisMove = getBestMoveBlack(move, deep - 1,currEvo);
        if (hisMove.evo <= bestMove.evo)
        {
            bestMove.board = move;
            bestMove.evo = hisMove.evo;
        }
        else
            return bestMove;
    }
    return bestMove;
}
Move getBestMoveBlack(int[,] board, int deep,int currEvo)
{
    if (deep == 0)
    {
        LinkedList<int[,]> movesNoSeek = getAllMoves(board, true);
        Move bestMoveNoSeek = new Move();
        bestMoveNoSeek.evo = -1000000;
        foreach (int[,] move in movesNoSeek)
        {
            int thisEvo = evaluate(move);
            if (thisEvo >= bestMoveNoSeek.evo)
            {
                bestMoveNoSeek.evo = thisEvo;
                bestMoveNoSeek.board = move;
            }
        }
        return bestMoveNoSeek;
    }
    LinkedList<int[,]> moves = getAllMoves(board, false);
    Move bestMove = new Move();
    bestMove.evo = -1000000;
    foreach (int[,] move in moves)
    {
        Move hisMove = getBestMoveWhite(move, deep - 1,currEvo);
        if (hisMove.evo > bestMove.evo)
        {
            bestMove.board = move;
            bestMove.evo = hisMove.evo;
        }
        else
            return bestMove;
    }
    return bestMove;

}
int[,] board =
    {
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    {-1,-1,-1,-1,-1,-1,-1,-1 },
    {-4,-2,-3,-5,-6,-3,-2,-4 }
};
int[,] otherBoard =
    {
    { 4, 0, 3 ,5 ,6 ,0 ,2 ,4 },
    { 1, 1, 1, 0, 0, 1, 1, 0 },
    { 0, 0, 1, 0, 0, 0, 0, 0 },
    { 0, 0, 3, 0,-1, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, -3, 0, 0, 0, 0, 0, 0 },
    {-1,-1,-1,-1, 0,-1, 0,-1 },
    {-4,-2,-3,-5, 0, 5,-6, 0 }
};
int[,] smallBoard =
{
    { 4, 0, 0 },
    { 2, 0, 3 },
    { 0, -3, 0 }
};
int[,] nextMove = getBestMoveWhite(otherBoard,7,100000).board;
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
 * I have been working on it for just a couple houres now
 * 
 * 
 */

