namespace ProjectKitt.Core.Models;

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
   float Facing { get; }
}

public enum UnitType
{
   Armor = 1,
   Infantry = 2,
   MechInfantry = 3,
}


public abstract class MapGridObject : IMapGridObject
{
   public PointF Location { get; set; }
}

public class MapGridStaticObject : MapGridObject, IMapGridStaticObject
{
   public IEnumerable<PointF> PerimeterPoints { get; set; } = new[] { PointF.Empty };
   public Color PerimeterColor { get; set; } = Color.Yellow;
}

public class MapGridUnitObject : MapGridObject, IMapGridUnitObject
{
   public UnitType UnitType { get; set;  } = UnitType.Armor;
   public float Facing { get; set; }

}
