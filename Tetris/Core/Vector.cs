namespace Tetris.Core;

public struct Vector 
{ 
    public decimal X { get; set; }
    public decimal Y { get; set; }

    public Vector() { }

    public static Vector Right = new Vector(1, 0);
    public static Vector Up = new Vector(0, 1);
    public static Vector Left = new Vector(-1, 0);
    public static Vector Down = new Vector(0, -1);
    
    public Vector(decimal x, decimal y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Vector other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is Vector other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
    
    public static bool operator ==(Vector a, Vector b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(Vector a, Vector b)
    {
        return a.X != b.X && a.Y != b.Y;
    }

    public static Vector operator +(Vector a, Vector b)
    {
        return new Vector(a.X + b.X, a.Y + b.Y);
    }

    public static Vector operator -(Vector a, Vector b)
    {
        return new Vector(a.X - b.X, a.Y - b.Y);
    }

    public static Vector operator /(Vector a, Vector b)
    {
        return new Vector(a.X / b.X, a.Y / b.Y);
    }

    public static Vector operator /(Vector a, decimal scalar)
    {
        return new Vector(a.X / scalar, a.Y / scalar);
    }

    public static Vector operator *(Vector a, Vector b)
    {
        return new Vector(a.X * b.X, a.Y * b.Y);
    }

    public static Vector operator *(Vector a, int scalar)
    {
        return new Vector(a.X * scalar, a.Y * scalar);
    }

    public static Vector Normalize(Vector vector)
    {
        var normalizedVector = vector / GetVectorLenght(vector);
        return normalizedVector;
    }

    public static decimal GetVectorLenght(Vector vector)
    {
        var lenght = Math.Sqrt(Math.Pow((double)vector.X, 2) + Math.Pow((double)vector.Y, 2));
        return (decimal)lenght;
    }
}