using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using ClubMates.Models;

namespace ClubMates.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            List<Student> students = new List<Student>()
            {
                new Student { StudentID = 1, StudentName = "Karthik",DateOfBirth = new DateTime(2007,7,30), Height = 158, Weight = 78},
                new Student { StudentID = 2, StudentName = "Sudeep",DateOfBirth = new DateTime(2007,7,30), Height = 160, Weight = 78},
                new Student { StudentID = 3, StudentName = "Yash",DateOfBirth = new DateTime(2007,7,30), Height = 179, Weight = 78}
            };
            return View(students);
        }


        public ActionResult AddStudent()
        {
            return View(new Student());
        }


        [HttpPost]
        public ActionResult AddStudent(Student student)
        {

            return View();
        }
    }
}