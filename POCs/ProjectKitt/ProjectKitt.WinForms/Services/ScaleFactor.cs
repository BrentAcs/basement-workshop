using System.Reflection;

namespace ProjectKitt.WinForms.Services;

public enum ScaleFactor
{
   // given 1px, 1800 x 1200
   [ScaleFactoValue(1.0f)]
   OneToOne = 1,        // 1.11 x .68 miles
   
   [ScaleFactoValue(0.5f)]
   OneToFive,
   
   [ScaleFactoValue(0.1f)]
   OneToTen,            // 11.18 x 6.83 miles
   
   [ScaleFactoValue(0.05f)]
   OneToFifty,            // 11.18 x 6.83 miles

   [ScaleFactoValue(0.01f)]
   OneToOneHundred,     // 111.84 x 68.35 miles
   
   [ScaleFactoValue(0.001f)]
   OneToOneThousand,    // 1,1118.46 x 683.50 miles
}

[AttributeUsage(AttributeTargets.Field)]
public class ScaleFactoValueAttribute : Attribute
{
   public float Value { get; private set; }

   public ScaleFactoValueAttribute(float value)
   {
      Value = value;
   }
}

public static class EnumExtensions
{
   public static float GetScaleFactoValue(this ScaleFactor enumValue)
   {
      var value = enumValue.GetType()
         .GetMember(enumValue.ToString())
         .First()
         .GetCustomAttribute<ScaleFactoValueAttribute>()?
         .Value;

      return value ?? 1.0f;
   }
}