using ProjectKitt.WinForms.Models;

namespace ProjectKitt.WinForms.Services;

public interface IMapGridRepo
{
   MapGrid Get();
}

public class MapGridRepo : IMapGridRepo
{
   public MapGrid Get()
   {
      return new MapGrid
      {
         Size = new(5000,5000)
      };
   }
}
