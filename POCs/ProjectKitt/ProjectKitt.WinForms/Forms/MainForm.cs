using Bass.Shared.Extensions;
using ProjectKitt.WinForms.Services;

namespace ProjectKitt.WinForms.Forms;

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
      System.Diagnostics.Debug.WriteLine($"Client Size: {ClientSize}");

      var painter = new ViewPortRenderer(ClientRectangle);
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

