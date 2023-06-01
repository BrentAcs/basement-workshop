using ProjectKitt.Core.Game;
using ProjectKitt.Core.Models.Scenarios;

namespace ProjectKitt.Core.Tests.Services.Factories;

public class TheGameFactoryTests
{
   private static GameScenario ReadGameScenario(string filename)
   {
      var json = File.ReadAllText(filename);
      return JsonConvert.DeserializeObject<GameScenario>(json, JsonUtils.Settings)!;      
   }
   
   [Fact]
   public void CreateGame_FromScenario_WillSucceed()
   {
      var scenario = ReadGameScenario("./TestFiles/GameScenarioShort.json");
      var sut = new TheGameFactory(MapperUtils.Mapper);
      var game = sut.CreateGame(scenario);

      game.MapGrid.Size.Should().Be(new SizeF(100000, 100000));
      game.MapGrid.Objects.Should().HaveCount(2);
      game.UnitObjects.Should().HaveCount(1);
      game.UnitObjects.First().Owner.Name.Should().Be("Nato");
   }
}