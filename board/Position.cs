namespace board;

class Position 
{
    public int Line { get; set; }
    public int Column { get; set; }

    public Position(int line, int column)
    {
        SetValues(line, column);
    }

    public void SetValues(int line, int column)
    {
        Line = line;
        Column = column;
    }

    public override string ToString()
    {
        return $"{Line},{Column}";
    }
}