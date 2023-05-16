namespace ProjectKitt.WinForms.Models;

// W. Germany, 450 x 625 miles ( 750 x 1000)

public class MapGrid
{
   public List<IMapGridObject> Objects { get; set; } = new();
   public SizeF Size { get; set; } = new(735000, 1000000);
}

public interface IMapGridObject
{
   PointF Location { get; }
}
