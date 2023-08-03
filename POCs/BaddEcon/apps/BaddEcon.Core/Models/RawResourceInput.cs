namespace BaddEcon.Core.Models;

public interface IRawResourceInput
{
   RawResource Resource { get; }
   int Quantity { get; }
}

public class RawResourceInput : IRawResourceInput
{
   public RawResource Resource { get; set; }
   public int Quantity { get; set; }
}
