using board;
using chess;

try
{
    Board board = new Board(8, 8);

    board.PutPiece(new Tower(board, Color.Black), new Position(0, 0));
    board.PutPiece(new Tower(board, Color.Black), new Position(1, 3));
    board.PutPiece(new King(board, Color.Black), new Position(0, 2));

    board.PutPiece(new Tower(board, Color.White), new Position(3, 5));

    Screen.PrintBoard(board);
}
catch(BoardException exc)
{
    Console.WriteLine(exc.Message);
}

Console.ReadLine();

