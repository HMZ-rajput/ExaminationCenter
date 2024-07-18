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

        //Home page or Candidate Page
        [HttpGet("")]
        [HttpGet("Home/Index")]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {
                var requests = _context.requests.ToList();
                return View(requests);
            }
            return RedirectToAction("Login", "Login");

        }

        // Examination center
        [HttpGet("Home/ExaminationCenter")]
        public IActionResult ExaminationCenter(string searchText)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
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
            return RedirectToAction("Login", "Login");           

        }

        //Center Manager (accept/reject wala)
        [HttpGet("Home/CenterManagerPage")]
        public IActionResult CenterManagerPage()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {
                var requests = _context.requests.ToList();
                return View(requests);
            }
            return RedirectToAction("Login", "Login");
        }

        //Examination page Dr form wala
        [HttpGet("Home/ExaminationPage")]
        public IActionResult ExaminationPage()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {
                return View();
            }
            return RedirectToAction("Login", "Login");
        }

        //add request from candidate page
        [HttpPost("Home/AddRequest")]
        public IActionResult addRequest(IFormFile UserImage,Request request)
        {
            if (request != null && UserImage != null)
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
            return View("Index");
        }

        //Add examination form from examination page
        [HttpPost("Home/SubmitExamination")]
        public IActionResult SubmitExamination(Examination examination)
        {
            if(examination != null)
            {
                _context.examinations.Add(examination);
                _context.SaveChanges();
                return RedirectToAction("ExaminationPage");
            }
            return View("Index");
        }

        //accepted from center manager page
        public IActionResult AcceptReq(int id)
        {
            if (id != null)
            {
                var request = _context.requests.FirstOrDefault(r => r.Id == id);
                if (request != null)
                {
                    request.Status = "Accepted";
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("CenterManagerPage");
        }

        //rejected from center manager page
        public IActionResult RejectReq(int id)
        {
            if (id != null)
            {
                var request = _context.requests.FirstOrDefault(r => r.Id == id);
                if (request != null)
                {
                    request.Status = "Rejected";
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("CenterManagerPage");
        }

        //used to fetch examination from. not in use since no view for exam
        [HttpGet("/Home/GetExaminations")]
        public IActionResult GetExaminations()
        {
            var examinations = _context.examinations.ToList(); // Adjust this line to match your database structure
            return Json(examinations);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
