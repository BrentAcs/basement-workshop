using BaddEcon.Core.Models;
using Bass.Shared.Infrastructure.Storage;

namespace BaddEcon.Core.Services.Lookups;

public interface IRawResourceTypeLookup : IBaseCommodityTypeLookup<IRawResourceType>
{
}

public class RawResourceTypeLookup : BaseCommodityTypeLookup<IRawResourceType>, IRawResourceTypeLookup
{
   public RawResourceTypeLookup(IMongoRepository<IRawResourceType, int> repo) : base(repo)
   {
   }
}
