using ProjectKitt.Core.Models;
using ProjectKitt.Core.Services;

namespace ProjectKitt.Core.Game;

public interface ITheGame
{
   MapGrid MapGrid { get; }
   IFactionLookup Factions { get; }   
}

public class TheGame : ITheGame
{
   public TheGame(IFactionLookup factions)
   {
      Factions = factions;
      
      MapGrid = MapGridCreator.Get(factions);
   }

   public MapGrid MapGrid { get; set; } = new();
   public IFactionLookup Factions { get; }
}

public static  class MapGridCreator
{
   public static MapGrid Get(IFactionLookup factions)
   {
      //return CreateSampleFor1To1();
      return CreateSampleForUnitSetup( factions);
   }

   private static MapGrid CreateSampleForUnitSetup(IFactionLookup factions)
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
               Owner = factions.Get(IFaction.Nato),
               UnitType = UnitType.Armor,
               Orientation = 90f
            },
            new MapGridUnitObject
            {
               Location = new PointF(10500,7500),
               Owner = factions.Get(IFaction.Pact),
               UnitType = UnitType.Armor,
               Orientation = 290f
            },
            new MapGridUnitObject
            {
               Location = new PointF(7500,12500),
               Owner = factions.Get(IFaction.Nato),
               UnitType = UnitType.Armor,
               Orientation = 90f
            },
            new MapGridUnitObject
            {
               Location = new PointF(12500,12500),
               Owner = factions.Get(IFaction.Pact),
               UnitType = UnitType.Armor,
               Orientation = 290f
            },
            new MapGridUnitObject
            {
               Location = new PointF(7500,20000),
               Owner = factions.Get(IFaction.Nato),
               UnitType = UnitType.Armor,
               Orientation = 90f
            },
            new MapGridUnitObject
            {
               Location = new PointF(11500,20000),
               Owner = factions.Get(IFaction.Pact),
               UnitType = UnitType.Armor,
               Orientation = 290f
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
               Orientation = 315f
            },
            new MapGridUnitObject
            {
               Location = new PointF(760,310),
               UnitType = UnitType.MechInfantry,
               Orientation = 270f
            },
            new MapGridUnitObject
            {
               Location = new PointF(750,500),
               UnitType = UnitType.Infantry,
               Orientation = 45f
            }
         }
      };
   }
   
}