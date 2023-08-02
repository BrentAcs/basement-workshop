using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using BaddEcon.Core.Services.Attributes;

namespace BaddEcon.Core.Models;

public interface IBaseCommodityType
{
   string Name { get; }
   int Weight { get; } // In kilograms (2.2 lbs)
}

public class BaseCommodityType : IBaseCommodityType
{
   public string Name { get; init; } = string.Empty;
   public int Weight { get; init; }
}

public interface IProductionInput<out T> where T : IBaseCommodityType
{
   T CommodityType { get; }
   int Quantity { get; }
}


//  --- Raw Resource

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
}

public interface IRawResourceType : IBaseCommodityType
{
}

public class RawResourceType : BaseCommodityType, IRawResourceType
{
}


//  --- Refined Resource

public enum RefinedResource
{
   [RefinedResource("Copper Ingot", 10)]
   CopperIngot = 1,

   [RefinedResource("Iron Ingot", 10)]
   IronIngot,
}

public interface IRefinedResourceType : IBaseCommodityType
{
   // IEnumerable<IProductionInput<IRawResourceType>> RawInputs { get; }
}

public class RefinedResourceType : BaseCommodityType, IRefinedResourceType
{
   // public IEnumerable<IProductionInput<IRawResourceType>> RawInputs { get; set; } = new List<IProductionInput<IRawResourceType>>();
}


//  --- Product

public interface IProductType : IBaseCommodityType
{
}

public class ProductType : BaseCommodityType, IProductType
{
}
