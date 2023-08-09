using Bass.Shared.Utilities;

namespace Turncoats.Game;

public interface IMapPopulatpr
{
   void Populate(Map map, IStoneReserve stoneReserve, IRng rng);
}

public abstract class MapPopulatpr : IMapPopulatpr
{
   public abstract void Populate(Map map, IStoneReserve stoneReserve, IRng rng);
}

public class StockMapPopulator : MapPopulatpr
{
   public override void Populate(Map map, IStoneReserve stoneReserve, IRng rng)
   {
      PopulateHomeZones(map);

      foreach (var zone in map.Zones)
      {
         while (zone.Stones.TotalQuantity != 2)
         {
            var stone = stoneReserve.GetRandom(rng);
            if (stone == Stone.None)
               throw new InvalidOperationException();
            
            zone.Stones.Add(stone);
         }
      }
   }

   private static void PopulateHomeZones(Map map)
   {
      var homes = map.Zones.Where(z => z.IsHome);
      foreach (var home in homes)
      {
         home.Stones.Add(home.HomeFor);
         home.Stones.Add(home.HomeFor);
      }
   }
}
