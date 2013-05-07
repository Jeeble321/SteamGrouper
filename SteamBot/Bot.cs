using System;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using SteamKit2;
using SteamTrade;

namespace SteamBot
{
    public class Bot
    {
        public string BotControlClass;
        // If the bot is logged in fully or not.  This is only set
        // when it is.
        public static bool IsLoggedIn = false;

        // The bot's display name.  Changing this does not mean that
        // the bot's name will change.
        public string DisplayName { get; private set; }

        // The response to all chat messages sent to it.
        public string ChatResponse;

        // A list of SteamIDs that this bot recognizes as admins.
        public ulong[] Admins;
        public static SteamFriends SteamFriends;
        public SteamClient SteamClient;
        public SteamTrading SteamTrade;
        public static SteamUser SteamUser;

        // The current trade; if the bot is not in a trade, this is
        // null.
        public Trade CurrentTrade;

        public bool IsDebugMode = false;

        // The log for the bot.  This logs with the bot's display name.
        public Log log;

        public delegate UserHandler UserHandlerCreator(Bot bot, SteamID id);
        public UserHandlerCreator CreateHandler;
        Dictionary<ulong, UserHandler> userHandlers = new Dictionary<ulong, UserHandler>();

        List<SteamID> friends = new List<SteamID>();

        // The maximum amount of time the bot will trade for.
        public int MaximumTradeTime { get; private set; }

        // The maximum amount of time the bot will wait in between
        // trade actions.
        public int MaximiumActionGap { get; private set; }

        // The Steam Web API key.
        public string apiKey;

        // The prefix put in the front of the bot's display name.
        string DisplayNamePrefix;

        // Log level to use for this bot
        Log.LogLevel LogLevel;

        // The number, in milliseconds, between polls for the trade.
        int TradePollingInterval;

        public static string sessionId;
        public static string token;
        public string botSessionId = "";
        public string botToken = "";

        SteamUser.LogOnDetails logOnDetails;

        TradeManager tradeManager;

        static int retry = 0;
        static int successfulInvite = 0;

        public Bot(Configuration.BotInfo config, string apiKey, UserHandlerCreator handlerCreator, Interface gui, bool debug = false)
        {
            while (Interface.loginClicked == false)
            {
                // Wait for user to login
            }

            logOnDetails = new SteamUser.LogOnDetails
            {
                Username = Interface.username,
                Password = Interface.password
            };
            Admins = new ulong[1];
            Admins[0] = 123456789;
            this.apiKey = apiKey;
            LogLevel = Log.LogLevel.Info;
            //log = new Log(config.LogFile, this.DisplayName, LogLevel);
            CreateHandler = handlerCreator;
            BotControlClass = config.BotControlClass;

            // Hacking around https
            ServicePointManager.ServerCertificateValidationCallback += SteamWeb.ValidateRemoteCertificate;

            ////log.Debug("Initializing Steam Bot...");
            SteamClient = new SteamClient();
            SteamTrade = SteamClient.GetHandler<SteamTrading>();
            SteamUser = SteamClient.GetHandler<SteamUser>();
            SteamFriends = SteamClient.GetHandler<SteamFriends>();
            gui.UpdateLog("\r\nConnecting, please wait...");
            ////log.Info("Connecting...");
            SteamClient.Connect();

            Thread CallbackThread = new Thread(() => // Callback Handling
            {
                while (true)
                {
                    CallbackMsg msg = SteamClient.WaitForCallback(true);

                    HandleSteamMessage(msg, gui);
                }
            });

            CallbackThread.Start();
            ////log.Success("Done Loading Bot!");
            CallbackThread.Join();
        }

        /// <summary>
        /// Creates a new trade with the given partner.
        /// </summary>
        /// <returns>
        /// <c>true</c>, if trade was opened,
        /// <c>false</c> if there is another trade that must be closed first.
        /// </returns>
        public bool OpenTrade(SteamID other)
        {
            if (CurrentTrade != null)
                return false;

            SteamTrade.Trade(other);

            return true;
        }

        /// <summary>
        /// Closes the current active trade.
        /// </summary>
        public void CloseTrade()
        {
            if (CurrentTrade == null)
                return;

            UnsubscribeTrade(GetUserHandler(CurrentTrade.OtherSID), CurrentTrade);

            tradeManager.StopTrade();

            CurrentTrade = null;
        }

