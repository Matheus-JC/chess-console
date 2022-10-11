using board;
using chess;

try
{
    ChessGame chessGame = new ChessGame();

    while(!chessGame.Finished)
    {
        try
        {

            Console.Clear();
            Screen.PrintMatch(chessGame);
            
            Console.WriteLine();
            Console.Write("Origin Position: ");
            Position posOrigin = Screen.ReadChessPosition().ToPosition();

            chessGame.ValidateOriginPosition(posOrigin);

            Piece? originPiece = chessGame.Board.GetPiece(posOrigin);

            if(originPiece == null)
            {
                throw new BoardException("OPrigin Piece is Empty");
            }

            bool[,] possiblePositions = originPiece.PossibleMoves();

            Console.Clear();
            Screen.PrintBoard(chessGame.Board, possiblePositions);

            Console.WriteLine();
            Console.Write("Destiny Position: ");
            Position posDestiny = Screen.ReadChessPosition().ToPosition();
            chessGame.ValidateDestinyPosition(posOrigin, posDestiny);

            chessGame.MakesMove(posOrigin, posDestiny);
        }
        catch(BoardException exc)
        {
            Console.WriteLine(exc.Message);
            Console.ReadLine();
        }
    }
    
    if(chessGame.Board != null)
    {
        Screen.PrintBoard(chessGame.Board);
    }
}
catch(BoardException exc)
{
    Console.WriteLine(exc.Message);
}

Console.ReadLine();

