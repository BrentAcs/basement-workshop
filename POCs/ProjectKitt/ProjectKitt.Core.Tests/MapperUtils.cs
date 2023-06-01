using ProjectKitt.Core.Game;

namespace ProjectKitt.Core.Tests;

public static class MapperUtils
{
   public static IMapper Mapper => GetMapper();

   private static IMapper GetMapper()
   {
      var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
      return config.CreateMapper();
   }
}
