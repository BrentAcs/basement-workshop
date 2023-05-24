using ProjectKitt.Core.Models;

namespace ProjectKitt.Core.Game;

// public class UnitInfo
// {
//    public UnitType UnitType { get; set; }
//    public UnitSize UnitSize { get; set; }
//    public float DefaultZoneOfControlRadius { get; set; }
// }

// public enum UnitType
// {
//    Armor = 1,
//    Infantry = 2,
//    MechInfantry = 3,
// }
//
// public enum UnitSize
// {
//    Division = 1,
//    Brigade,
//    Regiment,
// }

public interface IUnitInfoLookup
{
   float GetZoneOfControlRadius(UnitSize unitSize);
}

public class UnitInfoLookup : IUnitInfoLookup
{
   private readonly IDictionary<UnitSize, float> _unitSizeToZoCRadius = new Dictionary<UnitSize, float>
   {
      {UnitSize.Division, 4000},
      {UnitSize.Brigade, 2000},
      {UnitSize.Regiment, 1500}
   };

   public float GetZoneOfControlRadius(UnitSize unitSize) => _unitSizeToZoCRadius[unitSize];
}
