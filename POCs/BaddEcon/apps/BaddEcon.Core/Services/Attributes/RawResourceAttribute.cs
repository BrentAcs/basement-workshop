using BaddEcon.Core.Models;

namespace BaddEcon.Core.Services.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class RawResourceAttribute : Attribute
{
   public string Name { get; }
   public int Weight{ get; }

   public RawResourceAttribute(string name, int weight)
   {
      Name = name;
      Weight = weight;
   }

   public static implicit operator RawResourceType(RawResourceAttribute attr) => new() {Name = attr.Name, Weight = attr.Weight};
}

[AttributeUsage(AttributeTargets.Field)]
public class RefinedResourceAttribute : Attribute
{
   public string Name { get; }
   public int Weight{ get; }

   public RefinedResourceAttribute(string name, int weight)
   {
      Name = name;
      Weight = weight;
   }

   public static implicit operator RefinedResourceType(RefinedResourceAttribute attr) => new() {Name = attr.Name, Weight = attr.Weight};
}

