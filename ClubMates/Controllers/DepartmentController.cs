using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClubMates.Models;

namespace ClubMates.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            Department department = new Department()
            {
                DepartmentId = 1,
                DepartmentName = "Medical"
            };

            List<Department> departmentList = new List<Department>()
            {
                new Department() { DepartmentId = 1, DepartmentName = "Engineering" },
                new Department() { DepartmentId = 2, DepartmentName = "Bio"}
            };

            return View(departmentList);


       
        }

        public ActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDepartment(Department department)
        {
            return View();
        }
    }
}