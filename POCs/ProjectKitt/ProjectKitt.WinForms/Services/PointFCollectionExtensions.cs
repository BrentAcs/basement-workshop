using Bass.Shared.Extensions;

namespace ProjectKitt.WinForms.Services;

public static class PointFCollectionExtensions
{
   public static IList<PointF> ToRotatePolygon(this ICollection<PointF> polygon, PointF center, float angle)
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
}
