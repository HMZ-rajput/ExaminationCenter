using Microsoft.AspNetCore.Mvc;

namespace ExaminationCenter.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
