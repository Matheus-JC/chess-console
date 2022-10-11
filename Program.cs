using board;
using chess;

try
{
    ChessGame game = new ChessGame();

    while(!game.Finished)
    {
        try
        {

            Console.Clear();
            
            if(game.Board == null)
            {
                throw new BoardException("Board is Empty");
            }
            
            Screen.PrintBoard(game.Board);
            Console.WriteLine();
            Console.WriteLine("Turn: " + game.Turn);
            Console.WriteLine("Waiting for play: " + game.CurrentPlayer);

            Console.WriteLine();
            Console.Write("Origin Position: ");
            Position posOrigin = Screen.ReadChessPosition().ToPosition();

            game.ValidateOriginPosition(posOrigin);

            Piece? originPiece = game.Board.GetPiece(posOrigin);

            if(originPiece == null)
            {
                throw new BoardException("OPrigin Piece is Empty");
            }

            bool[,] possiblePositions = originPiece.PossibleMoves();

            Console.Clear();
            Screen.PrintBoard(game.Board, possiblePositions);

            Console.WriteLine();
            Console.Write("Destiny Position: ");
            Position posDestiny = Screen.ReadChessPosition().ToPosition();
            game.ValidateDestinyPosition(posOrigin, posDestiny);

            game.MakesMove(posOrigin, posDestiny);
        }
        catch(BoardException exc)
        {
            Console.WriteLine(exc.Message);
            Console.ReadLine();
        }
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

