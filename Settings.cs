using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Configuration;
using HTTPMessageSender.Structures;
using Microsoft.VisualBasic.Logging;

namespace HTTPMessageSender
{
    public partial class Settings : Form
    {

        public List<string> Errors { get; private set; }
        public string MainFolder { get; private set; }
        public bool ProcessFiles { get; private set; }
        public string DateFormat { get; private set; }
        public string DownloadsFolder { get; private set; }
        public Settings()
        {
            InitializeComponent();
            MainFolder = "";
            DownloadsFolder = "";
            Errors = new List<string>();
            ProcessFiles = false;

        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        public int
            getErrorsCount()
        {
            return Errors.Count;
        }
        private void SelectFolderOnClick(object sender, EventArgs e)
        {
            // Create an instance of the FolderBrowserDialog
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // Display the FolderBrowserDialog and wait for the user to select a folder
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected folder path
                string selectedPath = folderBrowserDialog.SelectedPath;
                textBox1.Text = selectedPath;
            }
        }

        public static List<string> CheckFilesExistance(string selectedPath, string DownloadsFolder)
        {
            List<string> errorMessages = new List<string>();

            if (selectedPath.Equals(""))
            {
                errorMessages.Add("Please select a folder that contains the new configuration.");
                return errorMessages;
            }

            //Check if selectedPath exits
            if (!Directory.Exists(Path.Combine(selectedPath)))
            {
                errorMessages.Add("Selected folder does not exist.");
                return errorMessages;
            }
            // Check if files and repors folder exist
            if (!File.Exists(Path.Combine(selectedPath, "MUS.csv")))
            {
                errorMessages.Add("MUS csv file not found.");
            }
            if (!File.Exists(Path.Combine(selectedPath, "Headers.csv")))
            {
                errorMessages.Add("Headers csv file not found.");
            }
            if (!File.Exists(Path.Combine(selectedPath, "URL.json")))
            {
                errorMessages.Add("URL json file not found.");
            }
            if (!Directory.Exists(Path.Combine(DownloadsFolder)))
            {
                errorMessages.Add("Downloads folder not found.");
            }
            if (!Directory.Exists(Path.Combine(selectedPath, "reports")))
            {
                errorMessages.Add("reports folder not found.");
            }
            else
            {
                // Check if the selected folder contains at least one .csv file
                string[] csvFiles = Directory.GetFiles(Path.Combine(selectedPath, "reports"), "*.csv");
                if (csvFiles.Length == 0)
                {
                    errorMessages.Add("No .csv files found inside report folder.");
                }
            }



            return errorMessages;
        }
        private void DisplayErrors(List<string> errorMessages)
        {
            // Create a FlowLayoutPanel control
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel.WrapContents = false;
            flowLayoutPanel.AutoSize = false;
            flowLayoutPanel.Size = new Size(300, 120); // Set a fixed size for the FlowLayoutPanel

            // Loop through the error messages and create a Button control for each one
            for (int i = 0; i < errorMessages.Count; i++)
            {
                Button button = new Button();
                button.Text = errorMessages[i];
                button.Margin = new Padding(0);
                button.BackColor = Color.FromArgb(192, 192, 255);
                button.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 255);
                button.AutoSize = true; // Set AutoSize to true so the button adjusts to the text width
                button.Width = 280;
                flowLayoutPanel.Controls.Add(button);
            }

            // Set the VerticalScroll property to the maximum value to ensure it scrolls to the bottom
            flowLayoutPanel.VerticalScroll.Value = flowLayoutPanel.VerticalScroll.Maximum;

            // Clear any existing controls in the Panel and add the FlowLayoutPanel to it
            panel1.Controls.Clear();
            panel1.Controls.Add(flowLayoutPanel);

            // Show the Panel control
            panel1.Visible = true;
        }
        private void CheckFolderOnClick(object sender, EventArgs e)
        {
            MainFolder = textBox1.Text;
            DownloadsFolder = DownloadTextBox.Text;
            DateFormat = textBoxDate.Text;

            Errors.Clear();
            Errors = CheckFilesExistance(MainFolder, DownloadsFolder);


            if (Errors.Count > 0)
            {
                DisplayErrors(Errors);
            }
            else
            {
                ProcessFiles = true;
                this.Close();
            }
            ;
            /*
            List<Line> headers = ReadCSV(Path.Combine(MainFolder, "Headers.csv"));
            List<Line> MUS = ReadCSV(Path.Combine(MainFolder, "MUS.csv"));
            List<Report> reports = buildReports(Path.Combine(MainFolder, "reports"));
            foreach (var header in headers)
            {
                MessageBox.Show($"{header.key}: {header.value}");
            }

            foreach (var report in reports) {
                MessageBox.Show(report.name);
                foreach(var line in report.lines)
                {
                    MessageBox.Show($"{line.key}: {line.value}");
                }
            }*/

        }

        private void ChooseDownLoadFolderOnClick(object sender, EventArgs e)
        {
            // Create an instance of the FolderBrowserDialog
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // Display the FolderBrowserDialog and wait for the user to select a folder
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected folder path
                string selectedPath = folderBrowserDialog.SelectedPath;
                DownloadTextBox.Text = selectedPath;
            }
        }
    }
}
