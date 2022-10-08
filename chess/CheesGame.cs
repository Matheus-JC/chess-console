using board;

namespace chess;

class ChessGame
{
    public Board Board { get; private set; }
    private int _turn;
    private Color _currentPlayer;
    public bool finished { get; private set; }

    public ChessGame()
    {
        Board = new Board(8, 8);
        _turn = 1;
        _currentPlayer = Color.White;
        finished = false;
        PutPieces();
    }

    public void PerformMoviment(Position posOrigin, Position posDestiny)
    {
        if(Board == null)
        {
            throw new BoardException("Board is Empty!");
        }

        Piece? origemPiece = Board.RemovePiece(posOrigin);

        if(origemPiece == null)
        {
            throw new BoardException("Piece not found!");
        }

        origemPiece.IncreaseMovimentsNumber();
        Piece? caputuredPiece = Board.RemovePiece(posDestiny);
        Board.PutPiece(origemPiece, posDestiny);
    }

    private void PutPieces()
    {
        if(Board == null)
        {
            throw new BoardException("Board is Empty!");
        }

        Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('c', 1).ToPosition());
        Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('c', 2).ToPosition());
        Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('d', 2).ToPosition());
        Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('e', 2).ToPosition());
        Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('e', 1).ToPosition());
        Board.PutPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

        Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
        Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
        Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
        Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
        Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
        Board.PutPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
    }
}