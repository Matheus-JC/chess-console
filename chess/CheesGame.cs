using board;

namespace chess;

class ChessGame
{
    public Board Board { get; private set; }
    public int Turn { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool Finished { get; private set; }
    public bool Check { get; private set; }
    public Piece? VulnerableEnPassant { get; private set; }
    private HashSet<Piece> Pieces;
    private HashSet<Piece> CapturedPieces;

    public ChessGame()
    {
        Board = new Board(8, 8);
        Turn = 1;
        CurrentPlayer = Color.White;
        Finished = false;
        Check = false;
        VulnerableEnPassant = null;
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

        Piece? piece = Board.RemovePiece(posOrigin);

        if(piece == null)
        {
            throw new BoardException("Piece not found!");
        }

        piece.IncreaseMovimentsNumber();
        Piece? capturedPiece = Board.RemovePiece(posDestiny);
        Board.PutPiece(piece, posDestiny);
        
        if(capturedPiece != null)
        {
            CapturedPieces.Add(capturedPiece);
        }

        // #SpecialMove Castling kingside (short)
        if (piece is King && posDestiny.Column == posOrigin.Column + 2)
        {
            DoShortCastling(posOrigin);
        }

        // #SpecialMove Castling queenside (long)
        if (piece is King && posDestiny.Column == posOrigin.Column - 2)
        {
            DoLongCastling(posOrigin);
        }

        // #SpecialMove Castling En Passant
        if(piece is Pawn)
        {
            if(posOrigin.Column != posDestiny.Column && capturedPiece == null)
            {
                Position posPawn;

                if(piece.Color == Color.White)
                {
                    posPawn = new Position(posDestiny.Line + 1, posDestiny.Column);
                }
                else
                {
                    posPawn = new Position(posDestiny.Line - 1, posDestiny.Column);
                }

                capturedPiece = Board.RemovePiece(posPawn);
                
                if(capturedPiece != null)
                {
                    CapturedPieces.Add(capturedPiece);
                }
            }
        }

        return capturedPiece;
    }

    private void DoShortCastling(Position posOriginKing)
    {
        Position originRook = new Position(posOriginKing.Line, posOriginKing.Column + 3);
        Position destinyRook = new Position(posOriginKing.Line, posOriginKing.Column + 1);
        Piece? rook = Board.RemovePiece(originRook);
        
        if(rook == null)
        {
            throw new BoardException("Rook not found to castle!");
        }

        rook.IncreaseMovimentsNumber();
        Board.PutPiece(rook, destinyRook);
    }

    private void DoLongCastling(Position posOriginKing)
    {
        Position originRook = new Position(posOriginKing.Line, posOriginKing.Column - 4);
        Position destinyRook = new Position(posOriginKing.Line, posOriginKing.Column - 1);
        Piece? rook = Board.RemovePiece(originRook);
        
        if(rook == null)
        {
            throw new BoardException("Rook not found to castle!");
        }

        rook.IncreaseMovimentsNumber();
        Board.PutPiece(rook, destinyRook);
    }

    private void UndoShortCastling(Position posOriginKing)
    {
        Position originRook = new Position(posOriginKing.Line, posOriginKing.Column + 3);
        Position destinyRook = new Position(posOriginKing.Line, posOriginKing.Column + 1);
        Piece? rook = Board.RemovePiece(destinyRook);
        
        if(rook == null)
        {
            throw new BoardException("Rook not found to castle!");
        }

        rook.DecreaseMovimentsNumber();
        Board.PutPiece(rook, originRook);
    }

    private void UndoLongCastling(Position posOriginKing)
    {
        Position originRook = new Position(posOriginKing.Line, posOriginKing.Column - 4);
        Position destinyRook = new Position(posOriginKing.Line, posOriginKing.Column - 1);
        Piece? rook = Board.RemovePiece(destinyRook);
        
        if(rook == null)
        {
            throw new BoardException("Rook not found to castle!");
        }

        rook.DecreaseMovimentsNumber();
        Board.PutPiece(rook, originRook);
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

        // #SpecialMove Castling kingside (short)
        if (piece is King && posDestiny.Column == posOrigin.Column + 2)
        {
            UndoShortCastling(posOrigin);
        }

        // #SpecialMove Castling queenside (long)
        if (piece is King && posDestiny.Column == posOrigin.Column - 2)
        {
            UndoLongCastling(posOrigin);
        }

        // #SpecialMove Castling En Passant
        if(piece is Pawn)
        {
            if(posOrigin.Column != posDestiny.Column && capturedPiece == VulnerableEnPassant)
            {
                Piece? pawn = Board.RemovePiece(posDestiny);
                Position posPawn;

                if(piece.Color == Color.White)
                {
                    posPawn = new Position(3, posDestiny.Column);
                }
                else
                {
                    posPawn = new Position(4, posDestiny.Column);
                }

                if(pawn != null)
                {
                    Board.PutPiece(pawn, posPawn);
                }
            }
        }
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
            UndoMove(posOrigin, posDestiny, capturedPiece);
            throw new BoardException("You cannot put yourself in check!");
        }

        Piece? movedPiece = Board.GetPiece(posDestiny);

        // #SpecialMove Promotion
        if(movedPiece is Pawn)
        {
            if(
                (movedPiece.Color == Color.White && posDestiny.Line == 0) 
                || (movedPiece.Color == Color.Black && posDestiny.Line == 7)
            )
            {
                movedPiece = Board.RemovePiece(posDestiny);
                if(movedPiece != null)
                {
                    Pieces.Remove(movedPiece);
                    Piece queen = new Queen(Board, movedPiece.Color);
                    Board.PutPiece(queen, posDestiny);
                    Pieces.Add(queen);
                }
            }
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

        if(movedPiece != null)
        {
            // #SpecialMove En Passant
            if(movedPiece is Pawn && (posDestiny.Line == posOrigin.Line - 2 || posDestiny.Line == posOrigin.Line + 2)
            )
            {
                VulnerableEnPassant = movedPiece;
            }
            else
            {
                VulnerableEnPassant = null;
            }
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

        // White Pieces
        PutNewPiece('a', 1, new Rook(Board, Color.White));
        PutNewPiece('b', 1, new Knight(Board, Color.White));
        PutNewPiece('c', 1, new Bishop(Board, Color.White));
        PutNewPiece('d', 1, new Queen(Board, Color.White));
        PutNewPiece('e', 1, new King(Board, Color.White, this));
        PutNewPiece('f', 1, new Bishop(Board, Color.White));
        PutNewPiece('g', 1, new Knight(Board, Color.White));
        PutNewPiece('h', 1, new Rook(Board, Color.White));
        PutNewPiece('a', 2, new Pawn(Board, Color.White, this));
        PutNewPiece('b', 2, new Pawn(Board, Color.White, this));
        PutNewPiece('c', 2, new Pawn(Board, Color.White, this));
        PutNewPiece('d', 2, new Pawn(Board, Color.White, this));
        PutNewPiece('e', 2, new Pawn(Board, Color.White, this));
        PutNewPiece('f', 2, new Pawn(Board, Color.White, this));
        PutNewPiece('g', 2, new Pawn(Board, Color.White, this));
        PutNewPiece('h', 2, new Pawn(Board, Color.White, this));

        // Black Pieces
        PutNewPiece('a', 8, new Rook(Board, Color.Black));
        PutNewPiece('b', 8, new Knight(Board, Color.Black));
        PutNewPiece('c', 8, new Bishop(Board, Color.Black));
        PutNewPiece('d', 8, new Queen(Board, Color.Black));
        PutNewPiece('e', 8, new King(Board, Color.Black, this));
        PutNewPiece('f', 8, new Bishop(Board, Color.Black));
        PutNewPiece('g', 8, new Knight(Board, Color.Black));
        PutNewPiece('h', 8, new Rook(Board, Color.Black));
        PutNewPiece('a', 7, new Pawn(Board, Color.Black, this));
        PutNewPiece('b', 7, new Pawn(Board, Color.Black, this));
        PutNewPiece('c', 7, new Pawn(Board, Color.Black, this));
        PutNewPiece('d', 7, new Pawn(Board, Color.Black, this));
        PutNewPiece('e', 7, new Pawn(Board, Color.Black, this));
        PutNewPiece('f', 7, new Pawn(Board, Color.Black, this));
        PutNewPiece('g', 7, new Pawn(Board, Color.Black, this));
        PutNewPiece('h', 7, new Pawn(Board, Color.Black, this));
    }
}