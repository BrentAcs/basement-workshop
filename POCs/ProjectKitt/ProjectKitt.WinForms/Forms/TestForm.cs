using ProjectKitt.WinForms.Controls;
using ProjectKitt.WinForms.Services;

namespace ProjectKitt.WinForms.Forms;

public partial class TestForm : Form
{
   public TestForm()
   {
      InitializeComponent();

      //theMapGridView.ScaleFactor = ScaleFactor.OneToOne;
      //theMapGridView.ScaleFactor = ScaleFactor.OneToTen;
      //theMapGridView.ScaleFactor = ScaleFactor.OneToOneHundred;
      //theMapGridView.ScaleFactor = ScaleFactor.OneToTwoHundredFifty;
      theMapGridView.ScaleFactor = ScaleFactor.OneToFiveHundred;

      theMapGridView.ViewOptions = new MapGridViewOptions();
   }
}
