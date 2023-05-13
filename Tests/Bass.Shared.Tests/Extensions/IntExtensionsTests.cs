using System.Numerics;
using Bass.Shared.Extensions;

namespace Bass.Shared.Tests.Extensions;

public class IntExtensionsTests
{
// public static bool IsPositive(this int value) =>
// public static bool IsNegative(this int value) =>

   [Theory]
   [InlineData(0, true)]
   [InlineData(1, false)]
   [InlineData(2, true)]
   public void IsEven_Theories(int value, bool expected) =>
      value.IsEven().Should().Be(expected);
   
   [Theory]
   [InlineData(0, false)]
   [InlineData(1, true)]
   [InlineData(2, false)]
   public void IsOdd_Theories(int value, bool expected) =>
      value.IsOdd().Should().Be(expected);

   [Theory]
   [InlineData(-1, false)]
   [InlineData(0, false)]
   [InlineData(2, true)]
   public void IsPositive_Theories(int value, bool expected) =>
      value.IsPositive().Should().Be(expected);

   [Theory]
   [InlineData(-1, true)]
   [InlineData(0, false)]
   [InlineData(2, false)]
   public void IsNegative_Theories(int value, bool expected) =>
      value.IsNegative().Should().Be(expected);
   
   [Theory]
   [InlineData(3, "III")]
   [InlineData(4, "IV")]
   [InlineData(8, "VIII")]
   [InlineData(9, "IX")]
   [InlineData(123, "CXXIII")]
   public void ToRoman_Theories(int value, string expected) =>
      value.ToRoman().Should().Be(expected);
  
}
