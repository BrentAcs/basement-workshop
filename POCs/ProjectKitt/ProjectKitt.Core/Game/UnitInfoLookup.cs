using ProjectKitt.Core.Models;

namespace ProjectKitt.Core.Game;

public interface IUnitInfoLookup
{
   float GetAreaOfControlRadius(UnitSize unitSize);
}

public class UnitInfoLookup : IUnitInfoLookup
{
   private readonly IDictionary<UnitSize, float> _unitSizeToZoCRadius = new Dictionary<UnitSize, float>
   {
      {UnitSize.Division, 4000},
      {UnitSize.Brigade, 2000},
      {UnitSize.Regiment, 1500}
   };

   public float GetAreaOfControlRadius(UnitSize unitSize) => _unitSizeToZoCRadius[unitSize];
}
