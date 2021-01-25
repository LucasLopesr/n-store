using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NStore.WebApp.MVC.Controllers
{
    public class IdentidadeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
