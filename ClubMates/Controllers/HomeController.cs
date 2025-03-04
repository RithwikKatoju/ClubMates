using System;
using System.Collections.Generic;
using System.Linq;
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
            Student student = new Student()
            {
                StudentID = 1,
                StudentName = "Rithwik",
                DateOfBirth = new DateTime(2004, 05, 31),
                Height = 179,
                Weight = 70
            };

            List<Student> studnets = new List<Student>()
            {
                new Student() { StudentID = 1, StudentName = 
            }
            return View(student);
        }
    }
}