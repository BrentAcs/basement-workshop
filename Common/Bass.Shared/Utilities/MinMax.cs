using System.Numerics;

namespace Bass.Shared.Utilities;

public record MinMax<T> where T : INumber<T>
{
   public MinMax(T min, T max)
   {
      if (min >= max)
         throw new ArgumentOutOfRangeException($"{nameof(min)}",
            $"{nameof(min)} greater than or equal to {nameof(max)}");

      Min = min;
      Max = max;
   }

   public T Min { get; set; }
   public T Max { get; set; }
}
