namespace BaddEcon.Core.Models;


public interface IRefinedResourceType : IBaseCommodityType
{
   IEnumerable<IRawResourceInput> RawInputs { get; }
}

public class RefinedResourceType : BaseCommodityType, IRefinedResourceType
{
   public static RefinedResourceType Create(int id, string name, int weight, params RawResourceInput[] rawInputs) => 
      new() {Id = id, Name = name, Weight = weight, RawInputs = rawInputs};

   public IEnumerable<IRawResourceInput> RawInputs { get; set; } = new List<IRawResourceInput>();
}
