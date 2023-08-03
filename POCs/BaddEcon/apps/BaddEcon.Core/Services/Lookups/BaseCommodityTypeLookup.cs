using BaddEcon.Core.Models;

namespace BaddEcon.Core.Services.Lookups;

public interface IBaseCommodityTypeLookup<out TType, in TEnum> where TType : IBaseCommodityType
{
   IEnumerable<TType> GetAll();
   TType Get(TEnum value);
}

public abstract class BaseCommodityTypeLookup<TType, TEnum> : IBaseCommodityTypeLookup<TType, TEnum>
   where TType : IBaseCommodityType
{
   public abstract IEnumerable<TType> GetAll();
   public abstract TType Get(TEnum value);
}
