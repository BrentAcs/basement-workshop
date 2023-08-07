namespace Turncoats.Game;

public interface IStoneContainer
{
   void Reset();
   int QuantityFor(Stone stone);
   void Add(Stone stone);
   void Remove(Stone stone);
}

public interface IStoneReserve : IStoneContainer
{
}

public interface IStonesForZone : IStoneContainer
{
   Stone GetVictor();
}

public abstract class StoneContainer : IStoneContainer
{
   protected IDictionary<Stone, int> _quantities = new Dictionary<Stone, int>();
   protected abstract IDictionary<Stone, int> InitialState { get; }

   public StoneContainer()
   {
      Reset();
   }

   public void Reset() => _quantities = InitialState;

   public int QuantityFor(Stone stone) => _quantities[stone];

   public void Add(Stone stone) => _quantities[stone]++;

   public void Remove(Stone stone)
   {
      if (_quantities[stone] == 0)
         throw new InvalidOperationException();

      _quantities[stone]--;
   }
}

public class StonesForZone : StoneContainer, IStonesForZone
{
   protected override IDictionary<Stone, int> InitialState => new Dictionary<Stone, int>
   {
      {Stone.Red, 0},
      {Stone.Blue, 0},
      {Stone.Black, 0},
   };
   
   public Stone GetVictor()
   {
      var ordered = _quantities.OrderBy(_ => _.Value)
         .Reverse()
         .ToList();
      return ordered[0].Value == ordered[1].Value ? Stone.None : ordered[0].Key;
   }
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
