using demo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

namespace demo.Controllers
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
            string Xml = @"
            <Products>
                <Product>
                    <Name>Product 1</Name>
                    <Price>12.99</Price>
                    <Quantity>10</Quantity>
                    <Description>ABC</Description>
                </Product>
                 <Product>
                    <Name>Product 2</Name>
                    <Price>3.99</Price>
                    <Quantity>10</Quantity>
                    <Description>ABC</Description>
                </Product>
            </Products>";

            Product product = new Product()
            {
                Xml = Xml
            };

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7091");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(product);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/api/product", content);

            if (response.IsSuccessStatusCode)
            {
                // Request was successful
            }
            else
            {
                // Request was not successful
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}