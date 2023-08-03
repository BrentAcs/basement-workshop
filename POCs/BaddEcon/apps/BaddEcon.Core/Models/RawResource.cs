using BaddEcon.Core.Services.Attributes;

namespace BaddEcon.Core.Models;

public enum RawResource
{
   [RawResource("Copper Ore", 20)]
   CopperOre = 1,

   [RawResource("Iron Ore", 20)]
   IronOre,

   [RawResource("Silver Ore", 20)]
   SilverOre,

   [RawResource("Titanium Ore", 20)]
   TitaniumOre,

   [RawResource("Maple Wood", 10)]
   MapleWood,
}

public interface IRawResourceType : IBaseCommodityType
{
}

public class RawResourceType : BaseCommodityType, IRawResourceType
{
}
