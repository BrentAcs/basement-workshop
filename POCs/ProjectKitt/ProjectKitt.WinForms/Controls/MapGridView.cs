using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using Bass.Shared.Extensions;
using ProjectKitt.Core.Extensions;
using ProjectKitt.Core.Models;
using ProjectKitt.WinForms.Extensions;
using ProjectKitt.WinForms.Services;

namespace ProjectKitt.WinForms.Controls;

public partial class MapGridView : UserControl
{
   private IMapGridObjectRendererFactory _rendererFactory = new MapGridObjectRendererFactory();
   private bool _controlKeyDown = false;
   private bool _shiftKeyDown = false;

   public MapGridView()
   {
      InitializeComponent();

      thePanel.MouseWheel += ThePanel_MouseWheel;
      KeyDown += ThePanel_KeyDown;
      KeyUp += ThePanel_KeyUp;
   }

   protected override void OnLoad(EventArgs e)
   {
      base.OnLoad(e);
      typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, thePanel, new object[] { true });
      typeof(Panel).InvokeMember("SetStyle", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic, null, thePanel, new object[] { ControlStyles.AllPaintingInWmPaint, true });
      typeof(Panel).InvokeMember("SetStyle", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic, null, thePanel, new object[] { ControlStyles.ResizeRedraw, true });
      typeof(Panel).InvokeMember("SetStyle", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic, null, thePanel, new object[] { ControlStyles.UserPaint, true });
      typeof(Panel).InvokeMember("SetStyle", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic, null, thePanel, new object[] { ControlStyles.OptimizedDoubleBuffer, true });
      typeof(Panel).InvokeMember("UpdateStyles", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic, null, thePanel, new object[] { });
   }

   // --- Properties - public
   public MapGrid MapGrid { get; set; } = new();
   public MapGridViewOptions ViewOptions { get; set; } = new();
   public PointF ViewPortOrigin { get; set; } = new(0, 0);
   public ScaleFactor ScaleFactor { get; set; } = ScaleFactor.OneToOne;

   // --- Events

   public event EventHandler<ScaleFactorChangedArgs> ScaleFactorChanged;

   // --- Properties - private

   private SizeF ViewSize => new(thePanel.ClientSize.Width, thePanel.ClientSize.Height);
   private float ScaleFactorValue => ScaleFactor.GetScaleFactorValue();

   // --- Event Handlers

   private void ThePanel_KeyDown(object? sender, KeyEventArgs e) { _controlKeyDown = e.Control; _shiftKeyDown = e.Shift; }

   private void ThePanel_KeyUp(object? sender, KeyEventArgs e) { _controlKeyDown = false; _shiftKeyDown = false; }

   private void TacticalGridView_Load(object sender, EventArgs e) => ResetView();

   private void thePanel_Paint(object sender, PaintEventArgs e) => HandlePaint(e);

   private void MapGridView_SizeChanged(object sender, EventArgs e) => CheckViewPortOrigin();

   private void ThePanel_MouseWheel(object? sender, MouseEventArgs e) => HandleMouseWheel(e);

   // --- Event Invokers
   protected virtual void OnScaleFactorChanged(ScaleFactorChangedArgs e) => ScaleFactorChanged?.Invoke(this, e);

   // --- Methods

   private void ResetView()
   {
      ScaleFactor = ScaleFactor.OneToOne;
      MapGrid = Globals.MapGridRepo.Get();
   }

   private void OffsetOrigin(float deltaX, float deltaY)
   {
      ViewPortOrigin = new PointF(ViewPortOrigin.X + deltaX, ViewPortOrigin.Y + deltaY);
      CheckViewPortOrigin();

      thePanel.Invalidate();
      thePanel.Update();
   }

   private void HandlePaint(PaintEventArgs e)
   {
      RenderGrid(e.Graphics);
      RenderObjects(e.Graphics);
   }

   private void HandleMouseWheel(MouseEventArgs e)
   {
      if (_shiftKeyDown)
      {
         if (e.Delta.IsNegative())
         {
            var values = Enum.GetValues<ScaleFactor>().ToList();
            var index = values.IndexOf(ScaleFactor);
            if (index > 0)
            {
               ScaleFactor = values[index - 1];
               CheckViewPortOrigin();
               OnScaleFactorChanged(new ScaleFactorChangedArgs(ScaleFactor));
            }

            OffsetOrigin(0, 0);
         }

         if (e.Delta.IsPositive())
         {
            var values = Enum.GetValues<ScaleFactor>().ToList();
            var index = values.IndexOf(ScaleFactor);
            if (index < values.Count - 1)
            {
               ScaleFactor = values[index + 1];
               CheckViewPortOrigin();
               OnScaleFactorChanged(new ScaleFactorChangedArgs(ScaleFactor));
            }

            OffsetOrigin(0, 0);
         }

         return;
      }

      if (e.Delta.IsPositive() && !_controlKeyDown)
      {
         OffsetOrigin(0, -100);
      }

      if (e.Delta.IsPositive() && _controlKeyDown)
      {
         OffsetOrigin(-100, 0);
      }

      if (e.Delta.IsNegative() && !_controlKeyDown)
      {
         OffsetOrigin(0, 100);
      }

      if (e.Delta.IsNegative() && _controlKeyDown)
      {
         OffsetOrigin(100, 0);
      }
   }

   private void CheckViewPortOrigin()
   {
      var scaledGridSize = MapGrid.Size.ScaleBy(ScaleFactorValue);
      var newX = ViewPortOrigin.X;
      var newY = ViewPortOrigin.Y;

      if (newX > scaledGridSize.Width - ViewSize.Width)
         newX = (float)(Math.Round((scaledGridSize.Width - ViewSize.Width) / ViewOptions.Grid.Step) * ViewOptions.Grid.Step);
      if (newX < 0)
         newX = 0;

      if (newY > scaledGridSize.Height - ViewSize.Height)
         newY = (float)(Math.Round((scaledGridSize.Height - ViewSize.Height) / ViewOptions.Grid.Step) * ViewOptions.Grid.Step);
      if (newY < 0)
         newY = 0;

      ViewPortOrigin = new PointF(newX, newY);
   }

   private void RenderGrid(Graphics g)
   {
      if (!ViewOptions.Grid.Visible)
         return;

      using var font = new Font(ViewOptions.Grid.Font.Name, ViewOptions.Grid.Font.Size, ViewOptions.Grid.Font.Style);
      using var textBrush = new SolidBrush(ViewOptions.Grid.Font.Color);
      using var pen = new Pen(ViewOptions.Grid.Color, ViewOptions.Grid.Width);
      using var heavyPen = new Pen(ViewOptions.Grid.Color, ViewOptions.Grid.Width * 2);
      using var format = new StringFormat(StringFormatFlags.DirectionVertical);

      for (float x = ViewPortOrigin.X; x < ViewSize.Width + ViewPortOrigin.X; x += ViewOptions.Grid.Step)
      {
         if (x % ViewOptions.Grid.HeavyStep != 0)
         {
            g.DrawLine(pen, x - ViewPortOrigin.X, 0, x - ViewPortOrigin.X, ViewSize.Height);
         }
         else
         {
            g.DrawLine(heavyPen, x - ViewPortOrigin.X, 0, x - ViewPortOrigin.X, ViewSize.Height);

            if (Math.Abs(x - ViewPortOrigin.X) < 1e-10)
               continue;

            var value = $"{x.InverseScaleBy(ScaleFactorValue).ToMetricDistance()}";
            var valueSize = g.MeasureString(value, font);
            g.DrawString(value, font, textBrush, x - ViewPortOrigin.X - (valueSize.Width / 2), 0);
            g.DrawString(value, font, textBrush, x - ViewPortOrigin.X - (valueSize.Width / 2), ViewSize.Height - valueSize.Height);
         }
      }

      for (float y = ViewPortOrigin.Y; y < ViewSize.Height + ViewPortOrigin.Y; y += ViewOptions.Grid.Step)
      {
         if (y % ViewOptions.Grid.HeavyStep != 0)
         {
            g.DrawLine(pen, 0, y - ViewPortOrigin.Y, ViewSize.Width, y - ViewPortOrigin.Y);
         }
         else
         {
            g.DrawLine(heavyPen, 0, y - ViewPortOrigin.Y, ViewSize.Width, y - ViewPortOrigin.Y);

            if (Math.Abs(y - ViewPortOrigin.Y) < 1e-10)
               continue;

            var value = $"{y.InverseScaleBy(ScaleFactorValue).ToMetricDistance()}";
            var valueSize = g.MeasureString(value, font);
            g.DrawString(value, font, textBrush, 0, y - ViewPortOrigin.Y - (valueSize.Width / 2), format);
            g.DrawString(value, font, textBrush, ViewSize.Width - valueSize.Height, y - ViewPortOrigin.Y - (valueSize.Width / 2), format);
         }
      }
   }

   private void RenderObjects(Graphics g)
   {
      foreach (var mapGridObject in MapGrid.Objects)
      {
         var renderer = _rendererFactory.GetRenderer(mapGridObject);
         if (renderer is null)
            continue;

         renderer.Initialize(mapGridObject, ScaleFactor).Render(g, ViewPortOrigin);
      }
   }
}


