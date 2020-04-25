using IdentityModel.Client;
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
        }
    }
}
