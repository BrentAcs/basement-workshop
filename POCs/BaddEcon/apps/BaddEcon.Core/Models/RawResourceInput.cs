namespace BaddEcon.Core.Models;

public interface IRawResourceInput
{
   int ResourceId { get; }
   int Quantity { get; }
}

public class RawResourceInput : IRawResourceInput
{
   public int ResourceId { get; set; }
   public int Quantity { get; set; }
}
