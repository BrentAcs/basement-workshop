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
   Division = 1,
   Brigade,
   Regiment,
}

public interface IMapGridUnitObject : IMapGridObject
{
   UnitType UnitType { get; }
   float Orientation { get; }
   float ZoneOfControlRadius { get; }
   IEnumerable<PointF> ZoneOfControlPoints { get; }
}

public class MapGridUnitObject : MapGridObject, IMapGridUnitObject
{
   private const float _controlRadius = 2000;
   public UnitType UnitType { get; set; } = UnitType.Armor;
   public float Orientation { get; set; }
   public float ZoneOfControlRadius { get; } = _controlRadius;
   public IEnumerable<PointF> ZoneOfControlPoints => ComputeZoneOfControl();

   private IEnumerable<PointF> ComputeZoneOfControl()
   {
      float[] angles = {330f, 30f, 90f, 150f, 210f, 270f};

      var points = angles
         .Select(angle => Location.ComputeRayEndPoint(angle, ZoneOfControlRadius)).ToList();

      var rotated = points.ToRotatePolygon(Location, Orientation);

      return rotated;
   }
}
