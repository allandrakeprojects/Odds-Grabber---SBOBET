using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odds_Grabber___sbobet
{
    public partial class Main_Form : Form
    {
        private ChromiumWebBrowser chromeBrowser;
        private string __app = "Odds Grabber";
        private string __app_type = "{edit this}";
        private string __brand_code = "{edit this}";
        private string __brand_color = "#274273";
        private string __url = "www.sbobet.com";
        private string __website_name = "sbobet";
        private string __app__website_name = "";
        private string __api_key = "youdieidie";
        private string __running_01 = "sbobet";
        private string __running_02 = "";
        private string __running_11 = "";
        private string __running_22 = "";
        private string __ts = "";
        private string __tk = "";
        private int __send = 0;
        private int __r = 39;
        private int __g = 66;
        private int __b = 115;
        private bool __is_close;
        private bool __is_login = false;
        private bool __m_aeroEnabled;
        List<String> __league_id = new List<String>();
        Form __mainFormHandler;

        // Drag Header to Move
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        // ----- Drag Header to Move

        // Form Shadow
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int CS_DBLCLKS = 0x8;
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                __m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!__m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (__m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        };
                        DwmExtendFrameIntoClientArea(Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
                m.Result = (IntPtr)HTCAPTION;
        }
        // ----- Form Shadow

        public Main_Form()
        {
            InitializeComponent();

            timer_landing.Start();
        }

        // Drag to Move
        private void panel_header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_title_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_loader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_brand_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel_landing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_landing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        // ----- Drag to Move

        // Click Close
        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Exit the program?", __app__website_name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                __is_close = true;
                Environment.Exit(0);
            }
        }

        // Click Minimize
        private void pictureBox_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        const UInt32 WM_CLOSE = 0x0010;

        void ___CloseMessageBox()
        {
            IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, "JavaScript Alert - http://mem.sghuatchai.com");

            if (windowPtr == IntPtr.Zero)
            {
                return;
            }

            SendMessage(windowPtr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        private void timer_close_message_box_Tick(object sender, EventArgs e)
        {
            ___CloseMessageBox();
        }

        private void timer_size_Tick(object sender, EventArgs e)
        {
            __mainFormHandler = Application.OpenForms[0];
            __mainFormHandler.Size = new Size(466, 168);
        }

        // Form Closing
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!__is_close)
            {
                DialogResult dr = MessageBox.Show("Exit the program?", __app__website_name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }

        // Form Load
        private void Main_Form_Load(object sender, EventArgs e)
        {
            __app__website_name = __app + " - " + __website_name;
            panel1.BackColor = Color.FromArgb(__r, __g, __b);
            panel2.BackColor = Color.FromArgb(__r, __g, __b);
            label_brand.BackColor = Color.FromArgb(__r, __g, __b);
            Text = __app__website_name;
            label_title.Text = __app__website_name;

            InitializeChromium();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Properties.Settings.Default.______is_send_telegram)
            {
                Properties.Settings.Default.______is_send_telegram = false;
                Properties.Settings.Default.Save();
                MessageBox.Show("Telegram Notification is Disabled.", __app__website_name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Properties.Settings.Default.______is_send_telegram = true;
                Properties.Settings.Default.Save();
                MessageBox.Show("Telegram Notification is Enabled.", __app__website_name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void timer_landing_Tick(object sender, EventArgs e)
        {
            panel_landing.Visible = false;
            panel_cefsharp.Visible = false;
            pictureBox_loader.Visible = true;
            timer_size.Start();
            timer_landing.Stop();
        }

        public static void ___FlushMemory()
        {
            Process prs = Process.GetCurrentProcess();
            try
            {
                prs.MinWorkingSet = (IntPtr)(300000);
            }
            catch (Exception err)
            {
                // leave blank
            }
        }

        private void timer_flush_memory_Tick(object sender, EventArgs e)
        {
            ___FlushMemory();
        }

        private void SendMyBot(string message)
        {
            try
            {
                string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                string apiToken = "772918363:AAHn2ufmP3ocLEilQ1V-IHcqYMcSuFJHx5g";
                string chatId = "@allandrake";
                string text = "-----" + __app__website_name + "-----%0A%0AIP:%20ABC PC%0ALocation:%20Pacific%20Star%0ADate%20and%20Time:%20[" + datetime + "]%0AMessage:%20" + message;
                urlString = String.Format(urlString, apiToken, chatId, text);
                WebRequest request = WebRequest.Create(urlString);
                Stream rs = request.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(rs);
                string line = "";
                StringBuilder sb = new StringBuilder();
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line != null)
                        sb.Append(line);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        __Flag();
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        SendMyBot(message);
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private void SendABCTeam(string message)
        {
            if (Properties.Settings.Default.______is_send_telegram)
            {
                try
                {
                    string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                    string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                    string apiToken = "651945130:AAGMFj-C4wX0yElG2dBU1SRbfrNZi75jPHg";
                    string chatId = "@odds_bot_abc_team";
                    string text = "Bot:%20-----" + __website_name.ToUpper() + "-----%0ADate%20and%20Time:%20[" + datetime + "]%0AMessage:%20<b>" + message + "</>&parse_mode=html";
                    urlString = String.Format(urlString, apiToken, chatId, text);
                    WebRequest request = WebRequest.Create(urlString);
                    Stream rs = request.GetResponse().GetResponseStream();
                    StreamReader reader = new StreamReader(rs);
                    string line = "";
                    StringBuilder sb = new StringBuilder();
                    while (line != null)
                    {
                        line = reader.ReadLine();
                        if (line != null)
                            sb.Append(line);
                    }
                    __send = 0;
                }
                catch (Exception err)
                {
                    __send++;

                    if (___CheckForInternetConnection())
                    {
                        if (__send == 5)
                        {
                            __Flag();
                            __is_close = false;
                            Environment.Exit(0);
                        }
                        else
                        {
                            SendABCTeam(message);
                        }
                    }
                    else
                    {
                        __is_close = false;
                        Environment.Exit(0);
                    }
                }
            }
        }

        private void timer_detect_running_Tick(object sender, EventArgs e)
        {
            //___DetectRunningAsync();
        }

        private async void ___DetectRunningAsync()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string password = __brand_code + datetime + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["app_type"] = __app_type,
                        ["last_update"] = datetime,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://192.168.10.252:8080/API/updateAppStatus", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        SendMyBot(err.ToString());
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___DetectRunning2Async();
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private async void ___DetectRunning2Async()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string password = __brand_code + datetime + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["app_type"] = __app_type,
                        ["last_update"] = datetime,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://zeus.ssitex.com:8080/API/updateAppStatus", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        SendMyBot(err.ToString());
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___DetectRunningAsync();
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        // CefSharp Initialize
        private void InitializeChromium()
        {
            CefSettings settings = new CefSettings();

            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CEF";
            Cef.Initialize(settings);
            chromeBrowser = new ChromiumWebBrowser("https://www.sbobet.com/euro/football/england");
            panel_cefsharp.Controls.Add(chromeBrowser);
            chromeBrowser.AddressChanged += ChromiumBrowserAddressChanged;
        }

        // CefSharp Address Changed
        private void ChromiumBrowserAddressChanged(object sender, AddressChangedEventArgs e)
        {
            __url = e.Address.ToString();
            Invoke(new Action(() =>
            {
                //panel3.Visible = true;
                panel4.Visible = true;
            }));

            if (e.Address.ToString().Contains("5.61"))
            {
                SendABCTeam("Please setup first VPN to the installed PC.");
                MessageBox.Show("Please setup first VPN to this PC.", __app__website_name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                __is_close = false;
                Environment.Exit(0);
            }
            else if (e.Address.ToString().Contains("www.sbobet.com"))
            {
                Invoke(new Action(() =>
                {
                    chromeBrowser.FrameLoadEnd += (sender_, args) =>
                    {
                        if (args.Frame.IsMain)
                        {
                            Invoke(new Action(async () =>
                            {
                                __is_login = true;

                                await chromeBrowser.GetSourceAsync().ContinueWith(taskHtml =>
                                {
                                     string html = taskHtml.Result;

                                    Regex pattern_ = new Regex(@"tilib_Token\((.*?)\)", RegexOptions.Compiled);
                                    int count = 0;
                                    foreach (Match responsebody_ in pattern_.Matches(html))
                                    {
                                        count++;

                                        if (count == 1)
                                        {
                                            string result_ = responsebody_.ToString().Replace("tilib_Token(", "").Replace(")", "").Trim();
                                            string[] line_01 = result_.ToString().Split(new string[] { "]," }, StringSplitOptions.None);
                                            string[] line_02 = line_01[1].ToString().Split(new string[] { "," }, StringSplitOptions.None);
                                            __ts = line_02[2].ToString().Replace("'", "");
                                        }
                                        else if (count == 2)
                                        {
                                            string result_ = responsebody_.ToString().Replace("tilib_Token(", "").Replace(")", "").Trim();
                                            string[] line_01 = result_.ToString().Split(new string[] { "]," }, StringSplitOptions.None);
                                            string[] line_02 = line_01[1].ToString().Split(new string[] { "," }, StringSplitOptions.None);

                                            for (int i = 0; i < 7; i++)
                                            {
                                                __tk += line_02[i].Replace("[", "").Trim() + ",";
                                            }
                                        }
                                    }

                                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                    doc.LoadHtml(@html);
                                    var rows = doc.DocumentNode.SelectNodes("//*[@id='ms-all-tos-1-3']/ul/li");
                                    foreach (var row in rows)
                                    {
                                        var pattern = "id=\"(.*?)\"";
                                        Match responsebody = Regex.Match(row.InnerHtml, pattern, RegexOptions.IgnoreCase);
                                        if (responsebody.Success)
                                        {
                                            string _id = responsebody.Value.Replace("id=\"", "").Replace("\"", "");
                                            if (_id.Contains("bu:ms:all-to:"))
                                            {
                                                _id = _id.Replace("bu:ms:all-to:", "");
                                                var _league_name = Regex.Replace(row.InnerText, @"[\d-]", string.Empty).Replace("()", "");
                                                __league_id.Add(_id + "*|*" + _league_name);
                                            }
                                        }
                                    }

                                    SendABCTeam("Firing up!");
                                    ___FIRST_RUNNINGAsync();
                                });
                            }));
                        }
                    };
                }));
            }
        }

        private bool __detect_ = false;

        // ----- Functions
        // DAFA888 -----
        private async void ___FIRST_RUNNINGAsync()
        {
            Invoke(new Action(() =>
            {
                panel4.BackColor = Color.FromArgb(0, 255, 0);
            }));

            try
            {
                var cookieManager = Cef.GetGlobalCookieManager();
                var visitor = new CookieCollector();
                cookieManager.VisitUrlCookies(__url, true, visitor);
                var cookies = await visitor.Task;
                var cookie = CookieCollector.GetCookieHeader(cookies);
                WebClient wc = new WebClient();
                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                wc.Headers["X-Requested-With"] = "XMLHttpRequest";
                string _date = DateTime.Now.ToString("yyyyMMdd");
                byte[] result = await wc.DownloadDataTaskAsync("https://www.sbobet.com/en/data/event?ts=" + __ts + "&tk=" + __tk + _date + ",4,4,0,4&ac=3");
                string responsebody = Encoding.UTF8.GetString(result);

                string password = __running_01 + __api_key;
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

                if (responsebody != "$P.onReload('ts');")
                {
                    var pattern_ = @"P.onUpdate\((.*?)\);";
                    Match responsebody_ = Regex.Match(responsebody, pattern_, RegexOptions.IgnoreCase);
                    if (responsebody_.Success)
                    {
                        string result_ = responsebody_.Value.Replace("P.onUpdate('od',", "").Replace(");", "");
                        var deserialize_object = JsonConvert.DeserializeObject(result_);
                        //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\testsbobet.txt";
                        //using (StreamWriter file = new StreamWriter(path, true))
                        //{
                        //    file.WriteLine(deserialize_object.ToString());
                        //}
                        //MessageBox.Show(deserialize_object.ToString());
                        JArray _jo = JArray.Parse(deserialize_object.ToString());
                        JToken _count = _jo.SelectToken("[2][0][1]");

                        JToken LeagueName = "";
                        JToken HomeTeamName = "";
                        JToken AwayTeamName = "";
                        JToken HomeScore = "";
                        JToken AwayScore = "";
                        JToken MatchTimeHalf = "";
                        JToken KickOffDateTime = "";
                        String StatementDate = "";
                        JToken MatchTimeMinute = "";
                        String MatchStatus = "";

                        JToken FTOdd = "";
                        JToken FTEven = "";

                        if (_count.Count() > 0)
                        {
                            for (int i = 0; i < _count.Count(); i++)
                            {
                                JToken FT1 = "";
                                JToken FT2 = "";
                                JToken FTX = "";
                                JToken FTHDP = "";
                                JToken FTHDPH = "";
                                JToken FTHDPA = "";
                                JToken FTH = "";
                                JToken FTA = "";
                                JToken FTOU = "";
                                JToken FTO = "";
                                JToken FTU = "";

                                JToken FH1 = "";
                                JToken FH2 = "";
                                JToken FHX = "";
                                JToken FHHDP = "";
                                JToken FHHDPH = "";
                                JToken FHHDPA = "";
                                JToken FHH = "";
                                JToken FHA = "";
                                JToken FHOU = "";
                                JToken FHO = "";
                                JToken FHU = "";

                                LeagueName = _jo.SelectToken("[2][0][1][" + i + "][1]");
                                foreach (var _league in __league_id)
                                {
                                    string[] line = _league.ToString().Split(new string[] { "*|*" }, StringSplitOptions.None);
                                    string _league_id = line[0];
                                    if (_league_id == LeagueName.ToString())
                                    {
                                        LeagueName = line[1];
                                        break;
                                    }
                                }
                                HomeTeamName = _jo.SelectToken("[2][0][1][" + i + "][2][1]");
                                AwayTeamName = _jo.SelectToken("[2][0][1][" + i + "][2][2]");
                                KickOffDateTime = _jo.SelectToken("[2][0][1][" + i + "][2][5]");
                                DateTime KickOffDateTime_Replace = DateTime.ParseExact("05/08/2019 00:00", "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);
                                try
                                {
                                    KickOffDateTime_Replace = DateTime.ParseExact(KickOffDateTime.ToString(), "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);
                                    if (__detect_)
                                    {
                                        __detect_ = false;
                                        SendMyBot("Okay.");
                                    }
                                }
                                catch (Exception err)
                                {
                                    if (!__detect_)
                                    {
                                        SendMyBot("Detect.");
                                    }
                                    __detect_ = true;
                                    break;
                                }
                                KickOffDateTime = KickOffDateTime_Replace.ToString("yyyy-MM-dd HH:mm:ss");
                                StatementDate = KickOffDateTime_Replace.ToString("yyyy-MM-dd 00:00:00");

                                JToken Detect_Count = _jo.SelectToken("[2][0][1][" + i + "][4]");
                                for (int ii = 0; ii < Detect_Count.Count(); ii++)
                                {
                                    if (ii != 0)
                                    {
                                        try
                                        {
                                            String Detect = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][1][0]").ToString();
                                            String Detect_ = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][1]").ToString();
                                            if (Detect.ToString() == "5")
                                            {
                                                FT1 = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][0]");
                                                FT2 = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][2]");
                                                FTX = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][1]");
                                            }

                                            if (Detect.ToString() == "8")
                                            {
                                                FH1 = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][0]");
                                                FH2 = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][2]");
                                                FHX = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][1]");
                                            }

                                            if (Detect.ToString() == "1" && FTHDP.ToString() == "")
                                            {
                                                FTHDP = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][1][5]");
                                                FTHDPH = "-" + FTHDP.ToString();
                                                FTHDPA = "+" + FTHDP.ToString();
                                                FTH = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][0]");
                                                FTA = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][1]");
                                            }

                                            if (Detect.ToString() == "7" && FHHDP.ToString() == "")
                                            {
                                                FHHDP = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][1][5]");
                                                FHHDPH = "-" + FHHDP.ToString();
                                                FHHDPA = "+" + FHHDP.ToString();
                                                FHH = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][0]");
                                                FHA = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][1]");
                                            }

                                            if (Detect.ToString() == "3")
                                            {
                                                FTOU = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][1][5]");
                                                FTO = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][0]");
                                                FTU = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][1]");
                                            }

                                            if (Detect.ToString() == "9")
                                            {
                                                FHOU = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][1][5]");
                                                FHO = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][0]");
                                                FHU = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][1]");
                                            }

                                            if (Detect.ToString() == "2")
                                            {
                                                FTOdd = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][0]");
                                                FTEven = _jo.SelectToken("[2][0][1][" + i + "][4][" + ii + "][2][1]");
                                            }
                                        }
                                        catch (Exception err)
                                        {

                                        }
                                    }
                                }

                                string ref_id_password = DateTime.Now.ToString("yyyy-MM-dd") + "10" + "Soccer" + LeagueName + HomeTeamName + AwayTeamName;
                                byte[] ref_id_encodedpassword = new UTF8Encoding().GetBytes(ref_id_password.Trim());
                                byte[] ref_hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(ref_id_encodedpassword);
                                string ref_token = BitConverter.ToString(ref_hash).Replace("-", string.Empty).ToLower().Substring(0, 8);
                                string ref_match_id = ref_token;

                                var reqparm_ = new NameValueCollection
                                {
                                    {"source_id", "10"},
                                    {"sport_name", ""},
                                    {"league_name", LeagueName.ToString().Trim()},
                                    {"home_team", HomeTeamName.ToString().Trim()},
                                    {"away_team", AwayTeamName.ToString().Trim()},
                                    {"home_team_score", "0"},
                                    {"away_team_score", "0"},
                                    {"ref_match_id", ref_match_id},
                                    {"odds_row_no", "1"},
                                    {"fthdph", (FTHDPH.ToString() != "") ? FTHDPH.ToString() : "0"},
                                    {"fthdpa", (FTHDPA.ToString() != "") ? FTHDPA.ToString() : "0"},
                                    {"fth", (FTH.ToString() != "-999") ? FTH.ToString() : "0"},
                                    {"fta", (FTA.ToString() != "-999") ? FTA.ToString() : "0"},
                                    {"betidftou", "0"},
                                    {"ftou", (FTOU.ToString() != "-999") ? FTOU.ToString() : "0"},
                                    {"fto", (FTO.ToString() != "-999") ? FTO.ToString() : "0"},
                                    {"ftu", (FTU.ToString() != "-999") ? FTU.ToString() : "0"},
                                    {"betidftoe", "0"},
                                    {"ftodd", "0"},
                                    {"fteven", "0"},
                                    {"betidft1x2", "0"},
                                    {"ft1", (FT1.ToString() != "-999") ? FT1.ToString() : "0"},
                                    {"ftx", (FTX.ToString() != "-999") ? FTX.ToString() : "0"},
                                    {"ft2", (FT2.ToString() != "-999") ? FT2.ToString() : "0"},
                                    {"specialgame", "0"},
                                    {"fhhdph", (FHHDPH.ToString() != "") ? FHHDPH.ToString() : "0"},
                                    {"fhhdpa", (FHHDPA.ToString() != "") ? FHHDPA.ToString() : "0"},
                                    {"fhh", (FHH.ToString() != "-999") ? FHH.ToString() : "0"},
                                    {"fha", (FHA.ToString() != "-999") ? FHA.ToString() : "0"},
                                    {"fhou", (FHOU.ToString() != "-999") ? FHOU.ToString() : "0"},
                                    {"fho", (FHO.ToString() != "-999") ? FHO.ToString() : "0"},
                                    {"fhu", (FHU.ToString() != "-999") ? FHU.ToString() : "0"},
                                    {"fhodd", "0"},
                                    {"fheven", "0"},
                                    {"fh1", (FH1.ToString() != "-999") ? FH1.ToString() : "0"},
                                    {"fhx", (FHX.ToString() != "-999") ? FHX.ToString() : "0"},
                                    {"fh2", (FH2.ToString() != "-999") ? FH2.ToString() : "0"},
                                    {"statement_date", StatementDate.ToString()},
                                    {"kickoff_date", KickOffDateTime.ToString()},
                                    {"match_time", "Upcoming"},
                                    {"match_status", "N"},
                                    {"match_minute", "0"},
                                    {"api_status", "R"},
                                    {"token_api", token},
                                };

                                try
                                {
                                    WebClient wc_ = new WebClient();
                                    byte[] result__ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                                    string responsebody__ = Encoding.UTF8.GetString(result__);
                                    __send = 0;
                                }
                                catch (Exception err)
                                {
                                    __send++;

                                    if (___CheckForInternetConnection())
                                    {
                                        if (__send == 5)
                                        {
                                            SendMyBot(err.ToString());
                                            __is_close = false;
                                            Environment.Exit(0);
                                        }
                                        else
                                        {
                                            await ___TaskWait_Handler(10);
                                            WebClient wc_ = new WebClient();
                                            byte[] result__ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                                            string responsebody__ = Encoding.UTF8.GetString(result__);
                                        }
                                    }
                                    else
                                    {
                                        __is_close = false;
                                        Environment.Exit(0);
                                    }
                                }
                            }
                        }
                    }
                }
                
                // send sbobet
                if (Properties.Settings.Default.______odds_issend_01)
                {
                    Properties.Settings.Default.______odds_issend_01 = false;
                    Properties.Settings.Default.Save();

                    SendABCTeam(__running_11 + " Back to Normal.");
                }

                // comment detect
                //Properties.Settings.Default.______odds_iswaiting_01 = false;
                //Properties.Settings.Default.Save();

                Invoke(new Action(() =>
                {
                    panel4.BackColor = Color.FromArgb(16, 90, 101);
                }));

                __send = 0;
                await ___TaskWait();
                ___FIRST_RUNNINGAsync();
            }
            catch (Exception err)
            {
                if (___CheckForInternetConnection())
                {
                    if (err.ToString().ToLower().Contains("401"))
                    {
                        chromeBrowser.Load("https://www.sportdafa.net/en/sports-df/sports");
                    }
                    else
                    {
                        await chromeBrowser.GetSourceAsync().ContinueWith(async taskHtml =>
                        {
                            var html = taskHtml.Result.ToString().ToLower();
                            if (html.Contains("dear user"))
                            {
                                SendABCTeam("Please setup first VPN to the installed PC.");
                                MessageBox.Show("Please setup first VPN to this PC.", __app__website_name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                __is_close = false;
                                Environment.Exit(0);
                            }
                            else
                            {
                                __send++;
                                if (__send == 5)
                                {
                                    //Properties.Settings.Default.______odds_iswaiting_01 = true;
                                    //Properties.Settings.Default.Save();

                                    //if (!Properties.Settings.Default.______odds_issend_01)
                                    //{
                                    //    Properties.Settings.Default.______odds_issend_01 = true;
                                    //    Properties.Settings.Default.Save();
                                    //    SendABCTeam(__running_11 + " Under Maintenance.");
                                    //}

                                    ___FIRST_RUNNINGAsync();
                                    SendMyBot(err.ToString());
                                }
                                else
                                {
                                    await ___TaskWait_Handler(10);
                                    ___FIRST_RUNNINGAsync();
                                }
                            }
                        });
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        public static bool ___CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task ___TaskWait()
        {
            Random _random = new Random();
            int _random_number = _random.Next(25, 30);
            string _randowm_number_replace = _random_number.ToString() + "000";
            await Task.Delay(Convert.ToInt32(_randowm_number_replace));
        }

        private async Task ___TaskWait_Handler(int sec)
        {
            sec++;
            Random _random = new Random();
            int _random_number = _random.Next(sec, sec);
            string _randowm_number_replace = _random_number.ToString() + "000";
            await Task.Delay(Convert.ToInt32(_randowm_number_replace));
        }

        public bool __is_numeric(string value)
        {
            return value.All(char.IsNumber);
        }

        private void __Flag()
        {
            string _flag = Path.Combine(Path.GetTempPath(), __app + " - " + __website_name + ".txt");
            using (StreamWriter sw = new StreamWriter(_flag, true))
            {
                sw.WriteLine("<<>>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<<>>");
            }
        }
    }
}