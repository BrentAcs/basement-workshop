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

   private void TestForm_Load(object sender, EventArgs e)
   {
      Location = UserSettings.Default.MainForm_Location;
      Size = UserSettings.Default.MainForm_Size;
      theMapGridView.ScaleFactor = (ScaleFactor)UserSettings.Default.MainForm_ScaleFactor;
      
      //_theGame = Globals.TheGame;
      //theMapGridView.SetGame(Globals.TheGame);
   }

   private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
   {
      UserSettings.Default.MainForm_Location = Location;
      UserSettings.Default.MainForm_Size = Size;
      UserSettings.Default.MainForm_ScaleFactor = (int)theMapGridView.ScaleFactor;
   }
}
