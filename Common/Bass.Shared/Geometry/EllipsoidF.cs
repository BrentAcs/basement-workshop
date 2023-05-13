namespace Bass.Shared.Geometry;

public record EllipsoidF(double X, double Y, double Z)
{
   public static readonly EllipsoidF Empty = new(default, default, default);  
}
