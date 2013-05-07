using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SteamKit2;
using SteamTrade;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using SteamGrouper;

namespace SteamBot
{
    public partial class Interface : Form
    {
        // Variables for group gathering
        public static string BotVersion = "v" + Application.ProductVersion.Remove(Application.ProductVersion.LastIndexOf('.'));
        string gid;
        public static bool inviteOffline = true;
        public static ulong currentSID = 0;
        static int retry = 0;
        public static int numMember = 0;
        public static int inviteSpeed = 18;
        public static int inviteSpeedMin = 0;
        public static int inviteSpeedMax = 0;
        public static bool MinMax = false;
        public static bool inviting = false;
        public static bool gathering = false;
        public static bool loginClicked = false;
        bool text_gather_cleared = false;
        bool text_invite_cleared = false;
        public static string username;
        public static string password;
        public static int subExInMinutes;
        string subNow = "";
        string subEx = "";
        StringBuilder gatherList = new StringBuilder();
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        bool done = false;
        DateTime startTime = DateTime.Now;

        public Interface()
        {
            InitializeComponent();
            this.FormClosing += Interface_FormClosing;
            if (SteamGrouper.Properties.Settings.Default.remember)
            {
                this.text_username.Text = SteamGrouper.Properties.Settings.Default.username;
                this.text_password.Text = Decrypt(SteamGrouper.Properties.Settings.Default.password);
                this.check_remember.Checked = true;
            }
        }

