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

    public abstract bool[,] PossibleMoviments();
}