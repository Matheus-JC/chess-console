using board;

namespace chess;

class Tower : Piece
{
    public Tower(Board board, Color color) : base(board, color)
    {}

    public override string ToString()
    {
        return "T";
    }

    private bool CanMove(Position pos)
    {
        Piece? piece = Board.GetPiece(pos);
        return piece == null || piece.Color != Color;
    }

    public override bool[,] PossibleMoviments()
    {
        bool[,] arr = new bool[Board.Lines, Board.Columns];

        Position pos = new Position(0, 0);

        if(Position != null){
            // north
            pos.SetValues(Position.Line - 1, Position.Column);
            while(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
                {
                    break;
                }
                pos.Line--;
            }

            // south
            pos.SetValues(Position.Line + 1, Position.Column);
            while(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
                {
                    break;
                }
                pos.Line++;
            }

            // east
            pos.SetValues(Position.Line, Position.Column + 1);
            while(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
                {
                    break;
                }
                pos.Column++;
            }

            // west
            pos.SetValues(Position.Line, Position.Column - 1);
            while(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
                {
                    break;
                }
                pos.Column--;
            }
        }

        return arr;
    }
}