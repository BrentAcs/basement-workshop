using Bass.Shared.Utilities;

namespace Bass.Shared.Tests.Utilities;

public class SimpleRngTests
{
   [Fact]
   public void Next_WhenCalledWithMinMaxInt_WillNeverReturnMax()
   {
      // yes, this is somewhat contrived.
      var sut = new SimpleRng();

      for (int i = 0; i < 1000; i++)
      {
         var rnd = sut.Next(0, 5);
         rnd.Should().BeInRange(0, 4);
      }
   }

   [Fact]
   public void Next_WhenCalledWithMinMaxDouble_WillNeverReturnMax()
   {
      // yes, this is somewhat contrived.
      var sut = new SimpleRng();

      for (int i = 0; i < 1000; i++)
      {
         var rnd = sut.Next(0.0, 5.0);
         rnd.Should().BeInRange(0.0, 4.999999);
      }
   }
}
