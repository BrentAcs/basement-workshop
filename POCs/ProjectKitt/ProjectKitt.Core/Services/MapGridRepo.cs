using ProjectKitt.Core.Models;

namespace ProjectKitt.Core.Services;

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
         Size = new(5000,5000),
         Objects = new List<IMapGridObject>
         {
            new MapGridStaticObject
            {
               Location = new PointF(12,12),
               PerimeterPoints = new []
               {
                  new PointF(-7,-7),
                  new PointF(7,-7),
                  new PointF(-7,7),
                  new PointF(7,7),
               }   
            }
         }
         
      };
   }
}
