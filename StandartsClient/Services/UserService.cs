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
    public class UserService
    {
        private readonly HttpClient client;

        public UserService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["hostAddress"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HashSet<string>> GetUsedUserNames()
        {
            var result = new List<string>();
            try
            {
                var response = await client.GetAsync("api/user/GetUsedUserNames");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<List<string>>();
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            return result.ToHashSet();
        }

        public Models.User LogIn(string login, string password)
        {
            var user = new Models.User();
            var response = client.GetAsync($"api/user/login/{login}/{password}").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = response.Content.ReadAsAsync<ApiResponse>().GetAwaiter().GetResult();
                if (apiResponse.Success)
                {
                    user = JsonConvert.DeserializeObject<Models.User>(apiResponse.Json);
                }
                else
                {
                    throw new Exception(apiResponse.Message);
                }
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

            return user;
        }

        public Models.User SignUp(string login, string password)
        {
            var user = new Models.User() { Login = login, Password = password };
            var response = client.PostAsJsonAsync("api/user/signup", user).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = response.Content.ReadAsAsync<ApiResponse>().GetAwaiter().GetResult();
                if (apiResponse.Success)
                {
                    user = JsonConvert.DeserializeObject<Models.User>(apiResponse.Json);
                }
                else
                {
                    throw new Exception(apiResponse.Message);
                }
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

            return user;
        }

        public async Task AddStandartToFavorites(int userId, int standartId)
        {
            try
            {
                var user = new XrefUserStandart() { StandartId = standartId, UserId = userId };
                var response = await client.PostAsJsonAsync("api/user/AddStandartToFavorites", user);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteStandartFromFavorites(int userId, int standartId)
        {
            try
            {
                var response = await client.DeleteAsync($"api/user/deletestandartfromfavorite/{standartId}/{userId}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
