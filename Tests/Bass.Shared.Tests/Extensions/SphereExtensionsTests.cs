using Bass.Shared.Extensions;
using Bass.Shared.Geometry;

namespace Bass.Shared.Tests.Extensions;

public class SphereExtensionsTests
{
   [Theory]
   [InlineData(1000, 12566370)]
   [InlineData(3958, 196861796)]
   public void CalcSurfaceArea_Theory(int radius, long expected)
   {
      var sphere = new Sphere(radius);
      var area = sphere.CalcSurfaceArea();
      area.Should().Be(expected);
   }
}
