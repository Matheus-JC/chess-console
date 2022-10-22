using board;

namespace chess;

class King : Piece
{
    private ChessGame Game;

    public King(Board board, Color color, ChessGame game) : base(board, color)
    {
        Game = game;
    }

    public override string ToString()
    {
        return "K";
    }

    private bool CanRookCastle(Position pos)
    {
        Piece? piece = Board.GetPiece(pos);
        return ( 
            piece != null 
            && piece is Rook 
            && piece.Color == Color 
            && piece.MovesNumber == 0
        );
    }

    public override bool[,] GetPossibleMoves()
    {
        bool[,] arr = new bool[Board.Lines, Board.Columns];

        Position pos = new Position(0, 0);

        if(Position != null){
            // north
            pos.SetValues(Position.Line - 1, Position.Column);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            // northeast
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            // east
            pos.SetValues(Position.Line, Position.Column + 1);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            // southeast
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            // south
            pos.SetValues(Position.Line + 1, Position.Column);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            // south-west
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            // west
            pos.SetValues(Position.Line, Position.Column - 1);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            // northwest
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            // #SpecialMove Castling kingside (short)
            if(MovesNumber == 0 && !Game.Check)
            {
                Position posRookShortCastle = new Position(Position.Line, Position.Column + 3);
                
                if(CanRookCastle(posRookShortCastle))
                {
                    bool posOneSquareRightOfKingIsFree = Board.GetPiece(new Position(Position.Line, Position.Column + 1)) == null;
                    bool posTwoSquareRightOfKingIsFree = Board.GetPiece(new Position(Position.Line, Position.Column + 2)) == null;
                    
                    if(posOneSquareRightOfKingIsFree && posTwoSquareRightOfKingIsFree)
                    {
                        arr[Position.Line, Position.Column + 2] = true;
                    }
                }
            }

            // #SpecialMove Castling Queenside (long)
            if(MovesNumber == 0 && !Game.Check)
            {
                Position posRookLongCastle = new Position(Position.Line, Position.Column - 4);
                
                if(CanRookCastle(posRookLongCastle))
                {
                    bool posOneSquareLeftOfKingIsFree = Board.GetPiece(new Position(Position.Line, Position.Column - 1)) == null;
                    bool posTwoSquareLeftOfKingIsFree = Board.GetPiece(new Position(Position.Line, Position.Column - 2)) == null;
                    bool posThreeSquareLeftOfKingIsFree = Board.GetPiece(new Position(Position.Line, Position.Column - 3)) == null;

                    if(
                        posOneSquareLeftOfKingIsFree 
                        && posTwoSquareLeftOfKingIsFree 
                        && posThreeSquareLeftOfKingIsFree
                    )
                    {
                        arr[Position.Line, Position.Column - 2] = true;
                    }
                }
            }
        }

        return arr;
    }
}