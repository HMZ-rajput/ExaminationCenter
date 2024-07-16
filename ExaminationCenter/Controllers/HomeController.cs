using ExaminationCenter.Data;
using ExaminationCenter.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult addRequest(IFormFile UserImage,Request request)
        {
            string filename = Path.GetFileName(UserImage.FileName);
            string filepath = Path.Combine(_env.WebRootPath, "images/" + filename);
            FileStream fs = new FileStream(filepath, FileMode.Create);
            UserImage.CopyTo(fs);
            request.UserImage = filename;

            _context.requests.Add(request);
            _context.SaveChanges();
            return View("Index");
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
