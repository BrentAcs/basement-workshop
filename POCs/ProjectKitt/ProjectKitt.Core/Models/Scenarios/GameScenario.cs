namespace ProjectKitt.Core.Models.Scenarios;

public class GameScenario
{
   public GameScenarioMapGrid MapGrid { get; set; } = new();
   
   public List<GameScenarioMapGridObject> Objects { get; set; } = new();

   [System.Text.Json.Serialization.JsonIgnore]
   [Newtonsoft.Json.JsonIgnore]
   public IEnumerable<GameScenarioMapGridUnitObject> UnitObjects => Objects.OfType<GameScenarioMapGridUnitObject>();
}

public class GameScenarioMapGrid
{
   public SizeF Size { get; set; } = new(735000, 1000000);
}

public abstract class GameScenarioMapGridObject
{
   public PointF Location { get; set; }
   public string OwnerName { get; set; } = string.Empty;
}

public class GameScenarioMapGridUnitObject : GameScenarioMapGridObject
{
   public UnitType UnitType { get; set; } = UnitType.Armor;
   public UnitSize UnitSize { get; set; } = UnitSize.Division;
   public float Orientation { get; set; }
   public bool OnMap { get; set; }
}
