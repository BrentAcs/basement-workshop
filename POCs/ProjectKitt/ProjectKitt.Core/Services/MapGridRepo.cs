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
      //return CreateSampleFor1To1();
      return CreateSampleForUnitSetup();
   }

   private static MapGrid CreateSampleForUnitSetup()
   {
      return new MapGrid
      {
         Size = new(100000, 100000),
         Objects = new List<IMapGridObject>
         {
            //new MapGridStaticObject
            //{
            //   Location = new PointF(1000,1000),
            //   PerimeterColor = Color.GreenYellow,
            //   PerimeterPoints = new []
            //   {
            //      new PointF(-100,-75),
            //      new PointF(100,-75),
            //      new PointF(125,0),
            //      new PointF(75,50),
            //      new PointF(-75,50),
            //      new PointF(-125,0),
            //   }
            //},
            new MapGridUnitObject
            {
               Location = new PointF(7500,7500),
               UnitType = UnitType.Armor,
               Facing = 90f
            },
            new MapGridUnitObject
            {
               Location = new PointF(10500,7500),
               UnitType = UnitType.Armor,
               Facing = 290f
            },
         }
      };
   }


   private static MapGrid CreateSampleFor1To1()
   {
      return new MapGrid
      {
         Size = new(5000,5000),
         Objects = new List<IMapGridObject>
         {
            new MapGridStaticObject
            {
               Location = new PointF(500,500),
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
