using Bass.Shared.Extensions;

namespace ProjectKitt.WinForms.Extensions;

public static class RectangleFExtensions
{
   public static RectangleF CenterOn(this RectangleF rect, PointF point) =>
      rect with { X = point.X - rect.X - rect.Width / 2, Y = point.Y - rect.Y - rect.Height / 2 };

   public static RectangleF Deflate(this RectangleF rect, float x, float y) => new(rect.X + x / 2, rect.Y + y / 2, rect.Width - x, rect.Height - y);

   public static IList<PointF> ToPointFs(this RectangleF rect) =>
      new List<PointF>
      {
         new(rect.X, rect.Y),
         new(rect.X+rect.Width, rect.Y),
         new(rect.X+rect.Width, rect.Y+rect.Height),
         new(rect.X, rect.Y+rect.Height),
      };
}

public static class PointFExtensions
{
   public static PointF ComputeRayEndPoint(this PointF origin, float angle, float length) =>
      new((float)(origin.X + length * Math.Sin(((double)angle).ToRadians())),
         (float)(origin.Y + length * -Math.Cos(((double)angle).ToRadians())));
}
