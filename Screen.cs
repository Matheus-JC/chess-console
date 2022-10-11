using board;
using chess;

class Screen
{
    public static void PrintBoard(Board board)
    {
        for (int i = 0; i < board.Lines; i++)
        {
            Console.Write(8 - i + " ");
            for(int j = 0; j < board.Columns; j++){
                PrintPiece(board.GetPiece(i, j));
            }

            Console.WriteLine();
        }

        Console.WriteLine("  a b c d e f g h");
    }

    public static void PrintBoard(Board board, bool[,] possiblePositions)
    {
        ConsoleColor originalBackGround = Console.BackgroundColor;
        ConsoleColor backGroundChanged = ConsoleColor.DarkGray;

        for (int i = 0; i < board.Lines; i++)
        {
            Console.Write(8 - i + " ");
            for(int j = 0; j < board.Columns; j++){
                Console.BackgroundColor = possiblePositions[i, j] ? backGroundChanged : originalBackGround;
                PrintPiece(board.GetPiece(i, j));
                Console.BackgroundColor = originalBackGround;
            }

            Console.WriteLine();
        }

        Console.WriteLine("  a b c d e f g h");
        Console.BackgroundColor = originalBackGround;
    }

    public static void PrintPiece(Piece? piece)
    {
        if(piece == null)
        {
            Console.Write("-");
        }
        else{
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

        Console.Write(" ");
    }

    public static ChessPosition ReadChessPosition()
    {
        string? typedValue = Console.ReadLine();

        if(String.IsNullOrEmpty(typedValue))
        {
            throw new BoardException("Entered value is invalid");
        }

        char column = typedValue[0];
        int line = int.Parse(typedValue[1] + "");
        return new ChessPosition(column, line);
    }
}