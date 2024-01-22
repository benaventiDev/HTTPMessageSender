using CsvHelper.Configuration;
using CsvHelper;
using HTTPMessageSender.Structures;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;
using HTTPMessageSender.http;
using HTTPMessageSender.interfaces;
using HTTPMessageSender.file;
using System.Security.Principal;

namespace HTTPMessageSender
{
    public partial class Form1 : Form, Observer
    {

        
        byte[] crypto_key = new byte[]{
    0xe0, 0x79, 0x65, 0x0d, 0x1d, 0xe0, 0xe1, 0xe4,
    0x87, 0x79, 0xc7, 0x50, 0x6d, 0x48, 0x36, 0xf8,
    0xb9, 0x67, 0x89, 0x02, 0x2e, 0x93, 0xb4, 0xf1,
    0x67, 0xc2, 0xc2, 0xaf, 0x48, 0xab, 0x4e, 0xb5
    };
        byte[] iv = new byte[]
{
    0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef,
    0xfe, 0xdc, 0xba, 0x09, 0x87, 0x65, 0x43, 0x21
};
        private bool repoAlive = false;
        

        private List<string> fileErrors = new List<string>();
        public enum FileType
        {
            MU, REPORT, HEADER
        }
        public List<Report> reportList = null;
        public List<Line> headers = null;
        public List<Line> MUS = null;
        public List<string> errorList = new List<string>();
        public bool isTestMode = false;
        public string MainFolder = "";
        public string DownloadFolder = "";
        public string DateFormat = "dd/MM/yy";

        string configFilePath = ".config.json";
        private string URI_HEADER;
        private string URL_REPORT_VIEW;


        public Form1()
        {

            InitializeComponent();
            InitializeFlowPanels();
            LoadCurrentConfiguration();
            MainFolder = null;
            WarningsLabel.Visible = false;
 
        }


        private void InitializeFlowPanels()
        {

            ReportLayoutPanel.AutoScroll = true;
            ReportLayoutPanel.FlowDirection = FlowDirection.TopDown;
            ReportLayoutPanel.WrapContents = false;
            ReportLayoutPanel.AutoSize = false;
            ReportLayoutPanel.Size = new Size(226, 260); // Set a fixed size for the FlowLayoutPanel

            MUFlowLayoutPanel.AutoScroll = true;
            MUFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            MUFlowLayoutPanel.WrapContents = false;
            MUFlowLayoutPanel.AutoSize = false;
            MUFlowLayoutPanel.Size = new Size(226, 260); // Set a fixed size for the FlowLayoutPanel

            resultFlowLayoutPanel.VerticalScroll.Value = resultFlowLayoutPanel.VerticalScroll.Maximum;
        }
        private void SettingsOnClick(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
            MainFolder = settings.MainFolder;
            DownloadFolder = settings.DownloadsFolder;
            DateFormat = settings.DateFormat;
            int count = settings.getErrorsCount();
            if (count == 0 && !MainFolder.Equals("") && settings.ProcessFiles)
            {
                fileErrors.Clear();
                SetHeadersMUReports();
                // Serialize the data to JSON format
                var data = new { fileName = MainFolder, downloads = DownloadFolder, dateFormat = DateFormat, isTestMode = false };
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                // Write the JSON to a file
                File.WriteAllText(configFilePath, json);
                WarningsLabel.Visible = false;


            }
            else
            {
                MessageBox.Show("Configuration not changed.");
            }
        }

        public static string GetValueFromCsv(string csvContent, string targetValue)
        {
            // Split the CSV content by new lines to get individual rows
            var rows = csvContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var row in rows)
            {
                // Split the row into columns
                var columns = row.Split(',');

                // If the second column's value matches the target value
                if (columns.Length > 1 && columns[1].Trim() == targetValue)
                {
                    return columns[0].Trim();  // Return the value of the first column
                }
            }

            return null; // Return null if no matching value is found
        }


