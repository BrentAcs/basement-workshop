using Newtonsoft.Json;
using ProjectKitt.Core.Models.Scenarios;
using ProjectKitt.WinForms.Controls;
using ProjectKitt.WinForms.Services;

namespace ProjectKitt.WinForms.Forms;

public partial class TestForm : Form
{
   public TestForm()
   {
      InitializeComponent();

      theMapGridView.ScaleFactor = ScaleFactor._1To1;

      theMapGridView.ViewOptions = new MapGridViewOptions();
      UpdateDisplayRatio();
   }

   private void theMapGridView_ScaleFactorChanged(object sender, ScaleFactorChangedArgs e) =>
      UpdateDisplayRatio();

   private void reloadScenarioButton_Click(object sender, EventArgs e) => LoadGameFromScenario();

   private void UpdateDisplayRatio() => label1.Text = theMapGridView.ScaleFactor.GetScaleFactorDisplayRatio();

   private void TestForm_Load(object sender, EventArgs e)
   {
      Location = UserSettings.Default.MainForm_Location;
      Size = UserSettings.Default.MainForm_Size;
      theMapGridView.ScaleFactor = (ScaleFactor)UserSettings.Default.MainForm_ScaleFactor;

      LoadGameFromScenario();
   }

   private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
   {
      UserSettings.Default.MainForm_Location = Location;
      UserSettings.Default.MainForm_Size = Size;
      UserSettings.Default.MainForm_ScaleFactor = (int)theMapGridView.ScaleFactor;
   }

   private void LoadGameFromScenario()
   {
      var json = File.ReadAllText("./TestFiles/GameScenarioShort.json");
      var settings = new JsonSerializerSettings
      {
         TypeNameHandling = TypeNameHandling.Auto,
         Formatting = Formatting.Indented
      };
      settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
      var scenario = JsonConvert.DeserializeObject<GameScenario>(json, settings)!;

      var theGame = Globals.TheGameFactory.CreateGame(scenario);
      theMapGridView.SetGame(theGame);
   }
}
