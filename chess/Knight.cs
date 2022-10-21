using board;

namespace chess;

class Knight : Piece
{
    public Knight(Board board, Color color) : base(board, color)
    {}

    public override string ToString()
    {
        return "N";
    }

    public override bool[,] GetPossibleMoves()
    {
        bool[,] arr = new bool[Board.Lines, Board.Columns];

        Position pos = new Position(0, 0);

        if(Position != null){
            pos.SetValues(Position.Line - 1, Position.Column - 2);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line - 2, Position.Column - 1);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line - 2, Position.Column + 1);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line - 1, Position.Column + 2);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 1, Position.Column + 2);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 2, Position.Column + 1);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 2, Position.Column - 1);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 1, Position.Column - 2);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
            }
        }

        return arr;
    }
}