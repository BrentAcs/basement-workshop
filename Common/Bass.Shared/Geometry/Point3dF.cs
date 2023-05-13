namespace Bass.Shared.Geometry;

public record Point3dF(double X, double Y, double Z)
{
   public static readonly Point3dF Empty = new(default, default, default);  
}