        private void SetHeadersMUReports()
        {
            warningsPanel.Controls.Clear();
            headers = ReadCSV(Path.Combine(MainFolder, "Headers.csv"));
            //headersResult = ReadCSV(Path.Combine(MainFolder, "HeadersResult.csv"));
            //MUS = ReadCSV(Path.Combine(MainFolder, "MUS.csv"));
            List<Line> MusSelected = ReadCSV(Path.Combine(MainFolder, "MUS.csv"));
            var musstr = Crypto.DecryptCsvFileToString(Path.Combine(MainFolder, "mus.dat"), crypto_key, iv);
            MUS = new List<Line>();
            for (int i = 0; i < MusSelected.Count; i++)
            {
                if (MusSelected[i].value == "TRUE")
                {
                    MUS.Add(new Line(MusSelected[i].key, GetValueFromCsv(musstr, MusSelected[i].key)));
                    ;
                }
            }
            reportList = BuildReports(Path.Combine(MainFolder, "reports"));
            ReadURLS();



            if (errorList.Count > 0)
            {
                AddWarnings(errorList);
                errorList.Clear();
            }
            else
            {
                ShowMUs();
                ShowReports();
            }


        }
        private void ReadURLS()
        {
            string urls = File.ReadAllText(Path.Combine(MainFolder, "URL.json"));
            // Deserialize the JSON string into a Dictionary<string, string>
            Dictionary<string, string> configValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(urls);
            URI_HEADER = configValues["URL_HEADER"];
            URL_REPORT_VIEW = configValues["URL_REPORT_VIEW"];
        }
        private void ShowMUs()
        {

            MUFlowLayoutPanel.Controls.Clear();
            for (int i = 0; i < MUS.Count; i++)
            {
                var panelItem = new FancyPanelItem(MUS[i].key, i, FileType.MU, this);
                MUFlowLayoutPanel.Controls.Add(panelItem);
            }



        }

        //TODO shrinkgae cancelado desde las 5pm EST hasta EOD, MOB 
        private void ShowReports()
        {
            ReportLayoutPanel.Controls.Clear();
            for (int i = 0; i < reportList.Count; i++)
            {
                var panelItem = new FancyPanelItem(reportList[i].name, i, FileType.REPORT, this);
                ReportLayoutPanel.Controls.Add(panelItem);
            }

        }

