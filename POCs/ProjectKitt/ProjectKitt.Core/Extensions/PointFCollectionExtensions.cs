using Bass.Shared.Extensions;
using ProjectKitt.WinForms.Extensions;

namespace ProjectKitt.Core.Extensions;

public static class PointFCollectionExtensions
{
   public static IList<PointF> ToRotatePolygon(this IEnumerable<PointF> polygon, PointF center, float angle)
   {
      var radians = ((double)angle).ToRadians();
      var rotated = new List<PointF>();
      foreach (var point in polygon)
      {
         var adjusted = new PointF(
            (float) (Math.Cos(radians) * (point.X - center.X) - Math.Sin(radians) * (point.Y-center.Y) + center.X),      
            (float) (Math.Sin(radians) * (point.X - center.X) + Math.Cos(radians) * (point.Y-center.Y) + center.Y)      
         );
         rotated.Add(adjusted);
      }
      
      return rotated;
   }
   
   public static void ClosePolygon(this IList<PointF> polygon)
   {
      var first = polygon.First();
      var last = polygon.Last();
      if( first != last)
         polygon.Add(first);
   }

   public static IEnumerable<PointF> Offset(this IEnumerable<PointF> polygon, PointF offset) =>
      polygon.Select(point => point.Offset(offset)).ToList();
}
