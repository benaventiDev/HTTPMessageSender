namespace HTTPMessageSender
{
    partial class Form1
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
            label1 = new Label();
            panel1 = new Panel();
            dateTimePicker = new DateTimePicker();
            label8 = new Label();
            LFRSession = new TextBox();
            JSessionID2 = new TextBox();
            JSessionID1 = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            settingsButton = new Button();
            warningsPanel = new Panel();
            label2 = new Label();
            label3 = new Label();
            WarningsLabel = new Label();
            button2 = new Button();
            resultFlowLayoutPanel = new FlowLayoutPanel();
            MUCheckBox = new CheckBox();
            ReportsCheckBoxk = new CheckBox();
            ReportLayoutPanel = new FlowLayoutPanel();
            MUFlowLayoutPanel = new FlowLayoutPanel();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            outputLabel = new Label();
            button1 = new Button();
            downloadsCheckBox = new CheckBox();
            button3 = new Button();
            button4 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(812, 613);
            label1.Name = "label1";
            label1.Size = new Size(363, 15);
            label1.TabIndex = 0;
            label1.Text = "This Software was designed, tested and coded by Benaventi Fuentes";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonShadow;
            panel1.Controls.Add(dateTimePicker);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(LFRSession);
            panel1.Controls.Add(JSessionID2);
            panel1.Controls.Add(JSessionID1);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Location = new Point(654, 34);
            panel1.Name = "panel1";
            panel1.Size = new Size(646, 237);
            panel1.TabIndex = 1;
            // 
            // dateTimePicker
            // 
            dateTimePicker.Location = new Point(225, 136);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(200, 23);
            dateTimePicker.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(22, 136);
            label8.Name = "label8";
            label8.Size = new Size(31, 15);
            label8.TabIndex = 7;
            label8.Text = "Date";
            // 
            // LFRSession
            // 
            LFRSession.Location = new Point(225, 96);
            LFRSession.Name = "LFRSession";
            LFRSession.Size = new Size(404, 23);
            LFRSession.TabIndex = 6;
            // 
            // JSessionID2
            // 
            JSessionID2.Location = new Point(225, 58);
            JSessionID2.Name = "JSessionID2";
            JSessionID2.Size = new Size(404, 23);
            JSessionID2.TabIndex = 5;
            // 
            // JSessionID1
            // 
            JSessionID1.Location = new Point(225, 21);
            JSessionID1.Name = "JSessionID1";
            JSessionID1.Size = new Size(404, 23);
            JSessionID1.TabIndex = 4;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(22, 96);
            label7.Name = "label7";
            label7.Size = new Size(111, 15);
            label7.TabIndex = 2;
            label7.Text = "LFR_SESSION_STATE";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(22, 58);
            label6.Name = "label6";
            label6.Size = new Size(75, 15);
            label6.TabIndex = 1;
            label6.Text = "JSESSIONID /";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(22, 18);
            label5.Name = "label5";
            label5.Size = new Size(100, 15);
            label5.TabIndex = 0;
            label5.Text = "JSESSIONID /supv";
            // 
            // settingsButton
            // 
            settingsButton.BackgroundImage = Properties.Resources.settings;
            settingsButton.BackgroundImageLayout = ImageLayout.Zoom;
            settingsButton.FlatAppearance.BorderColor = Color.White;
            settingsButton.FlatAppearance.BorderSize = 0;
            settingsButton.FlatStyle = FlatStyle.Flat;
            settingsButton.Location = new Point(1, -3);
            settingsButton.Name = "settingsButton";
            settingsButton.Size = new Size(80, 49);
            settingsButton.TabIndex = 2;
            settingsButton.UseVisualStyleBackColor = true;
            settingsButton.Click += SettingsOnClick;
            // 
            // warningsPanel
            // 
            warningsPanel.Location = new Point(278, 430);
            warningsPanel.Name = "warningsPanel";
            warningsPanel.Size = new Size(348, 164);
            warningsPanel.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 60);
            label2.Name = "label2";
            label2.Size = new Size(47, 15);
            label2.TabIndex = 6;
            label2.Text = "Reports";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(336, 60);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 7;
            label3.Text = "MUs";
            // 
            // WarningsLabel
            // 
            WarningsLabel.AutoSize = true;
            WarningsLabel.Location = new Point(278, 384);
            WarningsLabel.Name = "WarningsLabel";
            WarningsLabel.Size = new Size(68, 15);
            WarningsLabel.TabIndex = 8;
            WarningsLabel.Text = "Warning(s):";
            // 
            // button2
            // 
            button2.Location = new Point(34, 560);
            button2.Name = "button2";
            button2.Size = new Size(160, 34);
            button2.TabIndex = 9;
            button2.Text = "Execute Selected Steps";
            button2.UseVisualStyleBackColor = true;
            button2.Click += ExecuteReportsOnClick;
            // 
            // resultFlowLayoutPanel
            // 
            resultFlowLayoutPanel.AutoScroll = true;
            resultFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            resultFlowLayoutPanel.Location = new Point(654, 335);
            resultFlowLayoutPanel.Name = "resultFlowLayoutPanel";
            resultFlowLayoutPanel.Size = new Size(646, 259);
            resultFlowLayoutPanel.TabIndex = 10;
            resultFlowLayoutPanel.WrapContents = false;
            // 
            // MUCheckBox
            // 
            MUCheckBox.AutoSize = true;
            MUCheckBox.Location = new Point(509, 61);
            MUCheckBox.Name = "MUCheckBox";
            MUCheckBox.Size = new Size(15, 14);
            MUCheckBox.TabIndex = 11;
            MUCheckBox.UseVisualStyleBackColor = true;
            MUCheckBox.CheckedChanged += MUCheckBox_CheckedChanged;
            // 
            // ReportsCheckBoxk
            // 
            ReportsCheckBoxk.AutoSize = true;
            ReportsCheckBoxk.Location = new Point(195, 59);
            ReportsCheckBoxk.Name = "ReportsCheckBoxk";
            ReportsCheckBoxk.Size = new Size(15, 14);
            ReportsCheckBoxk.TabIndex = 12;
            ReportsCheckBoxk.UseVisualStyleBackColor = true;
            ReportsCheckBoxk.CheckedChanged += ReportsCheckBoxk_CheckedChanged;
            // 
            // ReportLayoutPanel
            // 
            ReportLayoutPanel.Location = new Point(23, 89);
            ReportLayoutPanel.Name = "ReportLayoutPanel";
            ReportLayoutPanel.Size = new Size(260, 260);
            ReportLayoutPanel.TabIndex = 0;
            // 
            // MUFlowLayoutPanel
            // 
            MUFlowLayoutPanel.Location = new Point(336, 89);
            MUFlowLayoutPanel.Name = "MUFlowLayoutPanel";
            MUFlowLayoutPanel.Size = new Size(260, 260);
            MUFlowLayoutPanel.TabIndex = 0;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(230, 430);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(15, 14);
            checkBox1.TabIndex = 13;
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.Visible = false;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(230, 485);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(15, 14);
            checkBox2.TabIndex = 15;
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.Visible = false;
            // 
            // outputLabel
            // 
            outputLabel.AutoSize = true;
            outputLabel.Location = new Point(654, 306);
            outputLabel.Name = "outputLabel";
            outputLabel.Size = new Size(45, 15);
            outputLabel.TabIndex = 16;
            outputLabel.Text = "Output";
            outputLabel.Visible = false;
            // 
            // button1
            // 
            button1.Location = new Point(34, 371);
            button1.Name = "button1";
            button1.Size = new Size(186, 40);
            button1.TabIndex = 17;
            button1.Text = "Delete Reports From Local Machine";
            button1.UseVisualStyleBackColor = true;
            button1.Click += DownloadsDeleteOnClick;
            // 
            // downloadsCheckBox
            // 
            downloadsCheckBox.AutoSize = true;
            downloadsCheckBox.Location = new Point(230, 385);
            downloadsCheckBox.Name = "downloadsCheckBox";
            downloadsCheckBox.Size = new Size(15, 14);
            downloadsCheckBox.TabIndex = 18;
            downloadsCheckBox.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(34, 417);
            button3.Name = "button3";
            button3.Size = new Size(182, 38);
            button3.TabIndex = 19;
            button3.Text = "Delete Reports From Webby";
            button3.UseVisualStyleBackColor = true;
            button3.UseWaitCursor = true;
            button3.Visible = false;
            button3.Click += DeleteReportsOnlineOnClickAsync;
            // 
            // button4
            // 
            button4.Location = new Point(34, 471);
            button4.Name = "button4";
            button4.Size = new Size(182, 40);
            button4.TabIndex = 20;
            button4.Text = "Downloads Reports From Webby";
            button4.UseVisualStyleBackColor = true;
            button4.Visible = false;
            button4.Click += DownloadReportsOnlineOnClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1390, 637);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(downloadsCheckBox);
            Controls.Add(button1);
            Controls.Add(outputLabel);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(MUFlowLayoutPanel);
            Controls.Add(ReportLayoutPanel);
            Controls.Add(ReportsCheckBoxk);
            Controls.Add(MUCheckBox);
            Controls.Add(resultFlowLayoutPanel);
            Controls.Add(button2);
            Controls.Add(WarningsLabel);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(warningsPanel);
            Controls.Add(settingsButton);
            Controls.Add(panel1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Report Generator";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private Button settingsButton;
        private Panel warningsPanel;
        private Label label2;
        private Label label3;
        private Label WarningsLabel;
        private Button button2;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox LFRSession;
        private TextBox JSessionID2;
        private TextBox JSessionID1;
        private FlowLayoutPanel resultFlowLayoutPanel;
        private CheckBox MUCheckBox;
        private CheckBox ReportsCheckBoxk;
        private FlowLayoutPanel ReportLayoutPanel;
        private FlowLayoutPanel MUFlowLayoutPanel;
        private Label label8;
        private DateTimePicker dateTimePicker;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Label outputLabel;
        private Button button1;
        private CheckBox downloadsCheckBox;
        private Button button3;
        private Button button4;
    }
}