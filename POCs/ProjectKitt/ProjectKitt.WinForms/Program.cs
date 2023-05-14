using System.Windows.Forms.VisualStyles;
using Bass.Shared.Extensions;

namespace ProjectKitt.WinForms;

internal static class Program
{
   /// <summary>
   ///  The main entry point for the application.
   /// </summary>
   [STAThread]
   static void Main()
   {
      // To customize application configuration such as set high DPI settings or default font,
      // see https://aka.ms/applicationconfiguration.
      //ApplicationConfiguration.Initialize();
      //Application.Run(new MainForm());

      //var heading = 0f;
      var starting = PointF.Empty;
      var distance = 10f;

      // var headings = new[] { 0f, 45f, 90f, 180f, 270f, 360f };
      // foreach (float heading in headings)
      // {
      //    var ending = ComputeNew(starting, heading, 10);
      //    System.Diagnostics.Debug.WriteLine($"heading: {heading} -> {ending}");
      // }

      for (float heading = 0; heading < 360; heading += 45)
      {
         var ending = ComputeNew(starting, heading, 10);
         // System.Diagnostics.Debug.WriteLine($"heading: {heading} -> {ending}");
      }

      //x = r cosθ and y = r sinθ
   }

   static PointF ComputeNew(PointF starting, float heading, float distance)
   {
      var radians = ((double)heading).ToRadians();

      // var radians = heading;

      var x = distance * Math.Sin(radians);
      var y = distance * Math.Cos(radians);

      System.Diagnostics.Debug.WriteLine($"degrees: {heading} -> {radians} --- {x}, {y}");
      // System.Diagnostics.Debug.WriteLine($"x, y: {x}, {y}");

      
      return new PointF((float)x, (float)y);
   }
}
