using BaddEcon.Core.Services.Attributes;

namespace BaddEcon.Core.Models;

public interface IRawResourceType : IBaseCommodityType
{
}

public class RawResourceType : BaseCommodityType, IRawResourceType
{
}
