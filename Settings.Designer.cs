namespace HTTPMessageSender
{
    partial class Settings
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
            button1 = new Button();
            textBox1 = new TextBox();
            button2 = new Button();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(430, 33);
            button1.Name = "button1";
            button1.Size = new Size(201, 23);
            button1.TabIndex = 0;
            button1.Text = "Select Configuration Folder";
            button1.UseVisualStyleBackColor = true;
            button1.Click += selectFolderOnClick;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 34);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(386, 23);
            textBox1.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(12, 172);
            button2.Name = "button2";
            button2.Size = new Size(196, 23);
            button2.TabIndex = 2;
            button2.Text = "Load New Configuration";
            button2.UseVisualStyleBackColor = true;
            button2.Click += checkFolderOnClick;
            // 
            // panel1
            // 
            panel1.Location = new Point(254, 88);
            panel1.Name = "panel1";
            panel1.Size = new Size(381, 120);
            panel1.TabIndex = 3;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(647, 225);
            Controls.Add(panel1);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Settings";
            Text = "Settings";
            Load += Settings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private Button button2;
        private Panel panel1;
    }
}