        void Interface_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    // Prompt user to save his data
                    Environment.Exit(0);
                }
                if (e.CloseReason == CloseReason.WindowsShutDown)
                {
                    // Autosave and clear up resources
                    Environment.Exit(0);
                }
            }
            catch (System.ObjectDisposedException)
            {

            }
        }

        public void RefreshSubscription()
        {
            string baseURL = "http://www.steamgrouper.com/app/get.php";
            string uri = baseURL + "?username=" + HttpUtility.UrlEncode(Welcome.username);
            GetSubscription(uri);
        }

        void GetSubscription(string uri)
        {
            string result = Welcome.HTTPRequest(uri);
            if (!result.Contains("Subscription error") || result != "")
            {
                subNow = Welcome.ParseBetween(result, "{CURRENT}", "{/CURRENT}");
                subEx = Welcome.ParseBetween(result, "{SUBEX}", "{/SUBEX}");
                Console.WriteLine("now: " + subNow + ", subex: " + subEx);
            }
            if (subNow != "" && subEx != "")
            {
                DateTime d_subNow = Convert.ToDateTime(subNow);
                DateTime d_subEx = Convert.ToDateTime(subEx);
                TimeSpan difference = d_subEx - d_subNow;
                subExInMinutes = Convert.ToInt32(difference.TotalMinutes);
                if (subExInMinutes <= 0)
                {
                    this.Invoke((Action)(() =>
                    {
                        Application.ExitThread();
                        Application.Exit();
                        Environment.Exit(0);
                        // Exit the program
                    }));
                }
            }
        }

        public void SteamGuard(bool retry = false)
        {
            SteamGuard steamGuard = new SteamGuard();
            if (retry)
            {
                steamGuard.UpdateLabel("An invalid authorization code was provided.\nEnter the authentication code sent to the proper email:");
            }
            steamGuard.ShowDialog();
            steamGuard.Focus();
        }

        public static void UpdateLabel(Label label, string text)
        {
            // If the current thread is not the UI thread, InvokeRequired will be true
            if (label.InvokeRequired)
            {
                // If so, call Invoke, passing it a lambda expression which calls
                // UpdateText with the same label and text, but on the UI thread instead.
                label.Invoke((Action)(() => UpdateLabel(label, text)));
                return;
            }
            // If we're running on the UI thread, we'll get here, and can safely update 
            // the label's text.
            label.Text = text;
        }

        public static void UpdateButton(Button button, string text)
        {
            // If the current thread is not the UI thread, InvokeRequired will be true
            if (button.InvokeRequired)
            {
                // If so, call Invoke, passing it a lambda expression which calls
                // UpdateText with the same label and text, but on the UI thread instead.
                button.Invoke((Action)(() => UpdateButton(button, text)));
                return;
            }
            // If we're running on the UI thread, we'll get here, and can safely update 
            // the button's text.
            button.Text = text;
        }

        public static void AppendText(TextBox textBox, string text)
        {
            // If the current thread is not the UI thread, InvokeRequired will be true
            if (textBox.InvokeRequired)
            {
                // If so, call Invoke, passing it a lambda expression which calls
                // UpdateText with the same label and text, but on the UI thread instead.
                textBox.Invoke((Action)(() => AppendText(textBox, text)));
                return;
            }
            // If we're running on the UI thread, we'll get here, and can safely update 
            // the label's text.
            textBox.AppendText(text);
            textBox.ScrollToCaret();
        }

        public void UpdateLog(string text)
        {
            // If the current thread is not the UI thread, InvokeRequired will be true
            if (text_log.InvokeRequired)
            {
                // If so, call Invoke, passing it a lambda expression which calls
                // UpdateText with the same label and text, but on the UI thread instead.
                text_log.Invoke((Action)(() => UpdateLog(text)));
                return;
            }
            // If we're running on the UI thread, we'll get here, and can safely update 
            // the label's text.
            text_log.AppendText(text);
            text_log.ScrollToCaret();
        }

        public static void UpdateProgress(ProgressBar progressBar, int percentage)
        {
            // If the current thread is not the UI thread, InvokeRequired will be true
            if (progressBar.InvokeRequired)
            {
                // If so, call Invoke, passing it a lambda expression which calls
                // UpdateText with the same label and text, but on the UI thread instead.
                progressBar.Invoke((Action)(() => UpdateProgress(progressBar, percentage)));
                return;
            }
            // If we're running on the UI thread, we'll get here, and can safely update 
            // the label's text.
            progressBar.Value = percentage;
        }

        private void button_gather_Click(object sender, EventArgs e)
        {
            if (Bot.IsLoggedIn)
            {
                if (text_gather.Text == "" || text_gather.Text == "Group to gather from")
                {
                    MessageBox.Show("You need to type in the group that you want to gather members from.\r\n"
                                  + "For example, if your group's URL is http://steamcommunity.com/groups/SteamGrouper, then you would type in \"steamgrouper\".",
                    "Blank Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                    return;
                }
                SteamGrouper.Properties.Settings.Default.group_gather_from = text_gather.Text;
                SteamGrouper.Properties.Settings.Default.Save();
                text_gather.Enabled = false;
                button_gather.Enabled = false;
                done = false;
                gatherList.Clear();
                numMember = 0;
                retry = 0;
                startTime = DateTime.Now;
                new Thread(() =>
                {
                    string path = "http://steamcommunity.com/groups/" + text_gather.Text + "/memberslistxml/?xml=1";
                    AppendText(text_log, "\r\n\r\nGathering list of SteamID64s from group \"" + text_gather.Text + "\"...\n");
                    gatherSID(text_gather.Text, path);
                }).Start();
            }
            else
            {
                MessageBox.Show("Please login to your Steam account first!",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
        }

        private static void text_gather_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_invite_Click(object sender, EventArgs e)
        {
            if (Bot.IsLoggedIn)
            {
                if (text_invite.Text != "" || text_invite.Text != "Group to invite members to")
                {
                    SteamGrouper.Properties.Settings.Default.group_invite_to = text_invite.Text;
                    SteamGrouper.Properties.Settings.Default.Save();
                    Thread thread1 = new Thread(() => Bot.invite(SelectInvite.selected, text_invite.Text, text_log, button_invite, label_invite));
                    if (!inviting)
                    {
                        SelectInvite selectInvite = new SelectInvite();
                        selectInvite.ShowDialog();
                        selectInvite.Focus();

                        while (Application.OpenForms.Count > 1)
                        {

                        }

                        if (SelectInvite.selectContinue)
                        {
                            string log = "\r\nInviting members of " + SelectInvite.selected + " to " + text_invite.Text + ", please wait...\n\n";
                            text_log.AppendText(log);
                            inviting = true;
                            thread1.IsBackground = true;
                            thread1.Start();
                            text_invite.Enabled = false;
                            button_invite.Text = "Cancel";
                        }
                    }
                    else
                    {
                        thread1.Interrupt();
                        thread1.Abort();
                        text_log.AppendText("\r\nProcess cancelled.\n");
                        text_invite.Enabled = true;
                        button_invite.Text = "Invite";
                        inviting = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please login to your Steam account first!",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
        }

        void gatherSID(string groupname, string path)
        {
            // Loading from a file, you can also load from a stream
            try
            {
                // ...
                var doc = XDocument.Load(path);

                var values = doc.Root.Descendants("members").Elements("steamID64")
                                                        .Select(x => x.Value)
                                                        .Distinct();
                var groupID = doc.Root.Descendants("groupID64")
                    .First()
                    .Value;

                gid = groupID;

                var memberCount = doc.Root.Descendants("memberCount")
                    .First()
                    .Value;

                var nextpage = doc.Root.Descendants("nextPageLink").Any();

                if (nextpage)
                {
                    if (!Welcome.trial)
                    {
                        new Thread(() =>
                        {
                            // Get next page
                            string newPath = doc.Root.Descendants("nextPageLink").First().Value;
                            this.gatherSID(groupname, newPath);
                        }).Start();
                    }
                }

                float progressFloat = 0;
                
                foreach (var name in values)
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        numMember++;
                        int count = Convert.ToInt32(memberCount);
                        if (Welcome.trial)
                        {
                            progressFloat = (float)numMember / 1000 * 100;
                        }
                        else
                        {
                            progressFloat = (float)numMember / count * 100;
                        }
                        UpdateProgress(progress_gather, (int)progressFloat);
                        if (Welcome.trial)
                        {
                            UpdateLabel(label_progress, "Processing " + numMember + " of 1000  members...");
                        }
                        else
                        {
                            if (numMember % 200 == 0)
                            {
                                TimeSpan timeRemaining = TimeSpan.FromTicks(DateTime.Now.Subtract(startTime).Ticks * (count - (numMember + 1)) / (numMember + 1));
                                double left = timeRemaining.TotalSeconds;
                                UpdateLabel(label_gather_est, "Estimated time remaining: " + Math.Ceiling(left) + " second(s)");
                            }
                            UpdateLabel(label_progress, "Processing " + numMember + " of " + count + " members...");
                        }
                        gatherList.Append(name).AppendLine();
                    }
                }

                if (!Welcome.trial)
                {
                    if (!nextpage)
                    {
                        for (int count = 0; count < 5; count++)
                        {
                            try
                            {
                                File.WriteAllText("lists/gl_" + groupname.ToLower(), gatherList.ToString());
                                if (!done)
                                    AppendText(text_log, "Done writing to file.");
                                done = true;
                                break;
                            }
                            catch
                            {
                                Console.WriteLine(count);
                                Thread.Sleep(2000);
                            }                            
                        }
                        if (!done)
                        {
                            AppendText(text_log, "Failed writing to file. Please try again!");
                        }
                        for (int count = 0; count < 5; count++)
                        {
                            // Stupid multithreaded... I have no idea what I'm doing
                            UpdateProgress(progress_gather, 0);
                            this.Invoke((Action)(() =>
                            {
                                text_gather.Enabled = true;
                                button_gather.Enabled = true;
                            }));
                            UpdateLabel(label_progress, "State: Idle");
                            UpdateLabel(label_gather_est, "Estimated time remaining:");
                            Thread.Sleep(100);
                        }
                    }
                }
                else if (!done)
                {
                    for (int count = 0; count < 5; count++)
                    {
                        try
                        {
                            File.WriteAllText("lists/gl_" + groupname.ToLower(), gatherList.ToString());
                            if (!done)
                                AppendText(text_log, "Done writing to file.");
                            done = true;
                            break;
                        }
                        catch
                        {
                            Console.WriteLine(count);
                            Thread.Sleep(2000);
                        }                        
                    }
                    UpdateProgress(progress_gather, 0);
                    this.Invoke((Action)(() =>
                    {
                        text_gather.Enabled = true;
                        button_gather.Enabled = true;
                    }));
                    UpdateLabel(label_progress, "State: Idle");
                    UpdateLabel(label_gather_est, "Estimated time remaining:");
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        if (retry <= 30)
                        {
                            Thread.Sleep(200);
                            retry++;
                            this.gatherSID(groupname, path);
                        }
                        else
                        {
                            AppendText(text_log, "Failed to gather info.");
                            retry = 0;
                        }
                    }
                    else
                    {
                        // no http status code available
                        if (retry <= 30)
                        {
                            Thread.Sleep(200);
                            retry++;
                            this.gatherSID(groupname, path);
                        }
                        else
                        {
                            AppendText(text_log, "Failed to gather info.");
                            retry = 0;
                        }
                    }
                    UpdateLabel(label_progress, "State: Idle");
                }
                else
                {
                    // no http status code available
                    if (retry <= 30)
                    {
                        Thread.Sleep(200);
                        retry++;
                        this.gatherSID(groupname, path);
                    }
                    else
                    {
                        AppendText(text_log, "Failed to gather info.");
                        retry = 0;
                    }
                    UpdateLabel(label_progress, "State: Idle");
                }
            }
            catch (Exception ex)
            {
                AppendText(text_log, "Error getting group info!");
                AppendText(text_log, ex.Message);
                UpdateLabel(label_progress, "State: Idle");
            }        
        }

        public static bool isOnline(string sid)
        {
            ulong steam = Convert.ToUInt64(sid);
            
            if (Bot.SteamFriends.GetFriendPersonaState(steam).ToString() == "Offline")
            {
                return false;
            }
            return true;
        }

        private void Countdown()
        {
            while (true)
            {
                Thread.Sleep(60000);
                Interface.subExInMinutes--;
                new Thread(RefreshSubscription).Start();
            }
        }

        private void Interface_Load(object sender, EventArgs e)
        {            
            progress_invite.Style = ProgressBarStyle.Blocks;
            offlineUsersToolStripMenuItem.Checked = true;
            label_info.Text = "Invite Speed: " + inviteSpeed + "s";
            label_targeting.Text = "Invite offline users: " + inviteOffline;
            if (Welcome.trial)
            {
                DateTime now = DateTime.UtcNow;
                text_log.AppendText(now.ToString("f") + " UTC\r\n\r\nWelcome to SteamGrouper " + BotVersion + "!\n\n");
                text_log.AppendText("\r\nYou are currently a trial user!\n\n");
            }
            else
            {
                unlockFullVersionToolStripMenuItem.Visible = false;
                deregisterThisComputerToolStripMenuItem.Visible = true;
                DateTime subNow = Convert.ToDateTime(Welcome.subNow);                
                DateTime subEx = Convert.ToDateTime(Welcome.subEx);
                TimeSpan difference = subEx - subNow;
                subExInMinutes = Convert.ToInt32(difference.TotalMinutes);
                Thread timer = new Thread(Countdown);
                timer.IsBackground = true;
                timer.Start();
                text_log.AppendText(subNow.ToString("f") + " UTC\r\n\r\nWelcome to SteamGrouper " + BotVersion + ", " + Welcome.username + "!\n\n");
                text_log.AppendText("Subscription expires: " + subEx + " UTC. That's " + difference.Days + " days, " + difference.Hours + " hours and " + difference.Minutes + " minutes left.\n\n");
            }
            text_gather.GotFocus += text_gather_GotFocus;
            text_invite.GotFocus += text_invite_GotFocus;
            inviteSpeed = SteamGrouper.Properties.Settings.Default.speed_normal;
            inviteSpeedMin = SteamGrouper.Properties.Settings.Default.speed_min;
            inviteSpeedMax = SteamGrouper.Properties.Settings.Default.speed_max;
            MinMax = SteamGrouper.Properties.Settings.Default.invite_minmax;
            if (MinMax)
            {
                label_info.Text = "Invite Speed: " + inviteSpeedMin + "s to " + inviteSpeedMax + "s";
            }
            else
            {
                label_info.Text = "Invite Speed: " + inviteSpeed + "s";
            }
            if (!String.IsNullOrEmpty(SteamGrouper.Properties.Settings.Default.group_invite_to))
                this.text_invite.Text = SteamGrouper.Properties.Settings.Default.group_invite_to;
            if (!String.IsNullOrEmpty(SteamGrouper.Properties.Settings.Default.group_gather_from))
                this.text_gather.Text = SteamGrouper.Properties.Settings.Default.group_gather_from;
        }

        private void text_gather_GotFocus(object sender, EventArgs e)
        {
            if (!text_gather_cleared)
            {
                text_gather.Clear();
                text_gather_cleared = true;
            }
        }

        private void text_invite_GotFocus(object sender, EventArgs e)
        {
            if (!text_invite_cleared)
            {
                text_invite.Clear();
                text_invite_cleared = true;
            }
        }

        private void offlineUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (offlineUsersToolStripMenuItem.Checked)
            {
                inviteOffline = true;
                label_targeting.Text = "Invite offline users: " + inviteOffline;
            }
            else
            {
                inviteOffline = false;
                label_targeting.Text = "Invite offline users: " + inviteOffline;
            }
        }

        private void speedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InviteSpeed inviteForm = new InviteSpeed();
            inviteForm.ShowDialog(this);
            inviteForm.Focus();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if (!loginClicked)
            {
                username = text_username.Text;
                password = text_password.Text;
                text_username.Enabled = false;
                text_password.Enabled = false;
                check_remember.Enabled = false;
                if (check_remember.Checked)
                {
                    SteamGrouper.Properties.Settings.Default.username = username;
                    SteamGrouper.Properties.Settings.Default.password = Encrypt(password);
                    SteamGrouper.Properties.Settings.Default.remember = true;
                    SteamGrouper.Properties.Settings.Default.Save();
                }
                else
                {
                    SteamGrouper.Properties.Settings.Default.username = "";
                    SteamGrouper.Properties.Settings.Default.password = "";
                    SteamGrouper.Properties.Settings.Default.remember = false;
                    SteamGrouper.Properties.Settings.Default.Save();
                }
                loginClicked = true;
            }
        }

        private void text_invite_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_gather_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Credits credits = new Credits();
            credits.ShowDialog();
            credits.Focus();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void unlockFullVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Welcome.trial)
            {
                MessageBox.Show("You will now be directed to http://www.steamgrouper.com/choose-a-subscription/.",
                                "Unlock Full Version",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);
                System.Diagnostics.Process.Start("http://www.steamgrouper.com/choose-a-subscription/");
            }
            else
            {
                MessageBox.Show("You are already using the full version! Thank you for supporting us.",
                                "Thank You",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void clearAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                string rootFolderPath = @"lists";
                string filesToDelete = @"gl_*";
                string[] fileList = System.IO.Directory.GetFiles(rootFolderPath, filesToDelete);
                foreach (string file in fileList)
                {
                    //Console.WriteLine(file + " will be deleted");
                    System.IO.File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(),
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string rootFolderPath = @"lists";
                string filesToDelete = @"bl_*";
                string[] fileList = System.IO.Directory.GetFiles(rootFolderPath, filesToDelete);
                foreach (string file in fileList)
                {
                    System.IO.File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(),
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string help = "To gather/invite from a group, you only need to enter the group"
                        + " name in their custom URL. For example, if the group URL is"
                        + " http://steamcommunity.com/groups/SteamGrouper, then you would"
                        + " type in \"steamgrouper\".";
            MessageBox.Show(help,
                            "Help",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
        }

        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        private void createCustomGatherlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SteamGrouper.CustomGatherlist createList = new SteamGrouper.CustomGatherlist();
            createList.Show();
            createList.Activate();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string help = "To gather/invite from a group, you only need to enter the group"
                        + " name in their custom URL. For example, if the group URL is"
                        + " http://steamcommunity.com/groups/SteamGrouper, then you would"
                        + " type in \"steamgrouper\".";
            MessageBox.Show(help,
                            "Help",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
        }

        private void whatsNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SteamGrouper.WhatsNew showNew = new SteamGrouper.WhatsNew();
            showNew.ShowDialog();
            showNew.Activate();
        }

        private void deregisterThisComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Are you sure? This will deregister your current computer and allow you to use SteamGrouper on a different one.";
            DialogResult choice = MessageBox.Show(new Form() { TopMost = true }, message,
                            "Deregister",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
            switch (choice)
            {
                case DialogResult.Yes:
                    if (!Deregister())
                    {
                        MessageBox.Show(new Form() { TopMost = true }, "Deregistration failed. Please try again later.",
                            "Failed to Deregister",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Hand,
                            MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        MessageBox.Show(new Form() { TopMost = true }, "Successfully deregistered! SteamGrouper will now close.",
                            "Successfully Deregistered",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                        Application.Exit();
                        Environment.Exit(0);
                    }
                    break;
                case DialogResult.No:                    
                    break;
            }
        }

        private bool Deregister()
        {
            //http://steamgrouper.com/app/dereg.php
            string hwid = Welcome.HWID();
            string hwid_hash = Welcome.CreatePasswordHash(hwid, "waylaidwanderer1158");
            string baseURL = "http://steamgrouper.com/app/dereg.php";
            string uri = baseURL + "?username=" + HttpUtility.UrlEncode(Welcome.username) + "&hwid=" + hwid_hash;
            string result = Welcome.HTTPRequest(uri);
            if (result != "done")
            {
                return false;
            }
            return true;
        }
    }
}
