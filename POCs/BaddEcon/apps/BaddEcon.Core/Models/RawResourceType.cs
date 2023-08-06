using BaddEcon.Core.Services.Attributes;
using Bass.Shared.Infrastructure.Storage;

namespace BaddEcon.Core.Models;

public interface IRawResourceType : IBaseCommodityType
{
}

[BsonCollection("RawResources")]
public class RawResourceType : BaseCommodityType, IRawResourceType
{
}
