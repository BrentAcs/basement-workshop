namespace Turncoats.Game;

public class StoneContainer
{
   private IDictionary<Stone, int> _quantities = new Dictionary<Stone, int>();

   public StoneContainer()
   {
      Reset();
   }

   public void Reset()
   {
      _quantities[ Stone.Red ] = 0;
      _quantities[ Stone.Blue ] = 0;
      _quantities[ Stone.Black ] = 0;
   }

   public int QuantityFor(Stone stone) =>
      _quantities[ stone ];

   public void Add(Stone stone) =>
      _quantities[ stone ]++;

   public void Remove(Stone stone)
   {
      if (_quantities[ stone ] == 0)
         throw new InvalidOperationException();
      
      _quantities[ stone ]--;
   }
}
