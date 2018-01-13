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
using Newtonsoft.Json;
using System.Data.SQLite;

namespace NiceMon
{
    public partial class Form1 : Form
    {

        private bool runningUpdate = false;
        private const string URL = "https://api.nicehash.com/api";
        private string urlParameters = "?method=stats.provider.ex&addr=";
        private string urlParametersWithKey = "";
        private const string myBTCaddress = "3BUJYidvCCseRrgKzq5xG2ENtD1FyYkemu";
        private SortedDictionary<int, decimal> snapshot = new SortedDictionary<int, decimal>();
        private SQLiteConnection dbCon;
        private string nicehashWalletAddress = "";

        public Form1()
        {
            InitializeComponent();
            donateLabel.Text += myBTCaddress;
            nicehashWalletAddressBox.Text = "39J9HLSEZz7BPiVfPdEPPrAxy1jyjK8dLL";//DEBUG ONLY THIS MUST BE REMOVED            
            System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initDB();
        }

        private void initDB()
        {
            if (!System.IO.File.Exists("NiceMonDB.sqlite"))
            {
                try
                {
                    SQLiteConnection.CreateFile("NiceMonDB.sqlite");
                    dbCon = new SQLiteConnection("Data Source=NiceMonDB.sqlite");
                    dbCon.Open();
                    string sql = "CREATE TABLE profit (address VARCHAR(40), timestamp INT, btc DECIMAL(15,8))";
                    SQLiteCommand command = new SQLiteCommand(sql, dbCon);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log(e.Message);
                }
            }
            else
                dbCon = new SQLiteConnection("Data Source=NiceMonDB.sqlite");
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
            nicehashWalletAddress = nicehashWalletAddressBox.Text;
            startUpdatingButton.Text = "Stop";
            urlParametersWithKey = urlParameters + nicehashWalletAddress;
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
                try
                {
                    log("Obtaining data...");
                    string s = await response.Content.ReadAsStringAsync();
                    if (s.IndexOf("You have reached your API request quota limit") >= 0)
                    {
                        log("API Quota limit exceeded. Skipping.");
                        return;
                    }

                    StatsProviderEx.JsonResult jsonOutput = JsonConvert.DeserializeObject<StatsProviderEx.JsonResult>(s);

                    string json = JsonConvert.SerializeObject(jsonOutput);

                    string jsonFormatted = Newtonsoft.Json.Linq.JValue.Parse(json).ToString(Formatting.Indented);                  
                    log("Received data for NiceHash wallet address " + jsonOutput.result.addr);

                    log("Processing data, please wait...");
                    for (int i = 0; i < jsonOutput.result.past.Count; i++)
                    {
                        for (int j = 0; j < jsonOutput.result.past[i].data.Count; j++)
                        {
                           try
                            {
                                snapshot.Add(Int32.Parse(jsonOutput.result.past[i].data[j][0].ToString()), decimal.Parse(jsonOutput.result.past[i].data[j][2].ToString()));
                            }
                            catch (System.ArgumentException e)
                            {
                                if (e.Message.IndexOf("An entry with the same key") >= 0)
                                {
                                    snapshot[Int32.Parse(jsonOutput.result.past[i].data[j][0].ToString())] = snapshot[Int32.Parse(jsonOutput.result.past[i].data[j][0].ToString())] + decimal.Parse(jsonOutput.result.past[i].data[j][2].ToString());
                                }
                                else
                                    log(e.Message);
                            }
                        }
                    }
                    log("Complete.");
                    /*foreach (KeyValuePair<int, decimal> kvp in snapshot)
                    {
                        log(kvp.Key + " : " + kvp.Value);
                    }*/
                    log("Updating database...");
                    dbCon.Open();
                    using (var cmd = new SQLiteCommand(dbCon))
                    {
                        using (var transaction = dbCon.BeginTransaction())
                        {
                            foreach (KeyValuePair<int, decimal> kvp in snapshot)
                            {

                                cmd.CommandText = "INSERT INTO profit (address, timestamp, btc) VALUES ('" + nicehashWalletAddress + "', '" + kvp.Key + "', '" + kvp.Value + "');";
                                cmd.ExecuteNonQuery();

                            }

                            transaction.Commit();
                        }
                    }
                    dbCon.Close();
                    log("Done.");
                    
                } 
                catch (Exception ex)
                {
                    log("ERROR");
                    log(ex.Message);
                    log(ex.ToString());
                    log(ex.StackTrace);
                }
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

        private void donateLabel_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(myBTCaddress);
            MessageBox.Show("Bitcoin address copied to clipboard", "Donate!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }

    class StatsProviderEx
    {

        public class JsonResult
        {
            public Result result { get; set; }
        }

        public class Result
        {
            public string nh_wallet { get; set; }
            public int attack_written_off { get; set; }
            public int attack_amount { get; set; }
            public string addr { get; set; }
            public int attack_repaid { get; set; }
            public List<object> current { get; set; }
            public List<Past> past { get; set; }
        }

        public class Past
        {
            public string algo { get; set; }
            public List<List<object>> data { get; set; }
        }

    }
}
