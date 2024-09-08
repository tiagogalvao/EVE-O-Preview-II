namespace EveOPreview.Core.Model;

public class Size : IEquatable<Size>
{
    public int Height { get; set; }
    public int Width { get; set; }

    public Size(int width, int height) 
    {
        Height = height;
        Width = width;
    }

    public bool Equals(Size? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Height == other.Height && Width == other.Width;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Size)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Height, Width);
    }
}