using Microsoft.AspNetCore.Mvc;
using MVCAssignmentThree.Models;
using MVCAssignmentThree.Service;

namespace MVCAssignmentThree.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Roles = _service.GetRoles(); // populate dropdown
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _service.RegisterUser(user); // Exampl

                return RedirectToAction("Login");
            }

            ViewBag.Roles = _service.GetRoles();
            return View(user);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _service.ValidateUser(email, password);
            if (user == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            // Role-based redirect
            return user.Role.RoleName switch
            {
                "Administrator" => RedirectToAction(
                    "AdminDashboard",
                    "Home",
                    new { name = user.FullName }
                ),
                "Coordinator" => RedirectToAction(
                    "CoordinatorDashboard",
                    "Home",
                    new { name = user.FullName }
                ),
                "Receptionist" => RedirectToAction(
                    "ReceptionistDashboard",
                    "Home",
                    new { name = user.FullName }
                ),
                _ => RedirectToAction("Index", "Home"),
            };
        }
    }
}
