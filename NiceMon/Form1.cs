using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NiceMon
{
    public partial class Form1 : Form
    {

        private bool runningUpdate = false;
        private const string URL = "https://api.nicehash.com/api";
        private string urlParameters = "?method=stats.provider.ex&addr=";
        private string urlParametersWithKey = "";

        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {           

        }

        private void startUpdatingButton_Click(object sender, EventArgs e)
        {
            if(this.runningUpdate)
            {
                disableUpdate();
            }
            else
            {
                enableUpdate();
            }
        }

        private void enableUpdate()
        {
            nicehashWalletAddressBox.Enabled = false;
            startUpdatingButton.Text = "Stop";
            urlParametersWithKey = urlParameters + nicehashWalletAddressBox.Text;
            log(urlParametersWithKey);
            Thread t = new Thread(new ThreadStart(getNiceHashStats));
            t.Start();           
            this.runningUpdate = true;
        }

        private void disableUpdate()
        {
            nicehashWalletAddressBox.Enabled = true;
            startUpdatingButton.Text = "Go!";
            this.runningUpdate = false;

        }

        private async void getNiceHashStats()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParametersWithKey).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                string s = await response.Content.ReadAsStringAsync();
                log(s); //NEED TO HANDLE THE JSON PROPERLY
            }
            else
            {
                log((int)response.StatusCode + " : " + response.ReasonPhrase);
            }
        }

        public void log(string s)
        {
            logOutput.AppendText(s + "\r\n");
        }

    }
}
