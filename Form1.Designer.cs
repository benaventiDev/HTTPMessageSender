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
            label4 = new Label();
            button2 = new Button();
            resultFlowLayoutPanel = new FlowLayoutPanel();
            MUCheckBox = new CheckBox();
            ReportsCheckBoxk = new CheckBox();
            ReportLayoutPanel = new FlowLayoutPanel();
            MUFlowLayoutPanel = new FlowLayoutPanel();
            checkBox1 = new CheckBox();
            label9 = new Label();
            checkBox2 = new CheckBox();
            label10 = new Label();
            button1 = new Button();
            checkBox3 = new CheckBox();
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
            panel1.Location = new Point(529, 31);
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
            label7.Size = new Size(152, 15);
            label7.TabIndex = 2;
            label7.Text = "LFR_SESSION_STATE_194630";
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
            warningsPanel.Location = new Point(23, 423);
            warningsPanel.Name = "warningsPanel";
            warningsPanel.Size = new Size(500, 100);
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
            label3.Location = new Point(278, 60);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 7;
            label3.Text = "MUs";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(23, 387);
            label4.Name = "label4";
            label4.Size = new Size(68, 15);
            label4.TabIndex = 8;
            label4.Text = "Warning(s):";
            // 
            // button2
            // 
            button2.Location = new Point(23, 560);
            button2.Name = "button2";
            button2.Size = new Size(160, 34);
            button2.TabIndex = 9;
            button2.Text = "Execute Reports";
            button2.UseVisualStyleBackColor = true;
            button2.Click += ExecuteReportsOnClick;
            // 
            // resultFlowLayoutPanel
            // 
            resultFlowLayoutPanel.AutoScroll = true;
            resultFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            resultFlowLayoutPanel.Location = new Point(551, 335);
            resultFlowLayoutPanel.Name = "resultFlowLayoutPanel";
            resultFlowLayoutPanel.Size = new Size(601, 259);
            resultFlowLayoutPanel.TabIndex = 10;
            resultFlowLayoutPanel.WrapContents = false;
            // 
            // MUCheckBox
            // 
            MUCheckBox.AutoSize = true;
            MUCheckBox.Location = new Point(462, 60);
            MUCheckBox.Name = "MUCheckBox";
            MUCheckBox.Size = new Size(15, 14);
            MUCheckBox.TabIndex = 11;
            MUCheckBox.UseVisualStyleBackColor = true;
            MUCheckBox.CheckedChanged += MUCheckBox_CheckedChanged;
            // 
            // ReportsCheckBoxk
            // 
            ReportsCheckBoxk.AutoSize = true;
            ReportsCheckBoxk.Location = new Point(205, 60);
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
            ReportLayoutPanel.Size = new Size(222, 260);
            ReportLayoutPanel.TabIndex = 0;
            // 
            // MUFlowLayoutPanel
            // 
            MUFlowLayoutPanel.Location = new Point(278, 89);
            MUFlowLayoutPanel.Name = "MUFlowLayoutPanel";
            MUFlowLayoutPanel.Size = new Size(225, 260);
            MUFlowLayoutPanel.TabIndex = 0;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(734, 278);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(15, 14);
            checkBox1.TabIndex = 13;
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(537, 277);
            label9.Name = "label9";
            label9.Size = new Size(131, 15);
            label9.TabIndex = 14;
            label9.Text = "Delete Previous Reports";
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(734, 304);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(15, 14);
            checkBox2.TabIndex = 15;
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(537, 303);
            label10.Name = "label10";
            label10.Size = new Size(144, 15);
            label10.TabIndex = 16;
            label10.Text = "Download after generated";
            // 
            // button1
            // 
            button1.Location = new Point(966, 277);
            button1.Name = "button1";
            button1.Size = new Size(186, 40);
            button1.TabIndex = 17;
            button1.Text = "Delete Reports From Local Machine";
            button1.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(1160, 291);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(15, 14);
            checkBox3.TabIndex = 18;
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1187, 637);
            Controls.Add(checkBox3);
            Controls.Add(button1);
            Controls.Add(label10);
            Controls.Add(checkBox2);
            Controls.Add(label9);
            Controls.Add(checkBox1);
            Controls.Add(MUFlowLayoutPanel);
            Controls.Add(ReportLayoutPanel);
            Controls.Add(ReportsCheckBoxk);
            Controls.Add(MUCheckBox);
            Controls.Add(resultFlowLayoutPanel);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(warningsPanel);
            Controls.Add(settingsButton);
            Controls.Add(panel1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
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
        private Label label4;
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
        private Label label9;
        private CheckBox checkBox2;
        private Label label10;
        private Button button1;
        private CheckBox checkBox3;
    }
}