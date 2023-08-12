using Bass.Shared.Utilities;
using ZstdSharp.Unsafe;

namespace Turncoats.Game;

public interface ITheGame
{
   Map Map { get; }

   void Start();
}

public class TheGame : ITheGame
{
   private IRng _rng = new SimpleRng();
   private StoneReserve _stoneReserve = new();

   public TheGame()
   {
      Map = new StockMapGenerator().Generate();
      new StockMapPopulator().Populate(Map, _stoneReserve, _rng);
   }
   
   public void Start()
   {
      Map = new StockMapGenerator().Generate();
      new StockMapPopulator().Populate(Map, _stoneReserve, _rng);
   }

   public Map Map { get; private set; }
}
