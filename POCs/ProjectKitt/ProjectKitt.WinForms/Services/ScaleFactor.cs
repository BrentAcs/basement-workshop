using System.Reflection;

namespace ProjectKitt.WinForms.Services;

public enum ScaleFactor
{
   // given 1px, 1800 x 1200
   [ScaleFactorValue(1.0f, "1:1")]
   _1To1 = 1,        // 1.11 x .68 miles  _1To1

   [ScaleFactorValue(0.4f, "1:2.5")]
   _1To2Point5,

   [ScaleFactorValue(0.2f, "1:5")]
   _1To5,

   [ScaleFactorValue(0.133333f, "1:7.5")]
   _1To7Point5,

   [ScaleFactorValue(0.1f, "1:10")]
   _1To10,            // 11.18 x 6.83 miles

   [ScaleFactorValue(0.04f, "1:25")]
   _1To25,

   [ScaleFactorValue(0.02f, "1:50")]
   _1To50,

   [ScaleFactorValue(0.01333333f, "1:75")]
   _1To75,

   [ScaleFactorValue(0.01f, "1:100")]
   _1To100,     // 111.84 x 68.35 miles

   [ScaleFactorValue(0.004f, "1:250")]
   _1To250,

   [ScaleFactorValue(0.002f, "1:500")]
   _1To500,

   [ScaleFactorValue(0.001f, "1:1000")]
   _1To1000,    // 1,1118.46 x 683.50 miles
}

[AttributeUsage(AttributeTargets.Field)]
public class ScaleFactorValueAttribute : Attribute
{
   public float Value { get; }
   public string RatioDisplay { get; }

   public ScaleFactorValueAttribute(float value, string ratioDisplay)
   {
      Value = value;
      RatioDisplay = ratioDisplay;
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

   public static string GetScaleFactorDisplayRatio(this ScaleFactor enumValue)
   {
      var value = enumValue.GetType()
         .GetMember(enumValue.ToString())
         .First()
         .GetCustomAttribute<ScaleFactorValueAttribute>()?
         .RatioDisplay;

      return value ?? "?:?";
   }

}
