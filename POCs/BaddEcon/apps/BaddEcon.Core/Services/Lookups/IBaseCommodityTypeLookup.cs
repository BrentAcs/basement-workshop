using BaddEcon.Core.Models;
using BaddEcon.Core.Services.Attributes;
using Bass.Shared.Extensions;

namespace BaddEcon.Core.Services.Repos;

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

public interface IRawResourceTypeLookup : IBaseCommodityTypeLookup<IRawResourceType, RawResource>
{
}

public class RawResourceTypeLookup : BaseCommodityTypeLookup<IRawResourceType, RawResource>, IRawResourceTypeLookup
{
   public override IEnumerable<IRawResourceType> GetAll() =>
      Enum.GetValues<RawResource>()
         .Select(value => (RawResourceType)value.GetAttributeOfType<RawResourceAttribute>());

   public override IRawResourceType Get(RawResource value) =>
      (RawResourceType)value.GetAttributeOfType<RawResourceAttribute>();
}

public interface IRefinedResourceTypeLookup : IBaseCommodityTypeLookup<IRefinedResourceType, RefinedResource>
{
}

public class RefinedResourceTypeLookup : BaseCommodityTypeLookup<IRefinedResourceType, RefinedResource>, IRefinedResourceTypeLookup
{
   public override IEnumerable<IRefinedResourceType> GetAll() =>
      Enum.GetValues<RefinedResource>()
         .Select(value => (RefinedResourceType)value.GetAttributeOfType<RefinedResourceAttribute>());

   public override IRefinedResourceType Get(RefinedResource value) =>
      (RefinedResourceType)value.GetAttributeOfType<RefinedResourceAttribute>();
}
