using ProjectKitt.Core.Models;

namespace ProjectKitt.Core.Tests.Models;

public class MapGridTests
{
   private readonly ITestOutputHelper _output;
   public MapGridTests(ITestOutputHelper output) {
      _output = output;
   }
   
   [Fact]
   public void Test1()
   {
      var sut = new MapGrid
      {
         Objects = new List<IMapGridObject>
         {
            new MapGridUnitObject(),
            new MapGridUnitObject(),
            new MapGridStaticObject(),
            new MapGridStaticObject(),
         }
      };

      sut.UnitObjects.Should().HaveCount(2);
   }

   private static MapGrid SerializeTest => new()
   {
      Size = new SizeF(100000, 100000),
      Objects = new List<IMapGridObject>
      {
         new MapGridUnitObject
         {
            Location = new PointF(7500, 20000),
            // Owner = factions.Get(IFaction.Nato),
            UnitType = UnitType.Armor,
            UnitSize = UnitSize.Division,
            Orientation = 90f,
            AreaOfControlRadius = 4000,
         },
         new MapGridUnitObject
         {
            Location = new PointF(11500, 20000),
            // Owner = factions.Get(IFaction.Pact),
            UnitType = UnitType.Armor,
            UnitSize = UnitSize.Division,
            Orientation = 290f,
            AreaOfControlRadius = 4000,
         }
      }
   };

   [Fact]
   public void CanSerialize()
   {
      var sut = SerializeTest;
      var json = JsonConvert.SerializeObject(sut, Formatting.Indented);
      _output.WriteLine($"{json}");
   }
}
