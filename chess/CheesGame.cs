using board;

namespace chess;

class ChessGame
{
    public Board Board { get; private set; }
    public int Turn { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool Finished { get; private set; }
    public bool Check { get; private set; }
    private HashSet<Piece> Pieces;
    private HashSet<Piece> CapturedPieces;

    public ChessGame()
    {
        Board = new Board(8, 8);
        Turn = 1;
        CurrentPlayer = Color.White;
        Finished = false;
        Check = false;
        Pieces = new HashSet<Piece>();
        CapturedPieces = new HashSet<Piece>();
        PutPieces();
    }

    private Piece? PerformMoviment(Position posOrigin, Position posDestiny)
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

        return caputuredPiece;
    }

    private void UndoMove(Position posOrigin, Position posDestiny, Piece? capturedPiece)
    {
        Piece? piece = Board.RemovePiece(posDestiny);

        if(piece == null)
        {
            throw new BoardException("There is no piece in the specified destination position!");
        }

        piece.DecreaseMovimentsNumber();

        if(capturedPiece != null)
        {
            Board.PutPiece(capturedPiece, posDestiny);
            CapturedPieces.Remove(capturedPiece);
        }

        Board.PutPiece(piece, posOrigin);
    }

    private void SwitchPlayer()
    {
        CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
    }

    private Color GetAdversary(Color color)
    {
        return color == Color.White ? Color.Black : Color.White;
    }

    private Piece? GetKing(Color color)
    {
        return GetPiecesStillInPLay(color).FirstOrDefault(piece => piece is King);
    }

    public void MakesMove(Position posOrigin, Position posDestiny)
    {
        Piece? capturedPiece = PerformMoviment(posOrigin, posDestiny);

        if(IsCheck(CurrentPlayer))
        {
            UndoMove(posOrigin, posDestiny,capturedPiece);
            throw new BoardException("You cannot put yourself in check!");
        }

        Check = IsCheck(GetAdversary(CurrentPlayer));

        if(IsCheckMate(GetAdversary(CurrentPlayer)))
        {
            Finished = true;
        }
        else
        {
            Turn++;
            SwitchPlayer();
        }
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

        if(!originPiece.IsPossibleMove(posDestiny))
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

    public bool IsCheck(Color color)
    {
        Piece? king = GetKing(color);

        if(king == null)
        {
            throw new BoardException($"There is no {color} king on the board!");
        }

        if(king.Position == null)
        {
            throw new BoardException($"The {color} king is not in any position!");
        }

        foreach(Piece piece in GetPiecesStillInPLay(GetAdversary(color)))
        {
            bool[,] possibleMoves = piece.GetPossibleMoves();

            if(possibleMoves[king.Position.Line, king.Position.Column])
            {
                return true;
            }
        }

        return false;
    }

    public bool IsCheckMate(Color color)
    {
        if(!IsCheck(color)) 
        {
            return false;
        }

        foreach(Piece? piece in GetPiecesStillInPLay(color))
        {
            bool[,] possibleMoves = piece.GetPossibleMoves();

            for(int i = 0; i < Board.Lines; i++)
            {
                for(int j = 0; j < Board.Columns; j++)
                {
                    if(possibleMoves[i, j] && piece.Position != null)
                    {
                        Position posOrigin = piece.Position;
                        Position posDestiny = new Position(i, j);
                        Piece? capturedPiece = PerformMoviment(posOrigin, posDestiny);
                        bool isCheck = IsCheck(color);
                        UndoMove(posOrigin, posDestiny, capturedPiece);

                        // there is a movement that withdraws from the check
                        if(!isCheck)
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
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
        PutNewPiece('d', 1, new King(Board, Color.White));
        PutNewPiece('h', 7, new Tower(Board, Color.White));

        PutNewPiece('a', 8, new King(Board, Color.Black));
        PutNewPiece('b', 8, new Tower(Board, Color.Black));
    }
}