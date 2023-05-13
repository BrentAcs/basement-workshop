namespace Bass.Shared.Extensions;

public static class IntExtensions
{
   // odd/even & positive/negative checks
   
   public static bool IsEven(this int value) =>
      value % 2 == 0;
      
   public static bool IsOdd(this int value) =>
      value % 2 != 0;
      
   public static bool IsPositive(this int value) =>
      value > 0;
      
   public static bool IsNegative(this int value) =>
      value < 0;
   
   // roman numerals

   public static string ToRoman(this int number) =>
      number switch
      {
         < 0 or > 3999 => throw new ArgumentOutOfRangeException(nameof(number), " value should be between 1 and 3999"),
         < 1 => string.Empty,
         >= 1000 => "M" + ToRoman(number - 1000),
         >= 900 => "CM" + ToRoman(number - 900),
         >= 500 => "D" + ToRoman(number - 500),
         >= 400 => "CD" + ToRoman(number - 400),
         >= 100 => "C" + ToRoman(number - 100),
         >= 90 => "XC" + ToRoman(number - 90),
         >= 50 => "L" + ToRoman(number - 50),
         >= 40 => "XL" + ToRoman(number - 40),
         >= 10 => "X" + ToRoman(number - 10),
         >= 9 => "IX" + ToRoman(number - 9),
         >= 5 => "V" + ToRoman(number - 5),
         >= 4 => "IV" + ToRoman(number - 4),
         >= 1 => "I" + ToRoman(number - 1),
      };
}
