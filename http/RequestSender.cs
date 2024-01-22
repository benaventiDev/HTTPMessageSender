using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HTTPMessageSender.interfaces;
using HTTPMessageSender.Structures;
using System.IO;


namespace HTTPMessageSender.http
{
    public class RequestSender
    {
        private string URI_HEADER;
        private string JSessionID1;
        private string JSessionID2;
        private string LFRSession;
        public List<Line> headers = null;

        List<Observer> observers = new List<Observer>();
        public RequestSender(string URI_HEADER,
        string JSessionID1,
        string JSessionID2,
        string LFRSession,
        List<Line> headers) { 
            this.URI_HEADER = URI_HEADER;
            this.JSessionID1 = JSessionID1;
            this.JSessionID2 = JSessionID2;
            this.LFRSession = LFRSession;
            this.headers = headers;

        }
        public void subscribe(Observer observer) {
            observers.Add(observer);
        }
        public void unsubscribe(Observer observer) {
            observers.Remove(observer);
        }

        public async void SendRequest(Report report, Line mu, DateTime pickedDate, string dateFormat, Boolean printRequest)
        {
            {

                try
                {
                    string URL = "";
                    var randomToken = TokenGenerator.GetRandomToken();

                    //*******************************BODY
                    DateTime now = DateTime.UtcNow;
                    TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(now, easternTimeZone);
                    string easternDateString = easternTime.ToString(dateFormat); // easternTime.ToString("dd/MM/yy");
                    int easterDay = easternTime.Day;


                    string dateString = pickedDate.ToString(dateFormat); //pickedDate.ToString("dd/MM/yy");
                    string monthName = pickedDate.ToString("MMMM").ToUpper();
                    int pickedDay = easternTime.Day;

                    DateTime todayDate = DateTime.Now;
                    string todayString = pickedDate.ToString(dateFormat); //pickedDate.ToString("dd/MM/yy");

                    
                    List<Line> reportLines = new();
                    reportLines.AddRange(report.lines);
                    reportLines.Add(new Line("wfm_csrf_token", randomToken));
                    reportLines.Add(new Line("muIdParam", mu.value));
                    reportLines.Add(new Line("muNameParam", mu.value));
                    Line urlLine = report.lines.FirstOrDefault(line => line.key == "URL");
                    if (urlLine != null)
                    {
                        URL = urlLine.value;
                        //report.lines.Remove(urlLine);
                    }
                    var request = new HttpRequestMessage(HttpMethod.Post, URL);
                    //reportLines.Add(new Line("stAbsDate", dateString));
                    //reportLines.Add(new Line("endAbsDate", dateString));
                    if (!reportLines.Any(rl => rl.key == "stAbsDate"))
                    {
                        reportLines.Add(new Line("stAbsDate", dateString));
                    }

                    if (!reportLines.Any(rl => rl.key == "endAbsDate"))
                    {
                        reportLines.Add(new Line("endAbsDate", dateString));
                    }
                    if (!reportLines.Any(rl => rl.key == "scheduleStartDate"))
                    {
                        reportLines.Add(new Line("scheduleStartDate", dateString));
                    }


                    reportLines.Add(new Line("yearlyByOrdinalMonth", monthName));
                    reportLines.Add(new Line("yearlyMonth", monthName));
                    //reportLines.Add(new Line("scheduleStartDate", easternDateString));


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
                    //request.Headers.Add("refer", URL);
                    using var httpClient = new HttpClient(handler);
                    //***********************************HEADERS
                    foreach (Line line in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(line.key, line.value);
                    }
                    //httpClient.DefaultRequestHeaders.Add("Referer", URL);
                    // 
                    string contentString = await content.ReadAsStringAsync();
                    if (printRequest)
                    {                   
                        PrintRequestAsync(contentString, httpClient.DefaultRequestHeaders.ToString(), 
                            report.name, mu.key, randomToken, URL);
                    }
                    //***********************************Sending the HTTP request
                    var response = await httpClient.SendAsync(request);
                    // Get the response status code
                    HttpStatusCode statusCode = response.StatusCode;
                    // Read the response body as a string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    foreach (Observer observer in observers) {
                        observer.AddResult(((int)statusCode), report.name, mu.key);
                    }
                    

                }
                catch (Exception ex)
                {
                    foreach (Observer observer in observers)
                    {
                        if (printRequest)
                        {
                            PrintRequestAsync("", "",
                            report.name, mu.key, "", ex.Message);
                        }
                        observer.AddResult(999, report.name, mu.key + " " + ex.Message);
                    }
                }
            }

        }

        private CookieContainer SetCookies(string random)
        {
            //HttpClientHandler handler = new HttpClientHandler();
            //handler.CookieContainer = new CookieContainer();
            // Create a CookieContainer to store cookies
            var cookies = new CookieContainer();
            Cookie cookie1 = new("JSESSIONID", JSessionID1)
            {
                Expires = DateTime.Now.AddDays(2),
                Domain = "wfm-telus.corp.ads",
                Path = "/supv"
            };

            Cookie cookie2 = new("JSESSIONID", JSessionID2)
            {
                Expires = DateTime.Now.AddDays(2),
                Domain = "wfm-telus.corp.ads",
                Path = "/"
            };

            Cookie cookie3 = new("LFR_SESSION_STATE_194630", LFRSession)
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

        private static void PrintRequestAsync(string content, string headers, string reportName, string muName, string randomToken, string extraComments)
        {
            using (StreamWriter writer = new StreamWriter("results/request" + randomToken + "+" + reportName + " - " + muName + ".txt"))
            {
                writer.WriteLine("Headers:");
                // Save the headers and content to a file
                writer.Write(headers);
                writer.WriteLine();
                writer.WriteLine("Content:");
                writer.WriteLine(content);
                writer.WriteLine(extraComments);
                //"request.txt", headers + Environment.NewLine + " Content: " + content);
            }
        }


    }
}
