using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var httpClient = new HttpClient();
            var accessToken = await GetToken(httpClient);
            httpClient.SetBearerToken(accessToken);
            try
            {
                var response = await httpClient.GetAsync(
                    "https://localhost:5001/api/UserIdentity"
                    );
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(JArray.Parse(content));
                }
            }
            catch (Exception e)
            {

                throw;
            }
           
        }
        static async Task<string> GetToken(HttpClient client)
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                throw new Exception(JsonConvert.SerializeObject(disco));
            }
            else
            {
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "client",
                    ClientSecret = "secret",
                    Scope = "api1"
                });

                if (tokenResponse.IsError)
                {
                    throw new Exception(JsonConvert.SerializeObject(tokenResponse));
                }
                else
                {
                    Console.WriteLine(tokenResponse.AccessToken);
                    return tokenResponse.AccessToken;
                }
            }
        }

    }
}