        void OnTradeTimeout(object sender, EventArgs args)
        {
            // ignore event params and just null out the trade.
            GetUserHandler(CurrentTrade.OtherSID).OnTradeTimeout();
        }

        void OnTradeEnded(object sender, EventArgs e)
        {
            CloseTrade();
        }

        bool HandleTradeSessionStart(SteamID other)
        {
            if (CurrentTrade != null)
                return false;

            try
            {
                tradeManager.InitializeTrade(SteamUser.SteamID, other);
                CurrentTrade = tradeManager.StartTrade(SteamUser.SteamID, other);
            }
            catch (SteamTrade.Exceptions.InventoryFetchException ie)
            {
                // we shouldn't get here because the inv checks are also
                // done in the TradeProposedCallback handler.
                string response = String.Empty;

                if (ie.FailingSteamId.ConvertToUInt64() == other.ConvertToUInt64())
                {
                    response = "Trade failed. Could not correctly fetch your backpack. Either the inventory is inaccessable or your backpack is private.";
                }
                else
                {
                    response = "Trade failed. Could not correctly fetch my backpack.";
                }

                SteamFriends.SendChatMessage(other,
                                             EChatEntryType.ChatMsg,
                                             response);

                //log.Info("Bot sent other: " + response);

                CurrentTrade = null;
                return false;
            }

            CurrentTrade.OnClose += CloseTrade;
            SubscribeTrade(CurrentTrade, GetUserHandler(other));

            return true;
        }

