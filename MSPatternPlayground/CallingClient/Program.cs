using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CallingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client start");
            Run().Wait();
            Console.ReadLine();
        }

        static async Task Run()
        {
            
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }
            else
            {
                JsonSerializer serializer = new JsonSerializer();
                var tokenEndpoint = disco.TokenEndpoint;
                var keys = disco.KeySet.Keys;
                Console.WriteLine(JsonConvert.SerializeObject(tokenEndpoint, Formatting.Indented));
                Console.WriteLine(JsonConvert.SerializeObject(keys, Formatting.Indented));
            }
        }
    }
}
