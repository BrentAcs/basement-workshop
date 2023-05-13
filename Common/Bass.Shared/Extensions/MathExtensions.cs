﻿namespace Bass.Shared.Extensions;

public static class MathExtensions
{
   public static double ToRadians(this double degrees)
      => (Math.PI / 180) * degrees;
}
