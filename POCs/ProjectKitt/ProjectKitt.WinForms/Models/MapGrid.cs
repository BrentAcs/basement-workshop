namespace ProjectKitt.WinForms.Models;

// W. Germany, 450 x 625 miles ( 750 x 1000)

public class MapGrid
{
   public SizeF Size { get; set; } = new(735000, 1000000);

   public List<IMapGridObject> Objects { get; set; } = new();

   public IEnumerable<IMapGridStaticObject> StaticObjects => Objects.Cast<IMapGridStaticObject>();
}

