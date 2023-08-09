using FluentAssertions;

namespace Turncoats.Game.Tests;

public class StonesForZoneTests
{
   [Theory]
   [InlineData(Stone.Red)]
   [InlineData(Stone.Blue)]
   [InlineData(Stone.Black)]
   public void QuantityFor_WhenNew_Theories(Stone stone)
   {
      var sut = new StonesForZone();
      var result = sut.QuantityFor(stone);
      result.Should().Be(0);
   }
   
   [Theory]
   [InlineData(0, 0, 0, Stone.None)]
   [InlineData(1, 0, 0, Stone.Red)]
   [InlineData(0, 1, 0, Stone.Blue)]
   [InlineData(0, 0, 1, Stone.Black)]
   [InlineData(1, 0, 1, Stone.None)]
   [InlineData(0, 1, 1, Stone.None)]
   [InlineData(1, 2, 1, Stone.Blue)]
   public void GetVictor_Theories(int redCount, int blueCount, int blackCount, Stone expected)
   {
      var sut = new StonesForZone();
      for(int i=0; i<redCount; i++)
         sut.Add(Stone.Red);
      for(int i=0; i<blueCount; i++)
         sut.Add(Stone.Blue);
      for(int i=0; i<blackCount; i++)
         sut.Add(Stone.Black);
      
      var result = sut.GetVictor();
   
      result.Should().Be(expected);
   }

   [Theory]
   [InlineData(0, 0, 0, 0)]
   [InlineData(1, 1, 1, 3)]
   [InlineData(1, 0, 1, 2)]
   [InlineData(0, 1, 1, 2)]
   public void TotalQuantity_Theories(int redCount, int blueCount, int blackCount, int expected)
   {
      var sut = new StonesForZone();
      for (int i = 0; i < redCount; i++)
         sut.Add(Stone.Red);
      for (int i = 0; i < blueCount; i++)
         sut.Add(Stone.Blue);
      for (int i = 0; i < blackCount; i++)
         sut.Add(Stone.Black);

      sut.TotalQuantity.Should().Be(expected);
   }

}
