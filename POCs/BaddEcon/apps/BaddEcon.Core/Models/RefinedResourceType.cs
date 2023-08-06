using Bass.Shared.Infrastructure.Storage;

namespace BaddEcon.Core.Models;


public interface IRefinedResourceType : IBaseCommodityType
{
   IEnumerable<IRawResourceInput> RawInputs { get; }
}

[BsonCollection("RefinedResources")]
public class RefinedResourceType : BaseCommodityType, IRefinedResourceType
{
   public IEnumerable<IRawResourceInput> RawInputs { get; set; } = new List<IRawResourceInput>();
}
