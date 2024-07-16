using ExaminationCenter.Data;
using ExaminationCenter.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationCenter.Controllers
{
    public class LoginController : Controller
    {
        private MyContext _context;

        public LoginController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            ViewBag.message = ViewBag.message;
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addUser(User user)
        {
            _context.users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult loginUser(string username, string password) 
        {
            var row = _context.users.FirstOrDefault(u => u.Name == username && u.Password == password);

            if(row != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = "Wrong username or password!";
                return View("Login");
            }
        }


        public IActionResult Test()
        {
            return View();
        }
    }
}
