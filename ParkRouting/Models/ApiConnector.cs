using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace ParkRouting.Models
{
    public class ApiConnector
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly IMemoryCache _cache;
        public bool PrettyPrintJson { get; set; }

        public ApiConnector(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        private void PrettyPrint(string json)
        {
            var jobj = JObject.Parse(json);
            Console.WriteLine(jobj.ToString());
        }

        private string GetResponseString(string uri)
        {
            var response = _client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
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

        public List<Park> CheckCache()
        {
            //string json;
            //if(!_cache.TryGetValue(CacheKey.Entry, out json))
            //{
            //    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
            //    json = GetResponseString("https://seriouslyfundata.azurewebsites.net/api/parks");
            //    _cache.Set(CacheKey.Entry, json, cacheEntryOptions);
            //}
            //return JsonConvert.DeserializeObject<List<Park>>(json);

            List<Park> parks;
            if(!_cache.TryGetValue(CacheKey.Entry, out parks))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                string request = GetResponseString("https://seriouslyfundata.azurewebsites.net/api/parks");
                parks = JsonConvert.DeserializeObject<List<Park>>(request);

                _cache.Set(CacheKey.Entry, parks, cacheEntryOptions);
            }
            return parks;
        }



        public List<Park> GetParks(string query)
        {
            List<Park> data = CheckCache();
            data = data.Where(p => p.Parkname.ToLower().Contains(query.ToLower()) || p.Description.ToLower().Contains(query.ToLower())).ToList();
            return data;
        }

        public List<Park> GetAllParks()
        {          
            return CheckCache();
        }
    }
}