        void HandleSteamMessage(CallbackMsg msg, Interface gui)
        {
            //log.Debug(msg.ToString());

            #region Login
            msg.Handle<SteamClient.ConnectedCallback>(callback =>
            {
                //log.Debug("Connection Callback: " + callback.Result);

                if (callback.Result == EResult.OK)
                {
                    UserLogOn();
                }
                else
                {
                    gui.UpdateLog("Failed to connect to Steam Community, trying again...");
                    SteamClient.Connect();
                }

            });

            msg.Handle<SteamUser.LoggedOnCallback>(callback =>
            {
                //log.Debug("Logged On Callback: " + callback.Result);

                if (callback.Result != EResult.OK)
                {
                    gui.UpdateLog("\r\nLogin Error: " + callback.Result);
                    //log.Error("Login Error: " + callback.Result);
                    gui.Invoke((Action)(() => gui.text_username.Enabled = true));
                    gui.Invoke((Action)(() => gui.text_password.Enabled = true));
                    gui.Invoke((Action)(() => gui.button_login.Text = "Login"));                    
                }

                if (callback.Result == EResult.AccountLogonDenied)
                {
                    ////log.Interface("This account is protected by Steam Guard.  Enter the authentication code sent to the proper email: ");
                    gui.Invoke((Action)(() => gui.SteamGuard()));

                    while (Application.OpenForms.Count > 1)
                    {

                    }

                    logOnDetails.AuthCode = SteamGuard.authCode;
                }

                if (callback.Result == EResult.InvalidLoginAuthCode)
                {
                    gui.Invoke((Action)(() => gui.SteamGuard(true)));
                    
                    while (Application.OpenForms.Count > 1)
                    {

                    }

                    logOnDetails.AuthCode = SteamGuard.authCode;
                }
            });

            msg.Handle<SteamUser.LoginKeyCallback>(callback =>
            {
                while (true)
                {
                    bool authd = SteamWeb.Authenticate(callback, SteamClient, out sessionId, out token);
                    if (authd)
                    {
                        gui.UpdateLog("\r\nUser authenticated!");
                        break;
                    }
                    else
                    {
                        gui.UpdateLog("\r\nAuthentication failed, retrying in 2s...");
                        Thread.Sleep(2000);
                    }
                }

                SteamFriends.SetPersonaState(EPersonaState.Snooze);

                gui.UpdateLog("\r\nLogged in completely!\r\n");
                gui.Invoke((Action)(() => gui.button_login.Text = "Logged in"));

                IsLoggedIn = true;

                while (true)
                {
                    Console.ReadLine();
                }
            });

            // handle a special JobCallback differently than the others
            if (msg.IsType<SteamClient.JobCallback<SteamUser.UpdateMachineAuthCallback>>())
            {
                msg.Handle<SteamClient.JobCallback<SteamUser.UpdateMachineAuthCallback>>(
                    jobCallback => OnUpdateMachineAuthCallback(jobCallback.Callback, jobCallback.JobID)
                );
            }
            #endregion

            #region Friends
            /*msg.Handle<SteamFriends.FriendsListCallback>(callback =>
            {
                foreach (SteamFriends.FriendsListCallback.Friend friend in callback.FriendList)
                {
                    if (!friends.Contains(friend.SteamID))
                    {
                        friends.Add(friend.SteamID);
                        if (friend.Relationship == EFriendRelationship.PendingInvitee &&
                            GetUserHandler(friend.SteamID).OnFriendAdd())
                        {
                            SteamFriends.AddFriend(friend.SteamID);
                        }
                    }
                }
            });*/

            msg.Handle<SteamFriends.FriendMsgCallback>(callback =>
            {
                EChatEntryType type = callback.EntryType;

                if (callback.EntryType == EChatEntryType.ChatMsg ||
                    callback.EntryType == EChatEntryType.Emote)
                {
                    //log.Info(String.Format("Chat Message from {0}: {1}",
                                         //SteamFriends.GetFriendPersonaName(callback.Sender),
                                         //callback.Message
                                         //));
                    GetUserHandler(callback.Sender).OnMessage(callback.Message, type);
                }
            });
            #endregion

            #region Trading
            msg.Handle<SteamTrading.SessionStartCallback>(callback =>
            {
                bool started = HandleTradeSessionStart(callback.OtherClient);

                if (!started)
                    log.Error("Could not start the trade session.");
                else
                    log.Debug("SteamTrading.SessionStartCallback handled successfully. Trade Opened.");
            });

            msg.Handle<SteamTrading.TradeProposedCallback>(callback =>
            {
                try
                {
                    tradeManager.InitializeTrade(SteamUser.SteamID, callback.OtherClient);
                }
                catch
                {
                    SteamFriends.SendChatMessage(callback.OtherClient,
                                                 EChatEntryType.ChatMsg,
                                                 "Trade declined. Could not correctly fetch your backpack.");

                    SteamTrade.RespondToTrade(callback.TradeID, false);
                    return;
                }

                if (tradeManager.OtherInventory.IsPrivate)
                {
                    SteamFriends.SendChatMessage(callback.OtherClient,
                                                 EChatEntryType.ChatMsg,
                                                 "Trade declined. Your backpack cannot be private.");

                    SteamTrade.RespondToTrade(callback.TradeID, false);
                    return;
                }

                if (CurrentTrade == null && GetUserHandler(callback.OtherClient).OnTradeRequest())
                    SteamTrade.RespondToTrade(callback.TradeID, true);
                else
                    SteamTrade.RespondToTrade(callback.TradeID, false);
            });

            msg.Handle<SteamTrading.TradeResultCallback>(callback =>
            {
                //log.Debug("Trade Status: " + callback.Response);

                if (callback.Response == EEconTradeResponse.Accepted)
                {
                    //log.Info("Trade Accepted!");
                }
                if (callback.Response == EEconTradeResponse.Cancel ||
                    callback.Response == EEconTradeResponse.ConnectionFailed ||
                    callback.Response == EEconTradeResponse.Declined ||
                    callback.Response == EEconTradeResponse.Error ||
                    callback.Response == EEconTradeResponse.InitiatorAlreadyTrading ||
                    callback.Response == EEconTradeResponse.TargetAlreadyTrading ||
                    callback.Response == EEconTradeResponse.Timeout ||
                    callback.Response == EEconTradeResponse.TooSoon ||
                    callback.Response == EEconTradeResponse.VacBannedInitiator ||
                    callback.Response == EEconTradeResponse.VacBannedTarget ||
                    callback.Response == EEconTradeResponse.NotLoggedIn) // uh...
                {
                    CloseTrade();
                }

            });
            #endregion

            #region Disconnect
            msg.Handle<SteamUser.LoggedOffCallback>(callback =>
            {
                IsLoggedIn = false;
                gui.UpdateLog("\r\nLogged Off: " + callback.Result);
                //log.Warn("Logged Off: " + callback.Result);
            });

            msg.Handle<SteamClient.DisconnectedCallback>(callback =>
            {
                IsLoggedIn = false;
                CloseTrade();
                gui.UpdateLog("\r\nDisconnected from Steam Network! Trying to reconnect...");
                //log.Warn("Disconnected from Steam Network!");
                SteamClient.Connect();
            });
            #endregion
        }

