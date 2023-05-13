namespace Bass.Shared.Geometry;

public record Sphere : Ellipsoid
{
   public Sphere(int radius)
      : base(radius, radius, radius)
   {
      X = Y = Z = radius;
   }
}
