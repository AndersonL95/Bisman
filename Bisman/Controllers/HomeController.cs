using System.Diagnostics;
using Bisman.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bisman.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Menu");
        }

        public IActionResult Help()
        {
            return View("Contato");
        }

        public IActionResult Menu()
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
