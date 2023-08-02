using BaddEcon.Core.Models;
using BaddEcon.Core.Services.Attributes;
using Bass.Shared.Extensions;

namespace BaddEcon.Core.Services.Repos;

public interface IBaseCommodityTypeRepo<out TType, in TEnum> where TType : IBaseCommodityType
{
   IEnumerable<TType> GetAll();
   TType Get(TEnum value);
}

public abstract class BaseCommodityTypeRepo<TType, TEnum> : IBaseCommodityTypeRepo<TType, TEnum>
   where TType : IBaseCommodityType
{
   public abstract IEnumerable<TType> GetAll();
   public abstract TType Get(TEnum value);
}

public interface IRawResourceTypeRepo : IBaseCommodityTypeRepo<IRawResourceType, RawResource>
{
}

public class RawResourceTypeRepo : BaseCommodityTypeRepo<IRawResourceType, RawResource>, IRawResourceTypeRepo
{
   public override IEnumerable<IRawResourceType> GetAll() =>
      Enum.GetValues<RawResource>()
         .Select(value => (RawResourceType)value.GetAttributeOfType<RawResourceAttribute>());

   public override IRawResourceType Get(RawResource value) =>
      (RawResourceType)value.GetAttributeOfType<RawResourceAttribute>();
}

public interface IRefinedResourceTypeRepo : IBaseCommodityTypeRepo<IRefinedResourceType, RefinedResource>
{
}

public class RefinedResourceTypeRepo : BaseCommodityTypeRepo<IRefinedResourceType, RefinedResource>, IRefinedResourceTypeRepo
{
   public override IEnumerable<IRefinedResourceType> GetAll() =>
      Enum.GetValues<RefinedResource>()
         .Select(value => (RefinedResourceType)value.GetAttributeOfType<RefinedResourceAttribute>());

   public override IRefinedResourceType Get(RefinedResource value) =>
      (RefinedResourceType)value.GetAttributeOfType<RefinedResourceAttribute>();
}
