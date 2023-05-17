using System.Reflection;

namespace ProjectKitt.WinForms.Services;

public enum ScaleFactor
{
   // given 1px, 1800 x 1200
   [ScaleFactorValue(1.0f)]
   OneToOne = 1,        // 1.11 x .68 miles
   
   [ScaleFactorValue(0.5f)]
   OneToFive,
   
   [ScaleFactorValue(0.1f)]
   OneToTen,            // 11.18 x 6.83 miles

   [ScaleFactorValue(0.025f)]
   OneToTwentyFive,

   [ScaleFactorValue(0.05f)]
   OneToFifty,            // 11.18 x 6.83 miles

   [ScaleFactorValue(0.01f)]
   OneToOneHundred,     // 111.84 x 68.35 miles

   [ScaleFactorValue(0.0025f)]
   OneToTwoHundredFifty,

   [ScaleFactorValue(0.005f)]
   OneToFiveHundred,

   [ScaleFactorValue(0.001f)]
   OneToOneThousand,    // 1,1118.46 x 683.50 miles
}

[AttributeUsage(AttributeTargets.Field)]
public class ScaleFactorValueAttribute : Attribute
{
   public float Value { get; private set; }

   public ScaleFactorValueAttribute(float value)
   {
      Value = value;
   }
}

public static class EnumExtensions
{
   public static float GetScaleFactorValue(this ScaleFactor enumValue)
   {
      var value = enumValue.GetType()
         .GetMember(enumValue.ToString())
         .First()
         .GetCustomAttribute<ScaleFactorValueAttribute>()?
         .Value;

      return value ?? 1.0f;
   }
}
