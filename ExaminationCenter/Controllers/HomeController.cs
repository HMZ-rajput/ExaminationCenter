using ExaminationCenter.Data;
using ExaminationCenter.Dtos;
using ExaminationCenter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualBasic;
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
            if (HttpContext.Session.GetString("role")=="Candidate")
            {
                var requests = _context.requests.ToList();
                return View(requests);
            }
            return RedirectToAction("Error", "Error");
        }

        // Examination center
        [HttpGet("Home/ExaminationCenter")]
        public IActionResult ExaminationCenter(string searchText)
        {
            if (HttpContext.Session.GetString("role") == "Examination Center")
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
            return RedirectToAction("Error", "Error");

        }

        //Examination page Dr form wala
        [HttpGet("Home/ExaminationPage")]
        public IActionResult ExaminationPage()
        {
            if (HttpContext.Session.GetString("role") == "Doctor")
            {
                var requests = (from r in _context.requests
                                where r.AttendendeStatus == true
                                && !_context.examinations.Any(e => e.ReqId == r.Id)
                                select r).ToList();

                return View(requests);
            }
            return RedirectToAction("Error", "Error");
        }

        //Center Manager (accept/reject wala)
        [HttpGet("Home/CenterManagerPage")]
        public IActionResult CenterManagerPage()
        {
            if (HttpContext.Session.GetString("role") == "Center Director")
            {
                var requests = (from r in _context.requests
                                where r.AttendendeStatus == true
                                && _context.examinations.Any(e => e.ReqId == r.Id)
                                select r).ToList();

                return View(requests);
            }
            return RedirectToAction("Error", "Error");
        }

        [HttpGet("Home/RequestDetails")]
        public IActionResult RequestDetails()
        {
            if (HttpContext.Session.GetString("role") == "Examination Center" || HttpContext.Session.GetString("role") == "Candidate")
            {
                var requests = _context.requests.ToList();
                return View(requests);
            }
            return RedirectToAction("Error", "Error");
        }


        //add request from candidate page
        [HttpPost("Home/AddRequest")]
        public IActionResult addRequest(IFormFile UserImage,Request request)
        {
            if(HttpContext.Session.GetString("role") == "Candidate")
            {
                if (request != null && UserImage != null)
                { 
                string filename = Path.GetFileName(UserImage.FileName);
                string uniqueFileName = Guid.NewGuid().ToString()+"_"+filename;
                string filepath = Path.Combine(_env.WebRootPath, "images/" + uniqueFileName);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                UserImage.CopyTo(fs);
                request.UserImage = uniqueFileName;
                request.Id = 0;

                _context.requests.Add(request);
                _context.SaveChanges();
                return RedirectToAction("Index");
                }
                return View("Index");
            }
            return RedirectToAction("Error", "Error");
        }

        [HttpPost("Home/UpdateRequest")]
        public IActionResult updateRequest(IFormFile UserImage, Request request)
        {
            if (HttpContext.Session.GetString("role") == "Candidate")
            {
                if (request != null)
                {
                    var existingRequest = _context.requests.Find(request.Id);
                    if (existingRequest != null)
                    {
                        if (UserImage != null)
                        {
                            //remove image from root folder too
                            string filename = Path.GetFileName(UserImage.FileName);
                            string uniqueFileName = Guid.NewGuid().ToString() + "_" + filename;
                            string filepath = Path.Combine(_env.WebRootPath, "images/" + uniqueFileName);
                            using (FileStream fs = new FileStream(filepath, FileMode.Create))
                            {
                                UserImage.CopyTo(fs);
                            }
                            existingRequest.UserImage = uniqueFileName;
                        }

                        existingRequest.Id = request.Id;
                        existingRequest.Name = request.Name;
                        existingRequest.Identity = request.Identity;
                        existingRequest.Position = request.Position;
                        existingRequest.DateOfBirth = request.DateOfBirth;

                        //_context.Entry(existingRequest).State = EntityState.Modified;

                        _context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Error");
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
                
        }

        [HttpPost("Home/UpdateRequestEC")]
        public IActionResult updateRequestEC(IFormFile UserImage, Request request)
        {
            if (HttpContext.Session.GetString("role") == "Examination Center")
            {
                if (request != null)
                {
                var existingRequest = _context.requests.Find(request.Id);
                    
                existingRequest.Id = request.Id;
                existingRequest.Name = request.Name;
                existingRequest.Position = request.Position;

                //_context.Entry(existingRequest).State = EntityState.Modified;

                _context.SaveChanges();
                return RedirectToAction("ExaminationCenter");
                }
                return RedirectToAction("ExaminationCenter");
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }

        }

        [HttpDelete("Home/DeleteRequest/{id}")]
        public IActionResult DeleteRequest(int id)
        {
            if (HttpContext.Session.GetString("role") == "Candidate")
            {
                var request = _context.requests.Find(id);
                if (request != null)
                {
                    //remove image from root folder too
                    _context.requests.Remove(request);
                    _context.SaveChanges();
                    return Ok(); // Return 200 OK status
                }
                else
                {
                    return NotFound(); // Handle case where request with given ID is not found
                }
            }
            else
            {
                return NotFound();
            }
        }



        //Add examination form from examination page
        [HttpPost("Home/SubmitExamination")]
        public IActionResult SubmitExamination(Examination examination)
        {
            if (HttpContext.Session.GetString("role") == "Doctor")
            {
                if (examination != null)
                {
                    _context.examinations.Add(examination);
                    _context.SaveChanges();
                    return RedirectToAction("ExaminationPage");
                }
                return View("ExaminationPage");
            }
            else
            {
                return RedirectToAction("Error","Error");
            }
        }

        //accepted from center manager page
        public IActionResult AcceptReq(int id)
        {
            if (HttpContext.Session.GetString("role") == "Center Director")
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
            else
            {
                return RedirectToAction("Error","Error");
            }
        }

        [HttpPost("Home/UpdateAttendanceStatus")]
        public IActionResult UpdateAttendanceStatus([FromBody] UpdateAttendanceStatusRequest request)
        {
            if (HttpContext.Session.GetString("role") == "Examination Center")
            {
                var requestEntry = _context.requests.FirstOrDefault(r => r.Id == request.Id);
                if (requestEntry != null)
                {
                    requestEntry.AttendendeStatus = request.AttendanceStatus;
                    requestEntry.AttendenceTime = DateTime.Now;
                    _context.SaveChanges();
                    return Ok(); // Return 200 OK status
                }
                else
                {
                    return NotFound(); // Handle case where request with given ID is not found
                }
            }
            else
            {
                return Unauthorized(); // Handle unauthorized access
            }
        }






        //rejected from center manager page
        public IActionResult RejectReq(int id)
        {
            if (HttpContext.Session.GetString("role") == "Center Director")
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
            else
            {
                return RedirectToAction("Error","Error");
            }
        }

        //used to fetch examination from. not in use since no view for exam
        [HttpGet("/Home/GetExaminations")]
        public IActionResult GetExaminations()
        {
            var examinations = _context.examinations.ToList(); // Adjust this line to match your database structure
            return Json(examinations);
        }

        [HttpGet("Home/GetExaminationDetails/{userId}")]
        public IActionResult GetExaminationDetails(int userId)
        {
            if (HttpContext.Session.GetString("role") == "Center Director")
            {
                var examination = _context.examinations.FirstOrDefault(e => e.ReqId == userId);

                if (examination == null)
                {
                    return NotFound();
                }

                return Json(examination);
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
