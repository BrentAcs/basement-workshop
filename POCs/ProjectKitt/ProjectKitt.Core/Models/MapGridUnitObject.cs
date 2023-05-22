using ProjectKitt.Core.Extensions;
using ProjectKitt.WinForms.Extensions;

namespace ProjectKitt.Core.Models;

public enum UnitType
{
   Armor = 1,
   Infantry = 2,
   MechInfantry = 3,
}

public enum UnitSize
{
   Army = 1,
   Corps,
   Division,
   Brigade,
   Regiment,
}

public interface IMapGridUnitObject : IMapGridObject
{
   UnitType UnitType { get; }
   float Facing { get; }
   IEnumerable<PointF> ZoneOfControl { get; }
}

public class MapGridUnitObject : MapGridObject, IMapGridUnitObject
{
   public UnitType UnitType { get; set;  } = UnitType.Armor;
   public float Facing { get; set; }
   public IEnumerable<PointF> ZoneOfControl => ComputeZoneOfControl();

   private IEnumerable<PointF> ComputeZoneOfControl()
   {
      const float controlRadius = 2000;
      float[] angles = {330f, 30f, 90f, 150f, 210f, 270f};

      var points = angles
         .Select(angle => Location.ComputeRayEndPoint(angle, controlRadius)).ToList();
      
      var rotated = points.ToRotatePolygon(Location, Facing);

      return rotated;
   }
}
