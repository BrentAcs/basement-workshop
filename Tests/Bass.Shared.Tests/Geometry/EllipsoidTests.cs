using Bass.Shared.Geometry;

namespace Bass.Shared.Tests.Geometry;

public class EllipsoidTests
{
   // Inflate

   [Theory]
   [InlineData(100, 100, 100, 10, 20, 30, 110, 120, 130)]
   public void Inflate_Theories_ForEllipsoid(int origX, int origY, int origZ,
      int deltaX, int deltaY, int deltaZ, 
      int expectedX, int expectedY, int expectedZ)
   {
      var sut = new Ellipsoid(origX, origY, origZ);
      sut.Inflate(deltaX, deltaY, deltaZ);

      sut.X.Should().Be(expectedX);
      sut.Y.Should().Be(expectedY);
      sut.Z.Should().Be(expectedZ);
   }
   
   [Theory]
   [InlineData(100, 20, 120, 120, 120 )]
   public void Inflate_Theories_ForSphere(int origRadius, int deltaRadius, int expectedX, int expectedY, int expectedZ)
   {
      var sut = new Sphere(origRadius);
      sut.Inflate(deltaRadius);

      sut.X.Should().Be(expectedX);
      sut.Y.Should().Be(expectedY);
      sut.Z.Should().Be(expectedZ);
   }
}
