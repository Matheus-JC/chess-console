using board;

namespace chess;

class Pawn : Piece
{
    private const int LineEnPassantWhite =  3;
    private const int LineEnPassantBlack =  4;
    private ChessGame ChessGame;

    public Pawn(Board board, Color color, ChessGame chessGame) : base(board, color)
    {
        ChessGame = chessGame;
    }

    public override string ToString()
    {
        return "P";
    }

    private bool HasEnemy(Position pos)
    {
        Piece? piece = Board.GetPiece(pos);
        return piece != null && piece?.Color != Color;
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

                arr = CheckEnPassant(ref arr);
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

                CheckEnPassant(ref arr);
            }
        }

        return arr;
    }

    // #SpecialMove En Passant
    private bool[,] CheckEnPassant(ref bool[,] arr)
    {
        if(Position != null && IsValidLineEnPassant(Position.Line))
        {
            Position posLeft = new Position(Position.Line, Position.Column - 1);
            if(IsPossibleEnPassant(posLeft))
            {
                int capturePositionLine = Color == Color.White ? posLeft.Line - 1 : posLeft.Line + 1;

                arr[capturePositionLine, posLeft.Column] = true;
            }

            Position posRight = new Position(Position.Line, Position.Column + 1);
            if(IsPossibleEnPassant(posRight))
            {
                int capturePositionLine = Color == Color.White ? posRight.Line - 1 : posRight.Line + 1;

                arr[capturePositionLine, posRight.Column] = true;
            }
        }

        return arr;
    }

    private bool IsValidLineEnPassant(int line)
    {
        if(
            (line == LineEnPassantWhite && Color == Color.White)
            || (line == LineEnPassantBlack && Color == Color.Black)
        )
        {
            return true;
        }

        return false;
    }

    private bool IsPossibleEnPassant(Position pos)
    {
        if(
            Board.ValidPosition(pos) 
            && HasEnemy(pos) 
            && Board.GetPiece(pos) == ChessGame.VulnerableEnPassant
        )
        {
            return true;
        }

        return false;
    }
}