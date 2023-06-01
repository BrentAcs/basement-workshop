namespace ProjectKitt.Core.Models;

// W. Germany, 450 x 625 miles ( 750 x 1000)

public class MapGrid
{
   public SizeF Size { get; set; } = new(735000, 1000000);

   public List<IMapGridObject> Objects { get; set; } = new();

   [System.Text.Json.Serialization.JsonIgnore]
   [Newtonsoft.Json.JsonIgnore]
   public IEnumerable<IMapGridStaticObject> StaticObjects => Objects.OfType<IMapGridStaticObject>();
   
   [System.Text.Json.Serialization.JsonIgnore]
   [Newtonsoft.Json.JsonIgnore]
   public IEnumerable<IMapGridUnitObject> UnitObjects => Objects.OfType<IMapGridUnitObject>();
}