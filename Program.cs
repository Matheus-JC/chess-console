using board;
using chess;

try
{
    ChessGame game = new ChessGame();

    while(!game.finished)
    {
        Console.Clear();
        
        if(game.Board == null)
        {
            throw new BoardException("Board is Empty");
        }
        
        Screen.PrintBoard(game.Board);

        Console.WriteLine();
        Console.Write("Origin Position: ");
        Position posOrigin = Screen.ReadChessPosition().ToPosition();

        Piece? originPiece = game.Board.GetPiece(posOrigin);

        if(originPiece == null)
        {
            throw new BoardException("OPrigin Piece is Empty");
        }

        bool[,] possiblePositions = originPiece.PossibleMoviments();

        Console.Clear();
        Screen.PrintBoard(game.Board, possiblePositions);

        Console.WriteLine();
        Console.Write("Destiny Position: ");
        Position posDestiny = Screen.ReadChessPosition().ToPosition();

        game.PerformMoviment(posOrigin, posDestiny);
    }
    
    if(game.Board != null)
    {
        Screen.PrintBoard(game.Board);
    }
}
catch(BoardException exc)
{
    Console.WriteLine(exc.Message);
}

Console.ReadLine();

