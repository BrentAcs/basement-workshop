using Bass.Shared.Extensions;

namespace Bass.Shared.Utilities;

public class ValuesRequest<T>
{
   public ValuesRequest()
   {
   }

   public ValuesRequest(IEnumerable<T>? subsetSelection, int amountRequested=1)
   {
      SubsetSelection = subsetSelection;
      AmountRequested = amountRequested;
   }

   public int AmountRequested { get; set; }

   public IEnumerable<T>? SubsetSelection{ get; set; }

   public IEnumerable<T> GetValues(IRng rng)
      => GetValues(rng, AmountRequested);

   public IEnumerable<T> GetValues(IRng rng, int uniqueCount)
   {
      if (SubsetSelection is null)
         throw new InvalidOperationException("GetValues called w/o a subset selection.");
      if (uniqueCount < 0)
         throw new ArgumentOutOfRangeException(nameof(uniqueCount), "Must be positive.");
      if (uniqueCount > SubsetSelection.Count())
         throw new ArgumentOutOfRangeException(nameof(uniqueCount), "Must be less than or equal to subset selection count.");
      
      var values = new List<T>();

      for (int i = 0; i < uniqueCount; i++)
      {
         T value = rng.Next(SubsetSelection);
         while (values.Contains(value))
         {
            value = rng.Next(SubsetSelection);
         }
         
         values.Add(value);         
      }

      return values;
   }
}
