namespace Turncoats.Game;

public interface IMapPopulatpr
{
   void Populate(Map map);
}

public abstract class MapPopulatpr : IMapPopulatpr
{
   public abstract void Populate(Map map);
}

public class StockMapPopulator : MapPopulatpr
{
   public override void Populate(Map map)
   {
      
   }
}
