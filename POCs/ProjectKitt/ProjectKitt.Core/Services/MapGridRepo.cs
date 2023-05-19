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
               Location = new PointF(500,250),
               PerimeterPoints = new []
               {
                  new PointF(-25,-25),
                  new PointF(25,-25),
                  new PointF(25,25),
                  new PointF(-25,25),
               }   
            },
            new MapGridStaticObject
            {
               Location = new PointF(1000,500),
               PerimeterColor = Color.GreenYellow,
               PerimeterPoints = new []
               {
                  new PointF(-25,-25),
                  new PointF(25,-25),
                  new PointF(25,25),
                  new PointF(-25,25),
               }
            },
            new MapGridUnitObject
            {
               Location = new PointF(755,255),
               UnitType = UnitType.Armor,
               Facing = 315f
            },
            new MapGridUnitObject
            {
               Location = new PointF(760,310),
               UnitType = UnitType.MechInfantry,
               Facing = 270f
            },
            new MapGridUnitObject
            {
               Location = new PointF(750,500),
               UnitType = UnitType.Infantry,
               Facing = 45f
            }
         }
      };
   }
}
