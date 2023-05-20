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
         SuspendLayout();
         // 
         // theMapGridView
         // 
         theMapGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
         theMapGridView.AutoScroll = true;
         theMapGridView.AutoSize = true;
         theMapGridView.Location = new Point(12, 70);
         theMapGridView.Name = "theMapGridView";
         theMapGridView.Size = new Size(917, 603);
         theMapGridView.TabIndex = 0;
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
         FormClosed += TestForm_FormClosed;
         Load += TestForm_Load;
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private Controls.MapGridView theMapGridView;
      private Label label1;
   }
}
