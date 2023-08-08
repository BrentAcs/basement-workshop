using FluentAssertions;

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
}
