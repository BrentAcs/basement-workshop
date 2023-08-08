namespace Turncoats.Game;

public interface IStoneReserve : IStoneContainer
{
}

public class StoneReserve : StoneContainer, IStoneReserve
{
   protected override IDictionary<Stone, int> InitialState => new Dictionary<Stone, int>
   {
      {Stone.Red, 21},
      {Stone.Blue, 21},
      {Stone.Black, 21},
   };
}
