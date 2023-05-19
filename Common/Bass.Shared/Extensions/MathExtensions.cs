using System.Drawing;

namespace Bass.Shared.Extensions;

public static class MathExtensions
{
   public static double ToRadians(this double degrees)
      // => (Math.PI / 180) * degrees;
      => Math.PI / 180.0 * (degrees % 360);
}
