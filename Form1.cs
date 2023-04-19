using CsvHelper.Configuration;
using CsvHelper;
using HTTPMessageSender.Structures;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using static System.Windows.Forms.LinkLabel;
using System.Text.RegularExpressions;

namespace HTTPMessageSender
{
    public partial class Form1 : Form
    {
        public enum FileType
        {
            MU, REPORT, HEADER
        }
        public List<Report> reportList = null;
        public List<Line> headers = null;
        public List<Line> MUS = null;
        public List<string> errorList = new List<string>();
        public string MainFolder;
        string configFilePath = ".config.json";
        private string URI_HEADER = "https://wfm-telus.corp.ads";
        //private string URL = "https://wfm-telus.corp.ads/supv/ConformanceByIntervalReport.do";
        private string URL = "http://192.168.1.22:5000";
        public Form1()
        {
            InitializeComponent();
            InitializeFlowPanels();
            LoadCurrentConfiguration();
            MainFolder = null;

            //TODO load from configuration

            /*JSessionID1.Text = "SessionID1";
            JSessionID2.Text = "SessionID2";
            LFRSession.Text = "LFRSession";*/
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
            int count = settings.getErrorsCount();
            if (count == 0 && !MainFolder.Equals("") && settings.processFiles)
            {
                SetHeadersMUReports();
                // Serialize the data to JSON format
                var data = new { fileName = MainFolder };
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                // Write the JSON to a file
                File.WriteAllText(configFilePath, json);

            }
            else
            {
                MessageBox.Show("Configuration not changed.");
            }
        }


        private void SetHeadersMUReports()
        {
            warningsPanel.Controls.Clear();
            headers = ReadCSV(Path.Combine(MainFolder, "Headers.csv"));
            MUS = ReadCSV(Path.Combine(MainFolder, "MUS.csv"));
            reportList = BuildReports(Path.Combine(MainFolder, "reports"));
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

                // Deserialize the JSON string into a Dictionary<string, string>
                Dictionary<string, string> configValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                try
                {
                    // Access the config values using the keys
                    MainFolder = configValues["fileName"];
                }
                catch (Exception e)
                {
                    List<string> errors = new List<string>();
                    errors.Add(e.Message);
                    AddWarnings(errors);
                    return;
                }
                if (MainFolder != null)
                {

                    Settings settings = new Settings();
                    List<string> fileErrors = settings.checkFilesExistance(MainFolder);
                    if (fileErrors.Count != 0)
                    {
                        AddWarnings(fileErrors);
                        return;
                    }
                    else
                    {
                        SetHeadersMUReports();

                    }
                }


            }
            else
            {
                File.WriteAllText(".config.json", "{}");
            }
        }

