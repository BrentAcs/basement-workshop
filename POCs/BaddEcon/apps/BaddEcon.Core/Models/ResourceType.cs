using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace BaddEcon.Core.Models;

public enum RawResource
{
   [RawResourceType("Copper Ore", 20)]
   CopperOre = 1,

   [RawResourceType("Iron Ore", 20)]
   IronOre,

   [RawResourceType("Silver Ore", 20)]
   SilverOre,

   [RawResourceType("Titanium Ore", 20)]
   TitaniumOre,
}

public interface IBaseCommodityType
{
   // int Id { get; }
   string Name { get; }
   int Weight { get; } // In kilograms (2.2 lbs)
}

public interface IRawResourceType : IBaseCommodityType
{
}

public interface IRefinedResourceType : IBaseCommodityType
{
   // IEnumerable<IProductionInput<IRawResourceType>> RawInputs { get; }
}

public interface IProductType : IBaseCommodityType
{
}

public interface IProductionInput<out T> where T : IBaseCommodityType
{
   T CommodityType { get; }
   int Quantity { get; }
}

public class BaseCommodityType : IBaseCommodityType
{
   // public int Id { get; init; }
   public string Name { get; init; } = string.Empty;
   public int Weight { get; init; }
}

public class RawResourceType : BaseCommodityType, IRawResourceType
{
}

public class RefinedResourceType : BaseCommodityType, IRefinedResourceType
{
   // public IEnumerable<IProductionInput<IRawResourceType>> RawInputs { get; set; } = new List<IProductionInput<IRawResourceType>>();
}

public class ProductType : BaseCommodityType, IProductType
{
}
