using System;
using System.Threading;
using SteamKit2;
using SteamTrade;
using SteamGrouper;
using System.Windows.Forms;

namespace SteamBot
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Welcome login = new Welcome();
            Interface mainForm = new Interface();
            Splash splash = new Splash();

            Configuration config = new Configuration();
            Configuration.BotInfo info = new Configuration.BotInfo();
            info.Admins = new ulong[1];
            info.Admins[0] = 0123456789;
            info.BotControlClass = "";
            info.ChatResponse = "";
            info.DisplayName = "";
            info.DisplayNamePrefix = "";
            info.LogFile = "";
            info.LogLevel = "Info";
            info.MaximumActionGap = 0;
            info.MaximumTradeTime = 0;
            info.Password = "";
            info.TradePollingInterval = 999;
            info.Username = "";    
            
            splash.Show();
            
            new Thread(() =>
            {
                int crashes = 0;
                while (crashes < 1000)
                {
                    try
                    {                            
                        new Bot(info, "120F03A1543C9F22AA9BF4C7B6442154", (Bot bot, SteamID sid) => {
                            return (SteamBot.UserHandler)System.Activator.CreateInstance(Type.GetType(bot.BotControlClass), new object[] { bot, sid });  
                        }, mainForm, false);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error With Bot: " + e);
                        crashes++;
                    }
                }
            }).Start();

            Thread.Sleep(3000);
            splash.Close();
            splash.Dispose();
            Application.Run(login);

            while (!Welcome.trial || !Welcome.loggedIn)
            {
                if (Welcome.loggedIn || Welcome.trial)
                {
                    mainForm.Text = "SteamGrouper " + Interface.BotVersion;
                    mainForm.ShowDialog();
                    mainForm.Activate();
                    break;
                }
                else if (Application.OpenForms.Count == 0)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
