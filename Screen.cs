using board;

class Screen
{
    public static void PrintBoard(Board board)
    {
        for (int i = 0; i < board.Lines; i++)
        {
            for(int j = 0; j < board.Columns; j++){
                Piece piece = board.GetPiece(i, j);
                Console.Write(piece != null ? piece : '-' + " ");
            }

            Console.WriteLine();
        }
    }
}