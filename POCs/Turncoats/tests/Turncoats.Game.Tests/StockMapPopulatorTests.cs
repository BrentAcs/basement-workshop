using FluentAssertions;

namespace Turncoats.Game.Tests;

public class StockMapPopulatorTests
{
   [Fact]
   public void Populate_WillSet_HomeZone()
   {
      var map = new StockMapGenerator().Generate();
      var sut = new StockMapPopulator();
      sut.Populate(map);

      map.Zones.FirstOrDefault(z => z.HomeFor == Stone.Red)!
         .Stones.QuantityFor(Stone.Red)
         .Should().Be(2);
   }
}
