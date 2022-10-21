using board;

namespace chess;

class Queen : Piece
{
    public Queen(Board board, Color color) : base(board, color)
    {}

    public override string ToString()
    {
        return "Q";
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

            // northeast
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            while(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line - 1, pos.Column - 1);
            }

            // northwest
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            while(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line - 1, pos.Column + 1);
            }

            // southeast
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            while(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line + 1, pos.Column + 1);
            }

            // south-west
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            while(Board.ValidPosition(pos) && CanMove(pos))
            {
                arr[pos.Line, pos.Column] = true;
                if(Board.GetPiece(pos) != null && Board.GetPiece(pos)?.Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line + 1, pos.Column - 1);
            }
        }
        
        return arr;
    }
}