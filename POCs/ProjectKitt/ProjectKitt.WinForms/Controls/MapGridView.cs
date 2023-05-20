using System.ComponentModel;
using System.Reflection;
using Bass.Shared.Extensions;
using ProjectKitt.Core.Extensions;
using ProjectKitt.Core.Game;
using ProjectKitt.Core.Models;
using ProjectKitt.WinForms.Extensions;
using ProjectKitt.WinForms.Services;
using ProjectKitt.WinForms.Services.Rendering;

namespace ProjectKitt.WinForms.Controls;

public partial class MapGridView : UserControl
{
   private readonly ITheGame _theGame;
   private IMapGridObjectRendererFactory _rendererFactory = new MapGridObjectRendererFactory();
   private bool _controlKeyDown = false;
   private bool _shiftKeyDown = false;

   public MapGridView()
   {
      _theGame = Globals.TheGame;
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
   [Browsable(false)]
   [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   public MapGridViewOptions ViewOptions { get; set; } = new();

   [Browsable(false)]
   [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   public PointF ViewPortOrigin { get; set; } = new(0, 0);

   [Browsable(false)]
   [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   public ScaleFactor ScaleFactor { get; set; } = ScaleFactor._1To1;

   // --- Events

   public event EventHandler<ScaleFactorChangedArgs> ScaleFactorChanged;

   // --- Properties - private
   private MapGrid MapGrid => _theGame.MapGrid;
   private IFactionCollection Factions => _theGame.Factions;
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
      ScaleFactor = ScaleFactor._1To1;
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
         if (x > MapGrid.Size.Width.ScaleBy(ScaleFactorValue))
            break;

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
         if (y > MapGrid.Size.Height.ScaleBy(ScaleFactorValue))
            break;

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
         _rendererFactory.GetRenderer(mapGridObject)
            ?.Initialize(mapGridObject, ScaleFactor)
            .Render(g, ViewPortOrigin);
      }
   }
}


