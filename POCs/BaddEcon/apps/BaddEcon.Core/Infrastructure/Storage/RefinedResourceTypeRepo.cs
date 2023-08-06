using BaddEcon.Core.Models;
using Bass.Shared.Infrastructure.Storage;
using Microsoft.Extensions.Logging;

namespace BaddEcon.Core.Infrastructure.Storage;

public interface IRefinedResourceTypeRepo : IMongoRepository<RefinedResourceType, int>
{
}

public class RefinedResourceTypeRepo : MongoRepository<RefinedResourceType, int>, IRefinedResourceTypeRepo
{
   public RefinedResourceTypeRepo(IMongoContext? mongoContext, ILogger<RefinedResourceTypeRepo> logger) : base(
      mongoContext,
      logger)
   {
   }

   public override async Task SeedDataAsync(CancellationToken cancellationToken = default)
   {
      Logger.LogInformation("Attempting to seed data for {repo}", nameof(RefinedResourceTypeRepo));
      if (!await NeedsSeedDataAsync(cancellationToken).ConfigureAwait(false))
      {
         Logger.LogInformation("Collection contains data, skipping");
         return;
      }

      Logger.LogInformation("Seeding....");
      await InsertManyAsync(new[]
         {
            new RefinedResourceType
            {
               Id = 1,
               Name = "Copper Ingot",
               Weight = 10,
               RawInputs = new[] {new RawResourceInput {ResourceId = 1, Quantity = 2}}
            },
            new RefinedResourceType
            {
               Id = 2,
               Name = "Iron Ingot",
               Weight = 10,
               RawInputs = new[] {new RawResourceInput {ResourceId = 2, Quantity = 2}},
            },
            new RefinedResourceType
            {
               Id = 3,
               Name = "Maple Lumber",
               Weight = 10,
               RawInputs = new[] {new RawResourceInput {ResourceId = 3, Quantity = 2}}
            },
            new RefinedResourceType
            {
               Id = 4,
               Name = "Oak Lumber",
               Weight = 10,
               RawInputs = new[] {new RawResourceInput {ResourceId = 4, Quantity = 2}}
            }
         },
         cancellationToken).ConfigureAwait(false);
   }
}