public interface IMapGridObjectRenderer
{
   bool CanRender(IMapGridObject mapGridObject);
   IMapGridObjectRenderer Initialize(IMapGridObject mapGridObject, ScaleFactor scaleFactor);
   void Render(Graphics g, PointF viewPortOrigin);
}

public interface IMapGridObjectRendererFactory
{
   IMapGridObjectRenderer? GetRenderer(IMapGridObject? mapGridObject);
}

public class MapGridObjectRendererFactory : IMapGridObjectRendererFactory
{
   private readonly IEnumerable<IMapGridObjectRenderer> _renders = new List<IMapGridObjectRenderer>
   {
      new MapGridStaticObjectRenderer(),
      new MapGridUnitObjectRenderer()
   };

   public IMapGridObjectRenderer? GetRenderer(IMapGridObject? mapGridObject)
   {
      if (mapGridObject == null)
         return null;

      return _renders.FirstOrDefault(renderer => renderer.CanRender(mapGridObject));
   }
}


public abstract class MapGridObjectRenderer : IMapGridObjectRenderer
{
   protected IMapGridObject? MapGridObject { get; private set; }
   protected ScaleFactor ScaleFactor { get; private set; }
   protected float ScaleFactorValue { get; private set; }

   public abstract bool CanRender(IMapGridObject mapGridObject);