        public void AddWarnings(List<string> warningMessages)
        {
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
                var fileNameLabel = new Label();
                fileNameLabel.Text = fileName;
                fileNameLabel.Location = new Point(10, 5);
                fileNameLabel.AutoSize = true;
                this.Padding = new Padding(15, 0, 0, 0);
                this.Controls.Add(fileNameLabel);

                // Add the checkbox
                checkBox = new CheckBox();
                checkBox.Text = "";
                checkBox.Location = new Point(180, 0);
                checkBox.Tag = i; // store the index in the Tag property
                checkBox.CheckedChanged += (sender, e) =>
                {
                    CheckBox checkbox = (CheckBox)sender;
                    int index = (int)checkbox.Tag; // get the index from the Tag property
                    if (checkbox.Checked)
                    {
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
                this.Size = new Size(210, checkBox.Bottom);
            }
        }

        private void ExecuteReportsOnClick(object sender, EventArgs e)
        {
            ExecuteReports();
        }

        private async void ExecuteReports()
        {
            resultFlowLayoutPanel.Controls.Clear();
            foreach (Report report in reportList)
            {
                if (report.isChecked())
                {
                    foreach (Line mu in MUS)
                    {
                        if (mu.isChecked())
                        {
                            SendRequest(report, mu);
                            // Wait for 1-2 seconds before sending the next request
                            int delayTime = new Random().Next(1000, 2000); // Generate a random delay time between 1000ms and 2000ms
                            await Task.Delay(delayTime);
                        }
                    }
                }
            }
        }


        private async void SendRequest(Report report, Line mu)//string url, Dictionary<string, string> headers, string body)
        {
            {
                try
                {
                    string randomToken = GetRandomToken();
                    //setCookies(randomToken);


                    //*******************************BODY
                    DateTime now = DateTime.UtcNow;
                    TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(now, easternTimeZone);
                    string easternDateString = easternTime.ToString("dd/MM/yy");


                    DateTime date = dateTimePicker.Value;
                    string dateString = date.ToString("dd/MM/yy");
                    string monthName = date.ToString("MMMM").ToUpper();
                    var request = new HttpRequestMessage(HttpMethod.Post, URL);
                    List<Line> reportLines = new();
                    reportLines.AddRange(report.lines);
                    reportLines.Add(new Line("wfm_csrf_token", randomToken));
                    reportLines.Add(new Line("muIdParam", mu.value));
                    reportLines.Add(new Line("muNameParam", mu.value));
                    reportLines.Add(new Line("stAbsDate", dateString));
                    reportLines.Add(new Line("endAbsDate", dateString));
                    reportLines.Add(new Line("yearlyByOrdinalMonth", monthName));
                    reportLines.Add(new Line("scheduleStartDate", easternDateString));


                    //**********************************CONTENT
                    // Convert the list to key-value pairs
                    IEnumerable<KeyValuePair<string, string>> formData = reportLines.Select(l => new KeyValuePair<string, string>(l.key, l.value));
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
                    foreach (Line line in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(line.key, line.value);
                    }

                    string contentString = await content.ReadAsStringAsync();
                    printRequestAsync(contentString, httpClient.DefaultRequestHeaders.ToString());
                    //***********************************Sending the HTTP request
                    var response = await httpClient.SendAsync(request);
                    // Get the response status code
                    HttpStatusCode statusCode = response.StatusCode;
                    // Read the response body as a string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    AddResult(((int)statusCode), report.name, mu.key);

                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur
                    //Console.WriteLine($"Exception occurred: {ex.Message}");
                    //MessageBox.Show(ex.Message);
                    AddResult(999, report.name, mu.key + " " + ex.Message);
                }
            }
        }

        private void printRequestAsync(string content, string headers)
        {
            using (StreamWriter writer = new StreamWriter("request.txt"))
            {
                writer.WriteLine("Headers:");
                // Save the headers and content to a file
                writer.Write(headers);
                writer.WriteLine();
                writer.WriteLine("Content:");
                writer.WriteLine(content);
                //"request.txt", headers + Environment.NewLine + " Content: " + content);
            }
        }
        private CookieContainer SetCookies(string random)
        {
            //HttpClientHandler handler = new HttpClientHandler();
            //handler.CookieContainer = new CookieContainer();
            // Create a CookieContainer to store cookies
            var cookies = new CookieContainer();
            Cookie cookie1 = new("JSESSIONID", JSessionID1.Text)
            {
                Expires = DateTime.Now.AddDays(2),
                Domain = "wfm-telus.corp.ads",
                Path = "/supv"
            };

            Cookie cookie2 = new("JSESSIONID", JSessionID2.Text)
            {
                Expires = DateTime.Now.AddDays(2),
                Domain = "wfm-telus.corp.ads",
                Path = "/"
            };

            Cookie cookie3 = new("LFR_SESSION_STATE_194630", LFRSession.Text)
            {
                Expires = DateTime.Now.AddDays(2),
                Domain = "wfm-telus.corp.ads",
                Path = "/"
            };

            Cookie cookie4 = new("reportFormatTypeCookie", "XLS")
            {
                Expires = DateTime.Now.AddDays(2),
                Domain = "wfm-telus.corp.ads",
                Path = "/"
            };

            Cookie cookie5 = new("wfm_csrf_token", random)
            {
                Expires = DateTime.Now.AddDays(2),
                Domain = "wfm-telus.corp.ads",
                Path = "/"
            };
            // Add cookies to the cookie container
            cookies.Add(new Uri(URI_HEADER + "/supv"), cookie1);
            cookies.Add(new Uri(URI_HEADER), cookie2);
            cookies.Add(new Uri(URI_HEADER), cookie3);
            cookies.Add(new Uri(URI_HEADER), cookie4);
            cookies.Add(new Uri(URI_HEADER), cookie5);
            return cookies;
        }

        public static string GetRandomToken()
        {
            /*Random random = new Random();
            string randomString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            string randomSubstring = randomString.Substring(randomString.Length - 8);
            return randomSubstring;*/


            Random random = new Random();
            string randomString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            string randomSubstring = randomString.Substring(randomString.Length - 8);

            // Remove any non-alphanumeric characters
            Regex regex = new Regex("[^a-zA-Z0-9]");
            randomSubstring = regex.Replace(randomSubstring, "");

            return randomSubstring;
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
    }
}