using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WebAPIClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            textBox4.Text = string.Empty;
            if (checkURL(textBox1.Text))
            {
                textBox4.Text = "Waiting for response...";
                var baseAddress = new Uri(textBox1.Text + "/" + textBox3.Text);
                var cookieContainer = new CookieContainer();
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    cookieContainer.Add(baseAddress, new Cookie("API-Key", textBox2.Text));
                    var result = client.GetAsync("").Result;
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            label5.Text = "200:OK";
                            break;
                        case HttpStatusCode.BadRequest:
                            label5.Text = "400:Bad request";
                            break;
                        case HttpStatusCode.Unauthorized:
                            label5.Text = "401:Unauthorized request";
                            break;
                        case HttpStatusCode.NotFound:
                            label5.Text = "404:Resource not found";
                            break;
                        default:
                            label5.Text = result.StatusCode.ToString();
                            break;
                    }
                    textBox4.Text = result.StatusCode == HttpStatusCode.OK ? result.Content.ReadAsStringAsync().Result : "Something went wrong";
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            checkURL(textBox1.Text);
        }
        private bool checkURL(string url)
        {
            Match match = Regex.Match(url, "^(http(s)?:\\/\\/)[\\w.-]+(:\\d+)?(\\/[\\w-]+)*$"); //TODO: needs clarification
            if (!match.Success)
            {
                MessageBox.Show("You're entered not valid URL\nValid URL is http(s)://<domain or address>[:<port>][/some_path]");
                return false;
            }
            return true;
        }
    }
}
