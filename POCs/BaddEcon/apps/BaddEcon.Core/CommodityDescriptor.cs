namespace BaddEcon.Core;

public interface ICommodityDescriptor
{
   int Id { get; }
   string Name { get; }
   string ShortDescription { get; }
   string LongDescription { get; }
   int UnitsPerTon { get; }
}

public class CommodityDescriptor : ICommodityDescriptor
{
   public int Id { get; set; } = 0;
   public string Name { get; set; } = string.Empty;
   public string ShortDescription { get; set; } = string.Empty;
   public string LongDescription { get; set; } = string.Empty;
   public int UnitsPerTon { get; set; } = 1;
}

public class Cargo
{
   public int CommodityId { get; set; }
   public ICommodityDescriptor? Commodity { get; set; }
}
