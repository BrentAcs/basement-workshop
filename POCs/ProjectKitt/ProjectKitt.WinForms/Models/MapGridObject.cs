namespace ProjectKitt.WinForms.Models;

public interface IMapGridObject
{
   PointF Location { get; }
}

public interface IMapGridStaticObject : IMapGridObject
{
   IEnumerable<PointF> PerimeterPoints { get; }
   Color PerimeterColor { get; }
}

public interface IMapGridUnitObject : IMapGridObject
{
   UnitType UnitType { get; }
}

public enum UnitType
{
   Armor = 1,
   Infantry = 2,
   MechInfantry = 3,
}


public class MapGridObject : IMapGridObject
{
   public PointF Location { get; set; }
}
