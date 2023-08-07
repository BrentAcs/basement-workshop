namespace Turncoats.WinUI;

public partial class Form1 : Form
{
   public Form1()
   {
      InitializeComponent();
   }

   private int ColWidth { get; set; }
   private float CircleDim => (float)(ColWidth * .8);
   private float CirclePad => (float)(ColWidth * .2);

   protected override void OnPaint(PaintEventArgs e)
   {
      base.OnPaint(e);
      var g = e.Graphics;
      var cs = ClientSize;

      ColWidth = cs.Width / 5;
      var colCenter = ColWidth / 2;

      var location = ComputeLocation(0, 0);
      var rect = new RectangleF(location, new SizeF(CircleDim, CircleDim));

      using var pen = new Pen(Color.Green, 2);
      g.DrawEllipse(pen, rect);
   }

   private Point ComputeLocation(int x, int y)
   {
      var location = new Point((int)(0 + CirclePad / 2), (int)(0 + CirclePad / 2));

      return location;
   }
}
