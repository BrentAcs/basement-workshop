namespace ProjectKitt.Core.Tests;

public static class JsonUtils
{
   public static JsonSerializerSettings Settings => GetJsonSettings();
   
   private static JsonSerializerSettings GetJsonSettings()
   {
      var settings = new JsonSerializerSettings
      {
         TypeNameHandling = TypeNameHandling.Auto,
         Formatting = Formatting.Indented
      };
      settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
      return settings;
   }  
}
