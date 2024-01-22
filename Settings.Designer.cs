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
            DownloadTextBox = new TextBox();
            button3 = new Button();
            textBoxDate = new TextBox();
            label1 = new Label();
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
            button1.Click += SelectFolderOnClick;
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
            button2.Location = new Point(16, 224);
            button2.Name = "button2";
            button2.Size = new Size(196, 23);
            button2.TabIndex = 2;
            button2.Text = "Load New Configuration";
            button2.UseVisualStyleBackColor = true;
            button2.Click += CheckFolderOnClick;
            // 
            // panel1
            // 
            panel1.Location = new Point(397, 127);
            panel1.Name = "panel1";
            panel1.Size = new Size(381, 120);
            panel1.TabIndex = 3;
            // 
            // DownloadTextBox
            // 
            DownloadTextBox.Location = new Point(16, 74);
            DownloadTextBox.Name = "DownloadTextBox";
            DownloadTextBox.Size = new Size(382, 23);
            DownloadTextBox.TabIndex = 4;
            // 
            // button3
            // 
            button3.Location = new Point(430, 74);
            button3.Name = "button3";
            button3.Size = new Size(201, 23);
            button3.TabIndex = 5;
            button3.Text = "Select Download Folder";
            button3.UseVisualStyleBackColor = true;
            button3.Click += ChooseDownLoadFolderOnClick;
            // 
            // textBoxDate
            // 
            textBoxDate.AcceptsTab = true;
            textBoxDate.Location = new Point(24, 120);
            textBoxDate.Name = "textBoxDate";
            textBoxDate.Size = new Size(114, 23);
            textBoxDate.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(171, 121);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 7;
            label1.Text = "Date Format";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(781, 283);
            Controls.Add(label1);
            Controls.Add(textBoxDate);
            Controls.Add(button3);
            Controls.Add(DownloadTextBox);
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
        private TextBox DownloadTextBox;
        private Button button3;
        private TextBox textBoxDate;
        private Label label1;
    }
}