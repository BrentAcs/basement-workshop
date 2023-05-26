﻿using ProjectKitt.Core.Extensions;
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
   UnitSize UnitSize { get; }
   float Orientation { get; }
   float ZoneOfControlRadius { get; }
   //IEnumerable<PointF> ZoneOfControlPoints { get; }
}

public class MapGridUnitObject : MapGridObject, IMapGridUnitObject
{
   private const float _controlRadius = 2000;
   public UnitType UnitType { get; set; } = UnitType.Armor;
   public UnitSize UnitSize { get; set; } = UnitSize.Division;
   public float Orientation { get; set; }
   public float ZoneOfControlRadius { get; set; } = _controlRadius;
   // public IEnumerable<PointF> ZoneOfControlPoints => ComputeZoneOfControl();
   //
   // private IEnumerable<PointF> ComputeZoneOfControl()
   // {
   //    const float start = 30f;
   //    const float step = 60f;
   //
   //    var points = Location.ComputePointsAtRadius(ZoneOfControlRadius, start, step);
   //    var rotated = points.ToRotatedPolygon(Location, Orientation);
   //
   //    return rotated;
   // }
}