        public List<Report> BuildReports(string report_folder)
        {
            List<Report> reports = new List<Report>();
            string[] csvFiles = Directory.GetFiles(report_folder, "*.csv");
            foreach (var csvFile in csvFiles)
            {
                Report report = new Report();
                report.name = Path.GetFileNameWithoutExtension(csvFile);
                report.lines = ReadCSV(csvFile);
                foreach (var line in report.lines)
                {
                    if (line.value.EndsWith(".json"))
                    {
                        string jsonFilePath = Path.Combine(report_folder, line.value);
                        if (File.Exists(jsonFilePath))
                        {
                            string jsonContent = File.ReadAllText(jsonFilePath);
                            line.value = jsonContent;
                        }
                        else
                        {
                            // Handle the case where the JSON file is not found
                            // For example, log an error or set a default value
                        }
                    }
                }
                reports.Add(report);
            }

            return reports;
        }
        public List<Line> ReadCSV(string filePath)
        {
            List<Line> headers = new List<Line>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Quote = '"'
            };
            try
            {
                using (var fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var textReader = new StreamReader(fs, Encoding.UTF8))
                    using (var csv = new CsvReader(textReader, config))
                    {
                        var data = csv.GetRecords<Line>();
                        foreach (var val in data)
                        {
                            headers.Add(val);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine(ex.Message);
                //MessageBox.Show(ex.Message);
                errorList.Add(ex.Message + Environment.NewLine + Path.GetFileNameWithoutExtension(filePath));
            }

            return headers;
        }


        private void LoadCurrentConfiguration()
        {
            if (File.Exists(configFilePath))
            {

                // Read the contents of the file
                string json = File.ReadAllText(configFilePath);

                try
                {
                    // Deserialize the JSON string into a Dictionary<string, string>
                    Dictionary<string, string> configValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    // Access the config values using the keys
                    MainFolder = configValues["fileName"];
                    DownloadFolder = configValues["downloads"];
                    DateFormat = configValues["dateFormat"];
                    isTestMode = bool.Parse(configValues["isTestMode"]);
                }
                catch (Exception e)
                {
                    fileErrors.Add("Error occured while reading the json file: " + e.Message);
                    WarningsLabel.Visible = true;
                    AddWarnings(fileErrors);
                    return;
                }
                fileErrors.Clear();
                if (MainFolder != null)
                {

                    fileErrors = Settings.CheckFilesExistance(MainFolder, DownloadFolder);
                    if (fileErrors.Count != 0)
                    {
                        WarningsLabel.Visible = true;
                        AddWarnings(fileErrors);
                        return;
                    }
                    else
                    {

                        SetHeadersMUReports();

                    }
                }
                else
                {
                    fileErrors.Add("Configuration folder missing");
                }


            }
            else
            {
                File.WriteAllText(".config.json", "{}");
            }
        }

        public void AddWarnings(List<string> warningMessages)
        {
            foreach (string err in warningMessages)
            {
                MessageBox.Show(err.ToString());
            }
            warningsPanel.Controls.Clear();
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel.WrapContents = false;
            flowLayoutPanel.AutoSize = false;
            flowLayoutPanel.Size = new Size(480, 100); // Set a fixed size for the FlowLayoutPanel

            foreach (string warningMessage in warningMessages)
            {
                Label warningLabel = new Label();
                warningLabel.Text = warningMessage;
                warningLabel.BackColor = Color.Yellow; // set the background color of the warning label
                warningLabel.ForeColor = Color.Red; // set the foreground color of the warning label
                warningLabel.Size = new Size(450, 25); // set the AutoSize property to true so that the label adjusts its size to fit the text
                warningLabel.Padding = new Padding(10, 5, 0, 0); // add some padding to the label to separate it from other controls
                flowLayoutPanel.Controls.Add(warningLabel);
            }
            // Set the VerticalScroll property to the maximum value to ensure it scrolls to the bottom
            flowLayoutPanel.VerticalScroll.Value = flowLayoutPanel.VerticalScroll.Maximum;
            warningsPanel.Controls.Clear();
            warningsPanel.Controls.Add(flowLayoutPanel);
        }

        public class FancyPanelItem : Panel
        {
            public CheckBox checkBox { get; private set; }
            public FancyPanelItem(string fileName, int i, FileType type, Form1 f)
            {
                // Set the background color and border style
                var fileType = type;
                this.BackColor = Color.LightBlue;
                this.BorderStyle = BorderStyle.FixedSingle;
                Form1 form = f;

                // Add the file name label
                var fileNameLabel = new Label
                {
                    Text = fileName,
                    Location = new Point(10, 5),
                    AutoSize = true
                };
                this.Padding = new Padding(15, 0, 0, 0);
                this.Controls.Add(fileNameLabel);

                // Add the checkbox
                checkBox = new CheckBox
                {
                    Text = "",
                    Location = new Point(170, 0),
                    Tag = i // store the index in the Tag property
                };
                checkBox.CheckedChanged += (sender, e) =>
                {
                    CheckBox checkbox = (CheckBox)sender;
                    int index = (int)checkbox.Tag; // get the index from the Tag property
                    if (checkbox.Checked)
                    {
                        FlowLayoutPanel flowLayoutPanel = (fileType == FileType.REPORT ? form.ReportLayoutPanel : form.MUFlowLayoutPanel);
                        bool allChecked = true;


                        foreach (FancyPanelItem panelItem in flowLayoutPanel.Controls.OfType<FancyPanelItem>())
                        {
                            if (panelItem.checkBox != checkBox && !panelItem.checkBox.Checked)
                            {
                                allChecked = false;
                                break;
                            }
                        }

                        if (allChecked)
                        {
                            if (fileType == FileType.REPORT)
                            {
                                form.ReportsCheckBoxk.CheckedChanged -= form.ReportsCheckBoxk_CheckedChanged;
                                form.ReportsCheckBoxk.Checked = true;
                                form.ReportsCheckBoxk.CheckedChanged += form.ReportsCheckBoxk_CheckedChanged;
                            }
                            else if (fileType == FileType.MU)
                            {
                                form.MUCheckBox.CheckedChanged -= form.MUCheckBox_CheckedChanged;
                                form.MUCheckBox.Checked = true;
                                form.MUCheckBox.CheckedChanged += form.MUCheckBox_CheckedChanged;
                            }
                        }
                        if (fileType == FileType.REPORT)
                        {
                            form.reportList[index].check();
                        }
                        else if (fileType == FileType.MU)
                        {
                            form.MUS[index].check();
                        }
                    }
                    else
                    {
                        if (fileType == FileType.REPORT)
                        {
                            form.reportList[index].uncheck();
                            form.ReportsCheckBoxk.CheckedChanged -= form.ReportsCheckBoxk_CheckedChanged;
                            form.ReportsCheckBoxk.Checked = false;
                            form.ReportsCheckBoxk.CheckedChanged += form.ReportsCheckBoxk_CheckedChanged;
                        }
                        else if (fileType == FileType.MU)
                        {
                            form.MUS[index].uncheck();
                            form.MUCheckBox.CheckedChanged -= form.MUCheckBox_CheckedChanged;
                            form.MUCheckBox.Checked = false;
                            form.MUCheckBox.CheckedChanged += form.MUCheckBox_CheckedChanged;
                        }
                    }
                };
                this.Controls.Add(checkBox);

                // Set the size of the panel
                this.Size = new Size(200, checkBox.Bottom);
            }
        }

        private void ExecuteReportsOnClick(object sender, EventArgs e)
        {
            
            if (fileErrors.Count != 0)
            {
                return;
            }
            resultFlowLayoutPanel.Controls.Clear();
            if (downloadsCheckBox.Checked)
            {
                FileDeleter.DeleteDownloads(DownloadFolder);
            }
            ExecuteReports();
        }

        private async void ExecuteReports()
        {
            if (fileErrors.Count != 0)
            {
                return;
            }
            resultFlowLayoutPanel.Controls.Clear();
            outputLabel.Visible = true;
            DateTime pickedDate = dateTimePicker.Value;
            RequestSender rsender = new RequestSender(URI_HEADER, JSessionID1.Text, JSessionID1.Text, LFRSession.Text, headers);
            rsender.subscribe(this);
            foreach (Report report in reportList)
            {
                if (report.isChecked())
                {
                    foreach (Line mu in MUS)
                    {
                        if (mu.isChecked())
                        {
                            rsender.SendRequest(report, mu, pickedDate, DateFormat, isTestMode);
                            // Wait for 1-2 seconds before sending the next request
                            int delayTime = new Random().Next(2500, 3000); // Generate a random delay time between 1000ms and 2000ms
                            await Task.Delay(delayTime);
                        }
                    }
                }
            }
        }

        public void AddResult(int code, string report, string mu)
        {
            Label resultLabel = new();
            resultLabel.Text = "Code: " + code + ";            " + report + " - " + mu;
            if (code == 200)
            {
                resultLabel.BackColor = Color.LightGreen; // set the background color of the warning label
                resultLabel.ForeColor = Color.Black; // set the foreground color of the warning label
            }
            else
            {
                resultLabel.BackColor = Color.Yellow; // set the background color of the warning label
                resultLabel.ForeColor = Color.Red; // set the foreground color of the warning label
            }
            // Set the border style of the label to Bottom
            resultLabel.BorderStyle = BorderStyle.Fixed3D;
            // Set the color of the border to black
            resultLabel.ForeColor = Color.Black;
            resultLabel.Size = new Size(540, 25); // set the AutoSize property to true so that the label adjusts its size to fit the text
            resultLabel.Padding = new Padding(10, 5, 0, 0); // add some padding to the label to separate it from other controls
            resultFlowLayoutPanel.Controls.Add(resultLabel);
        }

        public void ClearResults()
        {
            resultFlowLayoutPanel.Controls.Clear();
        }

        private void MUCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = MUCheckBox.Checked;

            // Iterate over each checkbox in the flow layout panel
            foreach (FancyPanelItem item in MUFlowLayoutPanel.Controls.OfType<FancyPanelItem>())
            {
                // Set the checked state of the checkbox to match the main checkbox
                item.checkBox.Checked = isChecked;
            }
        }