   public IMapGridObjectRenderer Initialize(IMapGridObject mapGridObject, ScaleFactor scaleFactor)
   {
      MapGridObject = mapGridObject;
      ScaleFactor = scaleFactor;
      ScaleFactorValue = ScaleFactor.GetScaleFactorValue();
      return this;
   }
   public abstract void Render(Graphics g, PointF viewPortOrigin);

   protected PointF ScaleLocation(PointF viewPortOrigin) => new(
      MapGridObject!.Location.X.ScaleBy(ScaleFactorValue) - viewPortOrigin.X,
      MapGridObject.Location.Y.ScaleBy(ScaleFactorValue) - viewPortOrigin.Y);

   protected IEnumerable<PointF> ComputeObjectsPoints(IEnumerable<PointF> objectPoints, PointF center)
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


public class MapGridStaticObjectRenderer : MapGridObjectRenderer, IMapGridObjectRenderer
{
   public override bool CanRender(IMapGridObject mapGridObject) => mapGridObject is IMapGridStaticObject;

   public override void Render(Graphics g, PointF viewPortOrigin)
   {
      if (MapGridObject is not IMapGridStaticObject staticObject)
         throw new InvalidOperationException();

      using var pen = new Pen(staticObject.PerimeterColor);
      using var brush = new SolidBrush(staticObject.PerimeterColor);

      var location = ScaleLocation(viewPortOrigin);

      var points = ComputeObjectsPoints(staticObject.PerimeterPoints, location).ToList();
      //var rotated = points.ToRotatePolygon(display, heading);

      points.ClosePolygon();
      g.DrawPolygon(pen, points.ToArray());
      g.FillPolygon(brush, points.ToArray());
   }
}

public class MapGridUnitObjectRenderer : MapGridObjectRenderer, IMapGridObjectRenderer
{
   public override bool CanRender(IMapGridObject mapGridObject) => mapGridObject is IMapGridUnitObject;

   private static readonly RectangleF _overallRect = new(PointF.Empty, new SizeF(56, 56));
   private static readonly RectangleF _symbolRect = new(PointF.Empty, new SizeF(45, 30));

   private IMapGridUnitObject UnitObject
   {
      get
      {
         if (MapGridObject is not IMapGridUnitObject unitObject)
            throw new InvalidOperationException();
         return unitObject;
      }
   }

