using Bass.Shared.Utilities;

namespace Bass.Shared.Tests.Utilities;

public class MinMaxTests
{
   [Fact]
   public void Ctor_WillNotThrow_When_MinLessThanMax()
   {
      var action = () => new MinMax<int>(4, 5);

      action.Should().NotThrow();
   }
   
   [Fact]
   public void Ctor_WillThrow_When_MinGreaterMax()
   {
      var action = () => new MinMax<int>(6, 5);

      action.Should().Throw<ArgumentOutOfRangeException>();
   }

   [Fact]
   public void Ctor_WillThrow_When_MinGreaterEqualMax()
   {
      var action = () => new MinMax<int>(5, 5);

      action.Should().Throw<ArgumentOutOfRangeException>();
   }
}
