namespace ProjectKitt.WinForms.Forms
{
   partial class MainForm
   {
      /// <summary>
      ///  Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      ///  Clean up any resources being used.
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
      ///  Required method for Designer support - do not modify
      ///  the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         button1 = new Button();
         scaleListBox = new ListBox();
         SuspendLayout();
         // 
         // button1
         // 
         button1.Location = new Point(12, 12);
         button1.Name = "button1";
         button1.Size = new Size(75, 23);
         button1.TabIndex = 0;
         button1.Text = "button1";
         button1.UseVisualStyleBackColor = true;
         button1.Click += button1_Click;
         // 
         // scaleListBox
         // 
         scaleListBox.FormattingEnabled = true;
         scaleListBox.ItemHeight = 15;
         scaleListBox.Location = new Point(12, 41);
         scaleListBox.Name = "scaleListBox";
         scaleListBox.Size = new Size(90, 109);
         scaleListBox.TabIndex = 1;
         scaleListBox.SelectedIndexChanged += scaleListBox_SelectedIndexChanged;
         // 
         // MainForm
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(800, 450);
         Controls.Add(scaleListBox);
         Controls.Add(button1);
         Name = "MainForm";
         Text = "Form1";
         SizeChanged += MainForm_SizeChanged;
         Paint += MainForm_Paint;
         ResumeLayout(false);
      }

      #endregion

      private Button button1;
      private ListBox scaleListBox;
   }
}
