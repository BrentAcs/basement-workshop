namespace ProjectKitt.WinForms.Services;

public class ViewPortRenderer
{
   public ViewPortRenderer(RectangleF viewRect)
   {
      ViewRect = viewRect;
   }

   public ScaleFactor ScaleFactor { get; set; } = ScaleFactor._1To1;
   public ViewPortRendererOptions Options { get; set; } = new();
   public RectangleF ViewRect { get; set; }
   public PointF ViewCenter => GetViewCenter();

   private PointF GetViewCenter()
   {
      return new PointF(ViewRect.Width / 2, ViewRect.Height / 2);
   }

   private void DrawAxis(Graphics g)
   {
      if (!Options.Axis.Visible)
         return;

      using var pen = new Pen(Options.Axis.Color, Options.Axis.Width);
      g.DrawLine(pen, ViewRect.Width / 2, 0, ViewRect.Width / 2, ViewRect.Height);
      g.DrawLine(pen, 0, ViewRect.Height / 2, ViewRect.Width, ViewRect.Height / 2);
   }

   public void Render(Graphics g, IRenderedObject renderedObject, PointF point, float heading)
   {
      DrawAxis(g);

      var display = new PointF(ViewCenter.X + point.X, ViewCenter.Y - point.Y);
      using var pen = new Pen(Color.Red);

      foreach (var objPoints in renderedObject.ComplexPoints)
      {
         var points = ComputeObjectsPoints(objPoints, display);
         var rotated = points.ToRotatePolygon(display, heading);
         rotated.ClosePolygon();
         g.DrawPolygon(pen, rotated.ToArray());
      }
   }

   private IEnumerable<PointF> ComputeObjectsPoints(IRenderedObject renderedObject, PointF center) =>
      ComputeObjectsPoints(renderedObject.Points, center);

   private IEnumerable<PointF> ComputeObjectsPoints(IEnumerable<PointF> objectPoints, PointF center)
   {
      var scaleFactor = ScaleFactor.GetScaleFactorValue();
      var result = new List<PointF>();
      foreach (var point in objectPoints)
      {
         result.Add(new PointF(center.X + point.X * scaleFactor, center.Y + point.Y * scaleFactor));
      }

      return result;
   }
}
