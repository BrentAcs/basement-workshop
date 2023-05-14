namespace ProjectKitt.WinForms.Services;

public class ViewPortRenderer
{
   public ViewPortRenderer(RectangleF viewRect)
   {
      ViewRect = viewRect;
   }

   public ViewPortRendererOptions Options { get; set; } = new();
   public RectangleF ViewRect { get; set; }
   public PointF ViewCenter => GetViewCenter();

   private void DrawAxis(Graphics g)
   {
      if (!Options.Axis.Visible)
         return;

      using var pen = new Pen(Options.Axis.Color, Options.Axis.Width);
      g.DrawLine(pen, ViewRect.Width / 2, 0, ViewRect.Width / 2, ViewRect.Height);
      g.DrawLine(pen, 0, ViewRect.Height / 2, ViewRect.Width, ViewRect.Height / 2);
   }

   public void TestPaint(Graphics g, PointF point, float heading)
   {
      DrawAxis(g);

      var display = new PointF(ViewCenter.X + point.X, ViewCenter.Y - point.Y);
      using var pen = new Pen(Color.Red);

      var points = new List<PointF>
      {
         new(display.X - 4, display.Y - 12),
         new(display.X + 4, display.Y - 12),
         new(display.X + 4, display.Y + 12),
         new(display.X - 4, display.Y + 12),
      };
      var rotated = points.ToRotatePolygon(display, heading);
      rotated.ClosePolygon();
      g.DrawPolygon(pen, rotated.ToArray());
   }

   private PointF GetViewCenter()
   {
      return new PointF(ViewRect.Width / 2, ViewRect.Height / 2);
   }
}

public interface IRenderedObject
{
   IEnumerable<PointF> Points { get; }
}

public class TestRenderedObject : IRenderedObject
{
   private static IList<PointF> _points = new List<PointF>
   {
      new(-4, -12),
      new(4, -12),
      new(4, 12),
      new(-4, 12),
   };

   public IEnumerable<PointF> Points => _points;
}
