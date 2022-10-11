using board;

namespace chess;

class ChessGame
{
    public Board Board { get; private set; }
    public int Turn { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool Finished { get; private set; }

    public ChessGame()
    {
        Board = new Board(8, 8);
        Turn = 1;
        CurrentPlayer = Color.White;
        Finished = false;
        PutPieces();
    }

    private void PerformMoviment(Position posOrigin, Position posDestiny)
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

    private void SwitchPlayer()
    {
        CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
    }

    public void MakesMove(Position posOrigin, Position posDestiny)
    {
        PerformMoviment(posOrigin, posDestiny);
        Turn++;
        SwitchPlayer();
    }

    public void ValidateOriginPosition(Position pos)
    {
        Piece? piece = Board.GetPiece(pos);

        if(piece == null)
        {
            throw new BoardException("there isn't origin piece in the choosen position!");
        }

        if(CurrentPlayer != piece.Color)
        {
            throw new BoardException("The choosen origin piece is not yours!");
        }

        if(!piece.ExistPossibleMoves())
        {
            throw new BoardException("There aren't possible moves for the choosen origin piece!");
        }
    }

    public void ValidateDestinyPosition(Position posOrigin, Position posDestiny)
    {
        Piece? originPiece = Board.GetPiece(posOrigin);

        if(originPiece == null)
        {
            throw new BoardException("Origin piece is empty!");
        }

        if(!originPiece.CanMoveTo(posDestiny))
        {
            throw new BoardException("Invalid destiny position!");
        }
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