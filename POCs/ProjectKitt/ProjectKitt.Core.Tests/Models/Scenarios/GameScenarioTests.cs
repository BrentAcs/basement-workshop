using ProjectKitt.Core.Game;
using ProjectKitt.Core.Models;
using ProjectKitt.Core.Models.Scenarios;

namespace ProjectKitt.Core.Tests.Models.Scenarios;

public class GameScenarioTests
{
   private readonly ITestOutputHelper _output;

   public GameScenarioTests(ITestOutputHelper output)
   {
      _output = output;
   }

   private static GameScenario SerializeTest => new()
   {
      MapGrid = new GameScenarioMapGrid
      {
         Size = new SizeF(100000, 100000),
      },
      Objects = new List<GameScenarioMapGridObject>
      {
         // Deployed (on map)
         new GameScenarioMapGridUnitObject
         {
            Location = new PointF(7500, 20000),
            OwnerName = IFaction.Nato,
            UnitType = UnitType.Armor,
            UnitSize = UnitSize.Division,
            Orientation = 90f,
            OnMap = true
         },
         new GameScenarioMapGridUnitObject
         {
            Location = new PointF(11500, 20000),
            OwnerName = IFaction.Pact,
            UnitType = UnitType.Armor,
            UnitSize = UnitSize.Division,
            Orientation = 290f,
            OnMap = true,
         },

         // Reserves (off map)
         new GameScenarioMapGridUnitObject
         {
            //Location = new PointF(7500, 20000),
            OwnerName = IFaction.Nato,
            UnitType = UnitType.Armor,
            UnitSize = UnitSize.Division,
            Orientation = 90f,
            OnMap = false
         },
      }
   };

  
   [Fact]
   public void CanSerialize()
   {
      var sut = SerializeTest;
      var json = JsonConvert.SerializeObject(sut, JsonUtils.Settings);
      _output.WriteLine($"{json}");
   }

   [Theory]
   [InlineData("./TestFiles/GameScenarioShort.json")]
   public void CanDeserialize_Theories(string filename)
   {
      var json = File.ReadAllText(filename);
      var sut = JsonConvert.DeserializeObject<GameScenario>(json, JsonUtils.Settings);
   }
}
