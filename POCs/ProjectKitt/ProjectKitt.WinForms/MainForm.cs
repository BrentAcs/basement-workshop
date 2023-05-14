using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using Bass.Shared.Extensions;

namespace ProjectKitt.WinForms;

public partial class MainForm : Form
{
   private PointF _location = PointF.Empty;
   private float _heading = 0f;

   public MainForm()
   {
      InitializeComponent();
   }

   private void MainForm_SizeChanged(object sender, EventArgs e) => Invalidate();

   private void MainForm_Paint(object sender, PaintEventArgs e)
   {
      var painter = new ViewPortPainter(ClientRectangle);
      painter.TestPaint(e.Graphics, _location, _heading);
   }

   static PointF ComputeNew(PointF starting, float heading, float distance)
   {
      var radians = ((double)heading).ToRadians();
      return new PointF((float)(starting.X + distance * Math.Sin(radians)), (float)(starting.Y + distance * Math.Cos(radians)));
   }

   private void button1_Click(object sender, EventArgs e)
   {
      _location = ComputeNew(_location, _heading, 25);
      _heading += 15;
      Invalidate();
   }
}

public class ViewPortPainter
{
   public ViewPortPainter(RectangleF viewRect)
   {
      ViewRect = viewRect;
   }

   public ViewPortPainterOptions Options { get; set; } = new();
   public RectangleF ViewRect { get; set; }
   public PointF ViewCenter => GetViewCenter();

   private void DrawAxis(Graphics g)
   {
      if (!Options.Axis.Show)
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
      //g.DrawEllipse(pen, display.X - 2, display.Y - 2, 4, 4);

      //var rect = new RectangleF(
      //   display.X - 4,
      //   display.Y - 12,
      //   8,
      //   24);
      //g.DrawRectangle(pen, rect);


      //var radians = ((double)heading).ToRadians();
      //return new PointF((float)(starting.X + distance * Math.Sin(radians)), (float)(starting.Y + distance * Math.Cos(radians)));

      var points = new List<PointF>
      {
         new(display.X - 4, display.Y - 12),
         new(display.X + 4, display.Y - 12),
         new(display.X + 4, display.Y + 12),
         new(display.X - 4, display.Y + 12),
      };
      var rotated = RotatePolygon(points, display, heading);
      ClosePolygon(rotated);
      g.DrawPolygon(pen, rotated.ToArray());
   }

   private static IList<PointF> RotatePolygon(ICollection<PointF> polygon, PointF center, float angle)
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

   private static void ClosePolygon(ICollection<PointF> polygon)
   {
      var first = polygon.First();
      var last = polygon.Last();
      if( first != last)
         polygon.Add(first);
   }
   
   private PointF GetViewCenter()
   {
      return new PointF(ViewRect.Width / 2, ViewRect.Height / 2);
   }
}

public class ViewPortPainterOptions
{
   public AxisOptions Axis { get; set; } = new();

   public class AxisOptions
   {
      public bool Show { get; set; } = true;
      public Color Color { get; set; } = Color.Black;
      public int Width { get; set; } = 2;
   }
}
