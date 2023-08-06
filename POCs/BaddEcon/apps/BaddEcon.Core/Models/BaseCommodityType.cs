using Bass.Shared.Infrastructure.Storage;
using MongoDB.Bson;

namespace BaddEcon.Core.Models;

public interface IBaseCommodityType : IMongoDocument<int>
{
   int Id { get; }
   string Name { get; }
   int Weight { get; } // In kilograms (2.2 lbs)
}

public class BaseCommodityType : IBaseCommodityType
{
   public int Id { get; set; }
   public string Name { get; set; } = string.Empty;
   public int Weight { get; set; }
}
