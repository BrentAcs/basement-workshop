using BaddEcon.Core.Models;
using BaddEcon.Core.Services.Attributes;
using Bass.Shared.Extensions;

namespace BaddEcon.Core.Services.Lookups;

public interface IRefinedResourceTypeLookup : IBaseCommodityTypeLookup<IRefinedResourceType, RefinedResource>
{
}

public class RefinedResourceTypeLookup : BaseCommodityTypeLookup<IRefinedResourceType, RefinedResource>,
   IRefinedResourceTypeLookup
{
   public override IEnumerable<IRefinedResourceType> GetAll() =>
      Enum.GetValues<RefinedResource>()
         .Select(value => (RefinedResourceType)value.GetAttributeOfType<RefinedResourceAttribute>());

   public override IRefinedResourceType Get(RefinedResource value)
   {
      var refined = (RefinedResourceType)value.GetAttributeOfType<RefinedResourceAttribute>();
      refined.RawInputs = value.GetAttributesOfType<RawResourceInputAttribute>()
         .Select(attr => (RawResourceInput)attr)
         .Cast<IRawResourceInput>()
         .ToList();
      
      return refined;
   }
}
