using FluentAssertions;

namespace Turncoats.Game.Tests;

public class StockMapPopulatorTests
{
   [Fact]
   public void Populate_WillSet_HomeZone()
   {
      var reserve = new StoneReserve();
      var map = new StockMapGenerator().Generate();
      var sut = new StockMapPopulator();
      sut.Populate(map, reserve);

      map.Zones.FirstOrDefault(z => z.HomeFor == Stone.Red)!
         .Stones.QuantityFor(Stone.Red)
         .Should().Be(2);
      map.Zones.FirstOrDefault(z => z.HomeFor == Stone.Blue)!
         .Stones.QuantityFor(Stone.Blue)
         .Should().Be(2);
      map.Zones.FirstOrDefault(z => z.HomeFor == Stone.Black)!
         .Stones.QuantityFor(Stone.Black)
         .Should().Be(2);
   }

   [Fact]
   public void Populate_WillSet_NoneHomeZones()
   {
      var reserve = new StoneReserve();
      var map = new StockMapGenerator().Generate();
      var sut = new StockMapPopulator();
      sut.Populate(map, reserve);

      var nonHomes = map.Zones.Where(z => !z.IsHome);
      foreach (var nonHome in nonHomes)
      {
         //nonHome.Stones
      }
   }

}
