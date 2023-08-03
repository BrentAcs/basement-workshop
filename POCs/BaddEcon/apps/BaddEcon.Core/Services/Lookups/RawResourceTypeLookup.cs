using BaddEcon.Core.Models;
using BaddEcon.Core.Services.Attributes;
using Bass.Shared.Extensions;

namespace BaddEcon.Core.Services.Lookups;

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
