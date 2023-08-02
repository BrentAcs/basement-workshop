namespace BaddEcon.Core.Models;

[AttributeUsage(AttributeTargets.Field)]
public class RawResourceTypeAttribute : Attribute
{
   public string Name { get; }
   public int Weight{ get; }

   public RawResourceTypeAttribute(string name, int weight)
   {
      Name = name;
      Weight = weight;
   }

   public static implicit operator RawResourceType(RawResourceTypeAttribute attr) => new() {Name = attr.Name, Weight = attr.Weight};
}
