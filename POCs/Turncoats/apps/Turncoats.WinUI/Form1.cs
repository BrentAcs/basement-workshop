using System.Reflection;
using Turncoats.Game;

namespace Turncoats.WinUI;

public partial class Form1 : Form
{
   private readonly MapRenderer _renderer = new();

   private readonly ITheGame _game = new TheGame();

   public Form1()
   {
      InitializeComponent();
   }

   protected override void OnLoad(EventArgs e)
   {
      base.OnLoad(e);
      typeof(Form).InvokeMember("DoubleBuffered",
         BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this,
         new object[] { true });
      typeof(Form).InvokeMember("SetStyle", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
         null, this, new object[] { ControlStyles.AllPaintingInWmPaint, true });
      typeof(Form).InvokeMember("SetStyle", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
         null, this, new object[] { ControlStyles.ResizeRedraw, true });
      typeof(Form).InvokeMember("SetStyle", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
         null, this, new object[] { ControlStyles.UserPaint, true });
      typeof(Form).InvokeMember("SetStyle", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
         null, this, new object[] { ControlStyles.OptimizedDoubleBuffer, true });
      typeof(Form).InvokeMember("UpdateStyles",
         BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic, null, this, new object[] { });
   }

   protected override void OnPaint(PaintEventArgs e)
   {
      base.OnPaint(e);
      if (!_game.Map.Zones.Any())
         return;

      _renderer.Render(e.Graphics, _game.Map, ClientSize);

      //var g = e.Graphics;
      //var cs = ClientSize;

      //ColWidth = new[] { cs.Width / _map.MaxSize.Width, cs.Height / _map.MaxSize.Height }.Min();
      //var colCenter = ColWidth / 2;

      //using var pen = new Pen(Color.DimGray, 2);

      //foreach (var zone in _map.Zones)
      //{
      //   var location = ComputeLocation(zone.Location.X, zone.Location.Y);
      //   var rect = new RectangleF(location, new SizeF(CircleDim, CircleDim));
      //   g.DrawEllipse(pen, rect);

      //   if (zone.IsHome)
      //   {
      //      var center = rect.GetCenter();
      //      var inner = new RectangleF( center, SizeF.Empty);
      //      inner.Inflate(10,10);
      //      g.DrawEllipse(pen, inner);

      //   }
      //}
   }

}

public static class RectangleFExtensions
{
   public static PointF GetCenter(this RectangleF rect) 
      => new(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
}

