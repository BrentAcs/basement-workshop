namespace Turncoats.Game;

public interface IMapGenerator
{
   Map Generate();
}

public abstract class MapGenerator : IMapGenerator
{
   public abstract Map Generate();
}

public class StockMapGenerator : MapGenerator
{
   public override Map Generate() =>
      new()
      {
         Zones = new List<Zone>
         {
            // Col 0
            Zone.Create(0, 0,0, Stone.Black),
            Zone.Create(1, 0,1),
            // Col 1
            Zone.Create(2, 1,1),
            Zone.Create(3, 1,2),
            // col 2
            Zone.Create(4, 2,0),
            Zone.Create(5, 2,2, Stone.Red),
            // col 3
            Zone.Create(6, 3,0, Stone.Blue),
            Zone.Create(7, 3,1),
            Zone.Create(8, 3,2),
            // col 4
            Zone.Create(9, 4,0),
            Zone.Create(10, 4,1),
         }
      };
}

