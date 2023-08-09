using Bass.Shared.Utilities;
using FluentAssertions;
using Moq;

namespace Turncoats.Game.Tests;

public class StonesReserveTests
{
   [Theory]
   [InlineData(Stone.Red)]
   [InlineData(Stone.Blue)]
   [InlineData(Stone.Black)]
   public void QuantityFor_WhenNew_Theories(Stone stone)
   {
      var sut = new StoneReserve();
      var result = sut.QuantityFor(stone);
      result.Should().Be(21);
   }

   [Theory]
   [InlineData(Stone.Blue)]
   public void GetRandom_Theories(Stone rndStone)
   {
      var rngMock = new Mock<IRng>();
      rngMock.Setup(_ => _.Next(It.IsAny<int>()))
         .Returns(((int) rndStone)-1);
      var sut = new StoneReserve();
      
      var result = sut.GetRandom(rngMock.Object);

      result.Should().Be(rndStone);
      sut.QuantityFor(rndStone).Should().Be(20);
   }
}
