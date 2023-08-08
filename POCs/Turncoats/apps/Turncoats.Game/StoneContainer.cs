namespace Turncoats.Game;

public interface IStoneContainer
{
   void Reset();
   int QuantityFor(Stone stone);
   void Add(Stone stone);
   void Remove(Stone stone);
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
