using Bass.Shared.Extensions;

namespace ProjectKitt.WinForms.Extensions;

public static class PointFExtensions
{
   
   public static PointF ComputeRayEndPoint(this PointF origin, float angle, float length) =>
      new((float)(origin.X + length * Math.Sin(((double)angle).ToRadians())),
         (float)(origin.Y + length * -Math.Cos(((double)angle).ToRadians())));

   public static float DistanceTo(this PointF lhs, PointF rhs)
   {
      var a = lhs.X - rhs.X;
      var b = lhs.Y - rhs.Y;

      return (float)Math.Sqrt((a * a) + (b * b));
   }

   public static bool IsInsideCircle(this PointF point, float radius) =>
      (point.X - radius) * (point.X - radius) +
      (point.Y - radius) * (point.Y - radius) <= radius * radius;

   public static bool IsInsideCircle(this IEnumerable<PointF> points, float radius) =>
      points.Any(point => point.IsInsideCircle(radius));

   // public static bool InsideCircle(this PointF point, float radius, PointF? centeredAt = null)
   // {
   //    centeredAt ??= new PointF();
   //
   //    // Ref:  https://www.geeksforgeeks.org/check-if-a-point-is-inside-outside-or-on-the-ellipse/
   //    double position = (Math.Pow((point.X - centeredAt.Value.X), 2) / Math.Pow(radius, 2)) +
   //                      (Math.Pow((point.Y - centeredAt.Value.Y), 2) / Math.Pow(radius, 2));
   //
   //    return !(position > 1.0);
   // }   
   
   public static PointF Offset(this PointF point, PointF offset) =>
       new PointF(point.X - offset.X, point.Y - offset.Y);

   public static IEnumerable<PointF> Offset(this IEnumerable<PointF> polygon, PointF offset) =>
      polygon.Select(point => point.Offset(offset)).ToList();

   // --- Polygon related
   
   public static IEnumerable<PointF> ToRotatedPolygon(this IEnumerable<PointF> polygon, PointF center, float angle)
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
   
   public static IEnumerable<PointF> ClosePolygon(this IEnumerable<PointF> polygon)
   {
      var points = polygon.ToList(); 
      var first = points.First();
      var last = points.Last();
      if( first != last)
         points.Add(first);

      return points;
   }
   
   public static IEnumerable<PointF> ComputePointsAtRadius(this PointF location, float radius, float start=30f, float step=60f)
   {
      var points = new List<PointF>();
      for(float angle = start; angle <= 360; angle +=step)
      {
         points.Add(location.ComputeRayEndPoint(angle, radius));
      }

      return points;
   }
   
   
   
   // public static bool IsPointInPolygon(this PointF point, IList<PointF> polygon)
   // {
   //    // Ref:  https://stackoverflow.com/questions/924171/geo-fencing-point-inside-outside-polygon
   //    int i, j;
   //    bool c = false;
   //    for ( i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
   //    {
   //       if ((((polygon[ i ].X <= point.X) && (point.X < polygon[ j ].X))
   //            || ((polygon[ j ].X <= point.X) && (point.X < polygon[ i ].X)))
   //           && (point.Y < (polygon[ j ].Y - polygon[ i ].Y) * (point.X - polygon[ i ].X)
   //              / (polygon[ j ].X - polygon[ i ].X) + polygon[ i ].Y))
   //       {
   //          c = !c;
   //       }
   //    }
   //
   //    return c;
   // }
}
