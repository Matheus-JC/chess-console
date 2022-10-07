using board;
using chess;

try
{
    Board board = new Board(8, 8);

    board.PutPiece(new Tower(board, Color.Black), new Position(0, 0));
    board.PutPiece(new Tower(board, Color.Black), new Position(1, 9));
    board.PutPiece(new King(board, Color.Black), new Position(2, 4));

    Screen.PrintBoard(board);

    Console.ReadLine();
}
catch(BoardException exc)
{
    Console.WriteLine(exc.Message);
}
