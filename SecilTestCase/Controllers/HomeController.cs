using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SecilTestCase.Models;
using System.Diagnostics;
using System.Reflection;

namespace SecilTestCase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ConfigurationReader configurationReader = new ConfigurationReader("SecilTestCase", "mongodb://localhost:27017/", 6000);
            var value = configurationReader.GetAllConfigurationItems();
            var value2 = configurationReader.GetValue<Object>("IsBasketEnabled");
            Console.WriteLine(value2.ToString());
            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name);
            return View(value);

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