   public override void Render(Graphics g, PointF viewPortOrigin)
   {
      using var pen = new Pen(Color.White, 2);
      var location = ScaleLocation(viewPortOrigin);
      //var overallRect = _overallRect.CenterOn(location);
      //var overallPoints = _overallRect.ToPointFs();
      var symbolRect = _symbolRect.CenterOn(location);

      //var symbolPoints = symbolRect.ToPointFs();
      //var rotated = symbolPoints.ToRotatePolygon(location, UnitObject.Facing);
      //symbolPoints.ClosePolygon();

      g.DrawRectangle(pen, symbolRect);
      //g.DrawPolygon(pen, symbolPoints.ToArray());

      //g.DrawEllipse(pen, overallRect);

      DrawUnitSymbol(g, pen, symbolRect);
      DrawFacingIndicator(g, location, Color.AliceBlue);
   }

   private void DrawUnitSymbol(Graphics g, Pen pen, RectangleF symbolRect)
   {
      switch (UnitObject.UnitType)
      {
         case UnitType.Armor:
            DrawArmorUnitSymbol(g, pen, symbolRect);
            break;
         case UnitType.Infantry:
            DrawInfantryUnitSymbol(g, pen, symbolRect);
            break;
         case UnitType.MechInfantry:
            DrawArmorUnitSymbol(g, pen, symbolRect);
            DrawInfantryUnitSymbol(g, pen, symbolRect);
            break;
            //default:
            //   throw new ArgumentOutOfRangeException();
      }
   }

   private void DrawArmorUnitSymbol(Graphics g, Pen pen, RectangleF symbolRect)
   {
      var rect = symbolRect.Deflate(10, 15);
      g.DrawEllipse(pen, rect);
   }

   private void DrawInfantryUnitSymbol(Graphics g, Pen pen, RectangleF symbolRect)
   {
      g.DrawLine(pen, 
         symbolRect.Location, 
         new PointF(symbolRect.Location.X + symbolRect.Width, symbolRect.Location.Y + symbolRect.Height));
      g.DrawLine(pen,
         symbolRect.Location with { Y = symbolRect.Location.Y + symbolRect.Height },
         symbolRect.Location with { X = symbolRect.Location.X + symbolRect.Width });
   }

   private void DrawFacingIndicator(Graphics g, PointF location, Color color)
   {
      if (MapGridObject is not IMapGridUnitObject unitObject)
         throw new InvalidOperationException();

      const float facingMarkerLength = 35;
      const float arrowHeadAngle = 135;
      const float arrowHeadLength = 7;

      using var facingPen = new Pen(Color.LimeGreen, 2);

      var radians = ((double)unitObject.Facing).ToRadians();
      var rayEnd = new PointF(
         (float)(location.X + facingMarkerLength * Math.Sin(radians)),
         (float)(location.Y + facingMarkerLength * -Math.Cos(radians)));
      g.DrawLine(facingPen, location, rayEnd);

      var arrowHeadLeft = new PointF(
         (float)(rayEnd.X + arrowHeadLength * Math.Sin(((double)unitObject.Facing - arrowHeadAngle).ToRadians())),
         (float)(rayEnd.Y + arrowHeadLength * -Math.Cos(((double)unitObject.Facing - arrowHeadAngle).ToRadians()))
      );
      g.DrawLine(facingPen, rayEnd, arrowHeadLeft);
      var arrowHeadRight = new PointF(
         (float)(rayEnd.X + arrowHeadLength * Math.Sin(((double)unitObject.Facing + arrowHeadAngle).ToRadians())),
         (float)(rayEnd.Y + arrowHeadLength * -Math.Cos(((double)unitObject.Facing + arrowHeadAngle).ToRadians()))
      );
      g.DrawLine(facingPen, rayEnd, arrowHeadRight);

   }
}

public static class RectangleFExtensions
{
   public static RectangleF CenterOn(this RectangleF rect, PointF point) =>
      rect with { X = point.X - rect.X - rect.Width / 2, Y = point.Y - rect.Y - rect.Height / 2 };

   public static RectangleF Deflate(this RectangleF rect, float x, float y) => new(rect.X + x/2, rect.Y + y/2, rect.Width-x, rect.Height-y);

   public static IList<PointF> ToPointFs(this RectangleF rect) =>
         new List<PointF>
         {
         new(rect.X, rect.Y),
         new(rect.X+rect.Width, rect.Y),
         new(rect.X+rect.Width, rect.Y+rect.Height),
         new(rect.X, rect.Y+rect.Height),
         };
}
