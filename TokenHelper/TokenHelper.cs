using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net.Http;
using System.Net;
using Discord.WebSocket;

namespace KaedithSpammer.TokenHelper
{
    public static class TokenHelper
    {
        public static List<string> Tokens { get; set; }
        public static void Setup()
        {
            Tokens = new List<string>();

            Tokens = File.ReadAllLines("Tokens.txt").ToList();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Kaedith RaidClient] Loading Token(s)...");
            Console.WriteLine("[Kaedith RaidClient] Checking Token(s)...");
            /*
            foreach (string token in Tokens)
            {
                    bool checc = CheckToken(token);
                    Console.ForegroundColor = (!checc ? ConsoleColor.Red : ConsoleColor.Green);
                    Console.WriteLine(!checc ? $"Token {token} is invalid. Removed from Token list." : $"Token {token} is valid. Kept in Token list.");

            }
            */
        }

        private static bool CheckToken(string token)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.Proxy = new WebProxy(ProxyHelper.ProxyHelper.GetRandomProxy());
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("Authorization", token);

            try
            {
                var response = client.GetAsync("https://discordapp.com/api/users/@me");

                if (response.Result.StatusCode == (HttpStatusCode)401)
                {
                    Tokens.Remove(token);

                    File.WriteAllLines("Tokens.txt", Tokens);

                    return false; //Invalid token. 
                }
                else
                {
                    if (response.Result.StatusCode == (HttpStatusCode)403)
                    {
                        Console.WriteLine($"Possibly invalid Token: {token}. Rechecking...");
                        Thread.Sleep(2000);
                        new Thread(() => CheckToken(token)).Start();

                        return false;
                    }
                    else
                    {
                        if (response.Result.StatusCode == (HttpStatusCode)405)
                        {
                            Console.WriteLine($"Possibly invalid Token: {token}. Rechecking...");
                            Thread.Sleep(2000);
                            new Thread(() => CheckToken(token)).Start();
                            return false;
                        }
                        else
                        {
                            return true; //Valid token.
                        }
                    }

                }
            }
           catch(Exception e)
            {
                if (e is System.Net.WebException)
                {
                    Console.WriteLine("A proxy died. Retrying...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    new Thread(() => CheckToken(token)).Start();
                    return false;
                }
                else
                {
                    Console.WriteLine($"An exception occurred: {e}");
                    Thread.Sleep(2000);
                    Console.Clear();
                    new Thread(() => CheckToken(token)).Start();
                    return false;
                }
            }
        }
        public static void SendMessage(string text, int am, ulong channelID)
        {
            try
            {
                new Thread(() =>
                {
                    for (int i = 0; i < am; i++)
                    {
                        foreach (string token in Tokens)
                        {

                            ServerSpammer.ServerAttack.Send(text, channelID, token);

                        }
                    }
                }).Start();
            }

            catch (Exception e)
            {
                Console.WriteLine($"An exception has occurred. Details: {e}");
                return;
            }
        }
        public static void JoinServer(string InviteUrl)
        {
            try
            {
                Parallel.Invoke(() =>
                {
                    new Thread(() =>
                    {
                        foreach (string token in Tokens)
                        {

                            ServerSpammer.ServerAttack.Join(token, ParseInvite(InviteUrl));


                        }
                    }).Start();
                });
               
            }
            catch(Exception e)
            {
                Console.WriteLine($"An exception has occurred. Details: {e}");
                return;
            }
          
        }
        public static void GroupJoin(string InviteUrl)
        {
            try
            {
                Parallel.Invoke(() =>
                {
                    new Thread(() =>
                    {
                        foreach (string token in Tokens)
                        {

                            ServerSpammer.ServerAttack.GroupJoin(token, ParseInvite(InviteUrl));


                        }
                    }).Start();
                });

            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception has occurred. Details: {e}");
                return;
            }

        }
        public static void SendDm(ulong who, string text)
        {
            try
            {
                Parallel.Invoke(() =>
                {
                    new Thread(() =>
                    {
                        foreach (string token in Tokens)
                        {
                            DmSpammer.DmAttack.SendMessage(who, token, text);
                        }
                    }).Start();
                });

            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception has occurred. Details: {e}");
                return;
            }

        }
        private static string ParseInvite(string inviteUrl)
        {
            string returninv = null;
            if (inviteUrl.Substring(0, 19) == "https://discord.gg/")
            {
                returninv = inviteUrl.Substring(19);
            }

            if (inviteUrl.Substring(0, 18) == "http://discord.gg/")
            {
                returninv = inviteUrl.Substring(18);
            }
            return (returninv == null ? null : returninv);
        }
    }
}
