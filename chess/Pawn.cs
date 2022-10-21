using board;

namespace chess;

class Pawn : Piece
{
    public Pawn(Board board, Color color) : base(board, color)
    {}

    public override string ToString()
    {
        return "P";
    }

    private bool HasEnemy(Position pos)
    {
        Piece? piece = Board.GetPiece(pos);
        return piece == null || piece.Color != Color;
    }

    private bool IsFree(Position pos)
    {
        return Board.GetPiece(pos) == null;
    }

    public override bool[,] GetPossibleMoves()
    {
        bool[,] arr = new bool[Board.Lines, Board.Columns];

        Position pos = new Position(0, 0);

        if(Position != null)
        {
            if(Color == Color.White)
            {
                pos.SetValues(Position.Line - 1, Position.Column);
                if(Board.ValidPosition(pos) && IsFree(pos))
                {
                    arr[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line - 2, Position.Column);
                if(Board.ValidPosition(pos) && IsFree(pos) && MovesNumber == 0)
                {
                    arr[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line - 1, Position.Column - 1);
                if(Board.ValidPosition(pos) && HasEnemy(pos))
                {
                    arr[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line - 1, Position.Column + 1);
                if(Board.ValidPosition(pos) && HasEnemy(pos))
                {
                    arr[pos.Line, pos.Column] = true;
                }
            }
            else
            {
                pos.SetValues(Position.Line + 1, Position.Column);
                if(Board.ValidPosition(pos) && IsFree(pos))
                {
                    arr[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line + 2, Position.Column);
                if(Board.ValidPosition(pos) && IsFree(pos) && MovesNumber == 0)
                {
                    arr[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line + 1, Position.Column - 1);
                if(Board.ValidPosition(pos) && HasEnemy(pos))
                {
                    arr[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line + 1, Position.Column + 1);
                if(Board.ValidPosition(pos) && HasEnemy(pos))
                {
                    arr[pos.Line, pos.Column] = true;
                }
            }
        }

        return arr;
    }
}