        public static void invite(string groupFrom, string group, TextBox text_log, Button button_invite, Label label_invite)
        {
            successfulInvite = 0;
            int counter = 0;
            string line = "";
            string path = "http://steamcommunity.com/groups/" + group + "/memberslistxml/?xml=1e";
            retry = 0;
            string groupID = getGroupID(group, path);
            if (groupID != "")
            {
                Interface.AppendText(text_log, "\r\nProcessing gather list... This may take a few minutes if you have large list. Please be patient.\n");
                // Read from the gather list
                System.IO.StreamReader file = new System.IO.StreamReader("lists/gl_" + groupFrom.ToLower());
                while ((line = file.ReadLine()) != null)
                {
                    if (Interface.subExInMinutes <= 0 && !Welcome.trial)
                    {
                        Console.WriteLine("Subscription expired.");
                        break;
                    }
                    if (!Interface.inviting)
                    {
                        Console.WriteLine("Inviting cancelled.");
                        break;
                    }
                    else
                    {
                        Interface.currentSID = Convert.ToUInt64(line);
                        // Check if user has already been invited
                        bool invited = false;
                        if (!File.Exists("lists/bl_" + group.ToLower()))
                        {
                            using (StreamWriter sw = File.CreateText("lists/bl_" + group.ToLower()))
                            {
                                sw.Close();
                            }
                        }
                        System.IO.StreamReader blacklist = new System.IO.StreamReader("lists/bl_" + group.ToLower());
                        string compare;
                        while ((compare = blacklist.ReadLine()) != null)
                        {
                            if (Interface.currentSID.ToString() == compare)
                            {
                                //Console.Write("User " + currentSID + " already invited.      \r");
                                invited = true;
                                break;
                            }
                        }
                        blacklist.Close();
                        if (!invited && Interface.inviteOffline)
                        {
                            if (successfulInvite % 250 == 0 && successfulInvite != 0 && Welcome.trial)
                            {
                                MessageBox.Show("For uninterrupted inviting, please consider buying a subscription! Visit http://www.steamgrouper.com/",
                                "Trial Mode",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);
                            }
                            counter++;
                            Interface.AppendText(text_log, "\r\nInviting user #" + counter + ": " + Interface.currentSID);
                            DoInvite(file, Interface.currentSID, groupID, group, text_log, label_invite);
                        }
                        else if (!invited && !Interface.inviteOffline)
                        {
                            if (successfulInvite % 250 == 0 && successfulInvite != 0 && Welcome.trial)
                            {
                                MessageBox.Show("For uninterrupted inviting, please consider buying a subscription! Visit http://www.steamgrouper.com/",
                                "Trial Mode",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);
                            }
                            string playerURL = "http://steamcommunity.com/profiles/" + Interface.currentSID + "?xml=1";
                            string playerProfile = SteamGrouper.Util.HTTPRequest(playerURL);
                            string status = "online";
                            if (playerProfile != "")
                            {
                                status = SteamGrouper.Util.ParseBetween(playerProfile, "<onlineState>", "</onlineState>");
                            }
                            if (status != "offline")
                            {
                                counter++;
                                Interface.AppendText(text_log, "\r\nInviting user #" + counter + ": " + Interface.currentSID);
                                DoInvite(file, Interface.currentSID, groupID, group, text_log, label_invite);
                            }
                        }
                    }
                }
                file.Close();
                Interface.AppendText(text_log, "\r\nDone. Invited " + counter + " members to your group.");
                Interface.UpdateLabel(label_invite, "State: Idle");
                Interface.UpdateButton(button_invite, "Invite");
                if (Interface.subExInMinutes <= 0 && !Welcome.trial)
                {
                    MessageBox.Show("We're sorry, but your subscription has just expired. To continue using the full version of SteamGrouper, please add more time to your subscription at http://www.steamgrouper.com/choose-a-subscription/",
                                    "Subscription Expired",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("Failed to get groupID. Please try again.",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
        }

        static void SkipOffline(StreamReader file, string group)
        {
            string line = "";
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine("Attempting to skip offline users");
                Interface.currentSID = Convert.ToUInt64(line);
                bool invited = false;
                if (!File.Exists("lists/bl_" + group.ToLower()))
                {
                    using (StreamWriter sw = File.CreateText("lists/bl_" + group.ToLower()))
                    {
                        sw.Close();
                    }
                }
                System.IO.StreamReader blacklist = new System.IO.StreamReader("lists/bl_" + group.ToLower());
                string compare;
                while ((compare = blacklist.ReadLine()) != null)
                {
                    if (Interface.currentSID.ToString() == compare)
                    {
                        invited = true;
                        break;
                    }
                }
                blacklist.Close();
                if (!invited)
                {
                    if (Interface.isOnline(line))
                    {
                        Interface.currentSID = Convert.ToUInt64(line);
                        break;
                    }
                }
            }
        }

        static string getGroupID(string groupname, string path)
        {
            string thisGroupID = "";

            // Loading from a file, you can also load from a stream
            try
            {
                // ...
                var doc = XDocument.Load(path);

                var groupID = doc.Root.Descendants("groupID64")
                    .First()
                    .Value;

                thisGroupID = groupID;
                return thisGroupID;
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
                            Thread.Sleep(500);
                            retry++;
                            getGroupID(groupname, path);
                        }
                        else
                        {
                            retry = 0;
                        }
                    }
                    else
                    {
                        // no http status code available
                        if (retry <= 30)
                        {
                            Thread.Sleep(500);
                            retry++;
                            getGroupID(groupname, path);
                        }
                        else
                        {
                            retry = 0;
                        }
                    }
                }
                else
                {
                    // no http status code available
                    if (retry <= 30)
                    {
                        Thread.Sleep(500);
                        retry++;
                        getGroupID(groupname, path);
                    }
                    else
                    {
                        retry = 0;
                    }
                }
            }
            catch (ArgumentNullException)
            {
                if (retry <= 30)
                {
                    Thread.Sleep(500);
                    retry++;
                    getGroupID(groupname, path);
                }
                else
                {
                    retry = 0;
                }
            }
            catch (System.Xml.XmlException)
            {
                if (retry <= 30)
                {
                    Thread.Sleep(500);
                    retry++;
                    getGroupID(groupname, path);
                }
                else
                {
                    retry = 0;
                }
            }

            return thisGroupID;
        }

