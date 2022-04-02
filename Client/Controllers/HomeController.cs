using Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {

            var response = await _client.GetAsync($"https://localhost:44361/api/home/getweather2");


            var header = Convert.ToBase64String(new UTF8Encoding()
                       .GetBytes("client_id" + ":" + "client_secret"));

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44358/token/token");
            request.Headers.Add("Authorization", "Basic " + header);
            request.Content = new StringContent("grant_type=client_credentials",
                                                    Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage httpResponse = await _client.SendAsync(request);

            string json = await httpResponse.Content.ReadAsStringAsync();

            dynamic item = JsonConvert.DeserializeObject<object>(json);
            var token = item["access_token"];

            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response12 = await _client.GetAsync($"https://localhost:44361/api/home/getweather2");


            // var response1 = await _client.GetAsync($"https://localhost:44361/api/home/getweather");
         
            return View();
        }

        public void GetHttpResponse()
        {
            //var header = Convert.ToBase64String(new UTF8Encoding()
            //           .GetBytes("client_id" + ":" + "client_secret"));

            //var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44358/token/token");
            //request.Headers.Add("Authorization", "Basic " + header);
            //request.Content = new StringContent("grant_type=client_credentials",
            //                                        Encoding.UTF8, "application/x-www-form-urlencoded");

            //HttpResponseMessage response = await _client.SendAsync(request);

            //string json = await response.Content.ReadAsStringAsync();
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
