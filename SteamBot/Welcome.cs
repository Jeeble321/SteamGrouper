using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Management;
using System.Net;
using System.Web;
using System.IO;
using System.Threading;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SteamBot
{
    public partial class Welcome : Form
    {
        public static bool loggedIn = false;
        public static bool trial = false;
        public static string version = Interface.BotVersion;
        public static string username;
        public static string regMsg;
        public static string subEx;
        public static string subNow;
        bool passwordCleared = false;
        string updateResponse;
        string hwid;

        public Welcome()
        {
            InitializeComponent();
            if (SteamGrouper.Properties.Settings.Default.sg_remember)
            {
                this.text_username.Text = SteamGrouper.Properties.Settings.Default.sg_username;
                this.text_password.Text = Interface.Decrypt(SteamGrouper.Properties.Settings.Default.sg_password);
                check_remember.Checked = true;
            }
            backgroundWorker1.RunWorkerAsync();
            hwid = HWID();
            text_password.GotFocus += text_password_GotFocus;
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if (text_username.Text == null || text_username.Text == "" || text_username.Text == "Username")
            {
                MessageBox.Show("Username cannot be blank!",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
            else if (text_password.Text == null || text_password.Text == "" || text_password.Text == "Password")
            {
                MessageBox.Show("Password cannot be blank!",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (check_remember.Checked)
                {
                    SteamGrouper.Properties.Settings.Default.sg_username = text_username.Text;
                    SteamGrouper.Properties.Settings.Default.sg_password = Interface.Encrypt(text_password.Text);
                    SteamGrouper.Properties.Settings.Default.sg_remember = true;
                    SteamGrouper.Properties.Settings.Default.Save();
                }
                else
                {
                    SteamGrouper.Properties.Settings.Default.sg_username = "";
                    SteamGrouper.Properties.Settings.Default.sg_password = "";
                    SteamGrouper.Properties.Settings.Default.sg_remember = false;
                    SteamGrouper.Properties.Settings.Default.Save();
                }
                string pw_hash = CreatePasswordHash(text_password.Text, "salt");
                string hwid_hash = CreatePasswordHash(hwid, "salt");
                string version_hash = CreatePasswordHash(Interface.BotVersion, "salt");
                string baseURL = "http://www.steamgrouper.com/app/login";
                string uri = baseURL + "?username=" + HttpUtility.UrlEncode(text_username.Text) + "&password=" + HttpUtility.UrlEncode(pw_hash) + "&hwid=" + hwid_hash + "&ver=" + HttpUtility.UrlEncode(version_hash);
                Thread loginThread = new Thread(() => login(uri));
                loginThread.IsBackground = true;
                loginThread.Start();
                while (loginThread.IsAlive)
                {
                    if (loggedIn)
                    {
                        this.Close();
                        break;
                    }
                }
            }
        }

        void login(string uri)
        {
            string result = HTTPRequest(uri);
            Console.WriteLine(result);
            if (!result.Contains("Welcome, brother."))
            {
                if (result == "")
                {
                    result = "I failed to get a response from the server. Please make sure you are connected to the internet, and if so, our servers may be busy or temporarily offline. Please try again later.";
                }
                MessageBox.Show(result,
                "No Response from Server",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
            else
            {
                regMsg = ParseBetween(result, "{REGMSG}", "{/REGMSG}");
                if (regMsg == "Error")
                {
                    MessageBox.Show(regMsg,
                    "Registration Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                }
                if (regMsg == "" || regMsg == null)
                {
                    subNow = ParseBetween(result, "{CURRENT}", "{/CURRENT}");
                    subEx = ParseBetween(result, "{SUBEX}", "{/SUBEX}");
                    Welcome.loggedIn = true;
                }
                if (regMsg != "" && regMsg != "Error")
                {
                    MessageBox.Show(regMsg,
                    "Registered",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                    subNow = ParseBetween(result, "{CURRENT}", "{/CURRENT}");
                    subEx = ParseBetween(result, "{SUBEX}", "{/SUBEX}");
                    Welcome.loggedIn = true;
                }
            }
        }

        public static string ParseBetween(string Subject, string Start, string End)
        {
            return Regex.Match(Subject, Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"\s*(((?!" + Regex.Replace(Start, @"[][{}()*+?.\\^$|]", @"\$0") + @"|" + Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0") + @").)+)\s*" + Regex.Replace(End, @"[][{}()*+?.\\^$|]", @"\$0"), RegexOptions.IgnoreCase).Value.Replace(Start, "").Replace(End, "");
        }

        public static string CreatePasswordHash(string _password, string _salt)
        {
            string saltAndPwd = String.Concat(_password, _salt);
            SHA512 sha512 = new System.Security.Cryptography.SHA512Managed();

            byte[] sha512Bytes = System.Text.Encoding.Default.GetBytes(saltAndPwd);

            byte[] cryString = sha512.ComputeHash(sha512Bytes);

            string hashedPwd = string.Empty;

            for (int i = 0; i < cryString.Length; i++)
            {
                hashedPwd += cryString[i].ToString("X");
            }

            sha512.Clear();
            sha512.Dispose();

            return hashedPwd;
        }

        private string HWID()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
            }
            return cpuInfo;
        }

        private void button_trial_Click(object sender, EventArgs e)
        {
            trial = true;
            this.Close();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            
        }

        private string Check_Version(string url)
        {
            return HTTPRequest(url);
        }

        private void text_password_GotFocus(object sender, EventArgs e)
        {
            if (!passwordCleared)
            {
                text_password.Clear();
                passwordCleared = true;
            }
        }

        public static string HTTPRequest(string url)
        {
            var result = "";
            try
            {
                using (var webClient = new WebClient())
                {
                    using (var stream = webClient.OpenRead(url))
                    {
                        using (var streamReader = new StreamReader(stream))
                        {
                            result = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var wtf = ex.Message;
            }

            return result;
        }

        private void text_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_username_TextChanged(object sender, EventArgs e)
        {
            Welcome.username = text_username.Text;
        }

        private void Purchase_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You will now be directed to http://www.steamgrouper.com/choose-a-subscription/.",
                                "Purchase",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);
            System.Diagnostics.Process.Start("explorer.exe", "http://www.steamgrouper.com/choose-a-subscription/");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string version_hash = CreatePasswordHash(Interface.BotVersion, "salt");
            string url = "http://www.steamgrouper.com/app/login?ver=" + HttpUtility.UrlEncode(version_hash);
            updateResponse = Check_Version(url);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (updateResponse.Contains("outdated"))
            {
                string message = updateResponse + ".\nWould you like to be taken to the download page? Click no to simply close SteamGrouper.";
                DialogResult choice = MessageBox.Show(new Form() { TopMost = true }, message,
                                "New Version Available",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);
                switch (choice)
                {
                    case DialogResult.Yes:
                        System.Diagnostics.Process.Start("explorer.exe", "http://www.steamgrouper.com/download/");
                        Environment.Exit(0);
                        break;
                    case DialogResult.No:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
