using System.Runtime.CompilerServices;
using Bass.Shared.Utilities;

namespace Turncoats.Game;

public interface IStoneReserve : IStoneContainer
{
   Stone GetRandom(IRng rng);
}

public class StoneReserve : StoneContainer, IStoneReserve
{
   protected override IDictionary<Stone, int> InitialState => new Dictionary<Stone, int>
   {
      {Stone.Red, 21},
      {Stone.Blue, 21},
      {Stone.Black, 21},
   };

   public Stone GetRandom(IRng rng)
   {
      if (TotalQuantity == 0)
         return Stone.None;
      
      var stones = Enum.GetValues<Stone>().Where(_ => _ != Stone.None).ToList();
      while (true)
      {
         var stone = stones[rng.Next(stones.Count)];
         if (_quantities[stone] <= 0)
            continue;
         
         Remove(stone);
         return stone;
      }
   }
}
