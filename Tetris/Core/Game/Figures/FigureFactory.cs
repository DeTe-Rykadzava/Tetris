namespace Tetris.Core.Game.Figures;

public static class FigureFactory
{
    private static Figure[] _figures = new[] 
    { 
        new Figure(
            new [] { new Vector(0, 0), new Vector(0, 1), new Vector(0, -1), new Vector(-1, 1) },
            new List<Vector[]>
            {
                new [] { new Vector(0, 0), new Vector( 0, 1), new Vector( 0, -1), new Vector(-1,  1) },
                new [] { new Vector(0, 0), new Vector(-1, 0), new Vector(-1, -1), new Vector( 1,  0) },
                new [] { new Vector(0, 0), new Vector( 0, 1), new Vector( 0, -1), new Vector( 1, -1) },
                new [] { new Vector(0, 0), new Vector(-1, 0), new Vector( 1,  0), new Vector( 1,  1) }
            }), // J
        new Figure(new [] { new Vector(0, 0), new Vector(0, 1), new Vector(-1, 0), new Vector(-1, 1) }), // O
        new Figure(
            new[] { new Vector(0, 0), new Vector(0, -1), new Vector(0, 1), new Vector(1, 1) },
            new List<Vector[]>
            {
                new [] { new Vector(0, 0), new Vector( 0, -1), new Vector( 0,  1), new Vector( 1,  1) },
                new [] { new Vector(0, 0), new Vector(-1,  0), new Vector( 1,  0), new Vector(-1,  1) },
                new [] { new Vector(0, 0), new Vector( 0, -1), new Vector( 0,  1), new Vector(-1, -1) },
                new [] { new Vector(0, 0), new Vector(-1,  0), new Vector( 1,  0), new Vector( 1, -1) }
            }), // L
        new Figure(
            new[] { new Vector(0, 0), new Vector(-1, 0), new Vector(0, 1), new Vector(1, 1) },
            new List<Vector[]>
            {
                new [] { new Vector(0, 0), new Vector(-1,  0), new Vector( 0, 1), new Vector( 1, 1) },
                new [] { new Vector(0, 0), new Vector( 0, -1), new Vector(-1, 0), new Vector(-1, 1) },
            }), // Z
        new Figure(
            new [] { new Vector(0, 0), new Vector(0, -1), new Vector(-1, 0), new Vector(1, 0) },
            new List<Vector[]>
            {
                new [] { new Vector(0, 0), new Vector( 0, -1), new Vector(-1,  0), new Vector(1,  0) },
                new [] { new Vector(0, 0), new Vector( 0, -1), new Vector( 1,  0), new Vector(0,  1) },
                new [] { new Vector(0, 0), new Vector(-1,  0), new Vector( 0,  1), new Vector(1,  0) },
                new [] { new Vector(0, 0), new Vector(-1,  0), new Vector( 0,  1), new Vector(0, -1) }
            }), // T
        new Figure(
            new[] { new Vector(0, 0), new Vector(1, 0), new Vector(0, 1), new Vector(-1, 1) },
            new List<Vector[]>
            {
                new [] { new Vector(0, 0), new Vector( 1, 0), new Vector( 0,  1), new Vector(-1, 1) },
                new [] { new Vector(0, 0), new Vector( 1, 0), new Vector( 0, -1), new Vector( 1, 1) },
            }), // S
        new Figure(
            new[] { new Vector(0, 0), new Vector(0, -1), new Vector(0, 1), new Vector(0, -2) },
            new List<Vector[]>
            {
                new [] { new Vector(0, 0), new Vector(0, -1), new Vector(0, 1), new Vector(0, -2) },
                new [] { new Vector(0, 0), new Vector( 1, 0), new Vector( -1, 0), new Vector( 2, 0) },
            }) // I
    };

    public static Figure GetRandomFigure()
    {
        var random = new Random();
        var figureIndex = random.Next(0, _figures.Length);
        return _figures[figureIndex];
    }

    public static Figure CreateJ()
    {
        return _figures[0];
    }
    
    public static Figure CreateO()
    {
        return _figures[1];
    }
    
    public static Figure CreateL()
    {
        return _figures[2];
    }

    public static Figure CreateZ()
    {
        return _figures[3];
    }
    
    public static Figure CreateT()
    {
        return _figures[4];
    }
    
    public static Figure CreateS()
    {
        return _figures[5];
    }
    
    public static Figure CreateI()
    {
        return _figures[6];
    }
}