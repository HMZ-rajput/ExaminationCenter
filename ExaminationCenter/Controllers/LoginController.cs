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

        [HttpGet("Login/Login")]
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
            // Check if the username already exists
            var existingUser = _context.users.FirstOrDefault(u => u.Name == user.Name);
            if (existingUser != null)
            {
                // Username already taken, set ViewBag error message and return view
                ViewData["message"] = "Username is already taken. Please choose a different one.";
                return View("Register");
            }

            // Username is available, add the user to the database
            _context.users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }


        [HttpPost("Login/loginUser")]
        public IActionResult loginUser(string username, string password, string role) 
        {
            var row = _context.users.FirstOrDefault(u => u.Name == username && u.Password == password && u.Role==role);

            if(row != null)
            {
                HttpContext.Session.SetString("id", row.Id.ToString());
                HttpContext.Session.SetString("role", row.Role);
                HttpContext.Session.SetString("name",row.Name);

                if (HttpContext.Session.GetString("role") == "Candidate")
                {
                    return RedirectToAction("Index", "Home");
                }
                if (HttpContext.Session.GetString("role") == "Examination Center")
                {
                    return RedirectToAction("ExaminationCenter", "Home");
                }
                if (HttpContext.Session.GetString("role") == "Doctor")
                {
                    return RedirectToAction("ExaminationPage", "Home");
                }
                if (HttpContext.Session.GetString("role") == "Center Director")
                {
                    return RedirectToAction("CenterManagerPage", "Home");
                }
                return RedirectToAction("Error", "Error");

            }
            else
            {
                ViewBag.message = "Wrong username, role or password!";
                return View("Login");
            }
        }

        [HttpGet("Login/ChangePasswordView")]
        public IActionResult ChangePasswordView()
        {
            ViewBag.message = ViewBag.message;
            return View();

        }

        [HttpPost("Login/ChangePassword")]
        public IActionResult ChangePassword(string username, string password)
        {
            var user = _context.users.Where(u => u.Name == username).FirstOrDefault();

            if (user != null)
            {
                user.Password = password;
                _context.SaveChanges();
                return View("Login");
            }
            ViewBag.message = "Invalid Username";
            return View("ChangePasswordView");
        }

        public IActionResult Logout()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {
                HttpContext.Session.Remove("id");
                HttpContext.Session.Remove("name");
                HttpContext.Session.Remove("role");
            }
            return View("Login");
        }


        public IActionResult Test()
        {
            return View();
        }
    }
}
