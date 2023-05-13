using Bass.Shared.Utilities;

namespace Bass.Shared.Extensions;

public static class RngExtensions
{
   // -- IEnumerable<T>

   public static T Next<T>(this IRng rng, IEnumerable<T> collection)
   {
      var enumerable = collection.ToList();
      return enumerable.ToList()[ rng.Next(0, enumerable.Count) ];
   }

   // -- MinMax<T>
   
   public static double Next(this IRng rng, MinMax<double> range) =>
      rng.Next(range.Min, range.Max);
   
   public static int Next(this IRng rng, MinMax<int> range) =>
      rng.Next(range.Min, range.Max+1);
}
