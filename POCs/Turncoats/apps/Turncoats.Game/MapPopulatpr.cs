namespace Turncoats.Game;

public interface IMapPopulatpr
{
   void Populate(Map map, IStoneReserve stoneReserve);
}

public abstract class MapPopulatpr : IMapPopulatpr
{
   public abstract void Populate(Map map, IStoneReserve stoneReserve);
}

public class StockMapPopulator : MapPopulatpr
{
   public override void Populate(Map map, IStoneReserve stoneReserve)
   {
      var homes = map.Zones.Where(z => z.IsHome);
      foreach (var home in homes)
      {
         home.Stones.Add(home.HomeFor);
         home.Stones.Add(home.HomeFor);
      }
   }
}
