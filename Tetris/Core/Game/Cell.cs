namespace Tetris.Core.Game;

public class Cell
{
    public Vector Position { get; private set; }
    public CellType Type { get; private set; }

    public Cell(Vector position, CellType type)
    {
        Position = position;
        Type = type;
    }
    
    public void ChangeType(CellType type)
    {
        Type = type;
    }

    public override string ToString()
    {
        switch (Type)
        {   
            case CellType.Void:
                return ".";
            case CellType.Border:
                return "#";
            case CellType.Figure:
                return "\u25a0";
            default:
                return "";
        }
    }
}