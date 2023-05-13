namespace Bass.Shared.Geometry;

public record Point3dF(double X, double Y, double Z)
{
   public static readonly Point3dF Empty = new(default, default, default);
   
   public double DistanceTo(Point3dF that)
   {
      var a = X - that.X;
      var b = Y - that.Y;
      var c = Z - that.Z;

      return Math.Sqrt((a * a) + (b * b) + (c * c));
   }
}
