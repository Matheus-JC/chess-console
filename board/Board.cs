namespace board;

class Board
{
    public int Lines { get; set; }
    public int Columns { get; set; }
    private Piece[,] _pieces;

    public Board(int lines, int columns)
    {
        Lines = lines;
        Columns = columns;
        _pieces = new Piece[lines, columns];
    }

    public Piece GetPiece(int line, int column)
    {
        return _pieces[line, column];
    }

    public Piece GetPiece(Position pos)
    {
        return _pieces[pos.Line, pos.Column];
    }

    public bool PieceExist(Position pos)
    {
        CheckValidPosition(pos);
        return GetPiece(pos) != null;
    }

    public void PutPiece(Piece piece, Position pos)
    {
        if(PieceExist(pos))
        {
            throw new BoardException("There is already a piece in that position!");
        }

        _pieces[pos.Line, pos.Column] = piece;
        piece.Position = pos;
    }

    public bool ValidPosition(Position pos)
    {
        if(
            pos.Line < 0
            || pos.Line >= Lines
            || pos.Column < 0
            || pos.Column >= Columns
        )
        {
            return false;
        }

        return true;
    }

    public void CheckValidPosition(Position pos)
    {
        if(!ValidPosition(pos))
        {
            throw new BoardException("Invalid Position!");
        }
    }
}