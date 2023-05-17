using System.Reflection;
using Bass.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using ProjectKitt.WinForms.Extensions;
using ProjectKitt.WinForms.Models;
using ProjectKitt.WinForms.Services;

namespace ProjectKitt.WinForms.Controls;

public partial class MapGridView : UserControl
{
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

   private void thePanel_Paint(object sender, PaintEventArgs e) => RenderGrid(e.Graphics);

   private void MapGridView_SizeChanged(object sender, EventArgs e) => CheckViewPortOrigin();

   private void ThePanel_MouseWheel(object? sender, MouseEventArgs e) => HandleMouseWheel(e);

   // --- Methods

   private void ResetView()
   {
      ScaleFactor = ScaleFactor.OneToOne;
      MapGrid = Globals.MapGridRepo.Get();
   }

   private float ScaleMe(float v) => v * ScaleFactorValue;
   private SizeF ScaleMe(SizeF v) => new(ScaleMe(v.Width), ScaleMe(v.Height));
   private float InverseScale(float v) => v / ScaleFactorValue;
   private SizeF InverseScale(SizeF v) => new(InverseScale(v.Width), InverseScale(v.Height));

   private void OffsetOrigin(float deltaX, float deltaY)
   {
      ViewPortOrigin = new PointF(ViewPortOrigin.X + deltaX, ViewPortOrigin.Y + deltaY);
      CheckViewPortOrigin();

      thePanel.Invalidate();
      thePanel.Update();
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
      var scaledGridSize = ScaleMe(MapGrid.Size);
      var newX = ViewPortOrigin.X;
      var newY = ViewPortOrigin.Y;

      if (newX < 0)
         newX = 0;
      if (newX > scaledGridSize.Width - ViewSize.Width)
         newX = (float)(Math.Round((scaledGridSize.Width - ViewSize.Width) / ViewOptions.Grid.Step) * ViewOptions.Grid.Step);

      if (newY < 0)
         newY = 0;
      if (newY > scaledGridSize.Height - ViewSize.Height)
         newY = (float)(Math.Round((scaledGridSize.Height - ViewSize.Height) / ViewOptions.Grid.Step) * ViewOptions.Grid.Step);

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

            var value = $"{InverseScale(x).ToMetricDistance()}";
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

            var value = $"{InverseScale(y).ToMetricDistance()}";
            var valueSize = g.MeasureString(value, font);
            g.DrawString(value, font, textBrush, 0, y - ViewPortOrigin.Y - (valueSize.Width / 2), format);
            g.DrawString(value, font, textBrush, ViewSize.Width - valueSize.Height, y - ViewPortOrigin.Y - (valueSize.Width / 2), format);
         }
      }
   }

   protected virtual void OnScaleFactorChanged(ScaleFactorChangedArgs e) => ScaleFactorChanged?.Invoke(this, e);
}
