using ExaminationCenter.Data;
using ExaminationCenter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Web;

namespace ExaminationCenter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;
        private IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, MyContext myContext, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = myContext;
            _env = env;
        }

        [HttpGet("")]
        [HttpGet("Home/Index")]
        public IActionResult Index()
        {
            var requests = _context.requests.ToList();
            return View(requests);
        }

        [HttpGet("Home/ExaminationCenter")]
        public IActionResult ExaminationCenter(string searchText)
        {
            var requests = _context.requests.ToList();

            if (!string.IsNullOrEmpty(searchText))
            {
                requests = requests.Where(r => r.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (requests.Count == 0)
                ViewBag.Message = "No matching result!";

            return View(requests);

        }

        [HttpPost("Home/AddRequest")]
        public IActionResult addRequest(IFormFile UserImage,Request request)
        {
            string filename = Path.GetFileName(UserImage.FileName);
            string uniqueFileName = Guid.NewGuid().ToString()+"_"+filename;
            string filepath = Path.Combine(_env.WebRootPath, "images/" + uniqueFileName);
            FileStream fs = new FileStream(filepath, FileMode.Create);
            UserImage.CopyTo(fs);
            request.UserImage = uniqueFileName;

            _context.requests.Add(request);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
