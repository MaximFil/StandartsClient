using Newtonsoft.Json;
using StandartsClient.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StandartsClient.Services
{
    public class StandartService
    {
        private readonly HttpClient client;

        public StandartService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["hostAddress"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Dictionary<string, StandartType>> GetStandartTypes()
        {
            try
            {
                var result = new Dictionary<string, StandartType>();
                var response = await client.GetAsync("api/standart/getstandarttypes");
                if (response.IsSuccessStatusCode)
                {
                    var standartTypes = JsonConvert.DeserializeObject<List<StandartType>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                    if(standartTypes != null)
                    {
                        result = standartTypes.ToDictionary(x => x.StandartTypeName);
                    }
                }

                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Dictionary<int, List<Standart>>> GetStandarts()
        {
            try
            {
                var result = new Dictionary<int, List<Standart>>();
                var response = await client.GetAsync("api/standart/getstandarttypes");
                if (response.IsSuccessStatusCode)
                {
                    var standarts = JsonConvert.DeserializeObject<List<Standart>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                    if (standarts != null)
                    {
                        result = standarts.GroupBy(x => x.StandartTypeId).ToDictionary(x => x.Key, x => x.ToList());
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Dictionary<int, Standart>> GetStandartsByTypeId(int standartTypeId)
        {
            try
            {
                var result = new Dictionary<int, Standart>();
                var response = client.GetAsync($"api/standart/getstandartsbytype/{standartTypeId}").GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var standarts = await response.Content.ReadAsAsync<List<Standart>>();
                    result = standarts.ToDictionary(x => x.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
