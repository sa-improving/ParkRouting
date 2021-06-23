using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ParkRouting.Models
{
    public class ApiConnector
    {
        private static readonly HttpClient _client = new HttpClient();
        public bool PrettyPrintJson { get; set; }

        private void PrettyPrint(string json)
        {
            var jobj = JObject.Parse(json);
            Console.WriteLine(jobj.ToString());
        }

        private string GetResponseString(string uri)
        {
            var response = _client.GetAsync(uri).Result;

            if(response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                if (PrettyPrintJson)
                {
                    PrettyPrint(responseBody);
                }
                return responseBody;
            }
            else
            {
                Console.WriteLine("Request failed; Status: {0}", response.ReasonPhrase);
                return null;
            }
        }

        public Park GetPark(string query)
        {
            var responseBody = GetResponseString(query);
            return JsonConvert.DeserializeObject<Park>(responseBody);
        }
    }
}
