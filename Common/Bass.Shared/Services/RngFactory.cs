using Bass.Shared.Utilities;

namespace Bass.Shared.Services;

public interface IRngFactory
{
   IRng Create();
   IRng Create(int seed);
   IRng GetForKey(object key, int? seed=null);
}

public class RngFactory : IRngFactory
{
   private Dictionary<object, IRng> _rngBySeed = new();

   public IRng Create() => new SimpleRng();

   public IRng Create(int seed) => new SimpleRng(seed);

   public IRng GetForKey(object key, int? seed=null)
   {
      if (!_rngBySeed.ContainsKey(key))
      {
         _rngBySeed[key] = (seed.HasValue) ? new SimpleRng(seed.Value) : new SimpleRng();
      }
         
      return _rngBySeed[key];
   }
}
