using BaddEcon.Core.Services.Attributes;

namespace BaddEcon.Core.Models;

public enum RefinedResource
{
   [RefinedResource("Copper Ingot", 10)]
   [RawResourceInput(RawResource.CopperOre)]
   CopperIngot = 1,

   [RefinedResource("Iron Ingot", 10)]
   [RawResourceInput(RawResource.IronOre)]
   IronIngot,
   
   [RawResource("Maple Lumber", 10)]
   MapleLumber,
}

public interface IRefinedResourceType : IBaseCommodityType
{
   IEnumerable<IRawResourceInput> RawInputs { get; }
}

public class RefinedResourceType : BaseCommodityType, IRefinedResourceType
{
   public IEnumerable<IRawResourceInput> RawInputs { get; set; } = new List<IRawResourceInput>();
}
