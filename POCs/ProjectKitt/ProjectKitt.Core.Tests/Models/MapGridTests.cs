using ProjectKitt.Core.Models;

namespace ProjectKitt.Core.Tests.Models;

public class MapGridTests
{
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
}
