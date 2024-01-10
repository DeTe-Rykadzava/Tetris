namespace Tetris.Core.Game.Figures;

public class Figure
{
    public Vector[] CellsPosition { get; private set; }

    private readonly List<Vector[]>? _figureRotations;

    private int _figureRotationIndex = 0;
    
    public Figure(Vector[] cellsPosition, List<Vector[]>? figureRotations = null)
    {
        CellsPosition = cellsPosition;
        _figureRotations = figureRotations;
    }

    public void Rotate()
    {
        if(_figureRotations == null|| _figureRotations.Count == 0)
            return;
        _figureRotationIndex++;
        if (_figureRotationIndex > _figureRotations.Count - 1)
            _figureRotationIndex = 0;
        CellsPosition = _figureRotations[_figureRotationIndex];
    }

    public void RotateBack()
    {
        if(_figureRotations == null|| _figureRotations.Count == 0)
            return;
        _figureRotationIndex--;
        if (_figureRotationIndex < 0)
            _figureRotationIndex = _figureRotations.Count - 1;
         CellsPosition = _figureRotations[_figureRotationIndex];
    }

    public IEnumerable<Vector> GetDownPoints()
    {
        var minX = CellsPosition.Min(x => x.X);
        var maxX = CellsPosition.Max(x => x.X);
        for (var xi = minX; xi <= maxX; xi++)
        {
            yield return new Vector(xi,CellsPosition.Where(x => x.X == xi).Max(x => x.Y));
        }
    }

    public IEnumerable<Vector> GetUpPoints()
    {
        var minX = CellsPosition.Min(x => x.X);
        var maxX = CellsPosition.Max(x => x.X);
        for (var xi = minX; xi <= maxX; xi++)
        {
            yield return new Vector(xi,CellsPosition.Where(x => x.X == xi).Min(x => x.Y));
        }
    }
    
    public IEnumerable<Vector> GetRightPoints()
    {
        var minY = CellsPosition.Min(x => x.Y);
        var maxY = CellsPosition.Max(x => x.Y);
        for (var yi = minY; yi <= maxY; yi++)
        {
            yield return new Vector(CellsPosition.Where(x => x.Y == yi).Max(x => x.X), yi);
        }
    }
    
    public IEnumerable<Vector> GetLeftPoints()
    {
        var minY = CellsPosition.Min(x => x.Y);
        var maxY = CellsPosition.Max(x => x.Y);
        for (var yi = minY; yi <= maxY; yi++)
        {
            yield return new Vector(CellsPosition.Where(x => x.Y == yi).Min(x => x.X), yi);
        }
    }
    
}