        public static void DoInvite(StreamReader file, ulong currentSID, string groupID, string group, TextBox text_log, Label label_invite)
        {
            InviteToGroup(currentSID, groupID, group, text_log, label_invite);
            if (!Interface.MinMax)
            {
                Interface.AppendText(text_log, "\r\nWaiting " + Interface.inviteSpeed + " seconds...\n");
                for (int count = Interface.inviteSpeed; count > 0; --count)
                {
                    Interface.UpdateLabel(label_invite, "State: Waiting " + count + " seconds...\n");
                    Thread.Sleep(1000);
                }
                Interface.UpdateLabel(label_invite, "State: Waiting 0 seconds...\n");
            }
            else
            {
                Random rnd = new Random();
                int wait = rnd.Next(Interface.inviteSpeedMin, Interface.inviteSpeedMax);
                Interface.AppendText(text_log, "\r\nWaiting " + wait + " seconds...\n");
                for (int count = wait; count > 0; --count)
                {
                    Interface.UpdateLabel(label_invite, "State: Waiting " + count + " seconds...\n");
                    Thread.Sleep(1000);
                }
                Interface.UpdateLabel(label_invite, "State: Waiting 0 seconds...\n");
            }
        }

        public static void InviteToGroup(ulong invitee, string gid, string groupname, TextBox text_log, Label label_invite)
        {
            CookieContainer cookies = new CookieContainer();
            cookies.Add(new Cookie("sessionid", Bot.sessionId, String.Empty, "steamcommunity.com"));
            cookies.Add(new Cookie("steamLogin", Bot.token, String.Empty, "steamcommunity.com"));
            var data = new NameValueCollection();
            data.Add("type", "groupInvite");
            data.Add("inviter", Bot.SteamUser.SteamID.ConvertToUInt64().ToString());
            data.Add("invitee", invitee.ToString());
            data.Add("group", gid);
            data.Add("sessionID", Bot.sessionId);
            try
            {
                HttpWebResponse webResponse = SteamWeb.Request("http://steamcommunity.com/actions/GroupInvite", "POST", data, cookies, false);
                Stream response = null;
                response = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(response);
                string result = reader.ReadToEnd();
                if (result.Contains("![CDATA[OK]]"))
                {
                    Interface.AppendText(text_log, "\r\nSuccess!\n");
                    Console.WriteLine("Success.");
                    blacklist(invitee, groupname);
                    successfulInvite++;
                }
                else
                {
                    int start = result.IndexOf("![CDATA[");
                    int stop = result.IndexOf("]]");
                    string output = result.Substring(start + 8, stop - start - 8);
                    Interface.AppendText(text_log, "\r\nError: " + output + "\n");
                    Console.WriteLine("Error: " + output);
                    blacklist(invitee, groupname);
                }
            }
            catch
            {
                Interface.UpdateLabel(label_invite, "Failed to get response; retrying...");
                InviteToGroup(invitee, gid, groupname, text_log, label_invite);
            }
        }

