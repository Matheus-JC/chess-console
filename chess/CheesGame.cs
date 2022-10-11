using board;

namespace chess;

class ChessGame
{
    public Board Board { get; private set; }
    public int Turn { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool Finished { get; private set; }
    private HashSet<Piece> Pieces;
    private HashSet<Piece> CapturedPieces;

    public ChessGame()
    {
        Board = new Board(8, 8);
        Turn = 1;
        CurrentPlayer = Color.White;
        Finished = false;
        Pieces = new HashSet<Piece>();
        CapturedPieces = new HashSet<Piece>();
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
        
        if(caputuredPiece != null)
        {
            CapturedPieces.Add(caputuredPiece);
        }
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

    public HashSet<Piece> GetCapturedPieces(Color color)
    {
        return CapturedPieces.Where(piece => piece.Color == color).ToHashSet();
    }

    public HashSet<Piece> GetPiecesStillInPLay(Color color)
    {
        HashSet<Piece> piecesInPlay = Pieces.Where(piece => piece.Color == color).ToHashSet();
        piecesInPlay.ExceptWith(GetCapturedPieces(color));
        return piecesInPlay;
    } 

    public void PutNewPiece(char column, int line, Piece piece)
    {
        Board.PutPiece(piece, new ChessPosition(column, line).ToPosition());
        Pieces.Add(piece);
    }

    private void PutPieces()
    {
        if(Board == null)
        {
            throw new BoardException("Board is Empty!");
        }

        PutNewPiece('c', 1, new Tower(Board, Color.White));
        PutNewPiece('c', 2, new Tower(Board, Color.White));
        PutNewPiece('d', 2, new Tower(Board, Color.White));
        PutNewPiece('e', 2, new Tower(Board, Color.White));
        PutNewPiece('e', 1, new Tower(Board, Color.White));
        PutNewPiece('d', 1, new King(Board, Color.White));

         PutNewPiece('c', 7, new Tower(Board, Color.Black));
        PutNewPiece('c', 8, new Tower(Board, Color.Black));
        PutNewPiece('d', 7, new Tower(Board, Color.Black));
        PutNewPiece('e', 7, new Tower(Board, Color.Black));
        PutNewPiece('e', 8, new Tower(Board, Color.Black));
        PutNewPiece('d', 8, new King(Board, Color.Black));
    }
}