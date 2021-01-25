using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NStore.WebApp.MVC.Models;
using System.Diagnostics;

namespace NStore.WebApp.MVC.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
