namespace ProjectKitt.WinForms.Forms
{
   partial class TestForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         theMapGridView = new Controls.MapGridView();
         label1 = new Label();
         reloadScenarioButton = new Button();
         showAoCCheckBox = new CheckBox();
         showAoCPointsCheckBox = new CheckBox();
         SuspendLayout();
         // 
         // theMapGridView
         // 
         theMapGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
         theMapGridView.AutoScroll = true;
         theMapGridView.AutoSize = true;
         theMapGridView.Location = new Point(12, 70);
         theMapGridView.Name = "theMapGridView";
         theMapGridView.Size = new Size(922, 611);
         theMapGridView.TabIndex = 0;
         theMapGridView.ScaleFactorChanged += theMapGridView_ScaleFactorChanged;
         // 
         // label1
         // 
         label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
         label1.AutoSize = true;
         label1.Location = new Point(896, 9);
         label1.Name = "label1";
         label1.Size = new Size(38, 15);
         label1.TabIndex = 1;
         label1.Text = "label1";
         // 
         // reloadScenarioButton
         // 
         reloadScenarioButton.Location = new Point(12, 12);
         reloadScenarioButton.Name = "reloadScenarioButton";
         reloadScenarioButton.Size = new Size(75, 23);
         reloadScenarioButton.TabIndex = 2;
         reloadScenarioButton.Text = "Reload Scenario";
         reloadScenarioButton.UseVisualStyleBackColor = true;
         reloadScenarioButton.Click += reloadScenarioButton_Click;
         // 
         // showAoCCheckBox
         // 
         showAoCCheckBox.AutoSize = true;
         showAoCCheckBox.Location = new Point(93, 12);
         showAoCCheckBox.Name = "showAoCCheckBox";
         showAoCCheckBox.Size = new Size(81, 19);
         showAoCCheckBox.TabIndex = 3;
         showAoCCheckBox.Text = "Show AoC";
         showAoCCheckBox.UseVisualStyleBackColor = true;
         showAoCCheckBox.CheckedChanged += showAoCCheckBox_CheckedChanged;
         // 
         // showAoCPointsCheckBox
         // 
         showAoCPointsCheckBox.AutoSize = true;
         showAoCPointsCheckBox.Location = new Point(93, 37);
         showAoCPointsCheckBox.Name = "showAoCPointsCheckBox";
         showAoCPointsCheckBox.Size = new Size(117, 19);
         showAoCPointsCheckBox.TabIndex = 4;
         showAoCPointsCheckBox.Text = "Show AoC Points";
         showAoCPointsCheckBox.UseVisualStyleBackColor = true;
         showAoCPointsCheckBox.CheckedChanged += showAoCPointsCheckBox_CheckedChanged;
         // 
         // TestForm
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(946, 693);
         Controls.Add(showAoCPointsCheckBox);
         Controls.Add(showAoCCheckBox);
         Controls.Add(reloadScenarioButton);
         Controls.Add(label1);
         Controls.Add(theMapGridView);
         KeyPreview = true;
         Name = "TestForm";
         Text = "TestForm";
         FormClosed += TestForm_FormClosed;
         Load += TestForm_Load;
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private Controls.MapGridView theMapGridView;
      private Label label1;
      private Button reloadScenarioButton;
      private CheckBox showAoCCheckBox;
      private CheckBox showAoCPointsCheckBox;
   }
}
