namespace ProjectKitt.WinForms.Services;

public class ViewPortRendererOptions
{
   public AxisOptions Axis { get; set; } = new();

   public class AxisOptions
   {
      public bool Visible { get; set; } = true;
      public Color Color { get; set; } = Color.Black;
      public int Width { get; set; } = 1;
   }
}
