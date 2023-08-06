using BaddEcon.Core.Models;
using Bass.Shared.Infrastructure.Storage;
using Microsoft.Extensions.Logging;

namespace BaddEcon.Core.Infrastructure.Storage;

public interface IRawResourceTypeRepo : IMongoRepository<RawResourceType, int>
{
}

public class RawResourceTypeRepo : MongoRepository<RawResourceType, int>, IRawResourceTypeRepo
{
   public RawResourceTypeRepo(IMongoContext? mongoContext, ILogger<RawResourceTypeRepo> logger) : base(mongoContext,
      logger)
   {
   }

   public override async Task SeedDataAsync(CancellationToken cancellationToken = default)
   {
      if (!await NeedsSeedDataAsync(cancellationToken))
         return;

      await InsertManyAsync(new[]
      {
         new RawResourceType {Id = 1, Name = "Copper Ore", Weight = 20,},
         new RawResourceType {Id = 2, Name = "Iron Ore", Weight = 20,},
         
         new RawResourceType {Id = 3, Name = "Maple Wood", Weight = 10,},
         new RawResourceType {Id = 4, Name = "Oak Wood", Weight = 10,}

      }, cancellationToken).ConfigureAwait(false);
   }
}
