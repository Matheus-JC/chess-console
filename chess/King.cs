using board;

namespace chess;

class King : Piece
{
    public King(Board board, Color color) : base(board, color)
    {}

    public override string ToString()
    {
        return "K";
    }

    private bool CanMove(Position pos)
    {
        Piece? piece = Board.GetPiece(pos);
        return piece == null || piece.Color != Color;
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
        }

        return arr;
    }
}