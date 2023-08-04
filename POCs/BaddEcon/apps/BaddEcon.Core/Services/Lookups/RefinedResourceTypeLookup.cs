using BaddEcon.Core.Models;
using BaddEcon.Core.Services.Attributes;
using Bass.Shared.Extensions;

namespace BaddEcon.Core.Services.Lookups;

public interface IRefinedResourceTypeLookup : IBaseCommodityTypeLookup<IRefinedResourceType>
{
}

public class RefinedResourceTypeLookup : BaseCommodityTypeLookup<IRefinedResourceType>,
   IRefinedResourceTypeLookup
{
   private static readonly IEnumerable<IRefinedResourceType> _resources = new List<IRefinedResourceType>
   {
      //new RawResourceType{Id = , Name = "", Weight = },

      // Metals
      RefinedResourceType.Create(LookupConstants.CopperIngot, "Copper Ingot", 20,
         new[] {new RawResourceInput {ResourceId = LookupConstants.CopperOre, Quantity = 2}}),
      RefinedResourceType.Create(LookupConstants.IronIngot, "Iron Ingot", 20,
         new[] {new RawResourceInput {ResourceId = LookupConstants.IronOre, Quantity = 2}}),

      // Woods
      RefinedResourceType.Create(LookupConstants.MapleLumber, "Maple Lumber", 20,
         new[] {new RawResourceInput {ResourceId = LookupConstants.MapleWood, Quantity = 2}}),
      RefinedResourceType.Create(LookupConstants.OakLumber, "Oak Lumber", 20,
         new[] {new RawResourceInput {ResourceId = LookupConstants.OakWood, Quantity = 2}}),
   };

   public override IEnumerable<IRefinedResourceType> GetAll()
   {
      AssertValid(_resources);
      return _resources;
   }

   public override IRefinedResourceType? Get(int id)
   {
      AssertValid(_resources);
      return _resources.FirstOrDefault(_ => _.Id == id);
   }
}
