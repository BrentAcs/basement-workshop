﻿namespace ProjectKitt.Core.Extensions;

public static class ScaleExtensions
{
   public static float ScaleBy(this float v, float factor) => v * factor;

   public static SizeF ScaleBy(this SizeF v, float factor) => new(ScaleBy(v.Width, factor), ScaleBy(v.Height, factor));

   public static float InverseScaleBy(this float v, float factor) => v / factor;

   public static SizeF InverseScaleBy(this SizeF v, float factor) => new(InverseScaleBy(v.Width, factor), InverseScaleBy(v.Height, factor));
}
