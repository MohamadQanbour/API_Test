using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://weatherapi-com.p.rapidapi.com/current.json?q=25.3575,55.3882"),
                Headers =
                    {
                        { "X-RapidAPI-Key", "08a3848c0bmsh7fc983ade9603b7p1d24dbjsn47c56e4bf22e" },
                        { "X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com" },
                    },
            };
            using (var response = await client.SendAsync(request))
            {
                var body = response.Content.ReadAsStringAsync().Result;
                dynamic weth = JObject.Parse(body);
                ViewData.Model = weth;
            }
            };
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}