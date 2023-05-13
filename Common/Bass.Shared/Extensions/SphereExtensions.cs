using Bass.Shared.Geometry;

namespace Bass.Shared.Extensions;

public static class SphereExtensions
{
   public static long CalcSurfaceArea(this Sphere sphere) =>
      (long) (4 * Math.PI * (sphere.X * sphere.X));
}
