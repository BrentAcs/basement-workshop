namespace ProjectKitt.WinForms.Controls
{
   partial class MapGridView
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

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         thePanel = new Panel();
         SuspendLayout();
         // 
         // thePanel
         // 
         thePanel.BackColor = Color.Black;
         thePanel.Dock = DockStyle.Fill;
         thePanel.Location = new Point(0, 0);
         thePanel.Name = "thePanel";
         thePanel.Size = new Size(376, 393);
         thePanel.TabIndex = 0;
         thePanel.Paint += thePanel_Paint;
         // 
         // MapGridView
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         AutoScroll = true;
         AutoSize = true;
         Controls.Add(thePanel);
         Name = "MapGridView";
         Size = new Size(376, 393);
         Load += TacticalGridView_Load;
         SizeChanged += MapGridView_SizeChanged;
         ResumeLayout(false);
      }

      #endregion

      private Panel thePanel;
   }
}
