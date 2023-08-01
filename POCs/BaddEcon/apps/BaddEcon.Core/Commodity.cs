namespace BaddEcon.Core;

public class Commodity
{
   public string Name { get; set; } = string.Empty;
   public string ShortDescription { get; set; } = string.Empty;
   public string LongDescription { get; set; } = string.Empty;
   public WeightType WeightType { get; set; } = WeightType.Unknown;
}
