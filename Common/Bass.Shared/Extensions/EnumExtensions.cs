namespace Bass.Shared.Extensions;

public static class EnumExtensions
{
   public static T? GetAttributeOfType<T>(this Enum enumVal) where T:System.Attribute => 
      GetAttributesOfType<T>(enumVal).FirstOrDefault();

   public static IEnumerable<T> GetAttributesOfType<T>(this Enum enumVal) where T:System.Attribute
   {
      var type = enumVal.GetType();
      var memInfo = type.GetMember(enumVal.ToString());
      var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
      return attributes.Cast<T>();
   }
}
