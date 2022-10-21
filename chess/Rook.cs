using board;

namespace chess;

class Rook : Piece
{
    public Rook(Board board, Color color) : base(board, color)
    {}

    public override string ToString()
    {
        return "R";
    }

    public override bool[,] GetPossibleMoves()
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