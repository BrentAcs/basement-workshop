using FluentAssertions;

namespace Turncoats.Game.Tests;

public class StoneContainerTests
{
   // --- Red
   [Fact]
   public void QuantityFor_WillReturnZero_ForRed_WhenNew()
   {
      var sut = new StoneContainer();
      var result = sut.QuantityFor(Stone.Red);
      result.Should().Be(0);
   }
   
   // --- Blue

   [Fact]
   public void QuantityFor_WillReturnZero_ForBlue_WhenNew()
   {
      var sut = new StoneContainer();
      var result = sut.QuantityFor(Stone.Blue);
      result.Should().Be(0);
   }

   // --- Black
   
   [Fact]
   public void QuantityFor_WillReturnZero_ForBlack_WhenNew()
   {
      var sut = new StoneContainer();
      var result = sut.QuantityFor(Stone.Black);
      result.Should().Be(0);
   }

}
