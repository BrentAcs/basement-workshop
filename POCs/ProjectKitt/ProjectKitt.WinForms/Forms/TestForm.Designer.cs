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
         var mapGrid2 = new Models.MapGrid();
         var resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
         var mapGridViewOptions2 = new Controls.MapGridViewOptions();
         var gridOptions2 = new Controls.MapGridViewOptions.GridOptions();
         var fontOptions2 = new Controls.MapGridViewOptions.FontOptions();
         theMapGridView = new Controls.MapGridView();
         label1 = new Label();
         SuspendLayout();
         // 
         // theMapGridView
         // 
         theMapGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
         theMapGridView.AutoScroll = true;
         theMapGridView.AutoSize = true;
         theMapGridView.Location = new Point(12, 70);
         mapGrid2.Objects = (List<Models.IMapGridObject>)resources.GetObject("mapGrid2.Objects");
         mapGrid2.Size = new SizeF(735000F, 1000000F);
         theMapGridView.MapGrid = mapGrid2;
         theMapGridView.Name = "theMapGridView";
         theMapGridView.ScaleFactor = Services.ScaleFactor.OneToOne;
         theMapGridView.Size = new Size(917, 603);
         theMapGridView.TabIndex = 0;
         gridOptions2.Color = Color.DarkGray;
         fontOptions2.Color = Color.LimeGreen;
         fontOptions2.Name = "Arial";
         fontOptions2.Size = 12;
         fontOptions2.Style = FontStyle.Regular;
         gridOptions2.Font = fontOptions2;
         gridOptions2.HeavyStep = 250;
         gridOptions2.Step = 50;
         gridOptions2.Visible = true;
         gridOptions2.Width = 1;
         mapGridViewOptions2.Grid = gridOptions2;
         theMapGridView.ViewOptions = mapGridViewOptions2;
         theMapGridView.ViewPortOrigin = (PointF)resources.GetObject("theMapGridView.ViewPortOrigin");
         theMapGridView.ScaleFactorChanged += theMapGridView_ScaleFactorChanged;
         // 
         // label1
         // 
         label1.AutoSize = true;
         label1.Location = new Point(12, 19);
         label1.Name = "label1";
         label1.Size = new Size(38, 15);
         label1.TabIndex = 1;
         label1.Text = "label1";
         // 
         // TestForm
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(941, 685);
         Controls.Add(label1);
         Controls.Add(theMapGridView);
         KeyPreview = true;
         Name = "TestForm";
         Text = "TestForm";
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private Controls.MapGridView theMapGridView;
      private Label label1;
   }
}
