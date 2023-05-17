using ProjectKitt.WinForms.Services;

namespace ProjectKitt.WinForms.Controls;

public class ScaleFactorChangedArgs : EventArgs
{
   public ScaleFactor ScaleFactor { get; }

   public ScaleFactorChangedArgs(ScaleFactor scaleFactor)
   {
      ScaleFactor = scaleFactor;
   }
}
