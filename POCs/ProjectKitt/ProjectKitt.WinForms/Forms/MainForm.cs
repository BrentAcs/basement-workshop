using Bass.Shared.Extensions;
using ProjectKitt.WinForms.Services;

namespace ProjectKitt.WinForms.Forms;

public partial class MainForm : Form
{
   private ViewPortRenderer _renderer;
   private PointF _location = PointF.Empty;
   private float _heading = 0f;
   private float _speed = 25;

   public MainForm()
   {
      InitializeComponent();
      _renderer = new ViewPortRenderer(ClientRectangle);
      // _renderer.ScaleFactor = ScaleFactor.OneToOneHundred;

      foreach (var scaleFactor in Enum.GetValues<ScaleFactor>())
      {
         scaleListBox.Items.Add(scaleFactor);
      }
      scaleListBox.SelectedIndex = 0;
   }

   private void MainForm_SizeChanged(object sender, EventArgs e) => Invalidate();

   private void scaleListBox_SelectedIndexChanged(object sender, EventArgs e) => Invalidate();

   private void MainForm_Paint(object sender, PaintEventArgs e)
   {
      _renderer.ScaleFactor = (ScaleFactor)scaleListBox.SelectedItem;
      _renderer.ViewRect = ClientRectangle;
      
      _renderer.Render(e.Graphics, new TestRenderedObject(), _location, _heading);
      
      // _renderer.Render(e.Graphics, new _10RenderedObject(), _location, _heading);
      // _renderer.Render(e.Graphics, new _100RenderedObject(), _location, _heading);
      // _renderer.Render(e.Graphics, new _200RenderedObject(), _location, _heading);
      // _renderer.Render(e.Graphics, new _1000RenderedObject(), _location, _heading);
   }

   private PointF ComputeNew(PointF starting, float heading, float distance)
   {
      distance *= _renderer.ScaleFactor.GetScaleFactoValue();
      var radians = ((double)heading).ToRadians();
      return new PointF((float)(starting.X + distance * Math.Sin(radians)),
         (float)(starting.Y + distance * Math.Cos(radians)));
   }

   private void button1_Click(object sender, EventArgs e)
   {
      _location = ComputeNew(_location, _heading, _speed);
      _heading += 15;
      _speed += 5;
      Invalidate();
   }
}
