using board;

class Screen
{
    public static void PrintBoard(Board board)
    {
        for (int i = 0; i < board.Lines; i++)
        {
            Console.Write(8 - i + " ");
            for(int j = 0; j < board.Columns; j++){
                Piece piece = board.GetPiece(i, j);
                
                if(piece != null)
                {
                    PrintPiece(piece);
                }
                else
                {
                    Console.Write("-");
                }

                Console.Write(" ");
            }

            Console.WriteLine();
        }

        Console.WriteLine("  a b c d e f g h");
    }

    public static void PrintPiece(Piece piece)
    {
        if(piece.Color == Color.White)
        {
            Console.Write(piece);
        }
        else
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(piece);
            Console.ForegroundColor = aux;
        }
    }
}