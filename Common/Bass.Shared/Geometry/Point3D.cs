namespace Bass.Shared.Geometry;

public record Point3d(int X, int Y, int Z)
{
   public static readonly Point3d Empty = new(default, default, default);  
}