        private void ReportsCheckBoxk_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = ReportsCheckBoxk.Checked;

            // Iterate over each checkbox in the flow layout panel
            foreach (FancyPanelItem item in ReportLayoutPanel.Controls.OfType<FancyPanelItem>())
            {
                // Set the checked state of the checkbox to match the main checkbox
                item.checkBox.Checked = isChecked;
            }

        }


        private void DownloadsDeleteOnClick(object sender, EventArgs e)
        {
            if (!repoAlive)
            {
                return;
            }
            FileDeleter.DeleteDownloads(DownloadFolder);
        }



        private async void DeleteReportsOnlineOnClickAsync(object sender, EventArgs e)
        {
            if (!repoAlive)
            {
                return;
            }
            //MessageBox.Show(WindowsIdentity.GetCurrent().Name);

            //string input = "C:\\Users\\benav\\Documents\\musraw.csv";
            //string output = "C:\\Users\\benav\\Documents\\mus.dat";

            //Crypto.EncryptCsvFile(input, output, crypto_key, iv);

            /*
            //TODO: Check the case when random token is empty
            var request = new HttpRequestMessage(HttpMethod.Post, URL);
            List<Line> bodyLines = new();
            bodyLines.Add(new Line("selectedTabAction", "/showReportList.do?lang=en_CA"));
            //**********************************CONTENT
            // Convert the list to key-value pairs
            IEnumerable<KeyValuePair<string, string>> formData = bodyLines.Select(l => new KeyValuePair<string, string>(l.key, l.value));
            // Encode the form data
            FormUrlEncodedContent content = new FormUrlEncodedContent(formData);
            // Set the content type header
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
            // Set the content length header
            content.Headers.ContentLength = content.ReadAsStringAsync().Result.Length;
            request.Content = content;
            //**********************************Coookies
            CookieContainer cookies = SetCookies(randomToken);
            var handler = new HttpClientHandler { CookieContainer = cookies };
            request.Headers.Add("Cookie", cookies.GetCookieHeader(new Uri(URI_HEADER + "/supv")));
            using var httpClient = new HttpClient(handler);
            //***********************************HEADERS
            foreach (Line line in headersResult)
            {
                httpClient.DefaultRequestHeaders.Add(line.key, line.value);
            }

            //string contentString = await content.ReadAsStringAsync();
            //PrintRequestAsync(contentString, httpClient.DefaultRequestHeaders.ToString(), report.name, mu.key, randomToken);
            //***********************************Sending the HTTP request
            var response = await httpClient.SendAsync(request);
            // Get the response status code
            HttpStatusCode statusCode = response.StatusCode;
            // Read the response body as a string
            string responseBody = await response.Content.ReadAsStringAsync();
            AddResult(((int)statusCode), "", "");*/
        }

        private void DownloadReportsOnlineOnClick(object sender, EventArgs e)
        {

        }


        
    }
}