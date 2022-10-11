namespace board;

abstract class Piece 
{
    public Position? Position { get; set; }
    public Color Color { get; protected set; }
    public int MovesNumber { get; protected set; }
    public Board Board { get; set; }

    public Piece(Board board, Color color)
    {
        Board = board;
        Color = color;
        Position = null;
        MovesNumber = 0;
    }

    public void IncreaseMovimentsNumber()
    {
        MovesNumber++;
    }

    public void DecreaseMovimentsNumber()
    {
        MovesNumber--;
    }

    public bool ExistPossibleMoves()
    {
        bool[,] possibleMoves = PossibleMoves();
        for(int i = 0; i < Board.Lines; i++)
        {
            for(int j = 0; j < Board.Columns; j++)
            {
                if(possibleMoves[i, j])
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool CanMoveTo(Position pos)
    {
        return PossibleMoves()[pos.Line, pos.Column];
    }

    public abstract bool[,] PossibleMoves();
}