namespace Bass.Shared.Extensions;

public static class MathExtensions
{
   public static double ToRadians(this double degrees)
      => (Math.PI / 180) * degrees;
   
   // public static float ToRadians(this float degrees)
   //    => (float) ((Math.PI / 180) * degrees);
}
