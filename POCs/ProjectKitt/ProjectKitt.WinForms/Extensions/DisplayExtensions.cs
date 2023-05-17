namespace ProjectKitt.WinForms.Extensions;

public static class DisplayExtensions
{
   public static string ToMetricDistance(this float value)
   {
      value = (float)Math.Round(value, 0);
      return value > 9999f ? $"{value / 1000} km" : $"{value} m";
   }
}
