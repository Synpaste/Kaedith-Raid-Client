using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KaedithSpammer.DmSpammer
{

    public class DmMessageClass
    {
        public string content { get; set; }
        public string nonce { get; set; }
        public bool tts { get; set; }
    }

    public class YeetResponseDmChannelGet
    {
        public string last_message_id { get; set; }
        public int type { get; set; }
        public string id { get; set; }

        
        public Recipient[] recipients { get; set; }
    }

    public class Recipient
    {
        public string username { get; set; }
        public string discriminator { get; set; }
        public string id { get; set; }
        public string avatar { get; set; }
    }

    public static class DmAttack
    {
        public static Recipient GetRecipient(YeetResponseDmChannelGet shit, ulong filter)
        {
            foreach(var user in shit.recipients)
            {
                if (user.id == filter.ToString())
                {
                    return user;
                }
            }
            return null;
        }
        public static ulong UserIDToDmChannelID(ulong who, string token)
        {
            try
            {

                        HttpClientHandler handler = new HttpClientHandler();
                        handler.Proxy = new WebProxy(ProxyHelper.ProxyHelper.GetRandomProxy());

                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Add("Authorization", token);

                        var response = client.GetAsync($"https://discordapp.com/api/v6/users/@me/channels");

                        var jsonResponse = JsonConvert.DeserializeObject<YeetResponseDmChannelGet>(response.Result.Content.ReadAsStringAsync().Result);
                        if (jsonResponse != null)
                        {
                            var recepient = GetRecipient(jsonResponse, who);

                            if (recepient != null)
                            {
                        Console.WriteLine(recepient.id);
                                return ulong.Parse(recepient.id);
                            }
                        }
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has occurred! Details: " + e);
                return 0;
            }
            return 0;
        }
        public static void SendMessage(ulong Who, string token, string text)
        {
            try
            {
                Parallel.Invoke(() =>
                {
                    new Thread(() =>
                    {

                        HttpClientHandler handler = new HttpClientHandler();
                        handler.Proxy = new WebProxy(ProxyHelper.ProxyHelper.GetRandomProxy());

                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Add("Authorization", token);

                        var response = client.PostAsync($"https://discordapp.com/api/v6/channels/{UserIDToDmChannelID(Who,token)}/messages", new StringContent(JsonConvert.SerializeObject(new DmMessageClass { content = text, nonce = $"{Who}", tts = false }), Encoding.UTF8, "application/json"));

                        if (response.Result.StatusCode == HttpStatusCode.OK)
                        {
                            Console.WriteLine($"{token} Sent {Who} a message!");
                        }
                    }).Start();
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has occurred! Details: " + e);
                return;
            }
        }
    }
}
