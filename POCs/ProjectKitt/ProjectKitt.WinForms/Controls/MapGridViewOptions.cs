namespace ProjectKitt.WinForms.Controls;

public class MapGridViewOptions
{
   public GridOptions Grid { get; set; } = new();

   public class GridOptions
   {
      public bool Visible { get; set; } = true;
      public Color Color { get; set; } = Color.DarkGray;
      public int Width { get; set; } = 1;
      public int Step { get; set; } = 50;
      public int HeavyStep { get; set; } = 250;
      public FontOptions Font { get; set; } = new();
   }

   public class FontOptions
   {
      public string Name { get; set; } = "Arial";
      public int Size { get; set; } = 12;
      public FontStyle Style { get; set; } = FontStyle.Regular;
      public Color Color { get; set; } = Color.DeepSkyBlue;
   }
}
