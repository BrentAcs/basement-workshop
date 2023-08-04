namespace BaddEcon.Core.Models;

public interface IBaseCommodityType
{
   int Id { get; }
   string Name { get; }
   int Weight { get; } // In kilograms (2.2 lbs)
}

public class BaseCommodityType : IBaseCommodityType
{
   public int Id { get; init; }
   public string Name { get; init; } = string.Empty;
   public int Weight { get; init; }
}
