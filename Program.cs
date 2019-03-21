using KaedithSpammer.Usefulstuff;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KaedithSpammer
{
    class Program
    {
        static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();
        public async Task Start()
        {
            Setup();
            Utils.Start();
            Console.Title = "Kaedith RaidClient made by Yaekith. Kaedith Gang 2019. Type 'help' or 'cmds' for commands.";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"====================================================================================================================================");
            Console.WriteLine(@"                         __   __         _    _ _   _                           _                                                   ");
            Console.WriteLine(@"                         \ \ / /        | |  (_) | | |                         | |                                                  ");
            Console.WriteLine(@"                          \ V /__ _  ___| | ___| |_| |__   __      ____ _ ___  | |__   ___ _ __ ___                                 ");
            Console.WriteLine(@"                           \ // _` |/ _ \ |/ / | __| '_ \  \ \ /\ / / _` / __| | '_ \ / _ \ '__/ _ \                                ");
            Console.WriteLine(@"                           | | (_| |  __/   <| | |_| | | |  \ V  V / (_| \__ \ | | | |  __/ | |  __/                                ");
            Console.WriteLine(@"                           \_/\__,_|\___|_|\_\_|\__|_| |_|   \_/\_/ \__,_|___/ |_| |_|\___|_|  \___|                                ");
            Console.WriteLine("                                                 ||  Don't fuck with me ||                                                           ");
            Console.WriteLine("=====================================================================================================================================");
            Console.WriteLine("Kaedith RaidClient has Loaded. Type 'help' or 'cmds' for a list of commands.");
            new Thread(() => ConsoleCmds()).Start();
            await Task.Delay(-1);
        }

        private void ConsoleCmds()
        {
            for(; ;)
            {
                string cmd = Console.ReadLine();

                switch(cmd.ToLower())
                {
                    case "cmds":
                    case "help":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("=                        KAEDITH CLIENT COMMANDS                        =");
                        Console.WriteLine("help -- Displays this command menu.");
                        Console.WriteLine("cmds -- Does the same as 'help'.");
                        Console.WriteLine("msgattack -- Spam a server with messages.");
                        Console.WriteLine("dmattack -- Attack someone's dms with messages.");
                        Console.WriteLine("friendattack -- Attack someone's friends list with requests.");
                        Console.WriteLine("joinattack -- Attack someone's servers with a bunch of accounts joining.");
                        Console.WriteLine("groupjoinattack -- Attack someone's discord group with a bunch of accounts joining.");
                        Console.WriteLine("clear -- Clears the console");
                        Console.WriteLine("========================================================================");
                        break;

                    case "joinattack":
                        Console.WriteLine("Invite Url?");
                        string inviteCode = Console.ReadLine();

                        try
                        {
                            TokenHelper.TokenHelper.JoinServer(inviteCode);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("An exception has occurred! Details: " + e);
                            return;
                        }
                        break;
                    case "groupjoinattack":
                        Console.WriteLine("Invite Url?");
                        string inviteCodee = Console.ReadLine();

                        try
                        {
                            TokenHelper.TokenHelper.GroupJoin(inviteCodee);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An exception has occurred! Details: " + e);
                            return;
                        }
                        break;
                    case "msgattack":
                        Console.WriteLine("Invite Url?");
                        string Code = Console.ReadLine();
                        Console.WriteLine("ChannelID?");
                        ulong chan = ulong.Parse(Console.ReadLine());
                        Console.WriteLine("Text?");
                        string text = Console.ReadLine();
                        Console.WriteLine("Message amount? ");
                        int messageAmount = int.Parse(Console.ReadLine());

                        try
                        {
                            //new Thread(() => 
                            TokenHelper.TokenHelper.JoinServer(Code);
                            //.Start();

                            TokenHelper.TokenHelper.SendMessage(text, messageAmount, chan);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An exception has occurred! Details: " + e);
                            return;
                        }
                        break;
                    case "dmattack":
                        Console.WriteLine("Invite Url?");
                        string inviteCodeef = Console.ReadLine();
                        Console.WriteLine("UserID to send message to?");
                        ulong userid = ulong.Parse(Console.ReadLine());

                        Console.WriteLine("Message?");
                        string msg = Console.ReadLine();
                        try
                        {
                            TokenHelper.TokenHelper.JoinServer(inviteCodeef);

                            TokenHelper.TokenHelper.SendDm(userid, msg);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An exception has occurred! Details: " + e);
                            return;
                        }
                        break;
                    case "clear":
                        Console.Clear();
                        Console.WriteLine(@"====================================================================================================================================");
                        Console.WriteLine(@"                         __   __         _    _ _   _                           _                                                   ");
                        Console.WriteLine(@"                         \ \ / /        | |  (_) | | |                         | |                                                  ");
                        Console.WriteLine(@"                          \ V /__ _  ___| | ___| |_| |__   __      ____ _ ___  | |__   ___ _ __ ___                                 ");
                        Console.WriteLine(@"                           \ // _` |/ _ \ |/ / | __| '_ \  \ \ /\ / / _` / __| | '_ \ / _ \ '__/ _ \                                ");
                        Console.WriteLine(@"                           | | (_| |  __/   <| | |_| | | |  \ V  V / (_| \__ \ | | | |  __/ | |  __/                                ");
                        Console.WriteLine(@"                           \_/\__,_|\___|_|\_\_|\__|_| |_|   \_/\_/ \__,_|___/ |_| |_|\___|_|  \___|                                ");
                        Console.WriteLine("                                                 ||  Don't fuck with me ||                                                           ");
                        Console.WriteLine("=====================================================================================================================================");
                        Console.WriteLine("Kaedith RaidClient has Loaded. Type 'help' or 'cmds' for a list of commands.");
                        break;
                }
            }
        }

        private void Setup()
        {
          if (!File.Exists("Tokens.txt"))
            {
                File.Create("Tokens.txt").Close();
            }

          if (!File.Exists("Proxies.txt"))
            {
                File.WriteAllText("Proxies.txt", new WebClient().DownloadString("https://api.proxyscrape.com/?request=displayproxies&proxytype=http"));
            }
          
           
        }
    }
}
