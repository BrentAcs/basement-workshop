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

   
   
   public static bool IsPointInPolygon(this PointF point, IList<PointF> polygon)
   {
      // Ref:  https://stackoverflow.com/questions/924171/geo-fencing-point-inside-outside-polygon
      int i, j;
      bool c = false;
      for ( i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
      {
         if ((((polygon[ i ].X <= point.X) && (point.X < polygon[ j ].X))
              || ((polygon[ j ].X <= point.X) && (point.X < polygon[ i ].X)))
             && (point.Y < (polygon[ j ].Y - polygon[ i ].Y) * (point.X - polygon[ i ].X)
                / (polygon[ j ].X - polygon[ i ].X) + polygon[ i ].Y))
         {
            c = !c;
         }
      }

      return c;
   }
}