        public static void blacklist(ulong user, string groupname)
        {
            try
            {
                StreamWriter wr = File.AppendText("lists/bl_" + groupname.ToLower());
                wr.WriteLine(user);
                wr.Close();
            }
            catch
            {

            }
        }

        void UserLogOn()
        {
            // get sentry file which has the machine hw info saved 
            // from when a steam guard code was entered
            FileInfo fi = new FileInfo(String.Format("{0}.sentryfile", logOnDetails.Username));

            if (fi.Exists && fi.Length > 0)
                logOnDetails.SentryFileHash = SHAHash(File.ReadAllBytes(fi.FullName));
            else
                logOnDetails.SentryFileHash = null;

            SteamUser.LogOn(logOnDetails);
        }

        UserHandler GetUserHandler(SteamID sid)
        {
            if (!userHandlers.ContainsKey(sid))
            {
                userHandlers[sid.ConvertToUInt64()] = CreateHandler(this, sid);
            }
            return userHandlers[sid.ConvertToUInt64()];
        }

        static byte[] SHAHash(byte[] input)
        {
            SHA1Managed sha = new SHA1Managed();

            byte[] output = sha.ComputeHash(input);

            sha.Clear();

            return output;
        }

        void OnUpdateMachineAuthCallback(SteamUser.UpdateMachineAuthCallback machineAuth, JobID jobId)
        {
            byte[] hash = SHAHash(machineAuth.Data);

            File.WriteAllBytes(String.Format("{0}.sentryfile", logOnDetails.Username), machineAuth.Data);

            var authResponse = new SteamUser.MachineAuthDetails
            {
                BytesWritten = machineAuth.BytesToWrite,
                FileName = machineAuth.FileName,
                FileSize = machineAuth.BytesToWrite,
                Offset = machineAuth.Offset,

                SentryFileHash = hash, // should be the sha1 hash of the sentry file we just wrote

                OneTimePassword = machineAuth.OneTimePassword, // not sure on this one yet, since we've had no examples of steam using OTPs

                LastError = 0, // result from win32 GetLastError
                Result = EResult.OK, // if everything went okay, otherwise ~who knows~

                JobID = jobId, // so we respond to the correct server job
            };

            // send off our response
            SteamUser.SendMachineAuthResponse(authResponse);
        }

        /// <summary>
        /// Subscribes all listeners of this to the trade.
        /// </summary>
        public void SubscribeTrade(Trade trade, UserHandler handler)
        {
            trade.OnClose += handler.OnTradeClose;
            trade.OnError += handler.OnTradeError;
            //trade.OnTimeout += OnTradeTimeout;
            trade.OnAfterInit += handler.OnTradeInit;
            trade.OnUserAddItem += handler.OnTradeAddItem;
            trade.OnUserRemoveItem += handler.OnTradeRemoveItem;
            trade.OnMessage += handler.OnTradeMessage;
            trade.OnUserSetReady += handler.OnTradeReady;
            trade.OnUserAccept += handler.OnTradeAccept;
        }

        /// <summary>
        /// Unsubscribes all listeners of this from the current trade.
        /// </summary>
        public void UnsubscribeTrade(UserHandler handler, Trade trade)
        {
            trade.OnClose -= handler.OnTradeClose;
            trade.OnError -= handler.OnTradeError;
            //Trade.OnTimeout -= OnTradeTimeout;
            trade.OnAfterInit -= handler.OnTradeInit;
            trade.OnUserAddItem -= handler.OnTradeAddItem;
            trade.OnUserRemoveItem -= handler.OnTradeRemoveItem;
            trade.OnMessage -= handler.OnTradeMessage;
            trade.OnUserSetReady -= handler.OnTradeReady;
            trade.OnUserAccept -= handler.OnTradeAccept;
        }
    }
}