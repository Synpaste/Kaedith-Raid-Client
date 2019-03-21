using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KaedithSpammer.ServerSpammer
{

    public class SendMsg
    {
        public string content { get; set; }
        public string nonce { get; set; }
        public bool tts { get; set; }
    }

    public class Channel
    {
        public object[] permission_overwrites { get; set; }
        public string name { get; set; }
        public string parent_id { get; set; }
        public bool nsfw { get; set; }
        public int position { get; set; }
        public string guild_id { get; set; }
        public int type { get; set; }
        public string id { get; set; }
        public object topic { get; set; }
        public int rate_limit_per_user { get; set; }
        public string last_message_id { get; set; }
        public int user_limit { get; set; }
        public int bitrate { get; set; }
    }

    public static class ServerAttack
    {
        public static void Join(string token, string inviteCode)
        {

                Parallel.Invoke(() =>
                {
                    new Thread(() =>
                    {

                    try
                    {
                        HttpClientHandler handler = new HttpClientHandler();
                        handler.Proxy = new WebProxy(ProxyHelper.ProxyHelper.GetRandomProxy());
                        HttpClient client = new HttpClient(handler);

                        client.DefaultRequestHeaders.Add("Authorization", token);

                        var resp = client.PostAsync("https://discordapp.com/api/v6/invite/" + inviteCode, new StringContent("{}", Encoding.UTF8, "application/json"));

                        Console.WriteLine(resp.Result.Content.ReadAsStringAsync().Result);
                        if (resp.Result.StatusCode == HttpStatusCode.OK)
                        {
                            Console.WriteLine($"{token} has joined.");
                        }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An exception has occurred! Details: " + e);
                            return;
                        }
                    }).Start();
                });






        }
        public static void GroupJoin(string token, string inviteCode)
        {

            Parallel.Invoke(() =>
            {
                new Thread(() =>
                {

                    try
                    {
                        HttpClientHandler handler = new HttpClientHandler();
                        handler.Proxy = new WebProxy(ProxyHelper.ProxyHelper.GetRandomProxy());
                        HttpClient client = new HttpClient(handler);

                        client.DefaultRequestHeaders.Add("Authorization", token);

                        var resp = client.PostAsync("https://discordapp.com/api/v6/invite/" + inviteCode, new StringContent("{}", Encoding.UTF8, "application/json"));

                        Console.WriteLine(resp.Result.Content.ReadAsStringAsync().Result);
                        if (resp.Result.StatusCode == HttpStatusCode.OK)
                        {
                            Console.WriteLine($"{token} has joined the group.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An exception has occurred! Details: " + e);
                        return;
                    }
                }).Start();
            });






        }

        internal static void Send(string text, ulong channelID, string token)
        {
 
                Parallel.Invoke(() =>
                {
                    new Thread(() =>
                    {
                    try
                    {
                        HttpClientHandler handler = new HttpClientHandler();
                        handler.Proxy = new WebProxy(ProxyHelper.ProxyHelper.GetRandomProxy());

                        HttpClient client = new HttpClient(handler);
                        client.DefaultRequestHeaders.Add("Authorization", token);
                        client.PostAsync($"https://discordapp.com/api/v6/channels/{channelID}/messages", new StringContent(JsonConvert.SerializeObject(new SendMsg { content = text, nonce = null, tts = false }), Encoding.UTF8, "application/json"));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An exception has occurred! Details: " + e);
                            return;
                        }
                    }).Start();
                });





        }
    }
}
