using BaddEcon.Core.Models;
using BaddEcon.Core.Services.Attributes;
using Bass.Shared.Extensions;

namespace BaddEcon.Core.Services.Lookups;

public interface IRawResourceTypeLookup : IBaseCommodityTypeLookup<IRawResourceType>
{
}

public class RawResourceTypeLookup : BaseCommodityTypeLookup<IRawResourceType>, IRawResourceTypeLookup
{
   private static readonly IEnumerable<IRawResourceType> _resources = new List<IRawResourceType>
   {
      //new RawResourceType{Id = , Name = "", Weight = },

      // Metals
      new RawResourceType{Id = LookupConstants.CopperOre, Name = "Copper Ore", Weight = 20 },
      new RawResourceType{Id = LookupConstants.IronOre, Name = "Iron Ore", Weight = 20 },
      
      // Woods
      new RawResourceType{Id = LookupConstants.MapleWood, Name = "Maple Wood", Weight = 10 },
      new RawResourceType{Id = LookupConstants.OakWood, Name = "Oak wood", Weight = 10 },
   };

   public override IEnumerable<IRawResourceType> GetAll()
   {
      AssertValid(_resources);
      return _resources;
   }

   public override IRawResourceType? Get(int id)
   {
      AssertValid(_resources);
      return _resources.FirstOrDefault(_ => _.Id == id);
   }
}
