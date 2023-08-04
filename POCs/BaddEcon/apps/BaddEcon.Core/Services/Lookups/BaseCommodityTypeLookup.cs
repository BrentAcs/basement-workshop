using BaddEcon.Core.Models;

namespace BaddEcon.Core.Services.Lookups;

public interface IBaseCommodityTypeLookup<out TType> where TType : IBaseCommodityType
{
   IEnumerable<TType> GetAll();
   TType? Get(int id);
}

public abstract class BaseCommodityTypeLookup<TType> : IBaseCommodityTypeLookup<TType>
   where TType : IBaseCommodityType
{
   public abstract IEnumerable<TType> GetAll();
   public abstract TType? Get(int id);
   
   protected void AssertValid<T>(IEnumerable<T> collection) where T : IBaseCommodityType
   {
      if (collection.GroupBy(x => x.Id)
          .Where(g => g.Count() > 1)
          .Select(y => y.Key)
          .Any())
         throw new InvalidOperationException();
   }
}
