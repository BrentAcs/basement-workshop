﻿namespace ProjectKitt.WinForms.Forms
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
         var mapGrid1 = new Models.MapGrid();
         var resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
         var mapGridViewOptions1 = new Controls.MapGridViewOptions();
         var gridOptions1 = new Controls.MapGridViewOptions.GridOptions();
         var fontOptions1 = new Controls.MapGridViewOptions.FontOptions();
         theMapGridView = new Controls.MapGridView();
         SuspendLayout();
         // 
         // theMapGridView
         // 
         theMapGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
         theMapGridView.AutoScroll = true;
         theMapGridView.AutoSize = true;
         theMapGridView.Location = new Point(12, 12);
         mapGrid1.Objects = (List<Models.IMapGridObject>)resources.GetObject("mapGrid1.Objects");
         mapGrid1.Size = new SizeF(735000F, 1000000F);
         theMapGridView.MapGrid = mapGrid1;
         theMapGridView.Name = "theMapGridView";
         theMapGridView.ScaleFactor = Services.ScaleFactor.OneToOneThousand;
         theMapGridView.Size = new Size(776, 426);
         theMapGridView.TabIndex = 0;
         gridOptions1.Color = Color.DarkGray;
         fontOptions1.Color = Color.LimeGreen;
         fontOptions1.Name = "Arial";
         fontOptions1.Size = 9;
         fontOptions1.Style = FontStyle.Regular;
         gridOptions1.Font = fontOptions1;
         gridOptions1.HeavyStep = 500;
         gridOptions1.Step = 50;
         gridOptions1.Visible = true;
         gridOptions1.Width = 1;
         mapGridViewOptions1.Grid = gridOptions1;
         theMapGridView.ViewOptions = mapGridViewOptions1;
         theMapGridView.ViewPortOrigin = (PointF)resources.GetObject("theMapGridView.ViewPortOrigin");
         // 
         // TestForm
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(800, 450);
         Controls.Add(theMapGridView);
         KeyPreview = true;
         Name = "TestForm";
         Text = "TestForm";
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private Controls.MapGridView theMapGridView;
   }
}
