using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCAssignmentThree.Models;

namespace MVCAssignmentThree.Controllers
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

        public IActionResult AdminDashboard(string name) =>
            Content($"Welcome {name}, you are logged in as Administrator!");

        public IActionResult CoordinatorDashboard(string name) =>
            Content($"Welcome {name}, you are logged in as Coordinator!");

        public IActionResult ReceptionistDashboard(string name) =>
            Content($"Welcome {name}, you are logged in as Receptionist!");

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                }
            );
        }
    }
}
