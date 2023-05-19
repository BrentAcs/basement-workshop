using ProjectKitt.WinForms.Controls;
using ProjectKitt.WinForms.Services;

namespace ProjectKitt.WinForms.Forms;

public partial class TestForm : Form
{
   public TestForm()
   {
      InitializeComponent();

      theMapGridView.ScaleFactor = ScaleFactor._1To1;
      //theMapGridView.ScaleFactor = ScaleFactor._1To10;
      //theMapGridView.ScaleFactor = ScaleFactor._1To100;
      //theMapGridView.ScaleFactor = ScaleFactor.OneToTwoHundredFifty;
      //theMapGridView.ScaleFactor = ScaleFactor.OneToFiveHundred;

      theMapGridView.ViewOptions = new MapGridViewOptions();
      UpdateDisplayRatio();
   }

   private void theMapGridView_ScaleFactorChanged(object sender, ScaleFactorChangedArgs e)
   {
      UpdateDisplayRatio();
   }

   private void UpdateDisplayRatio() => label1.Text = theMapGridView.ScaleFactor.GetScaleFactorDisplayRatio();
}
